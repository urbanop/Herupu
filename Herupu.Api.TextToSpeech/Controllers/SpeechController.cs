using Herupu.Api.TextToSpeech.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace Herupu.Api.TextToSpeech.Controllers
{
    [ApiController]
    [Route("speech")]
    public class SpeechController : ControllerBase
    {
        private readonly string _url;

        public SpeechController()
        {
            _url = Environment.GetEnvironmentVariable("smartOneTTSEndpoint");

        }

        [HttpGet()]
        public async Task<IActionResult> Get(string palavra)
        {
            try
            {
                if (string.IsNullOrEmpty(palavra) || string.IsNullOrWhiteSpace(palavra))
                    throw new Exception("É obrigatório informar uma palavra.");

                if (palavra.Split(" ").Length > 1)
                    throw new Exception("Para ler mais de uma palavra utilize o método post.");

                Authentication authentication = new Authentication();
                string token = authentication.GetAccessToken();

                using (ApiClient client = new ApiClient(token))
                {
                    var result = await client.PostAsync(new Uri(_url), MontarCorpoRequisicao(palavra));

                    Response.ContentType = "audio/mpeg";

                    return Ok(result.Content.ReadAsStreamAsync().Result);
                }
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post(LeituraModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            try
            {
                Authentication authentication = new Authentication();
                string token = authentication.GetAccessToken();
                
                using (ApiClient client = new ApiClient(token))
                {
                    var result = await client.PostAsync(new Uri(_url), MontarCorpoRequisicao(model.Frase));

                    Response.ContentType = "audio/mpeg";

                    return Ok(result.Content.ReadAsStreamAsync().Result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private StringContent MontarCorpoRequisicao(string texto)
        {
           string lang = Environment.GetEnvironmentVariable("smartOneTTSVoiceLocale");
           string voice = Environment.GetEnvironmentVariable("smartOneTTSVoice");

            var xmlContent = $"<speak version='1.0' xml:lang='{lang}'><voice xml:lang='{lang}' xml:gender='Female'" +
                        $" name = '{voice}' >{texto}" +
                        $"</voice></speak> ";

            return new StringContent(xmlContent, Encoding.UTF8, "application/ssml+xml");
        }
    }
}

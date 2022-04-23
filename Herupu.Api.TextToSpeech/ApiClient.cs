using System.Net.Http.Headers;

namespace Herupu.Api.TextToSpeech
{
    public class ApiClient : HttpClient
    {
        public ApiClient(string token)
        {
            DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/ssml+xml"));//ACCEPT header

            DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", "audio-16khz-32kbitrate-mono-mp3");
            DefaultRequestHeaders.Add("User-Agent", "Herupu.Api.TextToSpeech");
        }
    }
}

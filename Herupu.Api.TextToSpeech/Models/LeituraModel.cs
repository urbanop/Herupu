using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Herupu.Api.TextToSpeech.Models
{
    public class LeituraModel
    {
        [JsonProperty(PropertyName ="frase")]
        [Required(ErrorMessage = "É obrigatório informar uma palavra ou frase.")]
        public string Frase { get; set; }
    }
}

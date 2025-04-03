using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class UppgiftItem
    {
        [JsonPropertyName("kod")]
        public string Kod { get; set; }

        [JsonPropertyName("uppgift")]
        public List<string> Uppgift { get; set; }
    }
}

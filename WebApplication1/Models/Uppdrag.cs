using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Uppdrag
    {
        [JsonPropertyName("organ_kod")]
        public string OrganKod { get; set; }

        [JsonPropertyName("roll_kod")]
        public string RollKod { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("tom")]
        public string Tom { get; set; }

        [JsonPropertyName("uppgift")]
        [JsonConverter(typeof(UppgiftConverter))]  // Use the custom converter
        public List<string> Uppgift { get; set; }
    }

}

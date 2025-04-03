namespace WebApplication1.Models
{
    public class Uppgift
    {
        public string kod { get; set; }
        public List<string> uppgift { get; set; } // Eftersom "uppgift" är en lista i JSON
        public string typ { get; set; }
        public string intressent_id { get; set; }
        public string hangar_id { get; set; }
    }
}

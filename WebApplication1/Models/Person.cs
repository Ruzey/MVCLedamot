namespace WebApplication1.Models
{
    public class Person
    {
        public string tilltalsnamn { get; set; }
        public string efternamn { get; set; }
        public string sourceid { get; set; }
        public string hangar_guid { get; set; }
        public string fodd_ar { get; set; }
        public string kon { get; set; }
        public string iort { get; set; }
        public string parti { get; set; }
        public string valkrets { get; set; }
        public string status { get; set; }
        public string bild_url_80 { get; set; }
        public string bild_url_192 { get; set; }
        public string bild_url_max { get; set; }
        public string personuppdrag_url { get; set; }
        public Personuppgift personuppgift { get; set; }

    }
}

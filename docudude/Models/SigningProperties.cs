namespace docudude.Models
{
    public class SigningProperties
    {
        public string Bucket { get; set; }
        public string Key { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; } = "Certification";
        public KMSData KMSData { get; set; }
    }
}

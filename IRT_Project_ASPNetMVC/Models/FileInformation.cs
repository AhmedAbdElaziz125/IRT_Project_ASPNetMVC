namespace IRT_Project_ASPNetMVC.Models
{
    public class FileInformation
    {
        public string? DirectoryName { get; set; }
        public string? FileName { get; set; }
        public List<string>? FileTerms { get; set; } = new List<string>();
        public Dictionary<string, int>? TermsFrequencies { get; set; } = new Dictionary<string, int>();
    }
}

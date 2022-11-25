namespace IRT_Project_ASPNetMVC.Models
{
    public class Files
    {
        public string? Directory { get; set; }
        public List<FileContent>? files { get; set; }
        public Dictionary<string, int>? DocumentFreq { get; set; } = new Dictionary<string, int>();
    }
}

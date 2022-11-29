namespace IRT_Project_ASPNetMVC.Models
{
    public class DirectoryInformation
    {
        public string? Name { get; set; }
        public List<FileInformation>? Files { get; set; } = new List<FileInformation>();
    }
}

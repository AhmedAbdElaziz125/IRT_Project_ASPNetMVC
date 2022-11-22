namespace IRT_Project_ASPNetMVC.Models
{
    public class DOC
    {
        public int? DOC_Id { get; set; }
        public string? DOC_Name { get; set; }
        public List<string>? DOC_Terms { get; set; }
        public DOC()
        {
            DOC_Terms = new List<string>();
        }
    }
}

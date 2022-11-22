namespace IRT_Project_ASPNetMVC.Models
{
    public class Matrice
    {
        public List<DOC>? Documents { get; set; }
        public List<string>? Terms { get; set; }
        public int[][] posting { get; set; }
        public Matrice()
        {
            Documents = new List<DOC>();
            Terms = new List<string>();
            posting = new int[][] { };
        }
        
    }
}

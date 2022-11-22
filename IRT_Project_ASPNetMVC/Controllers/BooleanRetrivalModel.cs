using IRT_Project_ASPNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace IRT_Project_ASPNetMVC.Controllers
{
    public class BooleanRetrivalModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowMatrice(int DOC1_Id, string DOC1_Name, string DOC1_Text,
                                         int DOC2_Id, string DOC2_Name, string DOC2_Text)
        {
            
            /*-----------------------------------------------*/
            //////////////////////////////////////////////////
            var doc1 = new DOC();
            var doc1Terms = new List<string>();
            foreach (var term in DOC1_Text.Split(' ', '.', StringSplitOptions.RemoveEmptyEntries))
            {
                doc1Terms.Add(term.Trim());
            }
            doc1.DOC_Id = DOC1_Id;
            doc1.DOC_Name = DOC1_Name;
            doc1.DOC_Terms = doc1Terms;
            /*-----------------------------------------------*/
            //////////////////////////////////////////////////
            var doc2 = new DOC();
            var doc2Terms = new List<string>();
            foreach (var term in DOC2_Text.Split(' ', '.', StringSplitOptions.RemoveEmptyEntries))
            {
                doc2Terms.Add(term.Trim());
            }
            doc2.DOC_Id = DOC2_Id;
            doc2.DOC_Name = DOC2_Name;
            doc2.DOC_Terms = doc2Terms;
            /*-----------------------------------------------*/
            //////////////////////////////////////////////////
            var matrice = new Matrice();
            matrice.Documents = new List<DOC>();
            matrice.Documents.Add(doc1);
            matrice.Documents.Add(doc2);
            matrice.Terms = new List<string>();
            matrice.Terms.AddRange(doc1Terms);
            foreach(var term2 in doc2Terms)
            {
                if(!matrice.Terms.Contains(term2))
                    matrice.Terms.Add(term2);
            }
            return View(matrice);
        }
    }
    
}

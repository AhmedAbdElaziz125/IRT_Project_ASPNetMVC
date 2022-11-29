using IRT_Project_ASPNetMVC.AppServices;
using IRT_Project_ASPNetMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace IRT_Project_ASPNetMVC.Controllers
{
    public class BooleanRetrivalModel : Controller
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowMatrice() 
        { 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Dashboard(string path)
        {
            Services services = new Services();
            List<FileInformation> filesInformations = new List<FileInformation>();
            // Specify the directories you want to manipulate.
            List<FileInfo> files = services.GetDirectoryFiles(path);
            foreach (FileInfo file in files)
            {
                FileInformation fileInformation = new FileInformation();
                fileInformation.FileName = "DOC" + file.Name;
                fileInformation.FileTerms = services.GetFileTerms(file);
                fileInformation.TermsFrequencies = new Dictionary<string, int>();
                foreach(var term in fileInformation.FileTerms)
                {
                    fileInformation.TermsFrequencies.Add(term, services.TermFrequency(term, file));
                }
                filesInformations.Add(fileInformation);
            }
            return View(filesInformations);
        }
    }
    
}

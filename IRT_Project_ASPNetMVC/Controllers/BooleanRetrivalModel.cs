using IRT_Project_ASPNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

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
        public async Task<IActionResult> GetFiles() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> GetFiles(string path)
        {
            // Specify the directories you want to manipulate.
            DirectoryInfo dir = new DirectoryInfo(path);
            Files filesList = new Files();
            filesList.Directory = dir.FullName;
            filesList.files = new List<FileContent>();
            char[] delimiters = new char[] {' ',';', '\u002C', ';' };
            try
            {
                // Determine whether the directory exists.
                if (dir.Exists)
                {
                    // Indicate that the directory already exists.
                    foreach (FileInfo file in dir.GetFiles().ToList())
                    {
                        FileContent fileContent = new FileContent();
                        fileContent.FileName = file.Name;
                        var reader = file.OpenText();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            fileContent.FileWords = new List<string>();
                            foreach (var word in line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries))
                            {
                                fileContent.FileWords.Add(word.Trim());
                            }
                            fileContent.FileWords.Sort();
                        }
                        filesList.files.Add(fileContent);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest("That path not exists :" + dir.FullName + "The process failed: {0}" + e.ToString() );
            }
            finally { }
            return View(filesList);
        }
    }
    
}

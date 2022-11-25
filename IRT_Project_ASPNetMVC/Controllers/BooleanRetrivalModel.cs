using IRT_Project_ASPNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

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
            char[] delimiters = new char[] { ' ', ';', '\u002C', ';' };
            String[] stopWords = { "{", "}", ">", "<", "~", "^", ":", ";", "(", ")", "-", "|", "/", "*", "$", "$", "%", "#", "@", "!", "+", ",", ".", ".", ".", "a", "as", "about", "after", "afterwards", "aint", "all", "already", "also", "although", "always", "am", "an", "and", "any", "are", "arent", "as", "at", "be", "because", "been", "before", "beforehand", "behind", "being", "below", "beside", "besides", "both", "but", "by", "did", "didnt", "do", "does", "doesnt", "doing", "dont", "done", "during", "each", "edu", "eg", "either", "else", "elsewhere", "from", "had", "hadnt", "has", "hasnt", "have", "havent", "having", "he", "hes", "her", "here", "heres", "hereafter", "hereby", "herein", "hereupon", "hers", "herself", "hi", "him", "himself", "his", "hither", "how", "if", "immediate", "inasmuch", "inc", "into", "inward", "is", "isnt", "it", "itd", "itll", "its", "its", "itself", "just", "last", "lately", "later", "latter", "latterly", "least", "little", "ltd", "mainly", "many", "maybe", "me", "meanwhile", "merely", "might", "more", "moreover", "much", "must", "my", "myself", "name", "namely", "nd", "near", "nearly", "necessary", "neither", "never", "nevertheless", "new", "next", "nine", "no", "nobody", "non", "none", "noone", "nor", "normally", "not", "nothing", "novel", "now", "nowhere", "obviously", "of", "off", "often", "oh", "ok", "okay", "old", "on", "once", "one", "ones", "only", "onto", "or", "other", "others", "otherwise", "ought", "our", "ours", "ourselves", "out", "outside", "over", "overall", "own", "particular", "particularly", "per", "perhaps", "placed", "please", "plus", "possible", "presumably", "probably", "que", "quite", "qv", "rather", "rd", "re", "really", "reasonably", "regardless", "regards", "relatively", "respectively", "right", "same", "second", "secondly", "self", "selves", "sensible", "serious", "seriously", "seven", "several", "she", "since", "so", "some", "somebody", "somehow", "someone", "something", "sometime", "sometimes", "somewhat", "somewhere", "soon", "sorry", "sub", "such", "sup", "sure", "ts", "th", "than", "that", "thats", "thats", "the", "their", "theirs", "them", "themselves", "thence", "there", "theres", "thereafter", "thereby", "therefore", "therein", "theres", "thereupon", "these", "they", "theyd", "theyll", "theyre", "theyve", "this", "thorough", "thoroughly", "those", "though", "three", "through", "throughout", "thru", "thus", "together", "too", "toward", "towards", "tries", "truly", "try", "trying", "twice", "two", "un", "under", "unfortunately", "unless", "unlikely", "until", "unto", "up", "upon", "us", "use", "used", "useful", "uses", "using", "usually", "value", "various", "very", "via", "viz", "vs", "way", "we", "wed", "well", "weve", "welcome", "well", "whatever", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "yes", "yet", "you", "youd", "youll", "youre", "youve", "your", "yours", "yourself", "yourselves" };
            var map = new Dictionary<string, int>();
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
                            //foreach (var word in line.Split(delimiters,StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (!stopWords.Contains(word))
                                {
                                    fileContent.FileWords.Add(word.Trim());
                                    if (filesList.DocumentFreq.ContainsKey(word))
                                    {
                                        filesList.DocumentFreq[word]++;
                                        //int count = filesList.DocumentFreq[word];
                                        //filesList.DocumentFreq.Add(word, ++count);
                                    }
                                    else
                                        filesList.DocumentFreq[word] = 1;


                                }

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

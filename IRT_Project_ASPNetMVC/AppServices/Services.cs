using IRT_Project_ASPNetMVC.Models;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;

namespace IRT_Project_ASPNetMVC.AppServices
{
    public interface IServices
    {
        public char[] delimiters();
        public String[] stopWords();
        public List<FileInfo> GetDirectoryFiles(string path);
        public List<string> GetFileTerms(FileInfo file);
        public List<string> GetDirectoryTerms(List<FileInfo> files);
        public int TermFrequency(string term, FileInfo file);
        public int TermFrequency(string term, List<FileInfo> files);
    }
    public class Services : IServices
    {
        public char[] delimiters()
        {
            return new char[] { ' ', ';', '\u002C', ';' };
        }
        //return files in dierctory
        public List<FileInfo> GetDirectoryFiles(string path)
        {
            List<FileInfo> files = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                // Determine whether the directory exists.
                if (dir.Exists)
                {
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        files.Add(file);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally { }
            return files;
        }
        //return directory Terms
        public List<string> GetDirectoryTerms(List<FileInfo> files)
        {
            List<string> DirectoryTerms = new List<string>();
            foreach (FileInfo file in files)
            {
                List<string> FileTerms = new List<string>();
                FileTerms = GetFileTerms(file);
                DirectoryTerms.AddRange(FileTerms);
            }
            DirectoryTerms.Sort();
            return DirectoryTerms;
        }
        //return file Terms
        public List<string> GetFileTerms(FileInfo file)
        {
            List<string> FileTerms = new List<string>();
            var reader = file.OpenText();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                foreach (var word in line.Split(delimiters(), StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!stopWords().Contains(word) && !FileTerms.Contains(word))
                    {
                        FileTerms.Add(word.Trim());
                    }
                }
                FileTerms.Sort();
            }
            return FileTerms ;
        }
        //stopWords
        public string[] stopWords()
        {
            String[] stopWords = { "{", "}", ">", "<", "~", "^", ":", ";",
                                   "(", ")", "-", "|", "/", "*", "$", "$",
                                   "%", "#", "@", "!", "+", ",", ".", ".",
                                   ".", "a", "as", "about", "after", "afterwards",
                                   "aint", "all", "already", "also", "although",
                                   "always", "am", "an", "and", "any", "are", "arent",
                                   "as", "at", "be", "because", "been", "before", "beforehand",
                                   "behind", "being", "below", "beside", "besides", "both", "but",
                                   "by", "did", "didnt", "do", "does", "doesnt", "doing", "dont",
                                   "done", "during", "each", "edu", "eg", "either", "else", "elsewhere",
                                   "from", "had", "hadnt", "has", "hasnt", "have", "havent", "having", "he",
                                   "hes", "her", "here", "heres", "hereafter", "hereby", "herein", "hereupon",
                                   "hers", "herself", "hi", "him", "himself", "his", "hither", "how", "if", "immediate",
                                   "inasmuch", "inc", "into", "inward", "is", "isnt", "it", "itd", "itll", "its", "its",
                                   "itself", "just", "last", "lately", "later", "latter", "latterly", "least", "little",
                                   "ltd", "mainly", "many", "maybe", "me", "meanwhile", "merely", "might", "more", "moreover",
                                   "much", "must", "my", "myself", "name", "namely", "nd", "near", "nearly", "necessary", "neither",
                                   "never", "nevertheless", "new", "next", "nine", "no", "nobody", "non", "none", "noone", "nor", "normally",
                                   "not", "nothing", "novel", "now", "nowhere", "obviously", "of", "off", "often", "oh", "ok", "okay", "old",
                                   "on", "once", "one", "ones", "only", "onto", "or", "other", "others", "otherwise", "ought", "our", "ours",
                                   "ourselves", "out", "outside", "over", "overall", "own", "particular", "particularly", "per", "perhaps", "placed",
                                   "please", "plus", "possible", "presumably", "probably", "que", "quite", "qv", "rather", "rd", "re", "really",
                                   "reasonably", "regardless", "regards", "relatively", "respectively", "right", "same", "second", "secondly", "self",
                                   "selves", "sensible", "serious", "seriously", "seven", "several", "she", "since", "so", "some", "somebody", "somehow",
                                   "someone", "something", "sometime", "sometimes", "somewhat", "somewhere", "soon", "sorry", "sub", "such", "sup", "sure",
                                   "ts", "th", "than", "that", "thats", "thats", "the", "their", "theirs", "them", "themselves", "thence", "there",
                                   "theres", "thereafter", "thereby", "therefore", "therein", "theres", "thereupon", "these", "they", "theyd", "theyll",
                                   "theyre", "theyve", "this", "thorough", "thoroughly", "those", "though", "three", "through", "throughout", "thru",
                                   "thus", "together", "too", "toward", "towards", "tries", "truly", "try", "trying", "twice", "two", "un", "under",
                                   "unfortunately", "unless", "unlikely", "until", "unto", "up", "upon", "us", "use", "used", "useful", "uses", "using",
                                   "usually", "value", "various", "very", "via", "viz", "vs", "way", "we", "wed", "well", "weve", "welcome", "well",
                                   "whatever", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "yes", "yet", "you", "youd",
                                   "youll", "youre", "youve", "your", "yours", "yourself", "yourselves" };
            return stopWords;
        }
        //term&file
        public int TermFrequency(string term, FileInfo file)
        {
            int termfrequency = 0;
            var reader = file.OpenText();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                foreach (var word in line.Split(delimiters(), StringSplitOptions.RemoveEmptyEntries))
                {
                    if (word.Equals(term))
                    {
                        termfrequency++;
                    }
                }
            }
            return termfrequency;
        }
        //term&files
        public int TermFrequency(string term, List<FileInfo> files)
        {
            int termfrequency = 0;
            foreach (FileInfo file in files)
            {
                var reader = file.OpenText();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var word in line.Split(delimiters(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (word.Equals(term))
                        {
                            termfrequency++;
                        }
                    }
                }
            }
            return termfrequency;
        }
    }
}

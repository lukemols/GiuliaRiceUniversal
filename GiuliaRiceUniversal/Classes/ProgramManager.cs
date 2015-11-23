using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Windows.Storage;

namespace GiuliaRiceUniversal.Classes
{
    static class ProgramManager
    {
        public enum Status { BUSY, LOADED, EMPTY}
        static Status state;
        static public Status State { get { return state; } }

        static List<Espression> Grammar;
        static List<Espression> Idiomatic;
        static List<Espression> Multiple;
        static List<Espression> Paraphrasing;
        static List<Espression> Phrasal;
        static List<Espression> Verb;
        static List<Espression> Vocabulary;

        static List<Espression> Test;
        public static int ExpressionsLeft { get { return Test.Count; } }

        /// <summary>
        /// Inizializza le liste
        /// </summary>
        public static void Initialize()
        {
            state = Status.EMPTY;
            Grammar = new List<Espression>();
            Idiomatic = new List<Espression>();
            Multiple = new List<Espression>();
            Paraphrasing = new List<Espression>();
            Phrasal = new List<Espression>();
            Verb = new List<Espression>();
            Vocabulary = new List<Espression>();
            Test = new List<Espression>();
            Clear();
        }

        static void Clear()
        {
            Grammar.Clear();
            Idiomatic.Clear();
            Multiple.Clear();
            Paraphrasing.Clear();
            Phrasal.Clear();
            Verb.Clear();
            Vocabulary.Clear();
            Test.Clear();
        }

        static public List<string> GetFilesList()
        {
            List<string> files = new List<string>();

            string path = Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"\Files";

            string[] f = Directory.GetFiles(path);
            for(int i = 0; i < f.Length; i++)
            {
                string[] s = f[i].Split('\\');
                f[i] = s[s.Length - 1];
            }
            foreach (string s in f)
                files.Add(s);//path + @"\" + 
            return files;
        }

        /// <summary>
        /// Metodo che carica il file selezionato
        /// </summary>
        /// <param name="fileName"></param>
        static public async void LoadFile(string fileName, List<string> exprTypes)
        {
            Initialize();
            state = Status.BUSY;
            fileName = Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"\Files\" + fileName;
            StreamReader sr;
            if (File.Exists(fileName))
            {
                sr = new StreamReader(fileName);

                string actualList = "";
                string line;

                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (line == "")
                        continue;
                    if (line[0] == '=')
                    {
                        line = line.Remove(0, 1);
                        actualList = line;
                        continue;
                    }
                    if (!line.Contains(">"))
                        continue;
                    string[] parts = line.Split('>');
                    Espression expr = new Espression(parts[0], parts[1]);

                    switch (actualList)
                    {
                        case "Grammar":
                            Grammar.Add(expr);
                            break;
                        case "Idiomatic":
                            Idiomatic.Add(expr);
                            break;
                        case "Multiple":
                            Multiple.Add(expr);
                            break;
                        case "Paraphrasing":
                            Paraphrasing.Add(expr);
                            break;
                        case "Phrasal":
                            Phrasal.Add(expr);
                            break;
                        case "Verb":
                            Verb.Add(expr);
                            break;
                        case "Vocabulary":
                            Vocabulary.Add(expr);
                            break;
                        default:
                            break;
                    }
                }
                sr.Close();
            }
            LoadExpressions(exprTypes);
        }
        
        /// <summary>
        /// Metodo che carica le espressioni
        /// </summary>
        /// <param name="exprTypes">Tipi di esercizi</param>
        static public void LoadExpressions(List<string> exprTypes)
        {
            Test.Clear();
            foreach(string s in exprTypes)
            {
                switch (s)
                {
                    case "Grammar":
                        foreach (Espression expr in Grammar)
                            Test.Add(expr);
                        break;
                    case "Idiomatic":

                        foreach (Espression expr in Idiomatic)
                            Test.Add(expr);
                        break;

                    case "Multiple":

                        foreach (Espression expr in Multiple)
                            Test.Add(expr);
                        break;
                    case "Paraphrasing":

                        foreach (Espression expr in Paraphrasing)
                            Test.Add(expr);
                        break;
                    case "Phrasal":

                        foreach (Espression expr in Phrasal)
                            Test.Add(expr);
                        break;
                    case "Verb":

                        foreach (Espression expr in Verb)
                            Test.Add(expr);
                        break;
                    case "Vocabulary":

                        foreach (Espression expr in Vocabulary)
                            Test.Add(expr);

                        break;
                }
            }
            Random rnd = new Random();
            List<Espression> tmp = new List<Espression>();
            while(Test.Count != 0)
            {
                int i = rnd.Next(0, Test.Count);
                tmp.Add(Test[i]);
                Test.RemoveAt(i);
            }
            Test = tmp;
            state = Status.LOADED;
        }

        static public Espression NextExpression()
        {
            if (Test.Count == 0)
            {
                state = Status.EMPTY;
                throw new ArgumentNullException();
            }

            Espression e = Test[0];
            Test.RemoveAt(0);
            return e;
        }
    }
}

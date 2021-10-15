namespace WordSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;



    class ProgramLogic
    {
        public List<string> textListOne { get; } = new List<string>();
        public string pathToFile1 { get; } = @"TextFiles\File1.txt";



        //Reads file 
        public void ReadFile(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }

        }

        public List<string> AddFileToList(string filePath, List<string> stringList)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string textFile;
                while ((textFile = sr.ReadLine()) != null)
                {
                    textFile=System.Text.RegularExpressions.Regex.Replace(textFile, @"[^a-zA-Z0-9\s]", string.Empty);
                    var words = textFile.Split(" ");

                    foreach (var w in words)
                    {
                        stringList.Add(w);
                    }          
                }
            }
            return stringList;
        }

        public bool CheckValueInList(string value, List<string> list)
        {
            if (list.Contains(value)) return true;
            else return false;
        }

        //    public void PrintArray(string[] stringArray)
        //    {
        //        foreach (var s in stringArray)
        //        {
        //            Console.WriteLine(stringArray);
        //        }
        //    }
        //}
    }
}

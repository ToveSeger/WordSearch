namespace WordSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    class ProgramLogic
    {
        public List<string> textListOne { get; set; } = new List<string>();
        public List<string> textListTwo { get; set; } = new List<string>();
        public List<string> textListThree { get; set; } = new List<string>();
        public string pathToFile1 { get; } = @"TextFiles\File1.txt";
        public string pathToFile2 { get; } = @"TextFiles\File2.txt";
        public string pathToFile3 { get; } = @"TextFiles\File2.txt";



        ////Reads file 
        //public void ReadFile(string filePath)
        //{
        //    using (StreamReader sr = File.OpenText(filePath))
        //    {
        //        string s;
        //        while ((s = sr.ReadLine()) != null)
        //        {
        //            Console.WriteLine(s);
        //        }
        //    }

        //}

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
                        w.ToLower();
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

        public void CheckValueInDocuments(string value)
        {
             textListOne = AddFileToList(pathToFile1, textListOne);
             textListTwo = AddFileToList(pathToFile2, textListTwo);
             textListThree = AddFileToList(pathToFile3, textListThree);

            var listOneContainsValue = CheckValueInList(value, textListOne);
            int counterListOne=0;

            foreach (var item in textListOne)
            {
                Console.WriteLine(item);
            }
            //if (listOneContainsValue)
            //{
            //    foreach (var i in textListOne)
            //    {
            //        if (i == value) counterListOne++;
            //    }
            //    Console.WriteLine($"{value} exists {counterListOne} times in text one");
            //}

            var listTwoContainsValue = CheckValueInList(value, textListTwo);
            var listThreeContainsValue = CheckValueInList(value, textListThree);
        }
    }
}

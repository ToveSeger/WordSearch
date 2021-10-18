namespace WordSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    class ProgramLogic
    {
        public List<string> textListOne { get; set; } = new List<string>();
        public List<string> textListTwo { get; set; } = new List<string>();
        public List<string> textListThree { get; set; } = new List<string>();
        public string pathToFile1 { get; } = @"TextFiles\File1.txt";
        public string pathToFile2 { get; } = @"TextFiles\File2.txt";
        public string pathToFile3 { get; } = @"TextFiles\File3.txt";

        internal int counterListOne { get; set; } = 0;
        internal int counterListTwo { get; set; } = 0;
        internal int counterListThree { get; set; } = 0;

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
                        string word = w.ToLower();                       
                        stringList.Add(word);
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
            //int counterListOne=0;
            //int counterListTwo = 0;
            //int counterListThree = 0;

            foreach (var i in textListOne.Where(x => x == value))
            {
                counterListOne++;
            }
            foreach (var i in textListTwo.Where(x => x == value))
            {
                counterListTwo++;
            }
            foreach (var i in textListThree.Where(x => x == value))
            {
                counterListThree++;
            }

            Console.WriteLine($"{value} exists {counterListOne} times in text one");
            Console.WriteLine($"{value} exists {counterListTwo} times in text two");
            Console.WriteLine($"{value} exists {counterListThree} times in text three");
           

            var listTwoContainsValue = CheckValueInList(value, textListTwo);
            var listThreeContainsValue = CheckValueInList(value, textListThree);
        }

        public void CheckHighestOccurence()
        {
            Dictionary<string, int> Occurrence = new Dictionary<string, int>();
            Occurrence.Add("List One", counterListOne);
            Occurrence.Add("List Two", counterListTwo);
            Occurrence.Add("List Three", counterListThree);

            var sortedOccurence = from entry in Occurrence orderby entry.Value descending select entry;
            Console.WriteLine("Ranking:");
            foreach (KeyValuePair<string, int> entry in sortedOccurence)
            {
                Console.WriteLine(entry.Key);
            }

        }
    }
}







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
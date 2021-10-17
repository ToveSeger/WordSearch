namespace WordSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Linq;

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
                    var words = textFile.Split(' ', ',', '.', '!', ')');
                    foreach (var item in words)
                    {
                        string word = item.ToLower();
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
             int counterListOne = 0;

            var listOneContainsValue = CheckValueInList(value, textListOne);
            

            //foreach (var item in textListOne)
            //{
            //    Console.WriteLine(item);
            //}
            //if (listOneContainsValue)
            //{
                foreach (var i in textListOne.Where(x => x == value))
                {
                    counterListOne++;
                }

                Console.WriteLine($"{value} exists {counterListOne} times in text one");
            //}

            //var listTwoContainsValue = CheckValueInList(value, textListTwo);
            //var listThreeContainsValue = CheckValueInList(value, textListThree);
        }
    }
}

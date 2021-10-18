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

        internal Dictionary<string, int> Occurrence = new Dictionary<string, int>();
        Dictionary<string, int> SavedOccurrence = new Dictionary<string, int>();
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

            //var listOneContainsValue = CheckValueInList(value, textListOne);

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

            //var listTwoContainsValue = CheckValueInList(value, textListTwo);
            //var listThreeContainsValue = CheckValueInList(value, textListThree);

            CheckHighestOccurence();
        }

        public void CheckHighestOccurence()
        {
            
            Occurrence.Add("Text One", counterListOne);
            Occurrence.Add("Text Two", counterListTwo);
            Occurrence.Add("Text Three", counterListThree);

            var sortedOccurence = from entry in Occurrence orderby entry.Value descending select entry;
            Console.WriteLine("Word occurrence ranking in text documents\nPresented in descending order:");
            foreach (KeyValuePair<string, int> entry in sortedOccurence)
            {
                Console.WriteLine(entry.Key);
            }
        }

        public void SaveToStructure(string word)
        {
            Console.WriteLine("Want to save the search result (y/n)?");
            char input = Convert.ToChar(Console.ReadLine().ToLower());
            var sortedOccurence = from entry in Occurrence orderby entry.Value descending select entry;
            SavedOccurrence.Add($"Word:{word}", Convert.ToInt32(null)); 
            if (input == 'y')
            {
                foreach (var item in sortedOccurence)
                {
                    SavedOccurrence.Add(item.Key, item.Value);
                }
                Console.WriteLine("Following is saved:");
                foreach (KeyValuePair<string, int> entry in SavedOccurrence)
                {
                    Console.WriteLine(entry.Key, entry.Value);
                }
            }
            if (input == 'n')
            {
                Occurrence.Remove("Text One");
                Occurrence.Remove("Text Two");
                Occurrence.Remove("Text Three");
            }
        }

        public void Menu()
        {            
            int input=0;
            while (input!=4)
            {
                Console.WriteLine("Hello, welcome to Word Search!\nWhat do you want to do?");
                Console.WriteLine("1.Search for a specific word in all documents.");
                Console.WriteLine("2.");
                Console.WriteLine("3.");
                Console.WriteLine("4. Exit");
                input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Console.WriteLine("Enter word to search for:");
                        string wordToSearchFor = Console.ReadLine();
                        CheckValueInDocuments(wordToSearchFor);
                        SaveToStructure(wordToSearchFor);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        Console.WriteLine("Bye, and welcome back!");
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Went a bit fast huh? Please try again using a valid number.");
                        break;
                }
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
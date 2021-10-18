namespace WordSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    class ProgramLogic
    {
        internal static string pathToFile1 { get; } = @"TextFiles\File1.txt";
        internal static string pathToFile2 { get; } = @"TextFiles\File2.txt";
        internal static string pathToFile3 { get; } = @"TextFiles\File3.txt";
        internal static List<string> textListOne { get; set; } = new List<string>();
        internal static List<string> textListTwo { get; set; } = new List<string>();
        internal static List<string> textListThree { get; set; } = new List<string>();

        internal Dictionary<string, int> Occurrence = new Dictionary<string, int>();
        internal Dictionary<string, int> SavedOccurrence = new Dictionary<string, int>();

        internal int counterListOne { get; set; } = 0;
        internal int counterListTwo { get; set; } = 0;
        internal int counterListThree { get; set; } = 0;

        string separator = "********************";

        public void PopulateLists()
        {
            textListOne = AddFileToList(pathToFile1, textListOne);
            textListTwo = AddFileToList(pathToFile2, textListTwo);
            textListThree = AddFileToList(pathToFile3, textListThree);
        }
        public static List<string> AddFileToList(string filePath, List<string> stringList)
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
             //textListOne = AddFileToList(pathToFile1, textListOne);
             //textListTwo = AddFileToList(pathToFile2, textListTwo);
             //textListThree = AddFileToList(pathToFile3, textListThree);

            //var listOneContainsValue = CheckValueInList(value, textListOne);
            ClearCounters();
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

            Console.WriteLine(separator);
            Console.WriteLine($"{value} exists {counterListOne} times in text one");
            Console.WriteLine($"{value} exists {counterListTwo} times in text two");
            Console.WriteLine($"{value} exists {counterListThree} times in text three");
            Console.WriteLine(separator);

            
            //var listTwoContainsValue = CheckValueInList(value, textListTwo);
            //var listThreeContainsValue = CheckValueInList(value, textListThree);

            if(counterListOne!=0 && counterListTwo!=0 && counterListThree!=0)
            {
                CheckHighestOccurence();
            }
           
        }

        public void ClearCounters()
        {
            counterListOne = 0;
            counterListTwo = 0;
            counterListThree = 0;
        }

        public void CheckHighestOccurence()
        {      
            Occurrence.Add("Text One", counterListOne);
            Occurrence.Add("Text Two", counterListTwo);
            Occurrence.Add("Text Three", counterListThree);

            var sortedOccurence = from entry in Occurrence orderby entry.Value descending select entry;
            Console.WriteLine(separator);
            Console.WriteLine("Word occurrence ranking in text documents\nPresented in descending order:");
            foreach (KeyValuePair<string, int> entry in sortedOccurence)
            {
                Console.WriteLine(entry.Key);
            }
            Console.WriteLine(separator);
        }

        internal bool CheckIfKeyIsSaved(string word)
        {
            var isSaved = SavedOccurrence.ContainsKey($"Word:{word}") ? true : false;
            return isSaved;
        }

        int count = 1;
        public void SaveToStructure(string word)
        {
            Console.WriteLine("Want to save the search result (y/n)?");
            char input = Convert.ToChar(Console.ReadLine().ToLower());
            var sortedOccurence = from entry in Occurrence orderby entry.Value descending select entry;
            if (input == 'y' && !CheckIfKeyIsSaved(word))
            {               
                SavedOccurrence.Add($"Word:{word}", Convert.ToInt32(null));
                foreach (var item in sortedOccurence)
                {
                    string keyItem = count + "." + item.Key;
                    SavedOccurrence.Add(keyItem, item.Value);
                    count++;
                }

                Console.WriteLine(separator);
                Console.WriteLine("Following is saved:");
                Console.WriteLine("(Presented in descending order counting occurrences of searched word)");
                Console.WriteLine(separator);
                foreach (KeyValuePair<string, int> entry in SavedOccurrence)
                {
                    Console.WriteLine(entry.Key, entry.Value);
                }
                Occurrence.Clear();
            }
            else if(input == 'y' && CheckIfKeyIsSaved(word)) Console.WriteLine("This word has already been searched for and is to be found in searched results");

            if (input == 'n')
            {
                Console.WriteLine("Search results were not saved.");
                Occurrence.Clear();
            }
        }

        public void PrintSavedSearchResults()
        {
            foreach (KeyValuePair<string, int> entry in SavedOccurrence)
            {
                Console.WriteLine(entry.Key, entry.Value);
            }
        }

        public void Menu()
        {            
            int input=0;
            while (input!=4)
            {
                Console.WriteLine(separator);
                Console.WriteLine("Hello, welcome to Word Search!\nWhat do you want to do?");
                Console.WriteLine("1. Search for a specific word in all documents.");
                Console.WriteLine("2. Print saved search results.");
                Console.WriteLine("3. Sort the documents in alphabetical order");
                Console.WriteLine("4. Exit");
                Console.WriteLine(separator);
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Faulty input. Please try again.");
                }

                switch (input)
                {
                    case 1:
                        Console.WriteLine("Enter word to search for:");
                        string wordToSearchFor = Console.ReadLine();
                        CheckValueInDocuments(wordToSearchFor);
                        SaveToStructure(wordToSearchFor);
                        break;
                    case 2:
                        PrintSavedSearchResults();
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
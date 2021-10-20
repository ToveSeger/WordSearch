namespace WordSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
     /// This class handles the entire logic for the program. It has the menu that nagivates the program but also the methods that make it possible
     /// to run it.
    /// </summary>
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

        
        /// <summary>
        /// PopulateLists will create separate lists for the different text files.
        /// </summary>
        public void PopulateLists()//O(3)
        {
            textListOne = AddFileToList(pathToFile1, textListOne);
            textListTwo = AddFileToList(pathToFile2, textListTwo);
            textListThree = AddFileToList(pathToFile3, textListThree);
        }
      
       
        /// <summary>
        /// AddFileToList will take two parameters in order for the method to be able to read a text file as long as it's not empty.
        /// It then reads the file, removes any special characters, takes every word and makes it lower case and adds it to a list.
        /// Which is then returned when calling on this method.
        /// </summary>
        /// <param name="filePath">The destination of the text file that will be used to extract words from.</param>
        /// <param name="stringList">The list that is used to save the words.</param>
        /// <returns></returns>
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
                        if (string.IsNullOrWhiteSpace(word)) stringList.Remove(word);
                    }          
                }
            }
            return stringList;
        }

        /*
         * 
         */
        /// <summary>
        /// This method will simply check if the "value" parameter is in the "list" parameter. If that's the case, then it will return true.
        /// </summary>
        /// <param name="value">This is the chosen word that we will check for.</param>
        /// <param name="list">The specified list to check for word in.</param>
        /// <returns></returns>
        public bool CheckValueInList(string value, List<string> list)
        {
            if (list.Contains(value)) return true;
            else return false;
        }


        /// <summary>
        /// CheckValueInDocuments is the method that will count how many times the parameter "value" exists in the 3 different texts.
        /// The "value" parameter is the chosen word that the user types in.
        /// After the check is done, it will print to the console the results, then as long as the counters aren't 0 it will
        /// use the method CheckHighestOccurence() to present a ranking of the documents.
        /// </summary>
        /// <param name="value">The word that will be counted.</param>
        public void CheckValueInDocuments(string value)
        {
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

            if (counterListOne == 0 && counterListTwo == 0 && counterListThree == 0)
            {
                return;
            }
            CheckHighestOccurence();

        }

        
        /// <summary>
        /// This method is here to clear the counters once they've been used. This exists in order to prevent the counters from starting to
        /// add the values from different searched words.
        /// </summary>
        public void ClearCounters()
        {
            counterListOne = 0;
            counterListTwo = 0;
            counterListThree = 0;
        }

        
        /// <summary>
        /// CheckHighestOccurence is the method that will rank the text files(lists) depending on the occurrence of the chosen word by the user.
        /// A Dictionary is used in order to make it possible to store both a int value but also attach that to a string value.
        /// </summary>
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

        
        /// <summary>
        /// This bool method will check if the word has already been searched and saved by the user.
        /// </summary>
        /// <param name="word">The word that will be checked for.</param>
        /// <returns></returns>
        internal bool CheckIfKeyIsSaved(string word)
        {
            var isSaved = SavedOccurrence.ContainsKey($"Word:{word}") ? true : false;
            return isSaved;
        }

        //Counter to keep track of unique key values in Dictionary SavedOccurrence. Declared outside of method scope to avoid duplicates. 
        int count = 1;
        /// <summary>
        /// Method to save a specified search result in data structure. 
        /// </summary>
        /// <param name="word">Input word to save in data structure.</param>
        public void SaveToStructure(string word)
        {
            Console.WriteLine("Want to save the search result (y/n)?");
            string input = Console.ReadLine().ToLower();
            var sortedOccurence = from entry in Occurrence orderby entry.Value descending select entry;
            
            switch (input)
            {
                case "y":
                    if (CheckIfKeyIsSaved(word)) Console.WriteLine("This word has already been searched for and is to be found in searched results");

                    else
                    {
                        SavedOccurrence.Add($"Word:{word}", Convert.ToInt32(null));
                        foreach (var item in sortedOccurence)
                        {
                            string keyItem = count + "." + item.Key;
                            SavedOccurrence.Add(keyItem, item.Value);
                            count++;
                        }
                        
                        Console.WriteLine(separator);
                        PrintSavedSearchResults();
                    }
                    Occurrence.Clear();
                    break;
                case "n":
                    Console.WriteLine("Search results were not saved.");
                    Occurrence.Clear();
                    break;

                default:
                    Console.WriteLine("Faulty input.Please choose y / n.");
                    break;
            }           
           
        }

        /// <summary>
        /// Method to print search saved search results. 
        /// </summary>
        public void PrintSavedSearchResults()//O(n+4)
        {
            Console.WriteLine(separator);
            Console.WriteLine("Following is saved:");
            Console.WriteLine("Presented in descending order counting occurrences of searched word");
            Console.WriteLine("(if no text exists under searched word, the word is not presented in any text.)");
                     
            foreach (KeyValuePair<string, int> entry in SavedOccurrence)
            {
                Console.WriteLine(entry.Key, entry.Value);
            }
        }
        /// <summary>
        /// Method to determine which list that the user wants to search in. 
        /// </summary>
        /// <param name="listNumber">Input number that has a list representation</param>
        /// <returns>The list that represents the listNumber</returns>
        internal List<string> ListChooser(int listNumber)//O(3n)
        {
            if (listNumber == 1) return textListOne;
            if (listNumber == 2) return textListTwo;
            if (listNumber == 3) return textListThree;
            return null;
        }

        //Int to keep track of the index value of the current word in the list. Declared outside of method scope to avoid to zero set the variable. 
        int word = 0;
        /// <summary>
        /// Method to present the first given number of words in a given text. 
        /// Recursion is used to avoid nested loops and use the stack instead. 
        /// </summary>
        /// <param name="listNumber">Input number that has a list representation</param>
        /// <param name="amount">Amount of words that user wants presented</param>
        public void GetAmountOfWords(int listNumber, int amount)//O(log n)
        {           
            var listToSort = ListChooser(listNumber);

            listToSort.Sort();
            
            if (string.IsNullOrWhiteSpace(listToSort[word]) && listToSort[word] == String.Empty)
            {
                listToSort.Remove(listToSort[word]);
            }

            Console.WriteLine(listToSort[word]);

            if (word < amount - 1)
            {
                word++;
                GetAmountOfWords(listNumber, amount);
            }
            else
            {
                word = 0;
                return;
            }
          
        }

        /// <summary>
        /// Method to present a menu to user. Collects input from user and redirects to the correct method. 
        /// </summary>
        public void Menu()
        {            
            int input=0;
            while (input!=4)
            {
                Console.WriteLine(separator);
                Console.WriteLine("Hello, welcome to Word Search!\nWhat do you want to do?");
                Console.WriteLine("1. Search for a specific word in all documents.");
                Console.WriteLine("2. Print saved search results.");
                Console.WriteLine("3. Sort the documents in alphabetical order and present the first x words");
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
                        Console.WriteLine("Please choose document by entering one of the following numbers:");
                        Console.WriteLine("1. Text One");
                        Console.WriteLine("2. Text Two");
                        Console.WriteLine("3. Text Three");
                        try
                        {
                            int chosenText = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("How many words do you want to present?");
                            int chosenAmount = Convert.ToInt32(Console.ReadLine());
                            GetAmountOfWords(chosenText, chosenAmount);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Faulty input, please try again.");
                            goto case 3;
                        }
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

namespace WordSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;



    class ProgramLogic
    {
        public List<string> textListOne {get;}= new List<string>();
        public string pathToFile1 {get;} =@"TextFiles\File1.txt";


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

        public void AddFileToList(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    textListOne.Add(s);
                    //foreach (var item in textListOne)
                    //{
                    //    Console.WriteLine(item);
                    //}
                }
            }
        }

        public bool CheckValueInList(string value, List<string> list)
        {
            if (list.Contains(value)) return true;
            else return false;
        }
    }
}

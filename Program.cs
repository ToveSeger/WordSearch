namespace WordSearch
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {

           ProgramLogic pl = new ProgramLogic();
           var textList =  pl.AddFileToList(pl.pathToFile1, pl.textListOne);
           pl.ReadFile(pl.pathToFile1);
            //foreach (var s in textList)
            //{
            //    Console.WriteLine(s);
            //}


            //bool valueExists=pl.CheckValueInList("Ernst Cassirer", pl.textListOne);
            //Console.WriteLine(valueExists);
        }
    }
}

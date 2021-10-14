namespace WordSearch
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {

            ProgramLogic pl = new ProgramLogic();
            pl.AddFileToList(pl.pathToFile1);
            bool valueExists=pl.CheckValueInList("Ernst Cassirer", pl.textListOne);
            Console.WriteLine(valueExists);
        }
    }
}

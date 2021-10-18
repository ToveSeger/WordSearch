namespace WordSearch
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {


           ProgramLogic pl = new ProgramLogic();
            pl.PopulateLists();
            pl.Menu();
           //pl.CheckValueInDocuments("on");
           // pl.CheckHighestOccurence();
           //var textList =  pl.AddFileToList(pl.pathToFile1, pl.textListOne);

           // foreach (var s in textList)
           // {
           //     Console.WriteLine(s);
           // }


           // bool valueExists=pl.CheckValueInList("Ernst", pl.textListOne);
           // Console.WriteLine(valueExists);
        }
    }
}

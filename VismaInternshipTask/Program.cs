using System;

namespace VismaInternshipTask
{
    class Program
    {
        const string JsonFilePath = "..\\..\\..\\SampleData.json";
        static void Main(string[] args)
        {
            // deserialize JSON directly from a file to the Library
            Library Library = InOutServices.ReadJsonToLibrary(JsonFilePath);
            //Calling an interactive terminal interface for the user
            InOutServices.TerminalUI(Library);
            Console.WriteLine("Thank you for using Visma's services.");
        }
    }
}

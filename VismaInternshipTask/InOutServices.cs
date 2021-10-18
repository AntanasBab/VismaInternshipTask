using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace VismaInternshipTask
{
    class InOutServices
    {
        /// <summary>
        /// Reading JSON from file
        /// </summary>
        /// <param name="FilePath">File path to the JSON</param>
        /// <returns>Populated library with JSON data</returns>
        public static Library ReadJsonToLibrary(string FilePath)
        {
            List<Book> jsonfile = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(FilePath));
            Console.WriteLine(JsonConvert.SerializeObject(DateTime.Now));
            Library result = new Library(jsonfile);
            return result;
        }
        /// <summary>
        /// Prints a header of a book table to the terminal
        /// </summary>
        private static void BookTableHeader()
        {
            Console.WriteLine(new string('-', 164));
            Console.WriteLine(string.Format("| {0,2} | {1,20} | {2,20} | {3,20} | {4,10} | {5,18:yyyy-MM-dd} | {6,10} | {7,20} | {8,16:yyyy-MM-dd} |", "#", "Name", "Author", "Category", "Language", "Publication Date", "ISBN", "Borrower", "Return Date"));
            Console.WriteLine(new string('-', 164));
        }
        /// <summary>
        /// Main function which allows user to call commands on the library and interacts with the user's input
        /// </summary>
        /// <param name="Library">Library with all of the Book objects</param>
        public static void TerminalUI(Library Library)
        {
            List<string> ValidCommands = new List<string> { "add", "take", "return", "list", "delete", "exit" };
            Console.WriteLine("Welcome to Visma's Library!");
            Console.WriteLine();
            Console.WriteLine("Please enter a command to be executed.");
            while (true)
            {
                Console.Write("List of valid commands: ");
                foreach (string command in ValidCommands) //Displaying all valid commands
                {
                    Console.Write(string.Format(" {0}  ", command));
                }
                Console.WriteLine();
                string UserInput = Console.ReadLine();
                try
                {
                    switch (UserInput) //Covering all cases of valid commands for the library
                    {
                        case "add": //Asking the user for valid inputs line by line then processing input data with library functions
                            Console.WriteLine("Please enter the Book's characteristics as follows: ");
                            Console.Write("Name: "); string name = Console.ReadLine();
                            Console.Write("Author: "); string author = Console.ReadLine();
                            Console.Write("Category: "); string category = Console.ReadLine();
                            Console.Write("Language: "); string lang = Console.ReadLine();
                            Console.Write("PublicationDate: "); DateTime PubDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("ISBN: "); int isbn = int.Parse(Console.ReadLine());
                            Library.Add(new Book(name, author, category, lang, PubDate, isbn));
                            Console.WriteLine("The book was added successfully!");
                            break;

                        case "take": //Asking the user for valid inputs line by line then processing input data with library functions
                            Console.Write("ISBN of the book you are looking for: "); int Isbn = int.Parse(Console.ReadLine());
                            Console.Write("Your name: "); string borrower = Console.ReadLine();
                            Console.Write("Returning date: "); DateTime returndate = DateTime.Parse(Console.ReadLine());
                            Library.Take(Isbn, borrower, returndate);
                            Console.WriteLine("The book was borrowed successfully");
                            break;

                        case "return": //Asking the user for valid inputs line by line then processing input data with library functions
                            Console.Write("ISBN of your book: "); int returnisbn = int.Parse(Console.ReadLine());
                            Console.Write("Your name: "); string client = Console.ReadLine();
                            Console.Write("Returning date: "); DateTime returneddate = DateTime.Parse(Console.ReadLine());
                            Library.ReturnBook(returnisbn, client, returneddate);
                            Console.WriteLine("The book was returned successfully!");
                            break;

                        case "list":
                            List<string> ValidFilters = new List<string>() {"author", "category", "language", "isbn", "name", "available", "taken" };
                            Console.Write("By which parameter you would like to filter by ( \"all\" if no filters): "); string filter = Console.ReadLine();

                            int counter = 1;
                            if (filter == "all") //Checking if the user wants an unfiltered table
                            {
                                List<Book> FilteredBooks = Library.ListAllBooks(filter, "");
                                if (FilteredBooks.Count > 0) //Checking if the filtered library is empty
                                {
                                    BookTableHeader();
                                    foreach (var book in Library.ListAllBooks(filter, ""))
                                    {
                                        Console.WriteLine(string.Format("| {0,2} ", counter++) + book.ToString());
                                    }
                                    Console.WriteLine(new string('-', 164));
                                }
                                else
                                {
                                    Console.WriteLine("No books were found.");
                                }
                            }
                            else //Calling the function with a specified filter and any value to filter by
                            {
                                Console.Write("Enter wanted value to filter by: "); string value = Console.ReadLine();
                                List<Book> FilteredBooks = Library.ListAllBooks(filter, value);
                                if (FilteredBooks.Count > 0) //Checking if the filtered library is empty
                                {
                                    BookTableHeader();
                                    foreach (var book in Library.ListAllBooks(filter, value))
                                    {
                                        Console.WriteLine(string.Format("| {0,2} ", counter++) + book.ToString());
                                    }
                                    Console.WriteLine(new string('-', 164));
                                }
                                else
                                {
                                    Console.WriteLine("No books were found.");
                                }
                            }
                            break;

                        case "delete":
                            Console.Write("ISBN of the book to remove: "); int removeisbn = int.Parse(Console.ReadLine());
                            Library.Remove(removeisbn);
                            Console.WriteLine("The book was removed succesfully!");
                            break;

                        case "exit":
                            return;

                        default:
                            Console.WriteLine("An invalid command was entered. Please try again.");
                            Console.WriteLine();
                            continue;
                    }
                }
                catch(Exception ex) //Printing any error messages which are caused by invalid data input
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }
    }
}

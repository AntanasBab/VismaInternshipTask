using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaInternshipTask
{
    public class Library
    {
        //Data structure to store JSON data as Books
        private List<Book> Books;

        public Library() //Default constructor
        {
            Books = new List<Book>();
        }
        public Library(List<Book> initialList) //Constructor with an initialized list
        {
            Books = initialList;
        }
        /// <summary>
        /// Function to return a book to the library
        /// </summary>
        /// <param name="book">Any book object</param>
        public void Add(Book book) 
        {
            Books.Add(book);
        }
        /// <summary>
        /// Function to borrow a book from the library
        /// </summary>
        /// <param name="ISBN">Book's ISBN</param>
        /// <param name="Name">Borrower's name</param>
        /// <param name="ReturnDate">Expected book's return date</param>
        public void Take(int ISBN, string Name, DateTime ReturnDate) 
        {
            if (Books.Any(x => x.ISBN == ISBN && !x.Taken)) //Checking if any of the available books match given ISBN
            {
                if(Books.Where(x => x.Taken && x.GetBorrower() == Name).Count() < 3) //Checking if the user already has taken 3 books (maximum)
                {
                    TimeSpan Period = ReturnDate - DateTime.Now;
                    if(Period.Days < 0) //Checking if the return date is valid
                    {
                        throw new Exception("Your return date is in the past. We cannot timetravel!");
                    }
                    if(DateTime.Now.Add(Period) > DateTime.Now.AddMonths(2)) //Checking if return date does not exceed 2 month borrow time limit
                    {
                        throw new Exception("You can only take the book for two months. Please specify an earlier return date.");
                    }
                    //Converting an available book to taken
                    Book Taken = Books.First(x => x.ISBN == ISBN && !x.Taken);
                    Taken.Take(DateTime.Now.Add(Period), Name);
                }
                else
                {
                    throw new Exception("One person cannot borrow more than 3 books.");
                }
            }
            else
            {
                throw new Exception("There were no available books with the given ISBN.");
            }
        }
        /// <summary>
        /// Function to return a taken book to the library
        /// </summary>
        /// <param name="ISBN">Book's ISBN</param>
        /// <param name="name">Borrower's name</param>
        /// <param name="GivenDate">Return date of the book</param>
        public void ReturnBook(int ISBN, string name, DateTime GivenDate) 
        {
            if(!Books.Any(book => book.Taken && book.ISBN == ISBN && book.GetBorrower() == name)) //Checking if a book with specified parameters exists
            {
                throw new Exception("There are no taken books with the given ISBN or with your name, please review.");
            }
            else
            {
                Book Book = Books.First(x => ISBN == x.ISBN && x.GetBorrower() == name); //Processing the first book which matches given input
                var ReturnDate = Book.Return();
                if(ReturnDate < GivenDate) //Checking if the return date is late
                {
                    Console.WriteLine("You returned the book late. Time to pay for your sins.");
                }
            }
        }
        /// <summary>
        /// Function to get a filtered list of books
        /// </summary>
        /// <param name="parameter">Specified column to filter by</param>
        /// <param name="input">Any string input to filter values by</param>
        /// <returns></returns>
        public List<Book> ListAllBooks(string parameter, string input)
        {

            switch (parameter) //Creating cases for all possible filters
            {
                case "author":
                    return Books.Where(book => book.Author == input).ToList();
                case "category":
                    return Books.Where(book => book.Category == input).ToList();
                case "language":
                    return Books.Where(book => book.Language == input).ToList();
                case "isbn":
                    return Books.Where(book => book.ISBN == int.Parse(input)).ToList();
                case "name":
                    return Books.Where(book => book.Name == input).ToList();
                case "taken":
                    return Books.Where(book => book.Taken).ToList();
                case "available":
                    return Books.Where(book => !book.Taken).ToList();
                case "all":
                    return Books;
                default:
                    throw new Exception("Your filter selection is invalid. Please try again.");
            }
        }
        /// <summary>
        /// Function to remove any book from the library
        /// </summary>
        /// <param name="ISBN">Book's ISBN</param>
        public void Remove(int ISBN)
        {
            if (Books.Any(book => book.ISBN == ISBN)) Books.Remove(Books.First(x => x.ISBN == ISBN));
            else throw new Exception("No book is present with the given ISBN.");
        }
    }
}

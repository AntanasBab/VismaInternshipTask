using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaInternshipTask
{
    public class Book
    {
        public string Name { get ; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime Publication_Date { get; set; }
        public int ISBN { get; set; }
        public bool Taken { get; set; }
        private DateTime? ReturnDate;
        private string Borrower;
        public Book(string name, string author, string category, string language, DateTime publication_date, int iSBN)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            Publication_Date = publication_date;
            ISBN = iSBN;
            Taken = false;
        }
        /// <summary>
        /// Function to fill borrowed book's properties
        /// </summary>
        /// <param name="returnDate">Expected return date</param>
        /// <param name="borrower">Borrower's name</param>
        public void Take(DateTime returnDate, string borrower)
        {
            Taken = true;
            ReturnDate = returnDate;
            Borrower = borrower;
        }
        /// <summary>
        /// Fuunction to convert a taken book back to an available one
        /// </summary>
        /// <returns>Expected return date</returns>
        public DateTime? Return()
        {
            var date = ReturnDate;
            Borrower = null;
            Taken = false;
            ReturnDate = null;
            return date;
        }
        /// <summary>
        /// Function to get the book's borrower name
        /// </summary>
        /// <returns>book's borrower name</returns>
        public string GetBorrower()
        {
            return Borrower;
        }
        /// <summary>
        /// Overriding ToString() to properly format a printable table line with book data
        /// </summary>
        /// <returns>Formated book object as string</returns>
        public override string ToString()
        {
            return !Taken ? string.Format("| {0,20} | {1,20} | {2,20} | {3,10} | {4,18:yyyy-MM-dd} | {5,10} | {6,20} | {7,16:yyyy-MM-dd} |", this.Name, this.Author, this.Category, this.Language, this.Publication_Date, this.ISBN,"","") :
                string.Format("| {0,20} | {1,20} | {2,20} | {3,10} | {4,18:yyyy-MM-dd} | {5,10} | {6,20} | {7,16:yyyy-MM-dd} |", this.Name, this.Author, this.Category, this.Language, this.Publication_Date, this.ISBN, this.Borrower, this.ReturnDate);

        }
    }
}

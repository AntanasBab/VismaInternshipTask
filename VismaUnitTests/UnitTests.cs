using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using VismaInternshipTask;

namespace VismaUnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AddBookToLibrary_AddsOneBook_CountIsCorrect()
        {
            //Arange
            Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
            Library ExpectedResult = new Library(new List<Book>() { book });
            Library Lib = new Library();
            //Act
            Lib.Add(book);

            //Assert
            Assert.AreEqual(Lib.ListAllBooks("all", "").Count, ExpectedResult.ListAllBooks("all", "").Count);
        }
        [TestMethod]
        public void AddCopiesOfBookToLibrary_AddCopiedBooks_CountIsCorrect()
        {
            //Arange
            List<Book> BookList = new List<Book>();
            for(int i = 0; i < 5; i++)
            {
                Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
                BookList.Add(book);
            }
            Library ExpectedResult = new Library(BookList);
            Library Lib = new Library();
            //Act
            foreach(var book in BookList)
            {
                Lib.Add(book);
            }
            //Assert
            Assert.AreEqual(Lib.ListAllBooks("all", "").Count, ExpectedResult.ListAllBooks("all", "").Count);
        }
        [TestMethod]
        public void TakeBookFromLibrary_TakeOneBook_TakenBookCountCorrect()
        {
            //Arange
            int ExpecetdCount = 1;
            Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
            Library Lib = new Library(new List<Book>() { book });
            //Act
            Lib.Take(0,"Borrower",System.DateTime.Now.AddDays(30));
            //Assert
            Assert.AreEqual(Lib.ListAllBooks("taken", "").Count, ExpecetdCount);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TakeBookFromEmptyLibrary_TakeOneBook_ErrorMessageIsThrown()
        {
            //Arange
            Library Lib = new Library();
            //Act
            try
            {
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(30));
            }
            catch(Exception ex)
            {
                //Assert
                Assert.AreEqual("There were no available books with the given ISBN.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TakeBookFromLibrary_TakeBookBadISBN_ErrorMessageIsThrown()
        {
            //Arange
            Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
            Library Lib = new Library(new List<Book>() { book });
            //Act
            try
            {
                Lib.Take(112, "Borrower", System.DateTime.Now.AddDays(30));
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("There were no available books with the given ISBN.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TakeBookFromLibrary_TakeAlreadyTakenBook_ErrorMessageIsThrown()
        {
            //Arange
            Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
            Library Lib = new Library(new List<Book>() { book });
            //Act
            try
            {
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(30));
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(30));
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("There were no available books with the given ISBN.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TakeBookFromLibrary_TakeMoreThan3Books_ErrorMessageIsThrown()
        {
            //Arange
            Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
            Book book1 = new Book("dummy", "", "", "", System.DateTime.Now, 1);
            Book book2 = new Book("dummy", "", "", "", System.DateTime.Now, 2);
            Book book3 = new Book("dummy", "", "", "", System.DateTime.Now, 3);
            Library Lib = new Library(new List<Book>() { book, book1, book2, book3 });
            //Act
            try
            {
                Lib.Take(1, "Borrower", System.DateTime.Now.AddDays(30));
                Lib.Take(2, "Borrower", System.DateTime.Now.AddDays(30));
                Lib.Take(3, "Borrower", System.DateTime.Now.AddDays(30));
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(30));
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("One person cannot borrow more than 3 books.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TakeBookFromLibrary_BookWithBadReturnDate_ErrorMessageIsThrown()
        {
            //Arange
            Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
            Library Lib = new Library(new List<Book>() { book });
            //Act
            try
            {
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(-30));
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("Your return date is in the past. We cannot timetravel!", ex.Message);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TakeBookFromLibrary_BookWithTooLongReturnDate_ErrorMessageIsThrown()
        {
            //Arange
            Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
            Library Lib = new Library(new List<Book>() { book });
            //Act
            try
            {
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(100));
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("You can only take the book for two months. Please specify an earlier return date.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        public void ReturnBookToLibrary_ReturnOneBook_AvailableBookCountCorrect()
        {
            //Arange
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("dummy", "", "", "", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(50));
            Lib.ReturnBook(0, "Borrower", System.DateTime.Now.AddDays(20));
            //Assert
            Assert.AreEqual(Lib.ListAllBooks("taken","").Count, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReturnBookToLibrary_ReturnBookBadISBN_ErrorMessageIsThrown()
        {
            //Arange
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("dummy", "", "", "", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            try
            {
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(50));
                Lib.ReturnBook(1, "Borrower", System.DateTime.Now.AddDays(20));
            }
            catch(Exception ex)
            {
                //Assert
                Assert.AreEqual("There are no taken books with the given ISBN or with your name, please review.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReturnBookToLibrary_ReturnBookBadBorrowerName_ErrorMessageIsThrown()
        {
            //Arange
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("dummy", "", "", "", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            try
            {
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(50));
                Lib.ReturnBook(0, "Borrower2", System.DateTime.Now.AddDays(20));
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("There are no taken books with the given ISBN or with your name, please review.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReturnBookToLibrary_NoTakenBooksArePresent_ErrorMessageIsThrown()
        {
            //Arange
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("dummy", "", "", "", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            try
            {
                Lib.Take(0, "Borrower", System.DateTime.Now.AddDays(50));
                Lib.ReturnBook(0, "Borrower", System.DateTime.Now.AddDays(20));
                Lib.ReturnBook(0, "Borrower", System.DateTime.Now.AddDays(20));
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("There are no taken books with the given ISBN or with your name, please review.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        public void ListBooksFromLibrary_ListBooksByAuthor_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 5;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            var result = Lib.ListAllBooks("author","A");
            //Assert
            Assert.AreEqual(result.Count, ExpectedCount);
        }
        [TestMethod]
        public void ListBooksFromLibrary_ListBooksByCategory_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 5;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            var result = Lib.ListAllBooks("category", "B");
            //Assert
            Assert.AreEqual(result.Count, ExpectedCount);
        }
        [TestMethod]
        public void ListBooksFromLibrary_ListBooksByLanguage_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 5;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            var result = Lib.ListAllBooks("language", "C");
            //Assert
            Assert.AreEqual(result.Count, ExpectedCount);
        }
        [TestMethod]
        public void ListBooksFromLibrary_ListBooksByISBN_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 5;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, 0);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            var result = Lib.ListAllBooks("isbn", "0");
            //Assert
            Assert.AreEqual(result.Count, ExpectedCount);
        }
        [TestMethod]
        public void ListBooksFromLibrary_ListBooksByName_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 5;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            var result = Lib.ListAllBooks("name", "Dummy");
            //Assert
            Assert.AreEqual(result.Count, ExpectedCount);
        }
        [TestMethod]
        public void ListBooksFromLibrary_ListBooksByTaken_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 3;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            for (int i = 0; i < 3; i++)
            {
                Lib.Take(i,"Borrower", System.DateTime.Now);
            }
            //Act
            var result = Lib.ListAllBooks("taken", "");
            //Assert
            Assert.AreEqual(result.Count, ExpectedCount);
        }
        [TestMethod]
        public void ListBooksFromLibrary_ListBooksByAvailable_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 5;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            var result = Lib.ListAllBooks("available", "");
            //Assert
            Assert.AreEqual(result.Count, ExpectedCount);
        }
        [TestMethod]
        public void ListBooksFromLibrary_ListAllBooks_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 5;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            var result = Lib.ListAllBooks("all", "");
            //Assert
            Assert.AreEqual(result.Count, ExpectedCount);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ListBooksFromLibrary_WrongFilterUsed_ErrorMessageIsThrown()
        {
            //Arange
            Library Lib = new Library();
            //Act
            try
            {
                Lib.ListAllBooks("XXX", "XX");
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("Your filter selection is invalid. Please try again.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        public void RemoveBookFromLibrary_RemoveOneBook_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 4;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, i);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            Lib.Remove(0);
            //Assert
            Assert.AreEqual(Lib.ListAllBooks("all","").Count, ExpectedCount);
        }
        [TestMethod]
        public void RemoveBookFromLibrary_RemoveTwoIndenticalBooks_CountIsCorrect()
        {
            //Arange
            int ExpectedCount = 3;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            Lib.Remove(0);
            Lib.Remove(0);
            //Assert
            Assert.AreEqual(Lib.ListAllBooks("all", "").Count, ExpectedCount);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveBookFromLibrary_RemoveBookWithBadISBN_ErrorMessageIsThrown()
        {
            //Arange
            int ExpectedCount = 3;
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 5; i++)
            {
                Book book = new Book("dummy", "", "", "", System.DateTime.Now, 0);
                BookList.Add(book);
            }
            Library Lib = new Library(BookList);
            //Act
            try
            {
                Lib.Remove(1110);
            }
            catch(Exception ex)
            {
                //Assert
                Assert.AreEqual("No book is present with the given ISBN.", ex.Message);
                throw;
            }
        }
        [TestMethod]
        public void ReturnBook_ConvertTakenBook_ReturnDateIsCorrect()
        {
            //Arange
            Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, 11);
            //Act
            book.Take(DateTime.Parse("09/09/2009"),"");
            var Date = book.Return();
            //Assert
            Assert.AreEqual(Date, DateTime.Parse("09/09/2009"));
        }
        [TestMethod]
        public void CheckTakenBookBorrower_GetBorrower_ReturnsCorrectName()
        {
            //Arange
            Book book = new Book("Dummy", "A", "B", "C", System.DateTime.Now, 11);
            var expected = "Name";
            //Act
            book.Take(DateTime.Parse("09/09/2009"), "Name");
            //Assert
            Assert.AreEqual(book.GetBorrower(), expected);
        }
    }
}

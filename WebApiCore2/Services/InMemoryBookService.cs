using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore2.Models;

namespace WebApiCore2.Services
{
    public class InMemoryBookService : IBookService
    {
        //private DataContext _context;

        //public InMemoryBookService(DataContext context)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}
        List<Book> Books;

        public InMemoryBookService()
        {
            Books = new List<Book>()
            {
                new Book { Id = 1, Title = "Pizza", Description="Maryland", AmountOfPages=1},
                new Book { Id = 2, Title = "London", Description="London", AmountOfPages=1},
                new Book { Id = 3, Title = "California", Description = "California", AmountOfPages=1}
            };

        }

        public Book GetById(int id)
        {
            return Books.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Book> GetBooksByTitle(string title = null)
        {
            return from r in Books
                   where string.IsNullOrEmpty(title) || r.Title.StartsWith(title)
                   orderby r.Title
                   select r;
        }

        public int GetCountOfBooks()
        {
            return Books.Count();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(int authorId)
        {
            if (authorId <= 0)
            {
                throw new ArgumentException(nameof(authorId));
            }

            var result = Books
                .Where(b => b.AuthorId == authorId)
                .ToList();
            return await Task.FromResult(result);

            //return await _context.Books
            //    .Include(b => b.Author)
            //    .Where(b => b.AuthorId == authorId)
            //    .ToListAsync();
        }

        public async Task<Book> GetBookAsync(int authorId, int bookId)
        {
            if (authorId <= 0)
            {
                throw new ArgumentException(nameof(authorId));
            }

            if (bookId <= 0)
            {
                throw new ArgumentException(nameof(bookId));
            }

            var result = Books
              .Where(b => b.AuthorId == authorId && b.Id == bookId)
              .FirstOrDefault();
            return await Task.FromResult(result);

            //return await _context.Books
            //    .Include(b => b.Author)
            //    .Where(b => b.AuthorId == authorId && b.Id == bookId)
            //    .FirstOrDefaultAsync();
        }

        public async Task<Book> AddBook(Book bookToAdd)
        {
            if (bookToAdd == null)
            {
                throw new ArgumentNullException(nameof(bookToAdd));
            }

            var result = bookToAdd;
            Books.Add(bookToAdd);
            bookToAdd.Id = Books.Max(r => r.Id) + 1;
            return await Task.FromResult(bookToAdd);
            //_context.Add(bookToAdd);
        }

        public async Task<Book> UpdateBook(Book updatedBook)
        {
            if (updatedBook == null)
            {
                throw new ArgumentNullException(nameof(updatedBook));
            }

            var book = Books.SingleOrDefault(r => r.Id == updatedBook.Id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
            }

            return book;
        }

        public async Task<Book> Delete(int id)
        {
            var book = Books.FirstOrDefault(r => r.Id == id);
            if (book != null)
            {
                Books.Remove(book);
            }

            return null;
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true if 1 or more entities were changed
            var result = true;
            return await Task.FromResult(result);

            //return (await _context.SaveChangesAsync() > 0);
        }

        public int Commit()
        {
            return 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_context != null)
                //{
                //    _context.Dispose();
                //    _context = null;
                //}
            }
        }

    }
}

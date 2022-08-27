using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppApi6.Models;

namespace WebAppApi6.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<Book>> GetBooksAsync(int authorId);

        Task<Book> GetBookAsync(int authorId, int bookId);

        Task<Book> AddBook(Book bookToAdd);

        Task<bool> SaveChangesAsync();
    }
}
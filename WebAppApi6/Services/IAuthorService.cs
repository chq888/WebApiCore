using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppApi6.Models;

namespace WebAppApi6.Services
{
    public interface IAuthorService
    {
        Task<bool> AuthorExistsAsync(int authorId);

        Task<IEnumerable<Author>> GetAuthorsAsync();

        Task<Author> GetAuthorAsync(int authorId);

        void UpdateAuthor(Author author);

        Task<bool> SaveChangesAsync();
    }
}
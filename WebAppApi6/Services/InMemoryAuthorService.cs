using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppApi6.Models;

namespace WebAppApi6.Services
{
    public class InMemoryAuthorService : IAuthorService
    {
        //private DataContext _context;

        //public InMemoryAuthorService(DataContext context)
        //{
        //    _context = context;
        //}

        List<Author> Authors = new List<Author>();

        public InMemoryAuthorService()
        {
            Authors.Add(new Author()
                {
                    Id = 1,
                    FirstName = "A",
                    LastName = "Last",
                    Books = new List<Book>()}
                );
            Authors.Add(new Author()
            {
                Id = 2,
                FirstName = "B",
                LastName = "Last",
                Books = new List<Book>()
            });
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            var result = Authors.Any(a => a.Id == authorId);
            return await Task.FromResult(result);
            //return await Authors.AnyAsync(a => a.Id == authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            var result = Authors;
            return await Task.FromResult(result);
            //return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorAsync(int authorId)
        {
            if (authorId <= 0)
            {
                throw new ArgumentException(nameof(authorId));
            }

            var result = Authors.FirstOrDefault(a => a.Id == authorId);
            return await Task.FromResult(result);

            //return await _context.Authors
            //    .FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public void UpdateAuthor(Author author)
        {
            // no code in this implementation
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = true;
            return await Task.FromResult(result);

            // return true if 1 or more entities were changed
            // return (await _context.SaveChangesAsync() > 0);
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

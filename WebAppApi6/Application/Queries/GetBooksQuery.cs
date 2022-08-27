using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAppApi6.Models;
using WebAppApi6.Services;

namespace Application.CQRS.TodoLists.Queries.GetTodos
{
    /// <summary>
    /// Defines Todo items query.
    /// </summary>
    public class GetBooksQuery : IRequest<IEnumerable<Book>>
    { }

    /// <summary>
    /// Defines hadler for Todo items query.
    /// </summary>
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookService bookService;

        public GetBooksQueryHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IEnumerable<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await bookService.GetBooksAsync();
        }

    }
}
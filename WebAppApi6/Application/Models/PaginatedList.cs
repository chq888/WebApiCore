using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Models
{
    /// <summary>
    /// Represents items in the List format with pagination.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// List of items.
        /// </summary>
        public List<T> Items { get; }

        /// <summary>
        /// Index of the currect page.
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// Total pages count.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Total Items count.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Define <see cref="PaginatedList{T}"/> entity.
        /// </summary>
        /// <param name="items">Items to store in paginated list.</param>
        /// <param name="count">Entities total count.</param>
        /// <param name="pageIndex">Index of the current page.</param>
        /// <param name="pageSize">Entities count per page.</param>
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        /// <summary>
        /// Check whether there is previous page.
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// Check whether there is next page.
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;

        /// <summary>
        /// Create paginated list with Querable source.
        /// </summary>
        /// <param name="source">Querable Items to store.</param>
        /// <param name="pageIndex">Required page index.</param>
        /// <param name="pageSize">Required page size.</param>
        /// <returns></returns>
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
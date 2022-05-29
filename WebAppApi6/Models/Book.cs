using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppApi6.Models
{
    /// <summary>
    /// Book with id and name
    /// </summary>
    public class Book
    {
        /// <summary>
        /// id of Book
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of Book
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        public int? AmountOfPages { get; set; }

        public int AuthorId { get; set; }

        //public Author Author { get; set; }

    }
}

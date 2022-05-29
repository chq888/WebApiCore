using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppApi6.Models
{
    /// <summary>
    /// Author with Id, Name fields
    /// </summary>
    public class Author
    {

        /// <summary>
        /// id of author
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of author
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// FirstName of author
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName of author
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }

        /// <summary>
        /// Books by author
        /// </summary>
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}

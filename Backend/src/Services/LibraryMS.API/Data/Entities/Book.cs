using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMS.API.Data.Entities
{
    public class Book:BaseEntity
    {
        public int BookId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }
        [Required]
        [MaxLength(100)]
        public string ISBN { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        [ForeignKey("Library")]
        public int LibraryId { get; set; }
        public Library Library { get; set; }
        public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
    }
}

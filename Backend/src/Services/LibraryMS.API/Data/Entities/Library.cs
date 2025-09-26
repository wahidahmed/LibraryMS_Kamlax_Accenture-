using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMS.API.Data.Entities
{
    public class Library : BaseEntity
    {
        public int LibraryId { get; set; }
        [Required]
        [MaxLength(500)]
        public string LibraryName { get; set; }
        [Required]
        [MaxLength(1000)]
        public required string LibraryAddress { get; set; }
      
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Member> Members { get; set; } = new List<Member>();
        public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
    }
}

using LibraryMS.API.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMS.API.Data.Entities
{
    public class Member : BaseEntity
    {
       
        public int MemberId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Address { get; set; }
        public DateOnly MembershipDate { get; set; } = DateTime.UtcNow.ToDateOnly();

       [ForeignKey("Library")]
        public int LibraryId { get; set; }
        public Library Library { get; set; }
        public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
    }
}

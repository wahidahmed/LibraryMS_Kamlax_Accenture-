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
        [MaxLength(20)]
        public string Phone { get; set; }
        public DateTime MembershipDate { get; set; } = DateTime.UtcNow;

       [ForeignKey("Library")]
        public int LibraryId { get; set; }
        public Library Library { get; set; }
        public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMS.API.Data.Entities
{
    public class BookLoan : BaseEntity
    {

        public int BookLoanId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        [ForeignKey("Library")]
        public int LibraryId { get; set; }

        public DateOnly LoanDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }

       
        public Book Book { get; set; }
        public Member Member { get; set; }
        public Library Library { get; set; }
    }
}

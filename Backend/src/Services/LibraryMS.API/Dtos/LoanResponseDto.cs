namespace LibraryMS.API.Dtos
{
    public class LoanResponseDto
    {
       public int BookLoanId { get; set; }
        public string BookTitle { get; set; }
        public string MemberName { get; set; }
        public DateOnly? LoanDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public bool IsOverdue { get; set; }
    }
}

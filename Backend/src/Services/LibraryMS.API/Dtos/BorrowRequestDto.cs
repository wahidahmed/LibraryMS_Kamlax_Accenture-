namespace LibraryMS.API.Dtos
{
    public class BorrowRequestDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int LibraryId { get; set; }
    }
}

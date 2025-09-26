namespace LibraryMS.API.Dtos
{
    public class MemberDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateOnly MembershipDate { get; set; }
        public int LibraryId { get; set; }
        public string LibraryName { get; set; }
    }
}

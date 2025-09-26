namespace LibraryMS.API.Dtos
{
    public class LibraryStatsDto
    {
        public int LibraryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalBooks { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalMembers { get; set; }
        public int ActiveLoans { get; set; }
        public int OverdueLoans { get; set; }
    }
}

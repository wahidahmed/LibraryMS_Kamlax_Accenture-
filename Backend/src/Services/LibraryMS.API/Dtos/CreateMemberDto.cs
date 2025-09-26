using System.ComponentModel.DataAnnotations;

namespace LibraryMS.API.Dtos
{
    public class CreateMemberDto
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public int LibraryId { get; set; }
        [Required]
        public string Address { get; set; }
    }

    public class UpdateMemberDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? LibraryId { get; set; }
        public int MemberId { get; set; }
        public string Address { get; set; }
    }
}

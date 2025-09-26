using System.ComponentModel.DataAnnotations;

namespace LibraryMS.API.Dtos
{
    public class CreateLibraryDto
    {
        [Required]
        public string LibraryName { get; set; }
        [Required]
        public string LibraryAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class UpdateLibraryDto
    {
        public string LibraryName { get; set; }
        public string LibraryAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int LibraryId { get; set; }
    }
}

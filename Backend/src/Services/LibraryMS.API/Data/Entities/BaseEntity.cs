namespace LibraryMS.API.Data.Entities
{
    public class BaseEntity
    {
        public int createdBy {  get; set; }
        public DateTime createdOn { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

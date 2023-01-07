namespace TraqCop.auth.Model
{
    public class VisitorModel : BaseEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string State { get; set; }
        public string PurposeOfEntry { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public long ImageFileSize { get; set; }
        public string ImageOriginalFileName { get; set; }
    }
}

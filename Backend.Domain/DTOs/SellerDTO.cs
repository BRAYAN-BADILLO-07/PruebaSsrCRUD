namespace Backend.Domain.DTOs
{
    public class SellerDTO
    {
        public short Code { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public short City_Id { get; set; }
        public string NameCity { get; set; }
    }
}

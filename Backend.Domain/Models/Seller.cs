namespace Backend.Domain.Models
{
    public class Seller
    {
        public short Code { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public short City_Id { get; set; }

        public virtual City City { get; set; }
    }
}

using System.Collections.Generic;

namespace Backend.Domain.Models
{
    public class City
    {
        public short Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Seller> Sellers { get; set; }
    }
}

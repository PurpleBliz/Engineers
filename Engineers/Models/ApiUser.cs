using System.Collections.Generic;

namespace Engineers.Models
{
    public class ApiUser
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Education { get; set; }
        public int Balance { get; set; }
        public string Role { get; set; }
        public string Fulldescription { get; set; }
        public string MinDescription { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Engineers.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Comments { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Qualification { get; set; }
        public string Description { get; set; }
        public int Balance { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Respond> Responds { get; set; }
        public virtual ICollection<OrdersInWork> OrdersInWork { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public User()
        {
            Orders = new List<Order>();
            Reviews = new List<Review>();
            Responds = new List<Respond>();
            Messages = new HashSet<Message>();
            OrdersInWork = new List<OrdersInWork>();
        }
    }
}
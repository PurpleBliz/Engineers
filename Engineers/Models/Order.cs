using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineers.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public int Cost { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int State { get; set; }//0-close, 1-open
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public int InWorkId { get; set; }
        public virtual OrdersInWork InWork { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Respond> Responds { get; set; }
        public Order()
        {
            Reviews = new List<Review>();
            Responds = new List<Respond>();
        }
    }
}

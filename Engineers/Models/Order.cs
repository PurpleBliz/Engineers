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
        public string Address { get; set; }
        public int State { get; set; }//0-close, 1-open, 2-delete
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public User User { get; set; }
    }
}

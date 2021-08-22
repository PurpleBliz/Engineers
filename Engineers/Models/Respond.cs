using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineers.Models
{
    public class Respond
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User{ get; set; }
        public int OrderId { get; set; }
        public Order Order{ get; set; }
        public string Text { get; set; }
        public DateTime Created_at { get; set; }
    }
}
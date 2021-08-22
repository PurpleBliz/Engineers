using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineers.Models
{
    public class OrdersInWork
    {
        [Key]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string ExecutorId { get; set; } 
        public User Executor { get; set; }
        public Order Order { get; set; }
        public DateTime Started_at { get; set; }
        public DateTime Finished_at { get; set; }
    }
}
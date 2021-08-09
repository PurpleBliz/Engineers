using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Engineers.Models
{
    public class OrdersInWork
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Order_Id { get; set; }
    }
}
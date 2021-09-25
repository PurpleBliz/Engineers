using System.Collections.Generic;
using Engineers.Models;

namespace Engineers.Api.Models
{
    public class ApiUser
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Qualification { get; set; }
        public int Balance { get; set; }
        public string Role { get; set; }
        public string Comments { get; set; }
        public string Description { get; set; }
    }
}
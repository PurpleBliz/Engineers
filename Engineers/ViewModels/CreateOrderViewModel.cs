using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Engineers.ViewModels
{
    public class CreateOrderViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
    }
}

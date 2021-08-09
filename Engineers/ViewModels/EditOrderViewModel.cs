using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Engineers.ViewModels
{
    public class EditOrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public int Cost { get; set; }
        public string Address { get; set; }
        public int State { get; set; }//0-close, 1-open, 2-delete
        public DateTime Updated_at { get; set; }
    }
}

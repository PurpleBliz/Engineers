using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engineers.Models
{
    public class Response
    {
        public int Code { get; set; }
        public string Text { get; set; }
        public object Data { get; set; }
        public bool Success { get; set; }
    }
}

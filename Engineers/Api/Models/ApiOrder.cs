using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineers.Api.Models
{
    public class ApiOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public int Cost { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [NotMapped]
        public int State { get; set; }//0-close, 1-open
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public string OwnerId { get; set; }
        public string OwnerName { get; set; }

        public int InWorkId { get; set; }
    }
}
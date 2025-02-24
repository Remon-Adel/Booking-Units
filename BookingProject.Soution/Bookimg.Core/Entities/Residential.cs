using Booking.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Entities
{
    public class Residential:BaseEntity
    {
        public string ResidentialName { get; set; }
        public string Rooms { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string Beds { get; set; }
        public string Baths { get; set; }
        public decimal Price { get; set; }

    }
}

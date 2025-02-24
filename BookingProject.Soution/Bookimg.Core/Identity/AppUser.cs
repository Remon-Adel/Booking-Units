using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Identity
{
    public class AppUser:IdentityUser
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Age { get; set; }
    }
}

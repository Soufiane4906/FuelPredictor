using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using FuelPredictor.Models.V2;

namespace FuelPredictor.Models.Users
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 2;
        public byte[] ProfilePicture { get; set; }
        public virtual List<Station>? Stations { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    public class AppUser : IdentityUser
    {
        // Additional properties can be added here
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public Wishlist wishlist { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
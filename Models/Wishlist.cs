using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Models
{ 
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = "Default Wishlist";

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        [IgnoreDataMember]
        public AppUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ProductWishlist> Products { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; } = 0;
        [Precision(18, 2)]
        public decimal TotalPrice
        {
            get
            {
                return Price - (Price * Discount);
            }
        }
        [Required]
        public int StockAmount { get; set; }

        public int Discount { get; set; } = 0;

        [MaxLength(100)]
        [Required]
        public string Brand { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string ImageUrl { get; set; }

        public string ImageLocalPath { get; set; }

        [MaxLength(50)]
        [Required]
        public string ProductCode { get; set; }

        [Precision(3, 2)]
        public decimal AverageRating { get; set; } = 0;
        
        public int ReviewCount { get; set; } = 0;
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ProductWishlist> Wishlists { get; set; }
    }
}
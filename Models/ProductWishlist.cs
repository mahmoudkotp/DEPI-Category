using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ProductWishlist
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int WishlistId { get; set; }
        [ForeignKey("WishlistId")]
        public Wishlist Wishlist { get; set; }
    }
}
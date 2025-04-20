using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProductImage
    {
		[Key]
		public int ImageId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string ImageLocalPath { get; set; }
    }
}
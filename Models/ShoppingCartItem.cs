using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public int ShoppingCartId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Precision(18, 2)]
        public decimal TotalPrice => Quantity * Price;
    }
}
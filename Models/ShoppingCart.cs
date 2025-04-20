using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        [Precision(18, 2)]
        public decimal Price
        {
            get
            {
                decimal totalPrice = 0;
                if (Items is not null)
                    foreach (var item in Items)
                        totalPrice += item.TotalPrice;

                return totalPrice;
            }
        }

        public List<ShoppingCartItem> Items { get; set; }

        public string UserId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
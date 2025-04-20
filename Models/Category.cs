using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<Product> Products { get; set; } // Non-virtual for eager loading
    }
}
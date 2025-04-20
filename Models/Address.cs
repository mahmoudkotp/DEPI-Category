using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        [IgnoreDataMember]
        public AppUser User { get; set; }
        [Required,MaxLength(100)]
        public string Street { get; set; }
        [Required,MaxLength(30)]
        public string City { get; set; }
        [Required,MaxLength(30)]
        public string Governorate { get; set; }
        [Required,MaxLength(10)]
        public string PostalCode { get; set; }
    }
}
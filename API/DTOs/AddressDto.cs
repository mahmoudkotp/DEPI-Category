using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AddressDto
    {
        [Required, MaxLength(100)]
        public string Street { get; set; }
        [Required, MaxLength(30)]
        public string City { get; set; }
        [Required, MaxLength(30)]
        public string Governorate { get; set; }
        [Required, MaxLength(10)]
        public string PostalCode { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AddressDto
    {
        
		[Required(ErrorMessage = "Street is required")]
		[MaxLength(100, ErrorMessage = "Street can't exceed 100 characters")]
		public string Street { get; set; }

		[Required(ErrorMessage = "City is required")]
		[MaxLength(30, ErrorMessage = "City can't exceed 30 characters")]
		public string City { get; set; }

		[Required(ErrorMessage = "Governorate is required")]
		[MaxLength(30, ErrorMessage = "Governorate can't exceed 30 characters")]
		public string Governorate { get; set; }

		[Required(ErrorMessage = "Postal code is required")]
		[MaxLength(10, ErrorMessage = "Postal code can't exceed 10 characters")]
		public string PostalCode { get; set; }
	}
}

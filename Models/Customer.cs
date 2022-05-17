using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
	[DataContract]
	public class Customer
	{
		
		[DataMember(Name = "firstname")]
		public string FirstName { get; set; }
		[DataMember(Name = "lastname")]
		public string LastName { get; set; }
		[DataMember(Name = "id")]
		public string CustomerId { get; set; }
		[Required(ErrorMessage = "Email Required")]
		[DataMember(Name = "email")]
		public string Email { get; set; }
		[DataMember(Name = "phone_Number")]
		public string PhoneNumber { get; set; }
		[DataMember(Name = "country_code")]
		public string CountryCode { get; set; }
		[DataMember(Name = "gender")]
		public string Gender { get; set; }
		[DataMember(Name = "balance")]
		public decimal Balance { get; set; }
	}
}

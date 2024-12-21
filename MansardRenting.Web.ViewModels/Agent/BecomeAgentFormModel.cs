using System.ComponentModel.DataAnnotations;
using static MansardRenting.Common.EntityValidationConstants.Agent;

namespace MansardRenting.Web.ViewModels.Agent
{
	public class BecomeAgentFormModel
	{
		[Required]
		[StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
		[Phone]
		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; } = null!;
	}
}

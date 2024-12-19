using System.ComponentModel.DataAnnotations;
using static MansardRenting.Common.EntityValidationConstants.Agent;

namespace MansardRenting.Data.Models
{
	public class Agent
	{
        public Agent()
        {
            //Id = Guid.NewGuid(); // Autogenerate Guid Id in the DB
            ManagedHouses = new HashSet<House>();
        }
        [Key]
		public Guid Id { get; set; }

		[MaxLength(PhoneNumberMaxLength)]
		public required string PhoneNumber { get; set; }

		public required Guid UserId { get; set; }

		public ApplicationUser User { get; set; } = null!;

		public ICollection<House> ManagedHouses { get; set; }
	}
}

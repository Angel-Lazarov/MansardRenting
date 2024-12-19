using Microsoft.AspNetCore.Identity;

namespace MansardRenting.Data.Models
{

	/// <summary>
	/// This is custom user class that works with the default ASP.NET Core Identity.
	/// You can add additional info to the build-in users.
	/// </summary>
	public class ApplicationUser : IdentityUser<Guid>
	{
		public ApplicationUser()
		{
			//Id = Guid.NewGuid(); // Autogenerate Guid Id in the DB
            RentedHouses = new HashSet<House>();
		}

		public ICollection<House> RentedHouses { get; set; }
	}
}

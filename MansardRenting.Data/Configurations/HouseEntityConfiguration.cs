using MansardRenting.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MansardRenting.Data.Configurations
{
	public class HouseEntityConfiguration : IEntityTypeConfiguration<House>
	{
		public void Configure(EntityTypeBuilder<House> builder)
		{

			builder
				.Property(h => h.CreatedOn)
				.HasDefaultValueSql("GETDATE()");

			builder 
				.Property(h => h.IsActive)
				.HasDefaultValue(true);

			builder
				.HasOne(h => h.Category)
				.WithMany(c => c.Houses)
				.HasForeignKey(h => h.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(h => h.Agent)
				.WithMany(a => a.ManagedHouses)
				.HasForeignKey(h => h.AgentId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasData(GenerateHouses());
		}

		private House[] GenerateHouses()
		{
			ICollection<House> houses = new HashSet<House>();

			House house;

			house = new House()
			{
				Title = "Big House Marina",
				Address = "North London, UK (near the border)",
				Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
				ImageUrl = "https://www.pleionrealestate.com/wp-content/uploads/2023/09/1695372772095-1048cf28-f176-43c8-932e-a75ab6f6ee02_12.jpg",
				PricePerMonth = 2100.00M,
				CategoryId = 3,
				AgentId = Guid.Parse("FE5AFF5F-9DC7-4A62-8384-199CDC007836"),
				RenterId = Guid.Parse("863C6200-DA00-4CC4-20F1-08DD20683655")
			};
			houses.Add(house);

			house = new House()
			{
				Title = "Family House Comfort",
				Address = "Near the Sea Garden in Burgas, Bulgaria",
				Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
				ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
				PricePerMonth = 1200.00M,
				CategoryId = 2,
				AgentId = Guid.Parse("FE5AFF5F-9DC7-4A62-8384-199CDC007836"),
			};
			houses.Add(house);

			house = new House()
			{
				Title = "Grand House",
				Address = "Boyana Neighbourhood, Sofia, Bulgaria",
				Description = "This luxurious house is everything you will need. It is just excellent.",
				ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
				PricePerMonth = 2000.00M,
				CategoryId = 2,
				AgentId = Guid.Parse("FE5AFF5F-9DC7-4A62-8384-199CDC007836"),
			};
			houses.Add(house);

			return houses.ToArray();
		}
	}
}
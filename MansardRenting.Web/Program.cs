using MansardRenting.Data;
using MansardRenting.Data.Models;
using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Web.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MansardRenting.Web;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.AddServiceDefaults();

		//var connectionString = builder.Configuration.GetConnectionString("WorkConnection");
		var connectionString = builder.Configuration.GetConnectionString("HomeConnection");

		builder.Services.AddDbContext<MansardRentingDbContext>(options =>
			options.UseSqlServer(connectionString));

		builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
		{
			options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
			options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");
			options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
			options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
			options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
			options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
		})
			 .AddEntityFrameworkStores<MansardRentingDbContext>();
		builder.Services.AddApplicationServices(typeof(IHouseService));

		builder.Services.AddControllersWithViews();  // Register the controllers

		WebApplication app = builder.Build();

		app.MapDefaultEndpoints();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
			app.UseDeveloperExceptionPage(); // In error exception page will be displayed
		}
		else
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapDefaultControllerRoute();

		app.MapRazorPages();

		app.Run();
	}
}
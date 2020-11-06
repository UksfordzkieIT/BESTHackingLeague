using EkoApp.Entities;
using EkoApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.Data
{
	public class AppDbContext : IdentityDbContext<UserDb, IdentityRole<Guid>, Guid>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<UserDb> UserDbs { get; set; }
		public DbSet<FuelBill> FuelBills { get; set; }
		public DbSet<TicketBill> TicketBills { get; set; }
		public DbSet<WaterBill> WaterBills { get; set; }
	}
}

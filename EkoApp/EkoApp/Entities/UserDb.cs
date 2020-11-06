using EkoApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EkoApp.Entities
{
	public class UserDb : IdentityUser<Guid>
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual ICollection<FuelBill> FuelBills { get; set; }
		public virtual ICollection<WaterBill> WaterBills { get; set; }
		public virtual ICollection<TicketBill>TicketBills { get; set; }
	}
}

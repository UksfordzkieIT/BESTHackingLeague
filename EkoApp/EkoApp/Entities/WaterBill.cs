using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.Models
{
	public class WaterBill
	{
		[Key]
		public Guid Id { get; set; }
		public decimal Price { get; set; }
		public DateTime DateTime { get; set; }
		public decimal Volume { get; set; }
		public Guid UserId { get; set; }
	}
}

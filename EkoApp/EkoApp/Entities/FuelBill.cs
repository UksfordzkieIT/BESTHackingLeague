using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.Models
{
	public class FuelBill
	{
		[Key]
		public Guid Id { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime DateTime { get; set; }
		public decimal Volume { get; set; }
		public decimal PricePerLitr { get; set; }
		public Guid UserDbId { get; set; }

	}
}

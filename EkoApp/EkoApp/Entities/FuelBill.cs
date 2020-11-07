using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.Models
{
	public class FuelBill
	{
		[Key]
		public Guid Id { get; set; }
		[DisplayName("Całkowity koszt")]
		public float TotalPrice { get; set; }
		public DateTime DateTime { get; set; }
		[DisplayName("Objętość")]
		public float Volume { get; set; }
		[DisplayName("Cena za litr")]
		public float PricePerLitr { get; set; }
		public Guid UserDbId { get; set; }

	}
}

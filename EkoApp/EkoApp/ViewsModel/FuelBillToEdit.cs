using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.ViewsModel
{
	public class FuelBillToEdit
	{
		public Guid Id { get; set; }
		[DisplayName("Całkowity koszt")]
		public string TotalPrice { get; set; }
		[DisplayName("Objętość")]
		public string Volume { get; set; }
		[DisplayName("Cena za litr")]
		public float PricePerLitr { get; set; }
	}
}

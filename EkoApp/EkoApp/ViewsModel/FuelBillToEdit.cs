using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.ViewsModel
{
	public class FuelBillToEdit
	{
		public Guid Id { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal Volume { get; set; }
		public decimal PricePerLitr { get; set; }
	}
}

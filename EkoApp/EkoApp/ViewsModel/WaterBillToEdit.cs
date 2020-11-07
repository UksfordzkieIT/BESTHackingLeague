using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.ViewsModel
{
	public class WaterBillToEdit
	{
		public Guid Id { get; set; }
		[DisplayName("Całkowity koszt")]
		public decimal Price { get; set; }
		[DisplayName("Objętość")]
		public decimal Volume { get; set; }
	}
}

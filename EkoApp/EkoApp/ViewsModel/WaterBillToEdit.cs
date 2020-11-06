using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.ViewsModel
{
	public class WaterBillToEdit
	{
		public Guid Id { get; set; }
		public decimal Price { get; set; }
		public decimal Volume { get; set; }
	}
}

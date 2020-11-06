using EkoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.ViewsModel
{
	public class AllBillsModel
	{
		public List<WaterBill> WaterBills { get; set; }
		public List<TicketBill> TicketBills { get; set; }
		public List<FuelBill> FuelBills { get; set; }
	}
}

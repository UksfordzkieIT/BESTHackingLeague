using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.ViewsModel
{
	public class TicketBillToEdit
	{
		public Guid Id { get; set; }
		[DisplayName("Całkowity koszt")]
		public float Price { get; set; }
	}
}

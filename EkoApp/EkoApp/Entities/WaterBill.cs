using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EkoApp.Models
{
	public class WaterBill
	{
		[Key]
		public Guid Id { get; set; }
		[DisplayName("Całkowity koszt")]
		public float Price { get; set; }
		public DateTime DateTime { get; set; }
		[DisplayName("Objętość")]
		public float Volume { get; set; }
		public Guid UserDbId { get; set; }
	}
}

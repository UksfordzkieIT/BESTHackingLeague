using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EkoApp.Services.GeolocService;

namespace EkoApp.ViewsModel
{
	public class PetrolWithData
	{
			public Petrol Petrol { get; set; }
			public float Distance { get; set; }
			public double TravelCost { get; set; }
			public double RealPricePerLiter { get; set; }
		};
	
}

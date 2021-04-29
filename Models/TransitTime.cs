using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class TransitTime
	{
		[ForeignKey("Modul")]
		public int Id { get; set; }
		public string From{ get; set; }

		public string To{ get; set; }

		public TransitTimeAvailability Dispatch { get; set; }

		public TransitTimeAvailability Transit { get; set; }

		public TransitTimeAvailability Delivery { get; set; }
	}
}

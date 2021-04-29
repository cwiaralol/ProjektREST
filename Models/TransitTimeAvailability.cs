using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class TransitTimeAvailability
	{
		[ForeignKey("TransitTime")]
		public int Id { get; set; }
		public int Duration { get; set; }

		public bool Monday { get; set; }

		public bool Tuesday { get; set; }

		public bool Wednesday { get; set; }

		public bool Thursday { get; set; }

		public bool Friday { get; set; }

		public bool Saturday { get; set; }

		public bool Sunday { get; set; }
	}
}

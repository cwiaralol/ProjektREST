using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class TransitTimeAvailability
	{
		[ForeignKey("TransitTime")]
		public int Id { get; set; }
		[Required]

		public int Duration { get; set; }

		[Required]
		public bool Monday { get; set; }
		[Required]

		public bool Tuesday { get; set; }
		[Required]

		public bool Wednesday { get; set; }
		[Required]

		public bool Thursday { get; set; }
		[Required]

		public bool Friday { get; set; }
		[Required]

		public bool Saturday { get; set; }
		[Required]

		public bool Sunday { get; set; }
	}
}

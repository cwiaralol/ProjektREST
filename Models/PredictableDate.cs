using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class PredictableDate
	{
		[Required]
		public string From { get; set; }
		[Required]
		public string To { get; set; }
	}
}

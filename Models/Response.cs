using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class Response
	{
		[Required]
		public string PurchaseDate { get; set; }
		[Required]
		public PredictableDate[] PredictableDates { get; set; }
	}
}

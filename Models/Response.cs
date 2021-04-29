using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class Response
	{
		public string PurchaseDate { get; set; }

		public PredictableDate[] PredictableDates { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class Response
	{
		[Required]
		[Key]
		public int Id { get; set; }

		public string PurchaseDate { get; set; }

		[ForeignKey("Modul")]
		public int ModulID { get; set; }

		public virtual ICollection<PredictableDate> PredictableDates { get; set; }

	
	}
}

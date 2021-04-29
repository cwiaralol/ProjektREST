using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{

	public class DeliveryWindow
	{
		[ForeignKey("Modul")]
		public int Id { get; set; }
		[Required]
		public string From { get; set; }
		[Required]
		public string To { get; set; }
	}



}

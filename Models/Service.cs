using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class Service
	{
		[ForeignKey("Modul")]
		public int Id { get; set; }
		public string Name { get; set; }
	
		public virtual ICollection<TransitTime> TransitTimes { get; set; }
	}
}

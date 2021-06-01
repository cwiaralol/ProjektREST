using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class Modul
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(3)]
		public string Code { get; set; }
		[Required]
		public virtual ICollection<Service> Services { get; set; }
		[Required]
		public DeliveryWindow DeliveryWindow { get; set; }

		
		




	}
}

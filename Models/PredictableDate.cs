using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
	public class PredictableDate
	{
		[Key]
		[IgnoreDataMember]
		public int Id { get; set; }
		[Required]
		public string From { get; set; }
		[Required]
		public string To { get; set; }
		[IgnoreDataMember]
		[ForeignKey("Response")]
		public int ResponseId { get; set; }
	}
}

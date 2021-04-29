using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using AplikacjaKurierska.API.Data;

namespace AplikacjaKurierska.API.Controllers
{

	public class ModulController : Controller
	{
		private readonly DataContext _context;
		public ModulController(DataContext context)
		{
			_context = context;

		}


		// autoryzacja / autentykacja JWT ZROBIONE 
		[Authorize(Roles = "admin")]
		[HttpPost("createModul")]
		public async Task<IActionResult> CreateModul([FromBody] AplikacjaKurierska.API.Models.Modul newModule)
		{
			
			// validacja //DataAnnotation

			if (newModule.Code == null)
			{
				//throw new ArgumentNullException("You need to write code ");
				return StatusCode((int)HttpStatusCode.BadRequest);
			}


			if (newModule.DeliveryWindow == null)
				{
				//throw new ArgumentNullException("You need to write DeliveryWindow");
				return StatusCode((int)HttpStatusCode.BadRequest);
			}

			if (newModule.DeliveryWindow.From == null)
			{
				//throw new ArgumentNullException("You need to write DeliveryWindow.from");
				return StatusCode((int)HttpStatusCode.BadRequest);
			}

			if (newModule.DeliveryWindow.To == null)
			{
				//throw new ArgumentNullException("You need to write DeliveryWindow.To");
				return StatusCode((int)HttpStatusCode.BadRequest);
			}

			if (newModule.Services == null)
			{
				//throw new ArgumentNullException("You need to write Services");
				return StatusCode((int)HttpStatusCode.BadRequest);
			}







			// zapis na bazie danych //Entity Framework
			_context.Moduls.Add(newModule);
			await _context.SaveChangesAsync();


			//throw new ArgumentException(newModule.Code); //sprawdzamy czy wczytuje
			return StatusCode((int)HttpStatusCode.Created);

		}

	}
}

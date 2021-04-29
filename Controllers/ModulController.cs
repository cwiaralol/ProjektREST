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



		[Authorize(Roles = "admin")]
		[HttpPost("createModul")]
		public async Task<IActionResult> CreateModul([FromBody] AplikacjaKurierska.API.Models.Modul newModule)
		{
			// autoryzacja / autentykacja JWT ZROBIONE 

			// validacja //DataAnnotation

			_context.Moduls.Add(newModule);
			await _context.SaveChangesAsync();
			// zapis na bazie danych //Entity Framework

			// odpwowiedź 201 / 400 / 403
			//throw new ArgumentException(newModule.Code); //sprawdzamy czy wczytuje
			return StatusCode((int)HttpStatusCode.Created);

		}

	}
}

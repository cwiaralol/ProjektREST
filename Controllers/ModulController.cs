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

			foreach (var item in newModule.Services)
			{

				if (item.Name == null) return StatusCode((int)HttpStatusCode.BadRequest);


				if (item.TransitTimes == null) return StatusCode((int)HttpStatusCode.BadRequest);

				foreach (var item2 in item.TransitTimes)
				{
					if (item2.From == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.To == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Delivery == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Dispatch == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit == null) return StatusCode((int)HttpStatusCode.BadRequest);
					/*
					if((item2.Transit.Duration==null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Monday== null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Tuesday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Wednesday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Thursday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Friday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Saturday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Sunday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					*/
					//if you don't add day of the week you will get always false, if you add transit it can be empty
				}

			}





			// zapis na bazie danych //Entity Framework
			_context.Moduls.Add(newModule);
			await _context.SaveChangesAsync();


			//throw new ArgumentException(newModule.Code); //sprawdzamy czy wczytuje
			return StatusCode((int)HttpStatusCode.Created);

		}


		//PUT editModul/5
		[Authorize(Roles = "admin")]
		[HttpPut("editModul/{id}")]
		public async Task<IActionResult> EditValue(int id, [FromBody] AplikacjaKurierska.API.Models.Modul newModule)
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

			foreach (var item in newModule.Services)
			{

				if (item.Name == null) return StatusCode((int)HttpStatusCode.BadRequest);


				if (item.TransitTimes == null) return StatusCode((int)HttpStatusCode.BadRequest);

				foreach (var item2 in item.TransitTimes)
				{
					if (item2.From == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.To == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Delivery == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Dispatch == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit == null) return StatusCode((int)HttpStatusCode.BadRequest);
					/*
					if((item2.Transit.Duration==null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Monday== null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Tuesday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Wednesday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Thursday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Friday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Saturday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					if (item2.Transit.Sunday == null) return StatusCode((int)HttpStatusCode.BadRequest);
					*/
					//if you don't add day of the week you will get always false, if you add transit it can be empty
				}
			}





				var data = await _context.Moduls.FindAsync(id);
				data.Code = newModule.Code;
				data.DeliveryWindow = newModule.DeliveryWindow;
				data.Services = newModule.Services;
				_context.Moduls.Update(data);
				await _context.SaveChangesAsync();
				return Ok(data);

			}




		//DELETE api/values/5
		[Authorize(Roles = "admin")]
		[HttpDelete("deleteModul/{id}")]
		public async Task<IActionResult> DeleteValue(int id)
		{
			var data = await _context.Moduls.FindAsync(id);
			_context.Moduls.Remove(data);
			await _context.SaveChangesAsync();
			return Ok(data);
		}




	}




	}


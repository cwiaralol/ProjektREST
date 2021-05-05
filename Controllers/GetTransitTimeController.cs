using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AplikacjaKurierska.API.Data;
using AplikacjaKurierska.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AplikacjaKurierska.API.Controllers
{
    //http:localhost:5000/api/Values
    [Authorize]
    [ApiController]
    public class GetTransitTimeController : ControllerBase
    {
        private readonly DataContext _context;
        public GetTransitTimeController(DataContext context)
        {
            _context = context;

        }



        //GET http://localhost:5000/DHL/transitTimes?serviceName=OneDay&purchaseDate=14042021T1046&fromCountry=PL&toCountry=DE
        [Authorize(Roles = "admin,user")] // more roles = [Authorize(Roles="admin,user")] etc.
        [HttpGet("{moduleCode}")]
        public async Task<IActionResult> GetValue(string moduleCode)
        {
            var value = await _context.Moduls.FirstOrDefaultAsync(x => x.Code == moduleCode);
            return Ok(value);
        }
        /*
        [AllowAnonymous]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetValue(int id)
        {

            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);

        }
        */

      


    }

}


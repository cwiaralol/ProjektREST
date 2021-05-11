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
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }



        //GET api/values http:localhost:5000/api/values
        [Authorize(Roles ="admin,user")] // more roles = [Authorize(Roles="admin,user")] etc.
        [HttpGet]
        public async Task <IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetValue(int id)
        {

            var value = await _context.Values.FirstOrDefaultAsync(x =>x.Id==id);
            return Ok(value);

        }

        //POST http:localhost:5000/api/values
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddValue([FromBody] Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value);

        }


        //PUT api/values/5
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditValue(int id,[FromBody] Value value)
        {
            var data =await _context.Values.FindAsync(id);
            data.Name = value.Name;
            _context.Values.Update(data);
            await _context.SaveChangesAsync();
            return Ok(data);

        }

        //DELETE api/values/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValue(int id)
        {
            var data =await _context.Values.FindAsync(id);
            _context.Values.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }


    }

}


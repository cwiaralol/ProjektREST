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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        [HttpGet("{moduleCode}/")]
        public async Task<IActionResult> GetValue(string moduleCode)
        {
            String datax="";

            var value2 = await _context.Moduls.FirstOrDefaultAsync(x => x.Code == moduleCode);

            var value3 = await _context.Responses.FirstOrDefaultAsync(y => y.Id == value2.Id);

            var value4 = await _context.PredictableDates.FirstOrDefaultAsync(yz => yz.ResponseId == value3.Id);

            var values = await _context.PredictableDates
                .Where(li => li.ResponseId == value3.Id)
                .ToListAsync();



            var xPurchaseDate = value3.PurchaseDate;
            // var xPredictableDateFrom = value4.From;
            // var xPredictableDateTo = value4.To;


            //var Roles = value3.PredictableDates.Select(r => new { from = r.From, to = r.To }).ToArray();

            
            
            Product product = new Product();
            product.purchaseDate = value3.PurchaseDate;
            product.PredictableDates = values;
            


            string json = JsonConvert.SerializeObject(product);
            return Ok(json);
        
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


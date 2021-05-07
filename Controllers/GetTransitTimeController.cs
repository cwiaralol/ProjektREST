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
using System.Web;
using System.Net;

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

            var value2 = await _context.Moduls.FirstOrDefaultAsync(x => x.Code == moduleCode);

            var value3 = await _context.Responses.FirstOrDefaultAsync(y => y.Id == value2.Id);

            var value4 = await _context.PredictableDates.FirstOrDefaultAsync(yz => yz.ResponseId == value3.Id);

            var values = await _context.PredictableDates
                .Where(li => li.ResponseId == value3.Id)
                .ToListAsync();


            // var xPredictableDateFrom = value4.From;
            // var xPredictableDateTo = value4.To;


            //var Roles = value3.PredictableDates.Select(r => new { from = r.From, to = r.To }).ToArray();



            Product product = new()
            {
                purchaseDate = value3.PurchaseDate,
                PredictableDates = values
            };



            string json = JsonConvert.SerializeObject(product);
            return Ok(json);
        
            }

        //OK DLA JEDNEGO PARAMETRU
        /*
        //GET http://localhost:5000/DHL/transitTimes?serviceName=OneDay&purchaseDate=14042021T1046&fromCountry=PL&toCountry=DE
        [Authorize(Roles = "admin,user")] // more roles = [Authorize(Roles="admin,user")] etc.
        [HttpGet("{moduleCode}/transitTimes")]
        public async Task<IActionResult> GetValue2(string moduleCode)
        {

            var urlparameters = Request.Query;
            string[] parameters = new string[4];
            int iterator = 0;
            foreach (var item2 in urlparameters)
            {
                parameters[iterator] = item2.Value;
                iterator +=1;
            }

            string serviceName = parameters[0];
            string purchaseDate = parameters[1];
            string fromCountry =parameters[2];
            string toCountry = parameters[3];

            //throw new Exception(serviceName+" "+purchaseDate+" "+fromCountry+" "+toCountry);


           var value2 = await _context.Moduls.FirstOrDefaultAsync(x => x.Code == moduleCode);
          
           var value2x = await _context.Responses.FirstOrDefaultAsync(x => x.PurchaseDate == purchaseDate);
           
           var value3 = await _context.Responses.FirstOrDefaultAsync(y => y.Id == value2.Id);

           var value4 = await _context.PredictableDates.FirstOrDefaultAsync(yz => yz.ResponseId == value3.Id);

            var values = await _context.PredictableDates
                .Where(li => li.ResponseId == value3.Id)
                .ToListAsync();


            // var xPredictableDateFrom = value4.From;
            // var xPredictableDateTo = value4.To;


            //var Roles = value3.PredictableDates.Select(r => new { from = r.From, to = r.To }).ToArray();



            Product product = new()
            {
                purchaseDate = value3.PurchaseDate,
                PredictableDates = values
            };



            string json = JsonConvert.SerializeObject(product);
            return Ok(json);

        }
        */

        //GET http://localhost:5000/DHL/transitTimes?serviceName=OneDay&purchaseDate=14042021T1046&fromCountry=PL&toCountry=DE
        [Authorize(Roles = "admin,user")] // more roles = [Authorize(Roles="admin,user")] etc.
        [HttpGet("{moduleCode}/transitTimes")]
        public async Task<IActionResult> GetValue2(string moduleCode)
        {

            var urlparameters = Request.Query;
            string[] parameters = new string[4];
            int iterator = 0;
            foreach (var item2 in urlparameters)
            {
                parameters[iterator] = item2.Value;
                iterator += 1;
            }

            string serviceName = parameters[0];
            string purchaseDate = parameters[1];
            string fromCountry = parameters[2];
            string toCountry = parameters[3];

            //throw new Exception(serviceName+" "+purchaseDate+" "+fromCountry+" "+toCountry);

            Modul value2=null;
            try
            {
                value2 = await _context.Moduls.FirstOrDefaultAsync(x => x.Code == moduleCode);
            }
            catch (InvalidOperationException e)
            {
                throw new Exception("Bad bad bad",e);
            }
            var value2x = await _context.Responses.FirstOrDefaultAsync(x => x.PurchaseDate == purchaseDate);

            var value3 = await _context.Responses.FirstOrDefaultAsync(y => y.Id == value2.Id);

            var value4 = await _context.PredictableDates.FirstOrDefaultAsync(yz => yz.ResponseId == value3.Id);

            var values = await _context.PredictableDates
                .Where(li => li.ResponseId == value3.Id)
                .ToListAsync();


            // var xPredictableDateFrom = value4.From;
            // var xPredictableDateTo = value4.To;


            //var Roles = value3.PredictableDates.Select(r => new { from = r.From, to = r.To }).ToArray();



            Product product = new()
            {
                purchaseDate = value3.PurchaseDate,
                PredictableDates = values
            };



            string json = JsonConvert.SerializeObject(product);
            return Ok(json);

        }











    }

            }


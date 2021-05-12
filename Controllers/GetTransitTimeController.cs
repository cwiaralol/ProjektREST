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
using System.Globalization;

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
        //[Authorize(Roles = "admin,user")] // more roles = [Authorize(Roles="admin,user")] etc.
        [AllowAnonymous]
        [HttpGet("{moduleCode}/transitTimes")]
        public async Task<IActionResult> GetValue2(string moduleCode, [FromQuery] string PurchaseDate, [FromQuery] string ServiceName, [FromQuery] string fromCountry, [FromQuery] string toCountry)
        {

            DateTime fromquery = DateTime.Today;
            DateTime Toquery = DateTime.Today;

            String[] EuropeanUnion = new string[27] { 
                "BE", 
                "BG",
                "CZ",
                "DK",
                "DE",
                "EE",
                "IE",
                "EL",
                "ES",
                "FR",
                "HR",
                "IT",
                "CY",
                "LV",
                "LT",
                "LU",
                "HU",
                "MT",
                "NL",
                "AT",
                "PL",
                "PT",
                "RO",
                "SI",
                "SK",
                "FI",
                "SE",
            };

            int idmodul = 0;
            var values = await _context.Moduls.Where(li => li.Code == moduleCode).ToListAsync();

            foreach (var dane_module in values)
            {
                if (dane_module.Code == moduleCode)
                {
                    idmodul = dane_module.Id;
                }


            }

            if (idmodul == 0)
            {
                return NotFound("Not found moduleCode");
            }

            // ADD RESPONSE // 

            DateTime today = DateTime.Today;
            String data = today.ToString("ddMMyyyyThhmm");

            Response responseAdd = new();

            responseAdd.PurchaseDate = PurchaseDate;
            responseAdd.ModulID = idmodul;


            _context.Responses.Add(responseAdd);
            await _context.SaveChangesAsync();

            //ADD PREDICTABLEDATES
            PredictableDate predictableDateAdd = new();
            predictableDateAdd.ResponseId = responseAdd.Id;

            //int dispatchid = 0;
            // int transitid = 0;
            //int deliveryid = 0;

            if (moduleCode == "GLS")
            {
                for (int i = 0; i < 27; i++)
                {
                    if (toCountry == EuropeanUnion[i]) toCountry = "UE";
                }
            }

            var uriwithallparameters = await _context.Moduls
                .Where(li => li.Code == moduleCode)
                .Include(x => x.Services)
                .ThenInclude(x => x.TransitTimes)
                .ThenInclude(x => x.Dispatch)
                .Include(x => x.Services)
                .ThenInclude(x => x.TransitTimes)
                .ThenInclude(x => x.Transit)
                .Include(x => x.Services)
                .ThenInclude(x => x.TransitTimes)
                .ThenInclude(x => x.Delivery)
                .Include(x => x.DeliveryWindow)
                .ToListAsync();
            DateTime converteddata = DateTime.Today;
            foreach (var datax in uriwithallparameters)
            {
                var modulid = datax.Id;

                int pom = 0;
                int pom2 = 0;
                foreach (var servicename in datax.Services)
                {

                    if (servicename.Name == ServiceName)
                    {
                        pom += 1;
                        foreach (var transittime in servicename.TransitTimes)
                        {

                     

                          
                            if (transittime.From == fromCountry && transittime.To == toCountry)
                            {
                                pom2 += 1;
                                string Ddatax = PurchaseDate;
                                converteddata = DateTime.ParseExact(Ddatax, "ddMMyyyyThhmm", System.Globalization.CultureInfo.InvariantCulture); //converted date from url

                                var dzien1 = transittime.Dispatch.Monday;
                                var dzien2 = transittime.Dispatch.Tuesday;
                                var dzien3 = transittime.Dispatch.Wednesday;
                                var dzien4 = transittime.Dispatch.Thursday;
                                var dzien5 = transittime.Dispatch.Friday;
                                var dzien6 = transittime.Dispatch.Saturday;
                                var dzien7 = transittime.Dispatch.Sunday;

                                var dzienobecny = converteddata.DayOfWeek;
                                string dzienobecnystring = dzienobecny.ToString();
                                int pula_dni = 0;
                                int durationtime = transittime.Dispatch.Duration;
                                int ostatecznyczas = 0;


                                var godzinaobecny = converteddata.TimeOfDay;
                                var ci = new CultureInfo("pl-EN");
                                var stacktimefrom = DateTime.ParseExact(datax.DeliveryWindow.From, "HHmm", null);
                                var stacktimeto = DateTime.ParseExact(datax.DeliveryWindow.To, "HHmm", null);
                                fromquery = stacktimefrom;
                                Toquery = stacktimeto;
                                if (!(stacktimefrom.Hour < converteddata.Hour && converteddata.Hour < stacktimeto.Hour)) converteddata = converteddata.AddDays(1);

                                dzienobecny = converteddata.DayOfWeek;
                                dzienobecnystring = dzienobecny.ToString();

                                for (int i = 0; i <= durationtime; i++)
                                    if (i != 0)
                                    {
                                        {
                                            if (dzienobecnystring == "Monday")
                                            {
                                                if (dzien1 == false) pula_dni += 1;

                                            }

                                            if (dzienobecnystring == "Tuesday")
                                            {
                                                if (dzien2 == false) pula_dni += 1;

                                            }

                                            if (dzienobecnystring == "Wednesday")
                                            {
                                                if (dzien3 == false) pula_dni += 1;

                                            }

                                            if (dzienobecnystring == "Thursday")
                                            {
                                                if (dzien4 == false) pula_dni += 1;

                                            }

                                            if (dzienobecnystring == "Friday")
                                            {
                                                if (dzien5 == false) pula_dni += 1;

                                            }

                                            if (dzienobecnystring == "Saturday")
                                            {
                                                if (dzien6 == false) pula_dni += 1;

                                            }

                                            if (dzienobecnystring == "Sunday")
                                            {
                                                if (dzien7 == false) pula_dni += 1;

                                            }
                                        }

                                    }
                                ostatecznyczas = durationtime + pula_dni;
                                converteddata = converteddata.AddDays(ostatecznyczas);

                                dzienobecny = converteddata.DayOfWeek;
                                dzienobecnystring = dzienobecny.ToString();

                                var dzien1T = transittime.Transit.Monday;
                                var dzien2T = transittime.Transit.Tuesday;
                                var dzien3T = transittime.Transit.Wednesday;
                                var dzien4T = transittime.Transit.Thursday;
                                var dzien5T = transittime.Transit.Friday;
                                var dzien6T = transittime.Transit.Saturday;
                                var dzien7T = transittime.Transit.Sunday;


                                int pula_dni2 = 0;
                                int transittimex = transittime.Transit.Duration;

                                for (int i = 0; i <= transittimex; i++)
                                {
                                    if (i != 0)
                                    {
                                        if (dzienobecnystring == "Monday")
                                        {
                                            if (dzien1T == false) pula_dni2 += 1;

                                        }

                                        if (dzienobecnystring == "Tuesday")
                                        {
                                            if (dzien2T == false) pula_dni2 += 1;

                                        }

                                        if (dzienobecnystring == "Wednesday")
                                        {
                                            if (dzien3T == false) pula_dni2 += 1;

                                        }

                                        if (dzienobecnystring == "Thursday")
                                        {
                                            if (dzien4T == false) pula_dni2 += 1;

                                        }

                                        if (dzienobecnystring == "Friday")
                                        {
                                            if (dzien5T == false) pula_dni2 += 1;

                                        }

                                        if (dzienobecnystring == "Saturday")
                                        {
                                            if (dzien6T == false) pula_dni2 += 1;

                                        }

                                        if (dzienobecnystring == "Sunday")
                                        {
                                            if (dzien7T == false) pula_dni2 += 1;

                                        }
                                    }

                                }

                                ostatecznyczas = transittimex + pula_dni2;
                                converteddata = converteddata.AddDays(ostatecznyczas);


                                dzienobecny = converteddata.DayOfWeek;
                                dzienobecnystring = dzienobecny.ToString();

                                var dzien1DT = transittime.Delivery.Monday;
                                var dzien2DT = transittime.Delivery.Tuesday;
                                var dzien3DT = transittime.Delivery.Wednesday;
                                var dzien4DT = transittime.Delivery.Thursday;
                                var dzien5DT = transittime.Delivery.Friday;
                                var dzien6DT = transittime.Delivery.Saturday;
                                var dzien7DT = transittime.Delivery.Sunday;




                                int pula_dni3 = 0;
                                int deliverytime = transittime.Delivery.Duration;
                                string currentday = dzienobecny.ToString();

                                for (int i = 0; i <= deliverytime; i++)
                                {
                                    if (i != 0)
                                    {
                                        if (dzienobecnystring == "Monday")
                                        {
                                            if (dzien1DT == false) pula_dni3 += 1;

                                        }

                                        if (dzienobecnystring == "Tuesday")
                                        {
                                            if (dzien2DT == false) pula_dni3 += 1;

                                        }

                                        if (dzienobecnystring == "Wednesday")
                                        {
                                            if (dzien3DT == false) pula_dni3 += 1;

                                        }

                                        if (dzienobecnystring == "Thursday")
                                        {
                                            if (dzien4DT == false) pula_dni3 += 1;

                                        }

                                        if (dzienobecnystring == "Friday")
                                        {
                                            if (dzien5DT == false) pula_dni3 += 1;

                                        }

                                        if (dzienobecnystring == "Saturday")
                                        {
                                            if (dzien6DT == false) pula_dni3 += 1;

                                        }

                                        if (dzienobecnystring == "Sunday")
                                        {
                                            if (dzien7DT == false) pula_dni3 += 1;

                                        }
                                    }

                                }

                                ostatecznyczas = deliverytime + pula_dni3;
                                converteddata = converteddata.AddDays(ostatecznyczas);




                            }
                        }

                    }




                }
                if (pom <= 0) return StatusCode((int)HttpStatusCode.BadRequest);
                if (pom2 <= 0) return StatusCode((int)HttpStatusCode.BadRequest);


            }


            String data2 = converteddata.ToString("ddMMyyyy");
            data2 += fromquery.ToString("HHmm");

            String data3 = converteddata.ToString("ddMMyyyy");
            data3 += Toquery.ToString("HHmm");



            predictableDateAdd.From = data2;
            predictableDateAdd.To = data3;


            _context.PredictableDates.Add(predictableDateAdd);
            await _context.SaveChangesAsync();

            Product product = new()
            {
                purchaseDate = PurchaseDate,
                From = data2,
                To = data3
            };

            string json = JsonConvert.SerializeObject(product);

            return Ok(json);

            //return Ok(moduleCode+" "+PurchaseDate + " "+ServiceName+" "+fromCountry+ " " + toCountry);
        }




    }
}


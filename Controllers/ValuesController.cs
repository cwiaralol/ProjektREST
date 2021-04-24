using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AplikacjaKurierska.API.Controllers
{
    //http:localhost:5000/api/Values
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>>Get()
        {
            return new string[]{"value1","value2","value3"};
        }
        
        [HttpGet("{id}")]

        public ActionResult<string>Get(int id){
            return "value";
        }


    }

}


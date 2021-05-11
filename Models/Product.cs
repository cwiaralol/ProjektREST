using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaKurierska.API.Models
{
    public class Product
    {
        public string purchaseDate { get; set; }


        public string From { get; set; }
        public string To { get; set; }
    }
}

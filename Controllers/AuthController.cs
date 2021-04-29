using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AplikacjaKurierska.API.Data;
using AplikacjaKurierska.API.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using AplikacjaKurierska.API.Dtos;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace AplikacjaKurierska.API.Controllers
{
    [Route("authorize")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository,IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }


        //http://localhost:5000/modules
        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)

        {

            //odczytywanie danych z przesłanego requestu 
            var userFromRepo = await _repository.Login(userForLoginDto.Username, userForLoginDto.Password);

            //sprawdzanie czy użytkownik istnieje
            if (userFromRepo == null)
            {
                return NotFound();
            }



  
            //create token

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.Username),
                new Claim(ClaimTypes.Role,userFromRepo.UserType)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });

         
    }




    }
}

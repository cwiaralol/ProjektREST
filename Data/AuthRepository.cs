using AplikacjaKurierska.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjaKurierska.API.Data;

namespace AplikacjaKurierska.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {

            _context = context;
        }



        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Username==username);

            if (user == null)
            {
                return null;
            }

            if (!(user.Password == password)) return null;

            return user;

        }

        public Task<User> Register(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username)) return true;

            return false;
        }




    }
}

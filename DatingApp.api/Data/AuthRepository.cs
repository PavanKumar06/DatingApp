using System;
using System.Threading.Tasks;
using DatingApp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext Context;
        public AuthRepository(DataContext context)
        {
            Context = context;

        }
        public async Task<User> Login(string username, string password)
        {
           var user = await Context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);

           if(user == null)
           {
               return null;
           }

           if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))// Since we are getting the user from the repo it has access to both the pwds
           {
               return null;
           }
           return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i < computedHash.Length; ++i)
                {
                    if(computedHash[i] != passwordHash[i]) return false;
                }                
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;//Generates a random key
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));// Converts the string to a byte array
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await Context.Users.AnyAsync(x => x.Username == username))//It compares the current username to anyother username in the entire db
            {
                return true;
            }
            return false;
        }
    }
}
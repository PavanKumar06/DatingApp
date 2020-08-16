using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext Context;
        public DatingRepository(DataContext context)
        {
            Context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            Context.Add(entity);// We haven't used async, since when we add something into our context we're not doing anything with the db at this point, this is going to be saved in memory until we actually save changes back to the db
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await Context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await Context.Photos.FirstOrDefaultAsync(p =>p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id)
        { 
            var user = await Context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);// Photo is considered a navigational property
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await Context.Users.Include(p => p.Photos).ToListAsync();
            
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await Context.SaveChangesAsync() > 0;// If this return more than 0 true is returned (it will return the numbers of users it saved)
        }
    }
}
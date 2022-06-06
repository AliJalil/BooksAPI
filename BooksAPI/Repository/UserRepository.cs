
using System.Collections.Generic;
using System.Linq;
using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Repository.IRepository;

namespace BooksApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public bool CreateUser(User nationalPark)
        {
            _db.Users.Add(nationalPark);
            return Save();
        }

        public bool DeleteUser(User nationalPark)
        {
            _db.Users.Remove(nationalPark);
            return Save();
        }

        public User GetUser(int userId)
        {
            return _db.Users.FirstOrDefault(a => a.UserId == userId);
        }

        public bool UserExist(int userId)
        {
            return _db.Users.Any(a => a.UserId == userId);
        }

        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(a => a.Name).ToList();
        }

        public bool UserExist(string userName)
        {
            bool value = _db.Users.Any(a => a.Username == userName);
            return value;
        }
        
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateUser(User nationalPark)
        {
            _db.Users.Update(nationalPark);
            return Save();
        }
    }
}
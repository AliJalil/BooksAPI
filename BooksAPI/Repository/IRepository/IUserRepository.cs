using System.Collections.Generic;
using BooksApi.Models;

namespace BooksApi.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();


        User GetUser(int depId);


        bool UserExist(int userId);

        bool UserExist(string name);

        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        bool Save();

    }
}
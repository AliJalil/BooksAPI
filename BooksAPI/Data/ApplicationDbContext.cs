using BooksApi.Models;
using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext > options): base(options)
        {

        }

        public DbSet<Department> Departments{ get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Book> Books{ get; set; }
    }
}
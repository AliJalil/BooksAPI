using System.ComponentModel.DataAnnotations;
using BooksApi.Models;

namespace BooksAPI.Models.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        [Required] public int DepId { get; set; }
        
    }
}
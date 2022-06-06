using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BooksApi.Models;
using BooksApi.Models.Dtos;
using BooksAPI.Models.Dtos.UserDtos;
using BooksApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers

{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        public IActionResult GetUsers()
        {
            var objDto = _userRepo.GetUsers().Select(obj => _mapper.Map<UserDto>(obj)).ToList();

            return Ok(objDto);
        }


        /// <summary>
        /// Get individual users
        /// </summary>
        /// <param name="userId"> The Id of the user </param>
        /// <returns></returns>
        [HttpGet("{userId:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int userId)
        {
            var obj = _userRepo.GetUser(userId);
            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<UserDto>(obj);
            //var objDto = new NationalParkDto()
            //{
            //    Created = obj.Created,
            //    Id = obj.Id,
            //    Name = obj.Name,
            //    State = obj.State,
            //};
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] UserCreateDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_userRepo.UserExist(userDto.Username))
            {
                ModelState.AddModelError("", "User Exists!");
                return StatusCode(404, ModelState);
            }

            var userObj = _mapper.Map<User>(userDto);
            if (_userRepo.CreateUser(userObj))
                return CreatedAtAction(nameof(GetUser), new { userId = userObj.UserId },
                    userObj);
            ModelState.AddModelError("", $"Something went wrong when saving the record {userDto.Name}");
            return StatusCode(500, ModelState);

        }


        [HttpPatch("{userId:int}", Name = "UpdateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser(int userId, [FromBody] UserUpdateDto userDto)
        {
            if (userDto == null || userId != userDto.DepId)
            {
                return BadRequest(ModelState);
            }

            var userObj = _mapper.Map<User>(userDto);
            if (_userRepo.UpdateUser(userObj)) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record {userObj.Name}");
            return StatusCode(500, ModelState);

        }
        
        
        [HttpDelete("{userId:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepo.UserExist(userId))
            {
                return NotFound();
            }

            var userObj = _userRepo.GetUser(userId);
            if (_userRepo.DeleteUser(userObj)) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when deleting the record {userObj.Name}");
            return StatusCode(500, ModelState);

        }

    }
}
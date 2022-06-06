using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BooksApi.Models;
using BooksApi.Models.Dtos;
using BooksApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers

{
    [Route("api/[controller]")]
    public class DepartmentsController : Controller
    {
        private IDepartmentRepository _depRepo;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentRepository depRepo, IMapper mapper)
        {
            _depRepo = depRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of departments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DepartmentDto>))]
        public IActionResult GetDepartments()
        {
            var objDto = _depRepo.GetDepartments().Select(obj => _mapper.Map<DepartmentDto>(obj)).ToList();

            return Ok(objDto);
        }


        /// <summary>
        /// Get individual departments
        /// </summary>
        /// <param name="departmentId"> The Id of the department </param>
        /// <returns></returns>
        [HttpGet("{departmentId:int}", Name = "GetDepartment")]
        [ProducesResponseType(200, Type = typeof(DepartmentDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetDepartment(int departmentId)
        {
            var obj = _depRepo.GetDepartment(departmentId);
            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<DepartmentDto>(obj);
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
        [ProducesResponseType(201, Type = typeof(DepartmentDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_depRepo.DepartmentExist(departmentDto.NameAr))
            {
                ModelState.AddModelError("", "Department Exists!");
                return StatusCode(404, ModelState);
            }

            var departmentObj = _mapper.Map<Department>(departmentDto);
            if (_depRepo.CreateDepartment(departmentObj))
                return CreatedAtAction(nameof(GetDepartment), new { departmentId = departmentObj.DepId },
                    departmentObj);
            ModelState.AddModelError("", $"Something went wrong when saving the record {departmentDto.NameAr}");
            return StatusCode(500, ModelState);

        }


        [HttpPatch("{departmentId:int}", Name = "UpdateDepartment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null || departmentId != departmentDto.DepId)
            {
                return BadRequest(ModelState);
            }

            var departmentObj = _mapper.Map<Department>(departmentDto);
            if (_depRepo.UpdateDepartment(departmentObj)) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record {departmentObj.NameAr}");
            return StatusCode(500, ModelState);

        }
        
        
        [HttpDelete("{departmentId:int}", Name = "DeleteDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_depRepo.DepartmentExist(departmentId))
            {
                return NotFound();
            }

            var departmentObj = _depRepo.GetDepartment(departmentId);
            if (_depRepo.DeleteDepartment(departmentObj)) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when deleting the record {departmentObj.NameAr}");
            return StatusCode(500, ModelState);

        }

    }
}
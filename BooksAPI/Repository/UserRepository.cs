
using System.Collections.Generic;
using System.Linq;
using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Repository.IRepository;

namespace BooksApi.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public bool CreateDepartment(Department nationalPark)
        {
            _db.Departments.Add(nationalPark);
            return Save();
        }

        public bool DeleteDepartment(Department nationalPark)
        {
            _db.Departments.Remove(nationalPark);
            return Save();
        }

        public Department GetDepartment(int departmentId)
        {
            return _db.Departments.FirstOrDefault(a => a.DepId == departmentId);
        }

        public bool DepartmentExist(int departmentId)
        {
            return _db.Departments.Any(a => a.DepId == departmentId);
        }

        public ICollection<Department> GetDepartments()
        {
            return _db.Departments.OrderBy(a => a.NameAr).ToList();
        }

        public bool DepartmentExist(string name)
        {
            bool value = _db.Departments.Any(a => a.NameAr == name);
            return value;
        }
        
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateDepartment(Department nationalPark)
        {
            _db.Departments.Update(nationalPark);
            return Save();
        }
    }
}
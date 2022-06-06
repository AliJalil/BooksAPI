using System.Collections.Generic;
using BooksApi.Models;

namespace BooksApi.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();


        Department GetDepartment(int depId);


        bool DepartmentExist(int departmentId);

        bool DepartmentExist(string name);

        bool CreateDepartment(Department department);

        bool UpdateDepartment(Department department);

        bool DeleteDepartment(Department department);

        bool Save();

    }
}
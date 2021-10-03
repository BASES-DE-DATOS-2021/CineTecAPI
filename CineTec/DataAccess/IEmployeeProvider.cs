using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public interface IEmployeeProvider
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        void DeleteEmployee(int id);
        void UpdateEmployee(Employee employee);
        void InsertEmployee(Employee employee);
        
        
    }
}

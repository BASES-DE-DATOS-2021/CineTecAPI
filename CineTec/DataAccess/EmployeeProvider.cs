using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private readonly PostgreSqlContext _context;

        public EmployeeProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        public List<Employee> GetAllEmployees()
        {

            return _context.employee.ToList();
   
        }

        public Employee GetEmployee(int id)
        {
            return _context.employee.FirstOrDefault(c => c.id == id);
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.employee.FirstOrDefault(t => t.id == id);
            _context.employee.Remove(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.employee.Update(employee);
            _context.SaveChanges();
        }

        public void InsertEmployee(Employee employee)
        {
            _context.employee.Add(employee);
            _context.SaveChanges();
        }
    }
}

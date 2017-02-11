using System.Collections.Generic;
using twmvc26.Models;

namespace twmvc26.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IEnumerable<Employee> GetAll()
        {
            return new EmployeesDatabase();
        }
    }
}
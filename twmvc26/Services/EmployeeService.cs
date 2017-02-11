using System.Collections.Generic;
using System.Linq;
using twmvc26.Models;
using twmvc26.Repositories;

namespace twmvc26.Services
{
    public class EmployeeService : IService<Employee>
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll().ToList();
        }
    }
}
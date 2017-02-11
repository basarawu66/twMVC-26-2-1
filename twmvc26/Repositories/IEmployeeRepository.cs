using System.Collections.Generic;
using twmvc26.Models;

namespace twmvc26.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
    }
}
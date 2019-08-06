using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IEmployeeRepository<Employee> : IRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesByTaskId(int id);
    }
}

using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mapper;
using Core.Interfaces;
using DLayer;
using DLayer.EF.EfEntities;
using DLayer.Entities;
using DLayer.Interfaces;
using System.Collections.Generic;

namespace BLayer.Services
{
    public class EmployeeService :BaseService<Employee,EmployeeDTO>, IService<EmployeeDTO>
    {
        private IUnitOfWork _DataBase { get; set; }
        private IMapper<Employee,EmployeeDTO> employeeMapper;
        public EmployeeService(IUnitOfWork dataBase)
        {
            _DataBase = dataBase;
            employeeMapper = new EmployeeMapper();
        }

        public int Add(EmployeeDTO entity)
        {
            var employee = employeeMapper.Map(entity);
            return _DataBase.Employee.Insert(employee);
        }

        public void Edit(EmployeeDTO entity)
        {
            var staff = employeeMapper.Map(entity);
            _DataBase.Employee.Edit(staff);
        }

        public IEnumerable<EmployeeDTO> GetAllWithPaging(int pageNumber)
        {
            return GetPaging(employeeMapper, _DataBase.Employee.GetAllWithPaging(pageNumber));
        }

        public EmployeeDTO GetById(int id)
        {
            var staff = _DataBase.Employee.GetById(id);
            return employeeMapper.Map(staff);
        }

        public void Delete(int id)
        {
            _DataBase.Employee.Delete(id);
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            return GetPaging(employeeMapper, _DataBase.Employee.GetAll());
        }
    }
}

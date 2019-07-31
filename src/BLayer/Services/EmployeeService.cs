using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mappers;
using Core.Interfaces;
using DLayer;
using DLayer.EFContext.EfEntities;
using DLayer.Entities;
using DLayer.Interfaces;
using System.Collections.Generic;

namespace BLayer.Services
{
    public class EmployeeService :BaseService<Employee,EmployeeDTO>, IService<EmployeeDTO>
    {
        private IUnitOfWork _DataBase { get; set; }
        private IMapper<Employee,EmployeeDTO> _employeeMapper;
        public EmployeeService(IUnitOfWork dataBase, IMapper<Employee, EmployeeDTO> employeeMapper)
        {
            _DataBase = dataBase;
            _employeeMapper = employeeMapper;
        }

        public int Add(EmployeeDTO entity)
        {
            var employee = _employeeMapper.Map(entity);
            return _DataBase.Employee.Insert(employee);
        }

        public void Edit(EmployeeDTO entity)
        {
            var staff = _employeeMapper.Map(entity);
            _DataBase.Employee.Edit(staff);
        }

        public IEnumerable<EmployeeDTO> GetAllWithPaging(int pageNumber)
        {
            return GetPaging(_employeeMapper, _DataBase.Employee.GetAllWithPaging(pageNumber));
        }

        public EmployeeDTO GetById(int id)
        {
            var staff = _DataBase.Employee.GetById(id);
            return _employeeMapper.Map(staff);
        }

        public void Delete(int id)
        {
            _DataBase.Employee.Delete(id);
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            return GetPaging(_employeeMapper, _DataBase.Employee.GetAll());
        }
    }
}

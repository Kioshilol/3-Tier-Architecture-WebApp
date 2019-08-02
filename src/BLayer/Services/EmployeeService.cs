using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mappers;
using Core;
using Core.Interfaces;
using DLayer;
using DLayer.EFContext.EfEntities;
using DLayer.Entities;
using DLayer.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BLayer.Services
{
    public class EmployeeService :BaseService<Employee,EmployeeDTO>, IService<EmployeeDTO>
    {
        private IUnitOfWork _DataBase { get; set; }
        private IMapper<Employee,EmployeeDTO> _employeeMapper;
        private IHostingEnvironment _hostingEnvironment;
        public EmployeeService(IUnitOfWork dataBase, IMapper<Employee, EmployeeDTO> employeeMapper, IHostingEnvironment environment)
        {
            _DataBase = dataBase;
            _employeeMapper = employeeMapper;
            _hostingEnvironment = environment;
        }

        public int Add(EmployeeDTO entity)
        {
            var employee = AddFileSavingAsync(entity);
            return _DataBase.Employee.Insert(employee);
        }

        public void Edit(EmployeeDTO entity)
        {
            var employee = AddFileSavingAsync(entity);
            _DataBase.Employee.Edit(employee);
        }

        public IEnumerable<EmployeeDTO> GetAllWithPaging(int pageNumber)
        {
            return GetPaging(_employeeMapper, _DataBase.Employee.GetAllWithPaging(pageNumber));
        }

        public EmployeeDTO GetById(int id)
        {
            var employee = _DataBase.Employee.GetById(id);
            employee.FilePath = AppSetting.GetPicturesFilePath() + employee.FilePath;
            return _employeeMapper.Map(employee);
        }

        public void Delete(int id)
        {
            _DataBase.Employee.Delete(id);
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            return GetPaging(_employeeMapper, _DataBase.Employee.GetAll());
        }

        private Employee AddFileSavingAsync(EmployeeDTO entity)
        {
            string filePath = AppSetting.GetFullPathToPictures() + entity.UploadedFile.FileName;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                entity.UploadedFile.CopyTo(stream);
            }

            var employee = _employeeMapper.Map(entity);
            employee.FilePath = entity.UploadedFile.FileName;
            return employee;
        }
    }
}

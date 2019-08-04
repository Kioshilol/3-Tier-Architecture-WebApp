using BLayer.DTO;
using BLayer.Interfaces;
using Core;
using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
using System.Collections.Generic;
using System.IO;

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
            var employee = AddFileSaving(entity);
            return _DataBase.Employee.Insert(employee);
        }

        public void Edit(EmployeeDTO entity)
        {
            var employee = AddFileSaving(entity);
            _DataBase.Employee.Edit(employee);
        }

        public IEnumerable<EmployeeDTO> GetAllWithPaging(int pageNumber)
        {
            return GetAll(_employeeMapper, _DataBase.Employee.GetAllWithPaging(pageNumber));
        }

        public EmployeeDTO GetById(int id)
        {
            var employee = _DataBase.Employee.GetById(id);
            return _employeeMapper.Map(employee);
        }

        public void Delete(int id)
        {
            _DataBase.Employee.Delete(id);
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            return GetAll(_employeeMapper, _DataBase.Employee.GetAll());
        }

        private Employee AddFileSaving(EmployeeDTO employeeDTO)
        {
            if(employeeDTO.UploadedFile != null)
            {
                string filePath = AppSetting.GetFullPathToPictures() + employeeDTO.UploadedFile.FileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    employeeDTO.UploadedFile.CopyTo(stream);
                }

                employeeDTO.FilePath = AppSetting.GetPicturesFilePath() + employeeDTO.UploadedFile.FileName;
            }
            else
            {
                if (employeeDTO.FilePath == null)
                    employeeDTO.FilePath = AppSetting.SetDefaultAvatar();
            }

            var employee = _employeeMapper.Map(employeeDTO);
            return employee;
        }

        public void UploadToXML()
        {
            var employeesDTO = GetAll(_employeeMapper, _DataBase.Employee.GetAll());

            foreach (var item in employeesDTO)
            {
                item.EmployeeTasks = null;
            }

            var employeesDataTable = ConvertToDataTable(employeesDTO);
            WriteAndSaveXMLFile(employeesDataTable);
        }

        public void UploadToExcel()
        {
            var employeesDTO = GetAll(_employeeMapper, _DataBase.Employee.GetAll());
            ExportToExcel(employeesDTO);
        }
    }
}

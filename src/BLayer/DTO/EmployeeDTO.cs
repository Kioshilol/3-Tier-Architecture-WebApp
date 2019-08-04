
using DLayer.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BLayer.DTO
{
    public class EmployeeDTO
    {
        public int? Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Position { get; set; }
        public IFormFile UploadedFile { get; set; }
        public string FilePath { get; set; }
        public ICollection<EmployeeTasksDTO> EmployeeTasks { get; set; }
    }
}

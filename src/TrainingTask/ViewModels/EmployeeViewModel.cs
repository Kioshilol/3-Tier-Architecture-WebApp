using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TrainingTask.ViewModels;

namespace TrainingTask.Models
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        public string SecondName { get; set; }
        [Required]
        public string Position { get; set; }
        public List<EmployeeTasksViewModel> EmployeeTasks { get; set; }
        [XmlIgnore]
        public IFormFile UploadedFile { get; set; }
        public string FilePath { get; set; }

        public override bool Equals(Object obj)
        {
            EmployeeViewModel employeeViewModel = (EmployeeViewModel)obj;
            return (this.Id == employeeViewModel.Id &&
                    this.Name == employeeViewModel.Name &&
                    this.Surname == employeeViewModel.Surname &&
                    this.SecondName == employeeViewModel.SecondName &&
                    this.Position == employeeViewModel.Position);
        }
    }
}

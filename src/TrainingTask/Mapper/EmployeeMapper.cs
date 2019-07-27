using BLayer.DTO;
using Core.Interfaces;
using TrainingTask.Models;

namespace TrainingTask.Mapper
{
    public class EmployeeMapper : IMapper<EmployeeDTO, EmployeeViewModel>
    {
        public EmployeeDTO Map(EmployeeViewModel item)
        {
            return new EmployeeDTO()
            {
                Id = item.Id.Value,
                Name = item.Name,
                Surname = item.Surname,
                SecondName = item.SecondName,
                Position = item.Position
            };
        }

        public EmployeeViewModel Map(EmployeeDTO item)
        {
            return new EmployeeViewModel()
            {
                Id = item.Id.Value,
                Name = item.Name,
                Surname = item.Surname,
                SecondName = item.SecondName,
                Position = item.Position
            };
        }
    }
}

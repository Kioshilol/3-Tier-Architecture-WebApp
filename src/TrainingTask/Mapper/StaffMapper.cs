using BLayer.DTO;
using BLayer.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.Models;

namespace TrainingTask.Mapper
{
    public class StaffMapper : IBaseMapper<StaffDTO, StaffViewModel>
    {
        public StaffDTO Map(StaffViewModel item)
        {
            return new StaffDTO()
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                SecondName = item.SecondName,
                Position = item.Position
            };
        }

        public StaffViewModel Map(StaffDTO item)
        {
            return new StaffViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                SecondName = item.SecondName,
                Position = item.Position
            };
        }
    }
}

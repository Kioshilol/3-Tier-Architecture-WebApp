using BLayer.DTO;
using DLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Mapper
{
    public class StaffMapper : IBaseMapper<Staff, StaffDTO>
    {
        public Staff Map(StaffDTO item)
        {
            return new Staff()
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                SecondName = item.SecondName,
                Position = item.Position
            };
        }

        public StaffDTO Map(Staff item)
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
    }
}

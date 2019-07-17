using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mapper;
using DLayer;
using DLayer.Entities;
using DLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Services
{
    public class StaffService : IService<StaffDTO>
    {
        private IUnitOfWork DataBase { get; set; }
        private IBaseMapper<Staff,StaffDTO> staffMapper;
        public StaffService()
        {
            DataBase = new UnitOfWork();
            staffMapper = new StaffMapper();
        }

        public void Add(StaffDTO entity)
        {
            var staff = staffMapper.Map(entity);
            DataBase.Staff.Insert(staff);
        }

        public void Edit(StaffDTO entity)
        {
            var staff = staffMapper.Map(entity);
            DataBase.Staff.Edit(staff);
        }

        public IEnumerable<StaffDTO> GetAll()
        {
            IEnumerable<Staff> staffList = DataBase.Staff.GetAll();
            var staffDTOList = new List<StaffDTO>();
            foreach(var staff in staffList)
            {
                var stafDTO = this.staffMapper.Map(staff);
                staffDTOList.Add(stafDTO);
            }
            return staffDTOList;
        }

        public StaffDTO GetById(int id)
        {
            var staff = DataBase.Staff.GetById(id);
            var staffDTO = staffMapper.Map(staff);
            return staffDTO;
        }

        public void Delete(int id)
        {
            DataBase.Staff.Delete(id);
        }
    }
}

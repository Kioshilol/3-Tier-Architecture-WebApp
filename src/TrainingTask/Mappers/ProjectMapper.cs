using BLayer.DTO;
using Core.Interfaces;
using DLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.Models;

namespace TrainingTask.Mapper
{
        public class ProjectMapper : IMapper<ProjectDTO, ProjectViewModel>
        {
            public ProjectDTO Map(ProjectViewModel item)
            {
                return new ProjectDTO()
                {
                    Id = item.Id.Value,
                    Name = item.Name,
                    ShortName = item.ShortName,
                    Description = item.Description
                };
            }

            public ProjectViewModel Map(ProjectDTO item)
            {
                return new ProjectViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ShortName = item.ShortName,
                    Description = item.Description
                };
            }
        }
    }

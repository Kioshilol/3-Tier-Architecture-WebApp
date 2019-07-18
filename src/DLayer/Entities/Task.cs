﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Text;

namespace DLayer.Entities
{
    public class Task : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TaskTime { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public  EnumTypeOfStatus TypeStatus { get; set; }
        public int ProjectId { get; set; }
        public enum EnumTypeOfStatus
        {
            [Display(Name = "Not Started")]
            NotStarted,
            [Display(Name = "In Process")]
            InProcess,
            [Display(Name = "Completed")]
            Completed,
            [Display(Name = "Delayed")]
            Delayed
        }
    }
}
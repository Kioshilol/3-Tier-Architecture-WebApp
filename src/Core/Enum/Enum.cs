using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Enum
{
        public enum Status
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

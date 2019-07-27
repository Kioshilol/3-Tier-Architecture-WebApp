using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.ViewModels
{
    public class IndexViewModel<T>
    {
        public int PageNumber { get; set; }
        public IEnumerable<T> ViewModelList { get; set; }
        public PageViewModel Page { get; set; }
    }
}

using System.Collections.Generic;

namespace TrainingTask.ViewModels
{
    public class IndexViewModel<T>
    {
        public int PageNumber { get; set; }
        public ICollection<T> ViewModelList { get; set; }
        public PageViewModel Page { get; set; }
    }
}

using Core;
using DLayer;

namespace TrainingTask.ViewModels
{
    public class PageSetting
    {
        public static int GetRowsPerPage()
        {
            int rowsPerPage = AppSetting.GetPageSize();
            return rowsPerPage;
        }
    }
}

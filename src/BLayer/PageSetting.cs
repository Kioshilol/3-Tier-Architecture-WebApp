using Core;
using System.Collections.Generic;

namespace TrainingTask.ViewModels
{
    public class PageSetting
    {
        public static int GetRowsPerPage()
        {
            int rowsPerPage = AppSetting.GetPageSize();
            return rowsPerPage;
        }

        public static int GetTotalPages<T>(List<T> collection)
        {
            var recordsNumber = collection.Count;
            int rowsPerPage = AppSetting.GetPageSize();
            var rest = recordsNumber % rowsPerPage;
            int totalPages;
            if (rest > 0)
                totalPages = (recordsNumber / rowsPerPage) + 1;
            else
                totalPages = recordsNumber / rowsPerPage;
            return totalPages;
        }
    }
}

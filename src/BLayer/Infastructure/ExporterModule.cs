using BLayer.DTO;
using BLayer.Exporters;
using BLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLayer.Infastructure
{
    public static class Exporter
    {
        public static void AddExportModule(this IServiceCollection services)
        {
            services.AddTransient<IExportToXML<ProjectDTO>, ExportToXML<ProjectDTO>>();
            services.AddTransient<IExportToXML<TaskDTO>, ExportToXML<TaskDTO>>();
            services.AddTransient<IExportToXML<EmployeeDTO>, ExportToXML<EmployeeDTO>>();
            services.AddTransient<IExportToExcel<ProjectDTO>, ExportProjectsToExcel>();
            services.AddTransient<IExportToExcel<TaskDTO>, ExportTasksToExcel>();
            services.AddTransient<IExportToExcel<EmployeeDTO>, ExportEmployeesToExcel>();
        }
    }
}

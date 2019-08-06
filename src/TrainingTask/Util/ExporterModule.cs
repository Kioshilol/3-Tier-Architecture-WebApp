using BLayer.DTO;
using BLayer.Exporters;
using BLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TrainingTask.Models;

namespace BLayer.Infastructure
{
    public static class Exporter
    {
        public static void AddExportModule(this IServiceCollection services)
        {
            services.AddTransient<IExportToXML<ProjectViewModel>, BaseExporter<ProjectViewModel>>();
            services.AddTransient<IExportToXML<TaskViewModel>, BaseExporter<TaskViewModel>>();
            services.AddTransient<IExportToXML<EmployeeViewModel>, BaseExporter<EmployeeViewModel>>();
            services.AddTransient<IExportToExcel<ProjectDTO>, ExportProjectsToExcel>();
        }
    }
}

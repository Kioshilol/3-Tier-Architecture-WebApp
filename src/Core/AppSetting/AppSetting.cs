using Microsoft.Extensions.Configuration;
using System.IO;

namespace Core
{
    public class AppSetting
    {
        public static string GetConnectionString()
        {
            var _connectionString = AddJsonFile().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            return _connectionString;
        }
        public static int GetPageSize()
        {
            var _size = AddJsonFile().GetSection("PageSize").GetSection("RowsPerPage").Value;
            return int.Parse(_size);
        }

        private static IConfigurationRoot AddJsonFile()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            return root;
        }

        public static bool EfConnect()
        {
            var connection = AddJsonFile().GetSection("Connection").GetSection("EfEnable").Value;
            return bool.Parse(connection);
        }

        public static bool isAutoMapperEnable()
        {
            var isMapperEnable = AddJsonFile().GetSection("Mapper").GetSection("AutoMapperEnable").Value;
            return bool.Parse(isMapperEnable);
        }
    }
}

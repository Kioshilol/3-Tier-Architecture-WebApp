using Microsoft.Extensions.Configuration;
using System.IO;

namespace Core
{
    public class AppSetting
    {
        public static string GetConnectionString()
        {
            var connectionString = SetJsonFile().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            return connectionString;
        }
        public static int GetPageSize()
        {
            var size = SetJsonFile().GetSection("PageSize").GetSection("RowsPerPage").Value;
            return int.Parse(size);
        }

        private static IConfigurationRoot SetJsonFile()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            return root;
        }

        public static bool IsEfConnect()
        {
            var connection = SetJsonFile().GetSection("Connection").GetSection("EfEnable").Value;
            return bool.Parse(connection);
        }

        public static bool isAutoMapperEnable()
        {
            var isMapperEnable = SetJsonFile().GetSection("Mapper").GetSection("AutoMapperEnable").Value;
            return bool.Parse(isMapperEnable);
        }

        public static string GetPicturesFilePath()
        {
            var filePath = SetJsonFile().GetSection("Files").GetSection("Pictures").Value;
            return filePath;
        }
        
        public static string GetFullPathToPictures()
        {
            var filePath = SetJsonFile().GetSection("Files").GetSection("FullPathToPictures").Value;
            return filePath;
        }

        public static string SetDefaultAvatar()
        {
            var filePath = SetJsonFile().GetSection("Files").GetSection("DefaultAvatar").Value;
            return filePath;
        }
    }
}

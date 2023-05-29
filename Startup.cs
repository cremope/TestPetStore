using Microsoft.Extensions.Configuration;
using System.IO;

namespace TestPetStore
{
    public static class Startup
    {
        public static IConfiguration APPSETTINGS { get; }

        static Startup()
        {
            APPSETTINGS = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();
        }
    }
}

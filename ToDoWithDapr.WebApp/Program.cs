using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ToDoWithDapr.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //UseContentRoot(Path.GetDirectoryName(typeof(Program).Assembly.Location)).
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

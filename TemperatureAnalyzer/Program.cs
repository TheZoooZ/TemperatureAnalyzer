using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;
using TemperatureAnalyzer.Components;
using TemperatureAnalyzer.Core.Components;

namespace TemperatureAnalyzer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            IConfiguration configuration;

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<Form1>();
                    services.AddLogging(configure => configure.AddConsole());
                    services.AddSingleton<ICOMListener, COMListener>();
                    services.AddScoped<ITemperatureCalculator, TemperatureCalculator>();
                });
            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var appService = services.GetRequiredService<Form1>();
                    Application.Run(appService);
                    //Application.SetCompatibleTextRenderingDefault(false);
                    //Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    //Application.EnableVisualStyles();

                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured");
                    Application.Exit();
                }
            }
        }
    }
}
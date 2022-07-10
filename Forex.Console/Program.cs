using Forex.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Forex.Services.Extensions;


var host = CreateHostBuilder(args).Build();
var commandFactory = host.Services.GetService<ICommandFactory>();
var dbInitializer = host.Services.GetService<IDatabaseInitializer>();

try
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Initializing database. Please wait. ");
    await dbInitializer.Initialize();
    Console.WriteLine("Please insert your command: ");
    var command = Console.ReadLine();
    var parameters = command.Split(" ");
    var commandExecutor = commandFactory.CreateCommand(parameters[0]);
    commandExecutor.Execute(parameters.Skip(1).ToArray());

}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
}



static IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            //add your service registrations
            services.RegisterAppServices();
        });

    return hostBuilder;
}
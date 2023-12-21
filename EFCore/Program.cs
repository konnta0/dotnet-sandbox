using EFCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = ConsoleApp.CreateBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddDbContext<CoreDbContext>(options =>
        options.UseSqlServer("Server=127.0.0.1,1433;Database=testdb;User Id=sa;Password=Passw0rd;Encrypt=false;TrustServerCertificate=true;"));
});

builder.Build().AddAllCommandType().Run();
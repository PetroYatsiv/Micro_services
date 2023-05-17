using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Profiles;
using PlatformService.SyncDataServices.Http;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddDbContext<PlatformService.Data.AppDbContext>(opt =>
            opt.UseInMemoryDatabase("inMemory"));
        builder.Services.AddTransient<IPlatformRepo, PlatformRepo>();
        PrepDb.PreparateDb(builder);
        builder.Services.AddControllers();

        builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(PlatformsProfile));
        var app = builder.Build();
        Console.WriteLine($" -->  Command Service endpoint:  {app.Configuration["CommandService"]}");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        
        app.Run();

        
    }
}
using eCommerceApp.Application.DependencyInjection; 
using eCommerceApp.Infrastructure.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
    .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
Log.Logger.Information("Application is building....");


// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAplicationService();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(builder =>
        builder.AddDefaultPolicy(options =>
        {
            options.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins()
            .AllowCredentials();
        }));
        
try
{
    

    var app = builder.Build();
    app.UseCors();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    Log.Logger.Information("Application is running........");

    app.Run();

}catch(Exception ex)
{
    Log.Logger.Error(ex, "Application faild to start....");
}
finally
{
    Log.CloseAndFlush();
}
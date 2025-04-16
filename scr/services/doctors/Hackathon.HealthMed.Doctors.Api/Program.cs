using Hackathon.HealthMed.Api.Core.Handlers;
using Hackathon.HealthMed.Doctors.Api.Endpoints;
using Hackathon.HealthMed.Doctors.Application;
using Hackathon.HealthMed.Doctors.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string contentRoot = builder.Environment.ContentRootPath;

string environmentName = builder.Environment.EnvironmentName;

Console.WriteLine($"environment utilizado : {environmentName}");


ILoggerFactory loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration,loggerFactory);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Configuration.AddEnvironmentVariables();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hackathon HealthMed API V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapDoctorEndpoints();

app.Run();

public partial class Program { }
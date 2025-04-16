using Hackathon.HealthMed.Api.Core.Handlers;
using Hackathon.HealthMed.Patients.Api.Endpoints;
using Hackathon.HealthMed.Patients.Application;
using Hackathon.HealthMed.Patients.Infrastructure;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string contentRoot = builder.Environment.ContentRootPath;

string environmentName = builder.Environment.EnvironmentName;

Console.WriteLine($"environment utilizado : {environmentName}");


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ILoggerFactory loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});


builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration, loggerFactory);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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

app.MapPatientEndpoints();

app.Run();

public partial class Program { }
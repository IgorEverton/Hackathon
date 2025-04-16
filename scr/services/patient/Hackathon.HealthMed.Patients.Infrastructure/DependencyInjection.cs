using Hackathon.HealthMed.Kernel.Data;
using Hackathon.HealthMed.Kernel.Shared;
using Hackathon.HealthMed.Patients.Application.Abstractions.Authentication;
using Hackathon.HealthMed.Patients.Domain.Patients;
using Hackathon.HealthMed.Patients.Infrastructure.Authentication;
using Hackathon.HealthMed.Patients.Infrastructure.Data;
using Hackathon.HealthMed.Patients.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hackathon.HealthMed.Patients.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger("Test");

        string connectionString = configuration.GetConnectionString("Database");

        logger.LogInformation("TestConnection");
        logger.LogInformation(connectionString);

        services.AddDbContext<ApplicationDbContext>(
            (sp, options) => options
                .UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IPatientRepository, PatientRepository>();

        var jwtOptions = new JwtOptions
        {
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "HealthMedPatient",
            Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "HealthMedPatient",
            SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "super-secret-key-value-dbb6504e-133d-4654-bf55-15536b019434!",
        };

        services.Configure<JwtOptions>(options =>
        {
            options.Issuer = jwtOptions.Issuer;
            options.Audience = jwtOptions.Audience;
            options.SecretKey = jwtOptions.SecretKey;
        });


        services.AddScoped<IJwtProvider, JwtProvider>();
    }
}
using Hackathon.HealthMed.Api.Core.Extensions;
using Hackathon.HealthMed.Doctors.Application.Doctors.AddAppointment;
using Hackathon.HealthMed.Doctors.Application.Doctors.AddSchedule;
using Hackathon.HealthMed.Doctors.Application.Doctors.AvailableSchedule;
using Hackathon.HealthMed.Doctors.Application.Doctors.CancelSchedule;
using Hackathon.HealthMed.Doctors.Application.Doctors.Create;
using Hackathon.HealthMed.Doctors.Application.Doctors.ListPaged;
using Hackathon.HealthMed.Doctors.Application.Doctors.Login;
using Hackathon.HealthMed.Doctors.Application.Doctors.UpdateSchedule;
using Hackathon.HealthMed.Doctors.Application.Doctors.UpdateStatusSchedule;
using Hackathon.HealthMed.Doctors.Domain.Doctors;
using Hackathon.HealthMed.Kernel.Shared;
using MediatR;

namespace Hackathon.HealthMed.Doctors.Api.Endpoints;

public static class DoctorsEndpoint
{
        public static void MapDoctorEndpoints(this IEndpointRouteBuilder app)
        {
            var medicos = app.MapGroup("/api/medicos");

            medicos.MapGet("/buscar", async (
                int page,
                int pageSize,
                string? search,
                Specialty? specialty,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new ListPagedDoctorQuery(page, pageSize, search, specialty), cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            }).RequireAuthorization("PatientOnly");

            medicos.MapGet("/{medicoId}/agenda-disponivel", async (
                Guid medicoId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new AvailableScheduleDoctorQuery(medicoId), cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            }).RequireAuthorization("PatientOnly");

            medicos.MapPost("/login", async (
                LoginDoctorCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            });

            medicos.MapPost("/", async (
                CreateDoctorCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            });

            medicos.MapPost("/horarios", async (
                AddScheduleDoctorCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            }).RequireAuthorization("DoctorOnly");

            medicos.MapPut("/horarios/{horarioId}", async (
                Guid horarioId,
                UpdateScheduleDoctorRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new UpdateScheduleDoctorCommand(horarioId, request.Date, request.Start, request.End, request.Price),
                    cancellationToken
                );
                return result.Match(Results.NoContent, CustomResults.Problem);
            }).RequireAuthorization("DoctorOnly");

            medicos.MapPost("/horarios/{horarioId}/agendamentos/{pacienteId}", async (
                Guid horarioId,
                Guid pacienteId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new AddAppointmentCommand(horarioId, pacienteId), cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            }).RequireAuthorization("PatientOnly");

            medicos.MapPatch("/horarios/{horarioId}/cancelar-agendamento", async (
                Guid horarioId,
                CancelScheduleDoctorRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new CancelScheduleDoctorCommand(horarioId, request.Reason), cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            }).RequireAuthorization("PatientOnly");

            medicos.MapPatch("/horarios/{horarioId}/status/{status}", async (
                Guid horarioId,
                bool status,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new UpdateStatusScheduleDoctorCommand(horarioId, status), cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            }).RequireAuthorization("DoctorOnly");
        }

}
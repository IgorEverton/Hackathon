using Hackathon.HealthMed.Kernel.DomainObjects;
using Hackathon.HealthMed.Kernel.Shared;

namespace Hackathon.HealthMed.Doctors.Domain.Doctors;

public sealed class Doctor : Entity
{
    private readonly List<DoctorSchedule> _doctorSchedules = new();

    public Doctor()
    { }
    
    public Doctor(Guid id, Name name, Email email, Cpf cpf, Crm crm, Password password, Specialty specialty) : base(id)
    {
        Name = name;
        Email = email;
        Cpf = cpf;
        Crm = crm;
        Password = password;
        Specialty = specialty;
    }

    public Name Name { get; set; }
    public Email Email { get; set; }
    public Cpf Cpf { get; set; }
    public Crm Crm { get; set; }
    public Password Password { get; set; }
    public Specialty Specialty { get; set; }

    public IReadOnlyCollection<DoctorSchedule> DoctorSchedules => _doctorSchedules;

    public static Doctor Create(Name name, Email email, Cpf cpf, Crm crm, Password password, Specialty specialty)
    {
        return new Doctor(Guid.NewGuid(), name, email, cpf, crm, password, specialty);
    }

    public void AddSchedule(DoctorSchedule doctorSchedule)
    {
        _doctorSchedules.Add(doctorSchedule);
    }
}

using System.Collections.Generic;

public class Appointment
{
    public string AppointmentServiceTitle { get; set; }
    public string AppointmentClientPassportSeries { get; set; }
    public string AppointmentClientPassportNumber { get; set; }
    public string AppointmentClientSurname { get; set; }
    public string AppointmentClientName { get; set; }
    public string AppointmentClientPatronymic { get; set; }
    public string AppointmentNationality { get; set; }
    public string AppointmentDate { get; set; }
    public string AppointmentDaytime { get; set; }
}

public class ServedAppointment
{
    public string AppointmentServiceTitle { get; set; }
    public string AppointmentClientPassportSeries { get; set; }
    public string AppointmentClientPassportNumber { get; set; }
    public string AppointmentClientSurname { get; set; }
    public string AppointmentClientName { get; set; }
    public string AppointmentClientPatronymic { get; set; }
    public string AppointmentNationality { get; set; }
    public string AppointmentDate { get; set; }
    public string AppointmentDaytime { get; set; }
}

public class Service
{
    public string ServiceType { get; set; }
    public string ServiceTitle { get; set; }
    public List<string> ServiceRequiredDocuments { get; set; }
    public string ServiceGovernmentStructure { get; set; }
    public int ServiceDurationDays { get; set; }
}
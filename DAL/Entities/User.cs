using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int AccountId { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? UserAvatar { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? Role { get; set; }

    public string Status { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Appointment> AppointmentDoctors { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentPatients { get; set; } = new List<Appointment>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CustomizedArvProtocol> CustomizedArvProtocolDoctors { get; set; } = new List<CustomizedArvProtocol>();

    public virtual ICollection<CustomizedArvProtocol> CustomizedArvProtocolPatients { get; set; } = new List<CustomizedArvProtocol>();

    public virtual DoctorInfo? DoctorInfo { get; set; }

    public virtual ICollection<EducationalResource> EducationalResources { get; set; } = new List<EducationalResource>();

    public virtual ICollection<Examination> ExaminationDoctors { get; set; } = new List<Examination>();

    public virtual ICollection<Examination> ExaminationPatients { get; set; } = new List<Examination>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}

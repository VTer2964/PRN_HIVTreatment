using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string Type { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime ScheduledTime { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public int? AppointmentId { get; set; }

    public int? ProtocolId { get; set; }

    public int? ExaminationId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Examination? Examination { get; set; }

    public virtual CustomizedArvProtocol? Protocol { get; set; }

    public virtual User? User { get; set; }
}

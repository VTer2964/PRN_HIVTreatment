using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class CustomizedArvProtocol
{
    public int CustomProtocolId { get; set; }

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }

    public int? BaseProtocolId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public virtual ArvProtocol? BaseProtocol { get; set; }

    public virtual ICollection<CustomizedArvProtocolDetail> CustomizedArvProtocolDetails { get; set; } = new List<CustomizedArvProtocolDetail>();

    public virtual User? Doctor { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User? Patient { get; set; }
}

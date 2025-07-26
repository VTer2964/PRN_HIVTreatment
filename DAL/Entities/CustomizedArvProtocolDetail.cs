using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class CustomizedArvProtocolDetail
{
    public int Id { get; set; }

    public int CustomProtocolId { get; set; }

    public int ArvId { get; set; }

    public string? Dosage { get; set; }

    public string? UsageInstruction { get; set; }

    public string Status { get; set; } = null!;

    public virtual Arv Arv { get; set; } = null!;

    public virtual CustomizedArvProtocol CustomProtocol { get; set; } = null!;
}

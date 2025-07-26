using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ArvProtocolDetail
{
    public int Id { get; set; }

    public int ProtocolId { get; set; }

    public int ArvId { get; set; }

    public string? Dosage { get; set; }

    public string? UsageInstruction { get; set; }

    public string Status { get; set; } = null!;

    public virtual Arv Arv { get; set; } = null!;

    public virtual ArvProtocol Protocol { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ArvProtocol
{
    public int ProtocolId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<ArvProtocolDetail> ArvProtocolDetails { get; set; } = new List<ArvProtocolDetail>();

    public virtual ICollection<CustomizedArvProtocol> CustomizedArvProtocols { get; set; } = new List<CustomizedArvProtocol>();
}

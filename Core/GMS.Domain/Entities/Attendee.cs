using GMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Entities;
public sealed class Attendee:Entity
{
    internal Attendee(Invitation invitation):base(Guid.NewGuid())
    {
        GatheringId = invitation.GatheringId;
        MemberId = invitation.MemberId;
        CreatedOnUtc = DateTime.UtcNow;
    }
    protected Attendee() : base(Guid.NewGuid())
    {
        
    }
    public Guid GatheringId { get; private set; }
    public Guid MemberId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
}

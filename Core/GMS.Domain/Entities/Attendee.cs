using GMS.Domain.Primitives;
using GMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Entities;
public sealed class Attendee:Entity<AttendeeId>
{
    internal Attendee(Invitation invitation)
        :base(AttendeeId.Create(Guid.NewGuid()).Value)
    {
        GatheringId = invitation.GatheringId;
        MemberId = invitation.MemberId;
        CreatedOnUtc = DateTime.UtcNow;
    }
    protected Attendee() : base(AttendeeId.Create(Guid.NewGuid()).Value)
    {
        
    }
    public GatheringId GatheringId { get; private set; }
    public MemberId MemberId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
}

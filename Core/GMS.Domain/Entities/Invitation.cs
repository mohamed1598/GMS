using GMS.Domain.Primitives;
using GMS.Domain.ValueObjects;

namespace GMS.Domain.Entities;
public sealed class Invitation:Entity<InvitationId>
{
    public Invitation(
        InvitationId id,
        Member member,
        Gathering gathering
        ):base(id)
    {
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreatedOnUtc = DateTime.UtcNow;
    }
    protected Invitation():base(InvitationId.Create(Guid.NewGuid()).Value)
    {
        
    }
    public GatheringId GatheringId { get; private set; }
    public MemberId MemberId { get; private set; }
    public InvitationStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ModifiedOnUtc { get; private set; }

    public void Expire()
    {
        Status = InvitationStatus.Expired;
        ModifiedOnUtc = DateTime.UtcNow;
    }
    public Attendee Accept()
    {
        Status = InvitationStatus.Accepted;
        ModifiedOnUtc = DateTime.UtcNow;

        var attendee = new Attendee(this);

        return attendee;
    }
}

public enum InvitationStatus
{
    Pending,
    Expired,
    Accepted
}
using GMS.Domain.Primitives;

namespace GMS.Domain.Entities;
public sealed class Invitation:Entity
{
    public Invitation(
        Guid id,
        Member member,
        Gathering gathering
        ):base(id)
    {
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreatedOnUtc = DateTime.UtcNow;
    }
    protected Invitation():base(Guid.NewGuid())
    {
        
    }
    public Guid GatheringId { get; private set; }
    public Guid MemberId { get; private set; }
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
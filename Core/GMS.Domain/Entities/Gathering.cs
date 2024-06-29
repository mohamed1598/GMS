using GMS.Domain.Errors;
using GMS.Domain.Primitives;
using GMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Entities;
public sealed class Gathering:Entity
{
    private readonly List<Invitation> _invitations = [];
    private readonly List<Attendee> _attendees = [];
    internal Gathering(
        Guid id,
        Member creator,
        GatheringType type,
        DateTime schedualedAtUtc,
        string name,
        string? location):base(id)
    {
        Creator = creator;
        Type = type;
        ScheduledAtUtc = schedualedAtUtc;
        Name = name;
        Location = location;
    }

    protected Gathering():base(Guid.NewGuid())
    {
        
    }
    public Member Creator { get; private set; }
    public GatheringType Type { get; private set; }
    public string Name { get; private set; }
    public DateTime ScheduledAtUtc { get; private set; }
    public string? Location { get; private set; }
    public int? MaximumNumberOfAttendees { get; private set; }
    public DateTime? InvitationsExpireAtUtc { get; private set; }
    public int NumberOfAttendees { get; set; }
    public IReadOnlyCollection<Attendee> Attendees => _attendees;
    public IReadOnlyCollection<Invitation> Invitations => _invitations;

    public static Result<Gathering> Create(
        Guid id,
        Member creator,
        GatheringType type,
        DateTime schedualedAtUtc,
        string name,
        string? location,
        int? maximumNumberOfAttendees,
        int? invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(
            Guid.NewGuid(),
            creator,
            type,
            schedualedAtUtc,
            name,
            location);

        //calculate gathering type details
        switch(gathering.Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
                    return Result.Failure<Gathering>(DomainErrors.Gathering.MaximumNumberOfAttendeesNotSpecified);
                gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;

            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                    return Result.Failure<Gathering>(DomainErrors.Gathering.ExpiredForInvitation);
                gathering.InvitationsExpireAtUtc = gathering.InvitationsExpireAtUtc!
                    .Value.AddHours(invitationsValidBeforeInHours.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }
        return gathering;
    }

    public Result<Invitation> SendInvitation(Member member)
    {
        //validate
        if (Creator.Id == member.Id)
            return Result.Failure<Invitation>(DomainErrors.Gathering.InvitingCreator);
        if (ScheduledAtUtc < DateTime.UtcNow)
            return Result.Failure<Invitation>(DomainErrors.Gathering.InvitingCreator);

        var invitation = new Invitation(Guid.NewGuid(), member, this);
        _invitations.Add(invitation);
        return invitation;
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {
        var expired =
            (Type == GatheringType.WithFixedNumberOfAttendees && NumberOfAttendees == MaximumNumberOfAttendees) ||
            (Type == GatheringType.WithExpirationForInvitations && InvitationsExpireAtUtc < DateTime.UtcNow);

        if (expired)
        {
            invitation.Expire();
            return null;
        }

        var attendee = invitation.Accept();

        _attendees.Add(attendee);
        NumberOfAttendees++;

        return attendee;
    }
}

public enum GatheringType
{
    WithExpirationForInvitations,
    WithFixedNumberOfAttendees
}
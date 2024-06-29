using GMS.Domain.DomainEvents;
using GMS.Domain.Errors;
using GMS.Domain.Primitives;
using GMS.Domain.Shared;
using GMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Entities;
public sealed class Gathering:AggregateRoot<GatheringId>
{
    private readonly List<Invitation> _invitations = [];
    private readonly List<Attendee> _attendees = [];
    internal Gathering(
        GatheringId id,
        Member creator,
        GatheringType type,
        DateTime schedualedAtUtc,
        Name name,
        string? location):base(id)
    {
        Creator = creator;
        Type = type;
        ScheduledAtUtc = schedualedAtUtc;
        Name = name;
        Location = location;
    }

    protected Gathering():base(GatheringId.Create(Guid.NewGuid()).Value)
    {
        
    }
    public Member Creator { get; private set; }
    public GatheringType Type { get; private set; }
    public Name Name { get; private set; }
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
        Name name,
        string? location,
        int? maximumNumberOfAttendees,
        int? invitationsValidBeforeInHours)
    {
        var gatheringIdResult = GatheringId.Create(Guid.NewGuid());
        if (gatheringIdResult.IsFailure)
            //log
            return Result.Failure<Gathering>(gatheringIdResult.Error!);

        var gathering = new Gathering(
            gatheringIdResult.Value,
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

        var invitationIdResult = InvitationId.Create(Guid.NewGuid());
        if (invitationIdResult.IsFailure)
            //log
            return Result.Failure<Invitation>(invitationIdResult.Error!);

        var invitation = new Invitation(invitationIdResult.Value, member, this);
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

        RaiseDomainEvent(new InvitationAcceptedDomainEvent(invitation.Id.Value, Id.Value));

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
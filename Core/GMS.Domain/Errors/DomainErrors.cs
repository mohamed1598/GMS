using GMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Gathering
        {
            public static readonly Error InvitingCreator = new Error(
            "Gathering.InvitingCreator",
            "Can't send invitation to the gathering creator.");

            public static readonly Error AlreadyPassed = new Error(
                "Gathering.AlreadyPassed",
                "Can't send invitation for gathering in the past.");

            public static readonly Error ExpiredForInvitation = new Error(
                "Gathering.ExpiredForInvitation",
                "invitationsValidBeforeInHours can't be null.");

            public static readonly Error MaximumNumberOfAttendeesNotSpecified = new Error(
                "Gathering.MaximumNumberOfAttendeesNotSpecified",
                $"{nameof(Entities.Gathering.MaximumNumberOfAttendees)} can't be null.");
        }
        
    }
}

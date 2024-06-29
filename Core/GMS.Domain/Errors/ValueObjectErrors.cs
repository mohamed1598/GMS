using GMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Errors
{
    public static class ValueObjectErrors
    {
        public static class FirstName
        {
            public static readonly Error Empty = new Error(
            "FirstName.Empty",
            "First name is empty.");

            public static readonly Error TooLong = new Error(
                "FirstName.TooLong",
                "First name is too long.");

        }

        public static class LastName
        {
            public static readonly Error Empty = new Error(
            "LastName.Empty",
            "Last name is empty.");

            public static readonly Error TooLong = new Error(
                "LastName.TooLong",
                "Last name is too long.");

        }

        public static class Email
        {
            public static readonly Error Empty = new Error(
            "Email.Empty",
            "Email is empty.");

            public static readonly Error NotValid = new Error(
                "Email.NotValid",
                "Email is not valid.");

        }

        public static class Name
        {
            public static readonly Error Empty = new Error(
            "Name.Empty",
            "Name is empty.");

            public static readonly Error TooLong = new Error(
                "Name.TooLong",
                "Name is too long.");

        }

        public static class MemberId
        {
            public static readonly Error NotValid = new Error(
                "MemberId.NotValid",
                "Member Id is not valid.");

        }

        public static class InvitationId
        {
            public static readonly Error NotValid = new Error(
                "InvitationId.NotValid",
                "Invitation Id is not valid.");

        }

        public static class AttendeeId
        {
            public static readonly Error NotValid = new Error(
                "AttendeeId.NotValid",
                "Attendee Id is not valid.");

        }

        public static class GatheringId
        {
            public static readonly Error NotValid = new Error(
                "GatheringId.NotValid",
                "Gathering Id is not valid.");

        }
    }
}

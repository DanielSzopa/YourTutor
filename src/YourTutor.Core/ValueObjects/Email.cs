﻿using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidEmailException($"This email is invalid: {value}");

            Value = value;
        }

        public static implicit operator string(Email email) => email.Value;

        public static implicit operator Email(string value) => new Email(value);
    }
}



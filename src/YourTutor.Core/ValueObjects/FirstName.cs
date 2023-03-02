﻿using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects
{
    public sealed class FirstName
    {
        public string Value { get; }
        public FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidFirstNameException($"This firstName is invalid: {value}");

            Value = value;
        }

        public static implicit operator FirstName(string value) => new FirstName(value);

        public static implicit operator string(FirstName firstName) => firstName.Value;
    }
}



﻿using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class Experience
    {
        public int ExperienceId { get; private set; }
        public string Name { get; private set; }

        public UserId UserId { get; private set; }
    }
}



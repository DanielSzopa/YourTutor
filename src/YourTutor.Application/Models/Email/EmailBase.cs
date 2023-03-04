namespace YourTutor.Application.Models.EmailBase
{
    public abstract class EmailBase
    {
        public EmailBase(string subject, string body, string tag, string to, string from)
        {
            Subject = subject;
            Body = body;
            Tag = tag;
            To = to;
            From = from;
        }

        public string Subject { get; }
        public string Body { get; }
        public string Tag { get; }
        public string To { get; }
        public string From { get; }
    }
}



namespace YourTutor.Core.Exceptions
{
    public sealed class NotFoundTutorException : CustomException
    {
        public NotFoundTutorException(Guid userId) : base($"Can not found tutor, id: {userId}")
        {
        }
    }
}



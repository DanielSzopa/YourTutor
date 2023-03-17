namespace YourTutor.Application.Abstractions
{
    public interface IHttpContextService
    {
        Guid GetUserIdFromClaims();
    }
}

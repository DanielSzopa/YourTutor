namespace YourTutor.Application.Abstractions.UserManager
{
    public interface ISignInManager
    {
        Task SignInAsync(bool isPersistent, Guid userId, string fullName);
    }
}

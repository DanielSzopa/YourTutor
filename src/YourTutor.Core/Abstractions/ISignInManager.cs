namespace YourTutor.Core.Abstractions
{
    public interface ISignInManager
    {
        Task SignInAsync(bool isPersistent, Guid userId);
    }
}

using YourTutor.Application.Abstractions.UnitOfWork;

namespace YourTutor.Infrastructure.DAL.UnitOfWork;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly YourTutorDbContext _yourTutorDbContext;

    public UnitOfWork(YourTutorDbContext yourTutorDbContext)
    {
        _yourTutorDbContext = yourTutorDbContext;
    }

    public async Task SaveChangesAsync()
    {
       await _yourTutorDbContext.SaveChangesAsync();
    }
}



using Persistence;

namespace Tests.Common;

public abstract class TestCommandBase : IDisposable
{
    protected readonly ApplicationDbContext Context;

    public TestCommandBase()
    {
        Context = BooksContextFactory.Create();
    }

    public void Dispose()
    {
        BooksContextFactory.Destroy(Context);
    }
}
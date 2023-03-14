using Application.Common.Mappings;
using Application.Interfaces;
using AutoMapper;
using Persistence;

namespace Tests.Common;

public class QueryTestFixture : IDisposable
{
    public ApplicationDbContext Context;
    public IMapper Mapper;
    
    public QueryTestFixture()
    {
        Context = BooksContextFactory.Create();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
        });
        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        BooksContextFactory.Destroy(Context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture>
{
}
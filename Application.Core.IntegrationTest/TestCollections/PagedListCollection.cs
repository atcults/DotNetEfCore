using Application.Core.IntegrationTest.TestFixtures;
using Xunit;

namespace Application.Core.IntegrationTest.TestCollections
{
    [CollectionDefinition("PagedList")]
    public class PagedListCollection : ICollectionFixture<SqlLiteWith20ProductsTestFixture>
    {
    }
}
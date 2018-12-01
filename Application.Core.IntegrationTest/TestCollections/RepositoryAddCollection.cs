using Application.Core.IntegrationTest.TestFixtures;
using Xunit;

namespace Application.Core.IntegrationTest.TestCollections
{
    [CollectionDefinition("RepositoryAdd")]
    public class RepositoryAddCollection : ICollectionFixture<SqlLiteWithEmptyDataTestFixture>
    {
    }
}
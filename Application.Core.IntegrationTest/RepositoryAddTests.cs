using Application.Core.Domain;
using Application.Core.EfWrapper;
using Application.Core.IntegrationTest.TestFixtures;
using Xunit;

namespace Application.Core.IntegrationTest
{
    public class RepositoryAddTest : IClassFixture<InMemoryTestFixture>
    {
        public RepositoryAddTest(InMemoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly InMemoryTestFixture _fixture;

        [Fact]
        public void ShouldAddNewProduct()
        {
            // Arrange 
            using (var uow = new UnitOfWork<AppDbContext>(_fixture.Context))
            {
                var repo = uow.GetRepository<Product>();
                var newProduct = new Product {Name = GlobalTestStrings.TestProductName};

                // Act
                repo.Add(newProduct);
                uow.SaveChanges();

                //Assert
                Assert.Equal(1, newProduct.Id);
            }
        }
    }
}
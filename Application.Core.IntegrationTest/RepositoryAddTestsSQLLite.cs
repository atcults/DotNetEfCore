using System;
using Application.Core.Domain;
using Application.Core.EfWrapper;
using Application.Core.IntegrationTest.TestFixtures;
using Xunit;

namespace Application.Core.IntegrationTest
{
    [Collection("RepositoryAdd")]
    public class RepositoryAddTestsSqlLite : IDisposable
    {
        public RepositoryAddTestsSqlLite(SqlLiteWithEmptyDataTestFixture fixture)
        {
            _fixture = fixture;
        }


        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWithEmptyDataTestFixture _fixture;

        [Fact]
        public void ShouldAddNewCategory()
        {
            //arange 
            using (var uow = new UnitOfWork<AppDbContext>(_fixture.Context))
            {
                var repo = uow.GetRepository<Category>();
                var newCategory = new Category {Name = GlobalTestStrings.TestProductCategoryName};
                //Act 
                repo.Add(newCategory);
                uow.SaveChanges();
                //Assert
                Assert.Equal(1, newCategory.Id);
            }
        }

        [Fact]
        public void ShouldAddNewProduct()
        {
            //Arrange
            using (var uow = new UnitOfWork<AppDbContext>(_fixture.Context))
            {
                var repo = uow.GetRepository<Product>();
                var newProduct = new Product
                {
                    Name = GlobalTestStrings.TestProductName,
                    Category = new Category {Id = 1, Name = GlobalTestStrings.TestProductCategoryName}
                };

                //Act 
                repo.Add(newProduct);
                uow.SaveChanges();

                //Assert
                Assert.Equal(1, newProduct.Id);
            }
        }
    }
}
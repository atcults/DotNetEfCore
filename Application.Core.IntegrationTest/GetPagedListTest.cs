using System;
using System.Linq;
using Application.Core.Domain;
using Application.Core.EfWrapper;
using Application.Core.EfWrapper.Paging;
using Application.Core.IntegrationTest.TestFixtures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Core.IntegrationTest
{
    [Collection("PagedList")]
    public class GetPagedListTest : IDisposable
    {
        public GetPagedListTest(SqlLiteWith20ProductsTestFixture fixture)
        {
            _testFixture = fixture;
        }

        public void Dispose()
        {
            _testFixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _testFixture;

        [Fact]
        public void GetPagedListIncludesTest()
        {
            using (var uow = new UnitOfWork<AppDbContext>(_testFixture.Context))
            {
                var cats = uow.GetRepository<Category>().GetList(include: source =>
                    source.Include(x => x.Products).ThenInclude(prod => prod.Category), size: 5);

                Assert.IsAssignableFrom<Paginate<Category>>(cats);

                Assert.Equal(20, cats.Count);
                Assert.Equal(4, cats.Pages);
                Assert.Equal(5, cats.Items.Count);
            }
        }


        [Fact]
        public void GetProductPagedListUsingPredicateTest()
        {
            //Arrange 
            using (var uow = new UnitOfWork<AppDbContext>(_testFixture.Context))
            {
                var repo = uow.GetRepository<Product>();
                //Act
                var productList = repo.GetList(predicate: x => x.CategoryId == 1).Items;
                //Assert
                Assert.Equal(5, productList.Count);
            }
        }

        [Fact]
        public void ShouldGet5ProductsOutOfStockMultiPredicateTest()
        {
            // Arrange
            using (var uow = new UnitOfWork<AppDbContext>(_testFixture.Context))
            {
                var repo = uow.GetRepository<Product>();
                //Act
                var productList = repo.GetList(predicate: x =>  x.Stock == 0 && x.InStock.Value == false).Items;
                //Assert
                Assert.Equal(5, productList.Count);
            }
            
        }
        [Fact]
        public void ShouldGetAllProductsFromSqlQuerySelect()
        {
            // Arrange
            var strSQL = "Select * from Products";
            using (var uow = new UnitOfWork<AppDbContext>(_testFixture.Context))
            {
                var repo = uow.GetRepository<Product>();
                //Act
                var productList = repo.Query(strSQL).AsEnumerable();
                //Assert
                Assert.Equal(20, productList.Count());
            }
        }
        
        [Fact]
        public void ShouldGetSqlQuerySelect()
        {
            //Arrange
            var strSQL = "Select p.* from Products p inner join Categories c on p.categoryid = c.id where c.id = 1";
            using (var uow = new UnitOfWork<AppDbContext>(_testFixture.Context))
            {
                var repo = uow.GetRepository<Product>();
                //Act
                var productList = repo.Query(strSQL).AsEnumerable();
                //Assert
                Assert.Equal(5, productList.Count());
            }
        }


        [Fact]
        public void ShouldBeReadOnlyInterface()
        {
            // Arrange 
            using (var uow = new UnitOfWork<AppDbContext>(_testFixture.Context))
            {
                //Act
                var repo = uow.GetReadOnlyRepository<Product>();
                //Assert
                Assert.IsAssignableFrom<IRepositoryReadOnly<Product>>(repo);
            }
        }

        [Fact]
        public void ShouldReadFromProducts()
        {
            // Arrange 
            using (var uow = new UnitOfWork<AppDbContext>(_testFixture.Context))
            {
                var repo = uow.GetReadOnlyRepository<Product>();
                //Act 
                var products = repo.GetList().Items;
                //Assert
                Assert.Equal(20, products.Count);
            }
        }
    }
}
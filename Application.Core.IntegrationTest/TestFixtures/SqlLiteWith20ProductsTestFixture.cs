using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Domain;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.IntegrationTest.TestFixtures
{
    public class SqlLiteWith20ProductsTestFixture : IDisposable
    {
        public AppDbContext Context => SqlLiteInMemoryContext();

        public void Dispose()
        {
            Context?.Dispose();
        }

        private AppDbContext SqlLiteInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new AppDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            context.Categories.AddRange(TestCategories());
            context.Products.AddRange(TestProducts());
            context.SaveChanges();
            return context;
        }

        private IEnumerable<Category> TestCategories()
        {
            return Builder<Category>.CreateListOfSize(20).Build().ToList();
        }

        private IEnumerable<Product> TestProducts()
        {
            var productList = Builder<Product>.CreateListOfSize(20)
                .TheFirst(5)
                    .With(x => x.CategoryId = 1)
                    .With(x => x.InStock = true)
                .TheNext(5)
                    .With(x => x.InStock = false)
                    .With(y => y.Stock = 0)
                .Build();
            
            return productList.ToList();
        }
    }
}
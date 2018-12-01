using System;
using Application.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.IntegrationTest.TestFixtures
{
    public class SqlLiteWithEmptyDataTestFixture : IDisposable
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

            return context;
        }
    }
}
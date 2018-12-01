using System;
using Application.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.IntegrationTest.TestFixtures
{
    public class InMemoryTestFixture : IDisposable
    {
        public AppDbContext Context => InMemoryContext();

        public void Dispose()
        {
            Context?.Dispose();
        }

        private static AppDbContext InMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new AppDbContext(options);

            return context;
        }
    }
}
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Version2
{
    public class UnitTest1
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitTest1()
        {
            var services = new ServiceCollection();

            // set up empty in-memory test db
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<TestContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemory").UseInternalServiceProvider(services.BuildServiceProvider());
                    options.EnableSensitiveDataLogging();
                });

            // set up service provider for tests
            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void Test1()
        {
            using (var ctx = _serviceProvider.GetService<TestContext>())
            {
                // Add a book without an author
                ctx.Books.Add(new Book { Id = 1 });
                ctx.SaveChanges();

                var book = ctx.Books.Include(x => x.Author).FirstOrDefault(x => x.Id == 1);

                Assert.NotNull(book);
            }
        }
    }
}

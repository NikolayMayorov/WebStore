using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDBInit
    {
        private readonly WebStoreDB _webStoreDb;

        public WebStoreDBInit(WebStoreDB webStoreDB)
        {
            _webStoreDb = webStoreDB;
        }

        public void Init()
        {
            InitAsync().Wait();
        }

        private async Task InitAsync()
        {
            await _webStoreDb.Database.EnsureCreatedAsync();

            if (await _webStoreDb.Products.AnyAsync().ConfigureAwait(false))
                return;

            await _webStoreDb.Database.MigrateAsync().ConfigureAwait(false);

            if (await _webStoreDb.Sections.CountAsync().ConfigureAwait(false) == 0)
            {
                await using var transaction = await _webStoreDb.Database.BeginTransactionAsync().ConfigureAwait(false);

                await _webStoreDb.Sections.AddRangeAsync(entities: TestData.Sections).ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");

                await _webStoreDb.SaveChangesAsync().ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            if (await _webStoreDb.Brands.CountAsync().ConfigureAwait(false) == 0)
            {
                await using var transaction = await _webStoreDb.Database.BeginTransactionAsync().ConfigureAwait(false);
                await _webStoreDb.Brands.AddRangeAsync(entities: TestData.Brands).ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");

                await _webStoreDb.SaveChangesAsync().ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            if (await _webStoreDb.Products.CountAsync().ConfigureAwait(false) == 0)
            {
                await using var transaction = await _webStoreDb.Database.BeginTransactionAsync().ConfigureAwait(false);
                await _webStoreDb.Products.AddRangeAsync(entities: TestData.Products).ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");

                await _webStoreDb.SaveChangesAsync().ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            await using (var transaction = await _webStoreDb.Database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await _webStoreDb.Employee.AddRangeAsync(TestData.Employees).ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employee] ON");

                await _webStoreDb.SaveChangesAsync().ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employee] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }
        }
    }
}
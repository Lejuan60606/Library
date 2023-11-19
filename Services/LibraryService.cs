using Repository;
using Services.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public class LibraryService
    {
        public void StartUp()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");
            var config = configuration.Build();
            string connectionString = config.GetConnectionString("Configuration");

            var libraryFactory = new LibraryFactory(new LibraryContext(connectionString));

            try
            {
                var builder = WebApplication.CreateBuilder();

                builder.Services.AddControllers(options =>
                {
                    options.OutputFormatters.Insert(0, new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
                });

                builder.Services.AddScoped<IBookRepo, BookRepo>();
                builder.Services.AddScoped<IMemberRepo, MemberRepo>();
                builder.Services.AddScoped<IBorrowTransactionRepo, BorrowTransactionRepo>();
                builder.Services.AddScoped(_ => new LibraryContext(connectionString));
                builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(connectionString),ServiceLifetime.Scoped); 

                builder.Services.AddTransient(sp => new BookController(libraryFactory.bookRepo));
                builder.Services.AddTransient(sp => new MemberController(libraryFactory.memberRepo));
                builder.Services.AddTransient(sp => new BorrowTransactionController(libraryFactory.borrowTransactionRepo));

                WebApplication app = builder.Build();
                app.MapControllers();
                app.Run();

            }
            catch (Exception ex)
            {

            }
        }
    }
}

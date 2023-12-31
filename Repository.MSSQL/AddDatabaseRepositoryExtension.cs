using Microsoft.EntityFrameworkCore;
using Repository.MSSQL;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class AddDatabaseRepositoryExtension
	{
        public static void AddDatabaseRepository<TDbContext>(this IServiceCollection serviceCollection, string databaseConnString) where TDbContext: DbContext
        {
			Console.WriteLine("Adding DbConext ...");
            Console.ForegroundColor = ConsoleColor.Green;
			if (databaseConnString == null)
            {
				Console.WriteLine("Using InMemoryDatabase Provider");
				serviceCollection.AddDbContext<TDbContext>(options => options.UseInMemoryDatabase("boraDatabase"));
			}	
            else
            {
				Console.WriteLine($"Using SqlServer Provider with {databaseConnString}");
				serviceCollection.AddDbContext<TDbContext>(options => options.UseSqlServer(databaseConnString));
				Console.WriteLine($"For use InMemory Database, remove the connectionString from the appsettings.");
			}
			Console.ResetColor();
			Console.WriteLine();

			serviceCollection.AddScoped<IDatabaseRepository, DatabaseRepository>();
        }
    }
}

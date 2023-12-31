using Microsoft.Extensions.DependencyInjection;
using Repository.AzureTables;

namespace Repository.Tests
{
	public class AzureTablesRepositoryTests : RepositoryTests
	{
		protected override void AddServices(ServiceCollection serviceCollection)
		{
			var storageConnectionString = "";
			serviceCollection.AddAzureTablesRepository(storageConnectionString);
		}

		[Fact]
		public void Where()
		{
			var repository = ServiceProvider.GetService<IAzureTablesRepository>();
			var accounts = repository.Where<Account>(e => e.Email == "lucasfogliarini@gmail.com" && e.Age >= 0);
		}

		[Fact]
		public async Task CommitAsync()
		{
			var repository = ServiceProvider.GetService<IAzureTablesRepository>()!;

			var count = repository.Where<Account>(e => e.Email == "lucasfogliarini@gmail.com" || e.Email == "luanabueno@gmail.com").Count();
			if (count < 2)
			{
				var accountEntities = new List<Account>
				{
					new() {
						Email = "lucasfogliarini@gmail.com",
						Age = 33
					},
					new() {
						Email = "luanabueno@gmail.com",
						Age = 30
					}
				};
				repository.AddRange(accountEntities);
				await repository.CommitAsync();
			}
			
			var lucasAccount = repository.Where<Account>(e => e.Email == "lucasfogliarini@gmail.com").FirstOrDefault();
			var luanaAccount = repository.Where<Account>(e => e.Email == "luanabueno@gmail.com").FirstOrDefault();

			repository.Remove(lucasAccount);

			luanaAccount.Age = 60;
			repository.Update(luanaAccount);
			await repository.CommitAsync();
		}
	}
}
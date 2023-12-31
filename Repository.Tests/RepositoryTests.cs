using Microsoft.Extensions.DependencyInjection;

namespace Repository.Tests
{
	public abstract class RepositoryTests
	{
        protected ServiceProvider ServiceProvider { get; set; }
		public RepositoryTests()
        {
			var serviceCollection = new ServiceCollection();
			AddServices(serviceCollection);
			ServiceProvider = serviceCollection.BuildServiceProvider();
		}

		protected abstract void AddServices(ServiceCollection serviceCollection);
	}
}
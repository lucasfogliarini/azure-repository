using Azure;
using Azure.Data.Tables;
using Repository.MSSQL;

namespace Repository.Tests
{
	public class Account : IEntity, ITableEntity
	{
		public string? RowKey { get; set; }
		public DateTimeOffset? Timestamp { get; set; }
		public string? Email { get; set; }
        public int Age { get; set; }
        public string? PartitionKey { get; set; }
		public ETag ETag { get; set; }
	}
}

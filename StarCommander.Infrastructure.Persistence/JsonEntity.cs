using System;
using System.Text.Json;
using StarCommander.Domain;

namespace StarCommander.Infrastructure.Persistence
{
	public abstract class JsonEntity<T, TU> where T : class, IAggregate where TU : class, T
	{
		public Guid Id { get; set; }

		public string Json { get; set; } = string.Empty;

		public void SetValuesFrom(T aggregate)
		{
			Id = aggregate.Id;
			Json = JsonSerializer.Serialize(aggregate);
			ProjectValues(aggregate);
		}

		protected virtual void ProjectValues(T aggregate)
		{
		}

		public T ToDomain()
		{
			return JsonSerializer.Deserialize<TU>(Json);
		}
	}
}
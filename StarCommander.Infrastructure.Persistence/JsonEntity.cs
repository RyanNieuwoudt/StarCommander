using System;
using Newtonsoft.Json;
using StarCommander.Domain;
using StarCommander.Infrastructure.Serialization;

namespace StarCommander.Infrastructure.Persistence
{
	public abstract class JsonEntity<T> where T : class, IAggregate
	{
		public Guid Id { get; set; }

		public string Json { get; set; } = string.Empty;

		public void SetValuesFrom(T aggregate)
		{
			Id = aggregate.Id;
			Json = JsonConvert.SerializeObject(aggregate, Formatting.Indented, SerializationSettings.Persistence);
			ProjectValues(aggregate);
		}

		protected virtual void ProjectValues(T aggregate)
		{
		}

		public T ToDomain()
		{
			return JsonConvert.DeserializeObject<T>(Json, SerializationSettings.Persistence);
		}
	}
}
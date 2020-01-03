using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StarCommander.Domain;

namespace StarCommander.Infrastructure.Serialization
{
	public sealed class DomainEventConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IDomainEvent);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
			JsonSerializer serializer)
		{
			var token = JToken.Load(reader);
			var name = token.Value<string>("Type") ?? token.Value<string>("type");
			var type = typeof(IDomainEvent).Assembly.GetType(name);

			if (!(FormatterServices.GetUninitializedObject(type) is IDomainEvent domainEvent))
			{
				throw new InvalidOperationException("Invalid domain event.");
			}

			serializer.Populate(token.CreateReader(), domainEvent);
			return domainEvent;
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			throw new InvalidOperationException("Use default serialization.");
		}
	}
}
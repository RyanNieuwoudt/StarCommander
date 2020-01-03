using System.Collections.Generic;
using Newtonsoft.Json;

namespace StarCommander.Infrastructure.Serialization
{
	public static class SerializationSettings
	{
		public static readonly JsonSerializerSettings Middleware = new JsonSerializerSettings
		{
			Formatting = Formatting.Indented,
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		};

		public static readonly JsonSerializerSettings Persistence = new JsonSerializerSettings
		{
			Formatting = Formatting.None,
			NullValueHandling = NullValueHandling.Ignore,
			TypeNameHandling = TypeNameHandling.None,
			Converters = new List<JsonConverter> { new DomainEventConverter(), new SingleValueObjectConverter() }
		};

		static SerializationSettings()
		{
			//TypeDescriptor.AddAttributes(typeof(SomeType), new TypeConverterAttribute(typeof(SomeTypeConverter)));
		}
	}
}
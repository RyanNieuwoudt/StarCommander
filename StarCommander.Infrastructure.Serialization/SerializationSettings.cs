using System.Collections.Generic;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace StarCommander.Infrastructure.Serialization;

public static class SerializationSettings
{
	public static readonly JsonSerializerSettings Middleware = new ()
	{
		Formatting = Formatting.Indented,
		ReferenceLoopHandling = ReferenceLoopHandling.Ignore
	};

	public static readonly JsonSerializerSettings Persistence = new ()
	{
		Formatting = Formatting.None,
		NullValueHandling = NullValueHandling.Ignore,
		TypeNameHandling = TypeNameHandling.None,
		Converters = new List<JsonConverter>
			{ new CommandConverter(), new DomainEventConverter(), new SingleValueObjectConverter() }
	};

	static SerializationSettings()
	{
		//TypeDescriptor.AddAttributes(typeof(SomeType), new TypeConverterAttribute(typeof(SomeTypeConverter)));
		
		//NodaTime does not support NewtonSoft directly, but this works.
		Middleware.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
		Persistence.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
	}
}
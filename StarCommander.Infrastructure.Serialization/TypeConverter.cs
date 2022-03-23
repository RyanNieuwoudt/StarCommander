using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StarCommander.Infrastructure.Serialization;

public abstract class TypeConverter<T> : JsonConverter
{
	public override bool CanRead => true;
	public override bool CanWrite => false;

	public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
	{
		throw new InvalidOperationException("Use default serialization.");
	}

	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(T);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
		JsonSerializer serializer)
	{
		var token = JToken.Load(reader);
		var name = token.Value<string>("Type") ?? token.Value<string>("type");
		var type = typeof(T).Assembly.GetType(name!)!;
		var result = FormatterServices.GetUninitializedObject(type);

		if (result is not T)
		{
			throw new InvalidOperationException($"Invalid {typeof(T)}.");
		}

		serializer.Populate(token.CreateReader(), result);
		return result;
	}
}
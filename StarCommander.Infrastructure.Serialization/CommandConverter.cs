using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StarCommander.Domain;

namespace StarCommander.Infrastructure.Serialization
{
	public class CommandConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ICommand);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
			JsonSerializer serializer)
		{
			var token = JToken.Load(reader);
			var name = token.Value<string>("Type") ?? token.Value<string>("type");
			var type = typeof(ICommand).Assembly.GetType(name);

			if (!(FormatterServices.GetUninitializedObject(type) is ICommand command))
			{
				throw new InvalidOperationException("Invalid command.");
			}

			serializer.Populate(token.CreateReader(), command);
			return command;
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			throw new InvalidOperationException("Use default serialization.");
		}
	}
}
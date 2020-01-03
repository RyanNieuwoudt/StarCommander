using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StarCommander.Shared.Model.Notifications;

namespace StarCommander.Shared
{
	public sealed class ActionConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IAction);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
			JsonSerializer serializer)
		{
			var token = JToken.Load(reader);
			var actionType = token.Value<string>("type");
			var payloadType = $"StarCommander.Shared.Model.Payload.{actionType.ToClassName()}";
			try
			{
				var type = typeof(Model.Notifications.Action<>).MakeGenericType(Type.GetType(payloadType));
				var action = FormatterServices.GetUninitializedObject(type) as IAction;
				serializer.Populate(token.CreateReader(), action);
				return action;
			}
			catch (ArgumentNullException)
			{
				return null;
			}
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			throw new InvalidOperationException("Use default serialization.");
		}
	}
}
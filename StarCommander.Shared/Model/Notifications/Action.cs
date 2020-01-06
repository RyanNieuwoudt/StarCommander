using System;
using Newtonsoft.Json;

namespace StarCommander.Shared.Model.Notifications
{
	public static class Action
	{
		public static Action<T> WithPayload<T>(T payload)
		{
			return new Action<T>(payload.GetType().Name.ToActionType(), payload);
		}
	}

	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Action<T> : IAction
	{
		[JsonConstructor]
		public Action(string type, T payload)
		{
			Type = type;
			Payload = payload;
		}

		[JsonProperty]
		public T Payload { get; private set; }

		[JsonProperty]
		public string Type { get; private set; }

		public static implicit operator string(Action<T> action)
		{
			return action.ToString();
		}

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this, SerializationSettings.Javascript);
		}
	}
}
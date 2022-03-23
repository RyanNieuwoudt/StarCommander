using System;
using Newtonsoft.Json;

namespace StarCommander.Domain;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class DomainEvent : IDomainEvent
{
	[JsonProperty]
	public string Type => GetType().FullName!;
}
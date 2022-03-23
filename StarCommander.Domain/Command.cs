using System;
using Newtonsoft.Json;

namespace StarCommander.Domain;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class Command : ICommand
{
	[JsonProperty]
	public string Type => GetType().FullName!;
}
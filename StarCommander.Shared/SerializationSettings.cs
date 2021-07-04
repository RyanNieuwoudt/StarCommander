using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StarCommander.Shared
{
	public static class SerializationSettings
	{
		public static readonly JsonSerializerSettings Javascript = new ()
		{
			Formatting = Formatting.Indented,
			NullValueHandling = NullValueHandling.Include, //NB To reset existing state on client
			TypeNameHandling = TypeNameHandling.None,
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			Converters = new List<JsonConverter> { new ActionConverter() }
		};
	}
}
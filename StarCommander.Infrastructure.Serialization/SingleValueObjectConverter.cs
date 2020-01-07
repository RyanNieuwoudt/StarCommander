using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TB.ComponentModel;

namespace StarCommander.Infrastructure.Serialization
{
	public class SingleValueObjectConverter : JsonConverter
	{
		static readonly ConcurrentDictionary<Type, Type> ConstructFromTypes = new ConcurrentDictionary<Type, Type>();

		static readonly Type[] Supported = { typeof(Guid), typeof(decimal), typeof(int) };

		public override bool CanConvert(Type objectType)
		{
			var typeToTest = TypeToUse(objectType);
			return typeToTest.Namespace != null && typeToTest.IsValueType && !typeToTest.IsPrimitive &&
			       typeToTest.Namespace.Contains("Domain") && HasImplicitConversion(typeToTest, Supported);
		}

		static bool HasImplicitConversion(Type objectType, params Type[] targetType)
		{
			return objectType.GetMethods(BindingFlags.Public | BindingFlags.Static)
				.Where(m => m.Name == "op_Implicit" && targetType.Contains(m.ReturnType))
				.Any(m => m.GetParameters().FirstOrDefault()?.ParameterType == objectType);
		}

		static bool IsNullable(Type objectType)
		{
			return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		static Type TypeToUse(Type objectType)
		{
			return IsNullable(objectType) ? objectType.GetGenericArguments()[0] : objectType;
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
			JsonSerializer serializer)
		{
			var isNullable = IsNullable(objectType);
			var token = JToken.Load(reader);

			if (token.Type == JTokenType.Null && isNullable)
			{
				return null;
			}

			var instance = GetInstance(reader, objectType, serializer);

			return isNullable ? instance.To(objectType) : instance;
		}

		static object GetInstance(JsonReader reader, Type objectType, JsonSerializer serializer)
		{
			var typeToUse = TypeToUse(objectType);
			var parameterType = ConstructFromTypes.GetOrAdd(objectType, GetBestConstructor(typeToUse));
			return Activator.CreateInstance(TypeToUse(objectType), serializer.Deserialize(reader, parameterType))!;
		}

		static Type GetBestConstructor(Type objectType)
		{
			var parameterTypes = objectType.GetTypeInfo()
				.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
				.Select(c => c.GetParameters())
				.Where(p => p.Length == 1)
				.Select(p => p.Single().ParameterType)
				.ToList();
			foreach (var type in Supported)
			{
				if (parameterTypes.Contains(type))
				{
					return type;
				}
			}

			throw new InvalidOperationException();
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			if (value == null)
			{
				serializer.Serialize(writer, null);
				return;
			}

			foreach (var type in Supported)
			{
				if (!value.IsConvertibleTo(type))
				{
					continue;
				}

				serializer.Serialize(writer, value.To(type));
				return;
			}
		}
	}
}
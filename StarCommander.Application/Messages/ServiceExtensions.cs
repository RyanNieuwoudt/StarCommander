using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Domain;

namespace StarCommander.Application.Messages
{
	public static class ServiceExtensions
	{
		public static void AddWorkerRegistry(this IServiceCollection serviceCollection, Type type)
		{
			var handlers = new Dictionary<string, List<string>>();

			AddHandlersOfType(serviceCollection, typeof(IHandleCommands<>), type.Assembly, handlers);
			AddHandlersOfType(serviceCollection, typeof(IHandleDomainEvents<>), type.Assembly, handlers);

			var lookup = handlers.SelectMany(p => p.Value, Tuple.Create).ToLookup(p => p.Item1.Key, p => p.Item2);

			serviceCollection.AddSingleton<IWorkerRegistry>(new WorkerRegistry(lookup));
		}

		static void AddHandlersOfType(IServiceCollection serviceCollection, Type openGenericType, Assembly assembly,
			IDictionary<string, List<string>> handlers)
		{
			foreach (var handler in GetAllTypesImplementingOpenGenericType(openGenericType, assembly)
				.Where(e => e.IsClass))
			{
				serviceCollection.AddScoped(handler);

				foreach (var i in handler.GetInterfaces()
					.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == openGenericType))
				{
					foreach (var messageType in i.GetGenericArguments())
					{
						var key = messageType.FullName;
						if (key == null)
						{
							continue;
						}

						var handlerType = handler.FullName;
						if (handlerType == null)
						{
							continue;
						}

						if (!handlers.ContainsKey(key))
						{
							handlers.Add(key, new List<string>());
						}

						if (!handlers[key].Contains(handlerType))
						{
							handlers[key].Add(handlerType);
						}
					}
				}
			}
		}

		static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType, Assembly assembly)
		{
			return from x in assembly.GetTypes()
				from z in x.GetInterfaces()
				let y = x.BaseType
				where y != null && y.IsGenericType &&
				      openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition()) ||
				      z.IsGenericType && openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition())
				select x;
		}
	}
}
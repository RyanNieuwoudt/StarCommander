using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Domain;

namespace StarCommander.Application.Events
{
	public static class ServiceExtensions
	{
		public static void AddWorkerRegistry(this IServiceCollection serviceCollection, Type type)
		{
			var iHandleCommands = typeof(IHandleCommands<>);
			var iHandleDomainEvents = typeof(IHandleDomainEvents<>);

			var handlers = new Dictionary<string, List<string>>();

			foreach (var commandHandler in GetAllTypesImplementingOpenGenericType(iHandleCommands, type.Assembly)
				.Where(e => e.IsClass))
			{
				serviceCollection.AddScoped(commandHandler);

				foreach (var i in commandHandler.GetInterfaces()
					.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == iHandleCommands))
				{
					foreach (var commandType in i.GetGenericArguments())
					{
						var key = commandType.FullName;
						if (key == null)
						{
							continue;
						}

						var handlerType = commandHandler.FullName;
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

			foreach (var eventHandler in GetAllTypesImplementingOpenGenericType(iHandleDomainEvents, type.Assembly)
				.Where(e => e.IsClass))
			{
				serviceCollection.AddScoped(eventHandler);

				foreach (var i in eventHandler.GetInterfaces()
					.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == iHandleDomainEvents))
				{
					foreach (var eventType in i.GetGenericArguments())
					{
						var key = eventType.FullName;
						if (key == null)
						{
							continue;
						}

						var handlerType = eventHandler.FullName;
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

			var lookup = handlers.SelectMany(p => p.Value, Tuple.Create).ToLookup(p => p.Item1.Key, p => p.Item2);

			serviceCollection.AddSingleton<IWorkerRegistry>(new WorkerRegistry(lookup));
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
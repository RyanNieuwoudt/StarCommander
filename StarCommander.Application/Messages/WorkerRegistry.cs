using System;
using System.Collections.Generic;
using System.Linq;
using StarCommander.Domain;

namespace StarCommander.Application.Messages;

public class WorkerRegistry : IWorkerRegistry
{
	readonly ILookup<string, string> handlers;

	public WorkerRegistry(ILookup<string, string> handlers) => this.handlers = handlers;

	public IEnumerable<string> GetHandlersFor(ICommand command) =>
		handlers.Contains(command.Type) ? handlers[command.Type].Distinct() : new List<string>();

	public IEnumerable<string> GetHandlersFor(IDomainEvent @event)
	{
		var result = new List<string>();

		if (handlers.Contains(@event.Type))
		{
			result.AddRange(handlers[@event.Type]);
		}

		switch (@event)
		{
			case INotifyPlayer _:
				result.AddRange(handlers[typeof(INotifyPlayer).FullName!]);
				break;
		}

		return result.Distinct();
	}

	public IEnumerable<string> GetHandlersFor(IHaveType payload) => payload switch
	{
		ICommand command => GetHandlersFor(command),
		IDomainEvent @event => GetHandlersFor(@event),
		_ => Array.Empty<string>()
	};
}
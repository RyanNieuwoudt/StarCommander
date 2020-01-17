using System.Collections.Generic;
using System.Linq;
using StarCommander.Domain;

namespace StarCommander.Application.Messages
{
	public class WorkerRegistry : IWorkerRegistry
	{
		readonly ILookup<string, string> handlers;

		public WorkerRegistry(ILookup<string, string> handlers)
		{
			this.handlers = handlers;
		}

		public IEnumerable<string> GetHandlersFor(ICommand command)
		{
			var result = new List<string>();

			if (handlers.Contains(command.Type))
			{
				result.AddRange(handlers[command.Type]);
			}

			return result.Distinct();
		}

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
	}
}
using System;
using System.Threading.Tasks;
using StarCommander.Domain;

namespace StarCommander.Application.Services
{
	public interface ICommandService
	{
		Task Issue(ICommand command, DateTimeOffset? scheduledFor = null);
	}
}
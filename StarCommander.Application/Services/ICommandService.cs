using System.Threading.Tasks;
using NodaTime;
using StarCommander.Domain;

namespace StarCommander.Application.Services;

public interface ICommandService
{
	Task Issue(ICommand command, Instant? scheduledFor = null);
}
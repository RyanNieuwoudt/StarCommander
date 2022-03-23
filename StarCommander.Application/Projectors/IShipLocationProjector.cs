using System;
using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.Projectors;

public interface IShipLocationProjector
{
	Task Project(Reference<Ship> ship, DateTimeOffset date, Heading heading, Position position, Speed speed);
}
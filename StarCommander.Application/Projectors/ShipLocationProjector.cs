using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using NodaTime;
using StarCommander.Domain;
using StarCommander.Domain.Ships;
using StarCommander.Infrastructure.Persistence.Projection.ShipLocations;

namespace StarCommander.Application.Projectors;

public class ShipLocationProjector : ProjectorBase, IShipLocationProjector
{
	readonly IDbContextScopeFactory dbContextScopeFactory;
	readonly IShipLocationRepository shipLocationRepository;

	public ShipLocationProjector(IDbContextScopeFactory dbContextScopeFactory,
		IShipLocationRepository shipLocationRepository)
	{
		this.dbContextScopeFactory = dbContextScopeFactory;
		this.shipLocationRepository = shipLocationRepository;
	}

	public async Task Project(Reference<Ship> ship, Instant instant, Heading heading, Position position, Speed speed)
	{
		using var dbContextScope = dbContextScopeFactory.Create();

		var existingLocations = (await shipLocationRepository.Fetch(ship)).ToList();

		var locations = new List<ShipLocation>
		{
			new ()
			{
				ShipId = ship,
				Heading = heading,
				Speed = speed,
				X = position.X,
				Y = position.Y,
				Created = instant
			}
		};

		var (toDelete, toUpdate, toInsert) = Split(existingLocations, locations);

		await Execute(shipLocationRepository, toDelete, toUpdate, toInsert);

		await dbContextScope.SaveChangesAsync();
	}
}
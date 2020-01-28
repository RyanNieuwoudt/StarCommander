using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Ships;
using StarCommander.Infrastructure.Persistence.Projection.ShipPositions;

namespace StarCommander.Application.Projectors
{
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

		public async Task Project(Reference<Ship> ship, DateTimeOffset date, Heading heading, Position position,
			Speed speed)
		{
			using var dbContextScope = dbContextScopeFactory.Create();

			var existingLocations = (await shipLocationRepository.Fetch(ship)).ToList();

			var locations = new List<ShipLocation>
			{
				new ShipLocation
				{
					ShipId = ship,
					Heading = heading,
					Speed = speed,
					X = position.X,
					Y = position.Y,
					Created = DateTimeOffset.Now,
				}
			};

			var (toDelete, toUpdate, toInsert) = Split(existingLocations, locations);

			await Execute(shipLocationRepository, toDelete, toUpdate, toInsert);

			dbContextScope.SaveChanges();
		}
	}
}
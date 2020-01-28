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
		readonly IShipPositionRepository shipPositionRepository;

		public ShipLocationProjector(IDbContextScopeFactory dbContextScopeFactory,
			IShipPositionRepository shipPositionRepository)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.shipPositionRepository = shipPositionRepository;
		}

		public async Task Project(Reference<Ship> ship, Position position)
		{
			using var dbContextScope = dbContextScopeFactory.Create();

			var existingPositions = (await shipPositionRepository.Fetch(ship)).ToList();

			var positions = new List<ShipPosition>
			{
				new ShipPosition { ShipId = ship, Created = DateTimeOffset.Now, X = position.X, Y = position.Y }
			};

			var (toDelete, toUpdate, toInsert) = Split(existingPositions, positions);

			await Execute(shipPositionRepository, toDelete, toUpdate, toInsert);

			dbContextScope.SaveChanges();
		}
	}
}
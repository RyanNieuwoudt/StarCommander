using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain;
using StarCommander.Domain.Ships;
using StarCommander.Shared.Model.Query;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipLocations
{
	public class ShipLocationRepository : RepositoryBase<ProjectionDataContext>, IShipLocationRepository
	{
		readonly IMapper mapper;

		public ShipLocationRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator, IMapper mapper) :
			base(ambientDbContextConfigurator)
		{
			this.mapper = mapper;
		}

		public async Task Delete(List<ShipLocation> shipLocations)
		{
			DataContext.RemoveRange(
				(await DataContext.ShipLocations.ToListAsync()).Where(a =>
					shipLocations.Any(b => b.HasSamePrimaryKeyAs(a))));
		}

		public async Task Insert(List<ShipLocation> shipLocations)
		{
			await DataContext.AddRangeAsync(shipLocations);
		}

		public async Task Update(List<ShipLocation> shipLocations)
		{
			foreach (var shipLocation in shipLocations)
			{
				var entity =
					(await DataContext.ShipLocations.ToListAsync()).SingleOrDefault(a =>
						a.HasSamePrimaryKeyAs(shipLocation));

				if (entity == null)
				{
					continue;
				}

				mapper.Map(shipLocation, entity);
				DataContext.Update(entity);
			}

			await Task.CompletedTask;
		}

		public async Task<IEnumerable<ShipLocation>> Fetch(Reference<Ship> ship)
		{
			return await DataContext.ShipLocations.AsNoTracking()
				.Where(s => s.ShipId == ship.Id)
				.OrderBy(s => s.ShipLocationId)
				.ToListAsync();
		}

		public async Task<IEnumerable<ScanResult>> ScanForNearbyShips(Reference<Ship> ship)
		{
			//TODO Filter on range...
			//const long range = 1000;

			var query =
				from sl in DataContext.ShipLocations.AsNoTracking()
				join ol in DataContext.ShipLocations.AsNoTracking() on 1 equals 1
				where sl.ShipId == ship.Id
				where sl.ShipId != ol.ShipId
				select new ScanResult { X = ol.X, Y = ol.Y };

			return await query.ToListAsync();
		}
	}
}
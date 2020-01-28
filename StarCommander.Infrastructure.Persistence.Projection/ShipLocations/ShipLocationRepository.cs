using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

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
	}
}
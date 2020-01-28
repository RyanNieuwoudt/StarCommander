using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipPositions
{
	public class ShipLocationRepository : RepositoryBase<ProjectionDataContext>, IShipLocationRepository
	{
		readonly IMapper mapper;

		public ShipLocationRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator, IMapper mapper) :
			base(ambientDbContextConfigurator)
		{
			this.mapper = mapper;
		}

		public async Task Delete(List<ShipLocation> shipPositions)
		{
			DataContext.RemoveRange(
				(await DataContext.ShipPositions.ToListAsync()).Where(a =>
					shipPositions.Any(b => b.HasSamePrimaryKeyAs(a))));
		}

		public async Task Insert(List<ShipLocation> shipPositions)
		{
			await DataContext.AddRangeAsync(shipPositions);
		}

		public async Task Update(List<ShipLocation> shipPositions)
		{
			foreach (var shipPosition in shipPositions)
			{
				var entity =
					(await DataContext.ShipPositions.ToListAsync()).SingleOrDefault(a =>
						a.HasSamePrimaryKeyAs(shipPosition));

				if (entity == null)
				{
					continue;
				}

				mapper.Map(shipPosition, entity);
				DataContext.Update(entity);
			}

			await Task.CompletedTask;
		}

		public async Task<IEnumerable<ShipLocation>> Fetch(Reference<Ship> ship)
		{
			return await DataContext.ShipPositions.AsNoTracking()
				.Where(s => s.ShipId == ship.Id)
				.OrderBy(s => s.ShipPositionId)
				.ToListAsync();
		}
	}
}
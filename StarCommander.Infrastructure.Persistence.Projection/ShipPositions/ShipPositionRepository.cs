using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipPositions
{
	public class ShipPositionRepository : RepositoryBase<ProjectionDataContext>, IShipPositionRepository
	{
		readonly IMapper mapper;

		public ShipPositionRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator, IMapper mapper) :
			base(ambientDbContextConfigurator)
		{
			this.mapper = mapper;
		}

		public async Task Delete(List<ShipPosition> shipPositions)
		{
			DataContext.RemoveRange(
				(await DataContext.ShipPositions.ToListAsync()).Where(a =>
					shipPositions.Any(b => b.HasSamePrimaryKeyAs(a))));
		}

		public async Task Insert(List<ShipPosition> shipPositions)
		{
			await DataContext.AddRangeAsync(shipPositions);
		}

		public async Task Update(List<ShipPosition> shipPositions)
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

		public async Task<IEnumerable<ShipPosition>> Fetch(Reference<Ship> ship)
		{
			return await DataContext.ShipPositions.AsNoTracking()
				.Where(s => s.ShipId == ship.Id)
				.OrderBy(s => s.ShipPositionId)
				.ToListAsync();
		}
	}
}
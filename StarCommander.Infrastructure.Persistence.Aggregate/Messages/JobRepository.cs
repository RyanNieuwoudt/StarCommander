using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmbientDbContextConfigurator;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain;
using StarCommander.Domain.Messages;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class JobRepository : RepositoryBase<MessageDataContext>, IJobRepository
	{
		public JobRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator) : base(
			ambientDbContextConfigurator)
		{
		}

		public async Task<ICollection<Domain.Messages.Job>> All()
		{
			return await DataContext.Jobs.AsNoTracking()
				.OrderBy(j => j.Created)
				.Select(a => a.ToDomain())
				.ToListAsync();
		}

		public async Task<Domain.Messages.Job> Fetch(Reference<Domain.Messages.Job> job)
		{
			var query = DataContext.Jobs.AsNoTracking().Where(j => j.Id == job.Id);
			return (await query.SingleAsync()).ToDomain();
		}

		public async Task Save(Domain.Messages.Job job)
		{
			var entity = await DataContext.Jobs.SingleOrDefaultAsync(j => j.Id == job.Id) ?? AddEntity(job);
			entity.SetValuesFrom(job);
		}

		public async Task SaveAll(ICollection<Domain.Messages.Job> jobs)
		{
			foreach (var job in jobs)
			{
				await Save(job);
			}
		}

		public async Task Remove(Domain.Messages.Job job)
		{
			var entity = await DataContext.Jobs.SingleOrDefaultAsync(j => j.Id == job.Id);
			if (entity != null)
			{
				DataContext.Remove(entity);
			}
		}

		public async Task RemoveAll(ICollection<Domain.Messages.Job> jobs)
		{
			foreach (var job in jobs)
			{
				await Remove(job);
			}
		}

		Job AddEntity(Domain.Messages.Job job)
		{
			var entity = new Job
			{
				Id = job.Id
			};
			DataContext.Add(entity);
			return entity;
		}
	}
}
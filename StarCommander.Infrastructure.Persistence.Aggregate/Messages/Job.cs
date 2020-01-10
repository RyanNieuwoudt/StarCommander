using System;
using static StarCommander.Domain.Reference;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class Job
	{
		public Guid Id { get; set; }
		public Guid QueueId { get; set; }
		public Guid MessageId { get; set; }
		public string Address { get; set; } = string.Empty;
		public string Handler { get; set; } = string.Empty;
		public DateTimeOffset Created { get; set; }

		public void SetValuesFrom(Domain.Messages.Job job)
		{
			Id = job.Id;
			QueueId = job.QueueId;
			MessageId = job.MessageId;
			Address = job.Address;
			Handler = job.Handler;
			Created = job.Created;
		}

		public Domain.Messages.Job ToDomain()
		{
			return new Domain.Messages.Job(To<Domain.Messages.Job>(Id), QueueId, MessageId, Address, Handler, Created);
		}
	}
}
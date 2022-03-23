using System;
using StarCommander.Domain;

namespace StarCommander.Application.Services;

public class RandomIdGenerator : IReferenceGenerator
{
	public Reference<T> NewReference<T>() where T : IAggregate => new (Guid.NewGuid());
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using StarCommander.Infrastructure.Persistence.Projection;

namespace StarCommander.Application.Projectors;

public abstract class ProjectorBase
{
	protected static bool AllEmpty(params ICollection[] collections)
	{
		return !collections.Any(collection => collection.Count > 0);
	}

	protected static async Task Execute<T>(IProjectionRepository<T> repository, List<T> toDelete, List<T> toUpdate,
		List<T> toInsert) where T : IProjectWithKey<T>
	{
		using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

		await repository.Delete(toDelete);
		await repository.Update(toUpdate);
		await repository.Insert(toInsert);

		scope.Complete();
	}

	protected static async Task<(bool, List<T>, List<T>, List<T>)> Project<T>(IProjectionRepository<T> repository,
		IReadOnlyList<T> current, IReadOnlyList<T> intended) where T : IProjectWithKey<T>
	{
		var (toDelete, toUpdate, toInsert) = Split(current, intended);

		if (AllEmpty(toDelete, toUpdate, toInsert))
		{
			return (false, toDelete, toUpdate, toInsert);
		}

		await Execute(repository, toDelete, toUpdate, toInsert);

		return (true, toDelete, toUpdate, toInsert);
	}

	protected static (List<T>, List<T>, List<T>) Split<T>(IReadOnlyList<T> current, IReadOnlyList<T> intended)
		where T : IProjectWithKey<T>
	{
		var indexC = new HashSet<T>();
		var toInsert = new List<T>(intended.Count);
		var toUpdate = new List<T>(intended.Count);

		var currentCurrents = current;

		// ReSharper disable once ForCanBeConvertedToForeach
		for (var ii = 0; ii < intended.Count; ii++)
		{
			var i = intended[ii];
			var shouldInsert = true;
			var nextCurrents = new List<T>(currentCurrents.Count);

			// ReSharper disable once ForCanBeConvertedToForeach
			for (var ci = 0; ci < currentCurrents.Count; ci++)
			{
				var c = currentCurrents[ci];

				if (shouldInsert)
				{
					// ReSharper disable once SwitchStatementMissingSomeCases
					switch (c.IsTo(i))
					{
						case IsToType.IsUpdateFor:
							shouldInsert = false;
							toUpdate.Add(i.WithPrimaryKeyFrom(c));
							indexC.Add(c);
							break;
						case IsToType.RepresentsSameData:
							shouldInsert = false;
							indexC.Add(c);
							break;
						default:
							nextCurrents.Add(c);
							break;
					}
				}
				else
				{
					nextCurrents.Add(c);
				}
			}

			if (shouldInsert)
			{
				toInsert.Add(i);
			}

			currentCurrents = nextCurrents;
		}

		var toDelete = current.Where(c => !indexC.Contains(c)).ToList();

		return (toDelete, toUpdate, toInsert);
	}
}
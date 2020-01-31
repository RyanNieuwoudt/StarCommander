namespace StarCommander.Infrastructure.Persistence.Projection
{
	public abstract class ProjectWithKeyBase<T> : IProjectWithKey<T>
	{
		public abstract bool HasSamePrimaryKeyAs(T other);

		public IsToType IsTo(T other)
		{
			return !RepresentsTheSameDataAs(other) ? IsToType.Nothing :
				TheDataEquals(other) ? IsToType.RepresentsSameData : IsToType.IsUpdateFor;
		}

		public bool IsUpdateFor(T other)
		{
			return RepresentsTheSameDataAs(other) && !TheDataEquals(other);
		}

		public abstract bool RepresentsTheSameDataAs(T other);

		public abstract T WithPrimaryKeyFrom(T other);

		protected abstract bool TheDataEquals(T other);
	}
}
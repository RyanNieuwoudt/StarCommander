namespace StarCommander.Infrastructure.Persistence.Projection;

public interface IProjectWithKey<T>
{
	bool HasSamePrimaryKeyAs(T other);
	IsToType IsTo(T other);
	bool IsUpdateFor(T other);
	bool RepresentsTheSameDataAs(T other);
	T WithPrimaryKeyFrom(T other);
}
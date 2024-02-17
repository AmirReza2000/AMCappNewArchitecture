namespace Domain.Seedwork.Abstractions;

public interface IEntityHasCultureId<TIdentity>
{
	TIdentity CultureId { get; }
}

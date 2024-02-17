namespace Domain.Seedwork.Abstractions;

public interface IEntityHasUpdateDateTime
{
	System.DateTimeOffset UpdateDateTime { get; }

	void SetUpdateDateTime();
}

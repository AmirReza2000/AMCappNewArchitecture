﻿namespace Domain.Seedwork.Abstractions;

public interface IEntityIdIsSetable<TIdentity>
{
	void SetId(TIdentity id);
}

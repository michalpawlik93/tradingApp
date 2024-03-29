﻿using FluentResults;

namespace TradingApp.Module.Quotes.Contract.Ports;

public interface IEntityDataService<TEntity> where TEntity : class
{
    Task<Result> Add(TEntity entity, CancellationToken cancellationToken);
    Task<Result<TEntity>> Get(Guid id, CancellationToken cancellationToken);
}

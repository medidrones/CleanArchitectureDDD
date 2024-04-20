using System.Linq.Expressions;

namespace CleanArchitecture.Domain.Abstractions;

public interface ISpecification<TEntity, TEntityId> 
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
    List<Expression<Func<TEntity, object>>>? Includes { get; }
    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnable { get; }
}

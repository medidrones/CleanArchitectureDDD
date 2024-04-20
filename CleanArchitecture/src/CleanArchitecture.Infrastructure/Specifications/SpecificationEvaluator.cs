using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Specifications;

public class SpecificationEvaluator<TEntity, TEntityId> 
    where TEntity : Entity<TEntityId> 
    where TEntityId : class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TEntityId> spec)
    {
        if (spec.Criteria is not null)
        {
            inputQuery = inputQuery.Where(spec.Criteria);
        }

        if (spec.OrderBy is not null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending is not null)
        {
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsPagingEnable)
        {
            inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
        }

        inputQuery = spec.Includes!.Aggregate(inputQuery, (current, include) => current
            .Include(include))
            .AsSplitQuery()
            .AsNoTracking();

        return inputQuery;
    }
}

using System;
using System.Linq.Expressions;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repository
{
	public interface IWalksRepository
	{
		Task<Walk> CreateAsync(Walk entity);

		Task<Walk?> UpdateAsync(Guid id, Walk entity);

		Task<List<Walk>> GetAllAsync(Expression<Func<Walk, bool>>? filer = null, string? filterOn = null, string? filterQuery = null,
									 string? sortBy = null, bool isAscending = true,
									 int pageNumber = 1, int pageSize =1000);

		Task<Walk?> GetByIdAsync(Expression<Func<Walk, bool>>? filter = null, bool tracked = true);

		Task<Walk?> DeleteAsync(Guid id);
	}
}


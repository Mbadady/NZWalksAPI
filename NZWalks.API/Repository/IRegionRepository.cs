using System;
using System.Linq.Expressions;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repository
{
	public interface IRegionRepository
	{

		Task<List<Region>> GetAllAsync();
		//OR
		//Task<List<Region>> GetAllAsync(Expression<Func<Region, bool>> filter = null);

		Task<Region?> GetByIdAsync(Guid id);

		//OR
		//Task<Region> GetByIdAsync(Expression<Func<Region, bool>> filter = null, bool tracked = true);

		Task<Region?> UpdateAsync(Guid id, Region entity);

		Task<Region> CreateAsync(Region entity);

		Task<Region?> DeleteAsync(Guid id);

	}
}


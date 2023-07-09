using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repository
{
	public class SqlRegionRepository: IRegionRepository
	{
        private readonly NZWalksDbContext _dbContext;
        public SqlRegionRepository(NZWalksDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task<Region> CreateAsync(Region entity)
        {
            await _dbContext.Regions.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingModel == null)
            {
                return null;
            }

            _dbContext.Regions.Remove(existingModel);

            await _dbContext.SaveChangesAsync();

            return existingModel;
        }

        //public async Task<List<Region>> GetAllAsync(Expression<Func<Region, bool>> filter = null)
        //{
        //    IQueryable<Region> query = _dbContext.Regions;

        //    if(filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    return await query.ToListAsync();
        //}

        //OR

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
           return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region entity)
        {
            var existingModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingModel == null)
            {
                return null;
            }

            existingModel.Code = entity.Code;
            existingModel.Name = entity.Name;
            existingModel.RegionImageUrl = entity.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            return existingModel;
        }
    }
}


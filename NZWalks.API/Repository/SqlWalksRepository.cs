using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repository
{
	public class SqlWalksRepository : IWalksRepository
	{
        private readonly NZWalksDbContext _dbContext;

        public SqlWalksRepository(NZWalksDbContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk entity)
        {
            await _dbContext.Walks.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingModel = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(existingModel == null)
            {
                return null;
            }
            _dbContext.Walks.Remove(existingModel);
            await _dbContext.SaveChangesAsync();

            return existingModel;
        }

        public async Task<List<Walk>> GetAllAsync(Expression<Func<Walk, bool>>? filter = null, string? filterOn = null, string? filterQuery = null,
                                                    string? sortBy = null, bool isAscending = true,
                                                    int pageNumber = 1, int pageSize = 1000)
        {
           IQueryable<Walk> query = _dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region);
            // Or
            //var query = _dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                query = query.Where(x => x.Name.Contains(filterQuery));
            }

            // Sorting

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isAscending ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isAscending ? query.OrderBy(x => x.LengthInKm) : query.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination

            var skipResults = (pageNumber - 1) * pageSize;

                return await query.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Expression<Func<Walk, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<Walk> query = _dbContext.Walks;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }

            return await query
                         .Include(x => x.Difficulty)
                         .Include(x => x.Region)
                         .FirstOrDefaultAsync();
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk entity)
        {
            var existingModel = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(existingModel == null)
            {
                return null;
            }

            existingModel.LengthInKm = entity.LengthInKm;
            existingModel.Name = entity.Name;
            existingModel.WalkImageUrl = entity.WalkImageUrl;
            existingModel.RegionId = entity.RegionId;
            existingModel.DifficultyId = entity.DifficultyId;

            await _dbContext.SaveChangesAsync();

            return existingModel;
        }
    }
}


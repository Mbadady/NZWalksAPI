using System;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repository
{
	public class LocalImageRepository : IImageRepository
	{
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
		{
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> UploadAsync(Image entity)
        {

            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{entity.FileName}{entity.FileExtension}");

            // Upload the image to local path
           using var stream = new FileStream(localFilePath, FileMode.Create);

            await entity.File.CopyToAsync(stream);


            // https://localhost:1234/images/image.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{entity.FileName}{entity.FileExtension}";

            entity.FilePath = urlFilePath;

            await dbContext.Images.AddAsync(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}


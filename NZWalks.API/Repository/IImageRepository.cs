using System;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repository
{
	public interface IImageRepository
	{
		Task<Image> UploadAsync(Image entity);
	}
}


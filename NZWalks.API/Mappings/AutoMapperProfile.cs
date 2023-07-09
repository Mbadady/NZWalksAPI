using System;
using AutoMapper;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Region, RegionDTO>().ReverseMap();

			CreateMap<Region, RegionCreateDTO>().ReverseMap();

			CreateMap<Region, RegionUpdateDTO>().ReverseMap();

			CreateMap<Walk, WalksCreateDTO>().ReverseMap();

			CreateMap<Walk, WalksUpdateDTO>().ReverseMap();

			CreateMap<Walk, WalksDTO>();

			CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
		}
	}
}


using AutoMapper;
using WebApp.API.Models.Domain;
using WebApp.API.Models.DTO;

namespace WebApp.API.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles() {
			CreateMap<Region, RegionDto>().ReverseMap();
			CreateMap<AddNewRegionRequestDto, Region>().ReverseMap();
			CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
			CreateMap<AddNewWalkRequestDto, Walk>().ReverseMap();
			CreateMap<Walk, WalkDto>().ReverseMap();
			CreateMap<Difficulty, DifficultyDto>().ReverseMap();
			CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
		}
	}
}

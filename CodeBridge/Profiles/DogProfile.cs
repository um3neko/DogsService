using AutoMapper;
using CodeBridge.Models.DTOs.In;
using CodeBridge.Models.Entities;

namespace CodeBridge.Profiles;

public class DogProfile : Profile
{
    public DogProfile()
    {
        CreateMap<CreateDogDTO, Dog>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.TailLength,
                opt => opt.MapFrom(src => src.TailLength))
            .ForMember(
                dest => dest.Weight,
                opt => opt.MapFrom(src => src.TailLength));
    }
}

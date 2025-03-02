using AutoMapper;
using FileManager.Application.DTOs;
using FileManager.Domain.Entities;

namespace FileManager.Application.MapProfiles;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<FileManagerFile, FileDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FolderId, opt => opt.MapFrom(src => src.FolderId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
            .ForMember(dest => dest.SizeInBytes, opt => opt.MapFrom(src => src.SizeInBytes))
            .ForMember(dest => dest.DownloadDate, opt => opt.MapFrom(src => src.DownloadDate));
    }
}
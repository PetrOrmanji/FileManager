using AutoMapper;
using FileManager.Application.DTOs;
using FileManager.Domain.Entities;

namespace FileManager.Application.MapProfiles;

public class FolderProfile : Profile
{
    public FolderProfile()
    {
        CreateMap<FileManagerFolder, FolderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ParentFolderId, opt => opt.MapFrom(src => src.ParentFolderId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.SubFolders, opt => opt.MapFrom(src => src.SubFolders))
            .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files));
    }
}
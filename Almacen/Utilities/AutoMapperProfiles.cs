using Almacen.DTOs;
using Almacen.Entities;
using AutoMapper;

namespace Almacen.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AreaDTO, Area>();

            CreateMap<Area, AreaDTO>();

            CreateMap<Inmueble, InmuebleDTO>()
                .ForMember(dest => dest.Area, opt=>opt.MapFrom(src => src.Area));

            CreateMap<InmuebleDTO, Inmueble>();

            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Usuario, UsuarioDTO>();

        }
    }
}

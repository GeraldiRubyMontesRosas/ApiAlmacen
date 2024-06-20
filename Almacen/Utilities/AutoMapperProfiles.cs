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

            CreateMap<Area, AreaDTO>()
                 .ForMember(dest => dest.Responsable, opt => opt.MapFrom(src => src.Responsable));

            CreateMap<Inmueble, InmuebleDTO>()
                .ForMember(dest => dest.Area, opt=>opt.MapFrom(src => src.Area));

            CreateMap<InmuebleDTO, Inmueble>();

            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();

            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre}"))
                .ForMember(dest => dest.Responsable, opt => opt.MapFrom(src => src.Responsable))
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol));

            CreateMap<Claim, ClaimDTO>()
               .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Rol.Id))
               .IncludeMembers(src => src.Rol);

            CreateMap<Rol, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Id));


            CreateMap<Responsable, ResponsableDTO>()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"))
                .ForMember(dest => dest.StrFechaNacimiento, opt => opt.MapFrom(src => $"{src.FechaNacimiento:dd/MM/yyyy}"));
            CreateMap<ResponsableDTO, Responsable>();

            CreateMap<Traslado, TrasladoDTO>()
                 .ForMember(dest => dest.AreaDestino, opt => opt.MapFrom(src => src.AreaDestino))
                 .ForMember(dest => dest.AreaOrigen, opt => opt.MapFrom(src => src.AreaOrigen))
                 .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
                 .ForMember(dest => dest.Inmueble, opt => opt.MapFrom(src => src.Inmueble))
                ;
            CreateMap<TrasladoDTO, Traslado>();

        }
    }
}

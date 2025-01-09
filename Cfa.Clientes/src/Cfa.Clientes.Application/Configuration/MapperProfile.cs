using AutoMapper;
using Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;
using Cfa.Clientes.Domain.Entities.Address;
using Cfa.Clientes.Domain.Entities.Client;
using Cfa.Clientes.Domain.Entities.Phone;

namespace Cfa.Clientes.Application.Configuration;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateClientModel, ClienteEntity>()
          .ForMember(dest => dest.Telefonos, opt => opt.MapFrom(src => src.Telefonos))
          .ForMember(dest => dest.Direcciones, opt => opt.MapFrom(src => src.Direcciones));

        CreateMap<TelefonoModel, TelefonoEntity>()
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.TelefonoID, opt => opt.MapFrom(src => src.TelefonoId));

        CreateMap<DireccionModel, DireccionEntity>()
            .ForMember(dest => dest.DireccionID, opt => opt.MapFrom(src => src.DireccionId));


        CreateMap<GetTelefonoModel, TelefonoEntity>()
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.TelefonoID, opt => opt.MapFrom(src => src.TelefonoId));

        CreateMap<GetDireccionModel, DireccionEntity>()
            .ForMember(dest => dest.DireccionID, opt => opt.MapFrom(src => src.DireccionId));
    }
}
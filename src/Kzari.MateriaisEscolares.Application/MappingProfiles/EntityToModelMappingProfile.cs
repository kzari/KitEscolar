using AutoMapper;
using Kzari.MateriaisEscolares.Application.Models;
using Kzari.MaterialEscolar.Domain.Entities;

namespace Kzari.MateriaisEscolares.Application.MappingProfiles
{
    public class EntityToModelMappingProfile : Profile
    {
        public EntityToModelMappingProfile()
        {
            CreateMap<Kit, KitModel>();
            CreateMap<Kit, KitExibirModel>();

            CreateMap<Produto, ProdutoModel>();
        }
    }
}

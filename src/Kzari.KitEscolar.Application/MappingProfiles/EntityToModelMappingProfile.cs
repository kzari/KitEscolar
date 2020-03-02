using AutoMapper;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;

namespace Kzari.KitEscolar.Application.MappingProfiles
{
    public class EntityToModelMappingProfile : Profile
    {
        public EntityToModelMappingProfile()
        {
            CreateMap<Kit, KitModel>();
            CreateMap<Kit, KitExibirModel>();
            CreateMap<Item, ItemModel>();
            CreateMap<Item, ItemExibirModel>();

            CreateMap<Produto, ProdutoModel>();
        }
    }
}

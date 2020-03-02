using AutoMapper;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;

namespace Kzari.KitEscolar.Application.MappingProfiles
{
    public class ModelToEntityMappingProfile : Profile
    {
        public ModelToEntityMappingProfile()
        {
            CreateMap<KitModel, Kit>();
            CreateMap<ItemModel, Item>();

            CreateMap<ProdutoModel, Produto>();
        }
    }
}

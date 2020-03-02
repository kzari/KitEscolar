using AutoMapper;
using Kzari.MateriaisEscolares.Application.Models;
using Kzari.MaterialEscolar.Domain.Entities;

namespace Kzari.MateriaisEscolares.Application.MappingProfiles
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

using AutoMapper;
using Kzari.KitEscolar.Application.AppServices.Base;
using Kzari.KitEscolar.Application.AppServices.Interfaces;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Interfaces.Repositories;
using Kzari.KitEscolar.Domain.Validators;

namespace Kzari.KitEscolar.Application.AppServices
{
    public class ProdutoAppService : AppServiceBaseValidation<Produto, ProdutoValidator, ProdutoModel>, IProdutoAppService
    {
        public ProdutoAppService(IMapper mapper, IEntityBaseRepository<Produto> repository) : base(mapper, repository)
        {
        }
    }
}

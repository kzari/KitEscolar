using AutoMapper;
using Kzari.MateriaisEscolares.Application.AppServices.Base;
using Kzari.MateriaisEscolares.Application.AppServices.Interfaces;
using Kzari.MateriaisEscolares.Application.Models;
using Kzari.MaterialEscolar.Domain.Entities;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using Kzari.MaterialEscolar.Domain.Validators;

namespace Kzari.MateriaisEscolares.Application.AppServices
{
    public class ProdutoAppService : AppServiceBaseValidation<Produto, ProdutoValidator, ProdutoModel>, IProdutoAppService
    {
        public ProdutoAppService(IMapper mapper, IEntityBaseRepository<Produto> repository) : base(mapper, repository)
        {
        }
    }
}

using Kzari.MateriaisEscolares.Application.AppServices.Base;
using Kzari.MateriaisEscolares.Application.Models;
using Kzari.MaterialEscolar.Domain.Entities;

namespace Kzari.MateriaisEscolares.Application.AppServices.Interfaces
{
    public interface IProdutoAppService : IAppServiceBase<Produto, ProdutoModel>
    {
    }
}
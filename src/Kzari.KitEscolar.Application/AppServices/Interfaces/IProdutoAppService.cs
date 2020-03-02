using Kzari.KitEscolar.Application.AppServices.Base;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;

namespace Kzari.KitEscolar.Application.AppServices.Interfaces
{
    public interface IProdutoAppService : IAppServiceBase<Produto, ProdutoModel>
    {
    }
}
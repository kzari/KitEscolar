using Kzari.KitEscolar.Application.AppServices.Base;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;
using System.Collections.Generic;

namespace Kzari.KitEscolar.Application.AppServices.Interfaces
{
    public interface IKitAppService : IAppServiceBase<Kit, KitModel>
    {
        IEnumerable<KitExibirModel> SelecionarTodos();
    }
}

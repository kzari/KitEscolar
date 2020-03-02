using Kzari.MateriaisEscolares.Application.AppServices.Base;
using Kzari.MateriaisEscolares.Application.Models;
using Kzari.MaterialEscolar.Domain.Entities;
using System.Collections.Generic;

namespace Kzari.MateriaisEscolares.Application.AppServices.Interfaces
{
    public interface IKitAppService : IAppServiceBase<Kit, KitModel>
    {
        IEnumerable<KitExibirModel> SelecionarTodos();
    }
}

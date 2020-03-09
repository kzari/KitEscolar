using Kzari.KitEscolar.Application.AppServices.Base;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Validators;

namespace Kzari.KitEscolar.Application.AppServices.Interfaces
{
    public interface IKitAppService : IAppServiceBase<Kit, KitModel, KitValidator>
    {
    }
}

using AutoMapper;
using FluentValidation;
using Kzari.KitEscolar.Application.AppServices.Base;
using Kzari.KitEscolar.Application.AppServices.Interfaces;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Interfaces.Repositories;
using Kzari.KitEscolar.Domain.Validators;

namespace Kzari.KitEscolar.Application.AppServices
{
    public class KitAppService : AppServiceBase<Kit, KitModel, KitValidator>, IKitAppService
    {
        public KitAppService(IMapper mapper, IEntityBaseRepository<Kit> repository, IValidator<Kit> validator)
            : base(mapper, repository, validator)
        {
        }
    }
}

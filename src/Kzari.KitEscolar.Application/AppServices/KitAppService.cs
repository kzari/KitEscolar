using AutoMapper;
using Kzari.KitEscolar.Application.AppServices.Base;
using Kzari.KitEscolar.Application.AppServices.Interfaces;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Interfaces.Repositories;
using Kzari.KitEscolar.Domain.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Kzari.KitEscolar.Application.AppServices
{
    public class KitAppService : AppServiceBaseValidation<Kit, KitValidator, KitModel>,  IKitAppService
    {
        public KitAppService(IMapper mapper, IKitRepository repository) : base(mapper, repository)
        {
        }

        public IEnumerable<KitExibirModel> SelecionarTodos()
        {
            IEnumerable<Kit> entidades = Repository.SelecionarAsNoTracking().Where(a => a.Ativo);

            return MapperInstance.Map<IEnumerable<KitExibirModel>>(entidades);
        }
    }
}

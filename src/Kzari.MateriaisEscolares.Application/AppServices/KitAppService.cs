using AutoMapper;
using Kzari.MateriaisEscolares.Application.AppServices.Base;
using Kzari.MateriaisEscolares.Application.AppServices.Interfaces;
using Kzari.MateriaisEscolares.Application.Models;
using Kzari.MaterialEscolar.Domain.Entities;
using Kzari.MaterialEscolar.Domain.Interfaces.Repositories;
using Kzari.MaterialEscolar.Domain.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Kzari.MateriaisEscolares.Application.AppServices
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

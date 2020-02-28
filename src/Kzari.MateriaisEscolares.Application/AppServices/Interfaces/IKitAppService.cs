using Kzari.MateriaisEscolares.Application.Models;
using System.Collections.Generic;

namespace Kzari.MateriaisEscolares.Application.AppServices.Interfaces
{
    public interface IKitAppService
    {
        int Criar(KitModel model);
        IEnumerable<KitExibirModel> ObterTodos();
        KitExibirModel Obter(int id);
        void Editar(int id, KitModel model);
    }
}

using Kzari.MaterialEscolar.Domain.Entities;
using System.Collections.Generic;

namespace Kzari.MaterialEscolar.Domain.Interfaces.Repositories
{
    public interface IKitRepository
    {
        int Inserir(Kit kit);
        Kit Obter(int id);
        IEnumerable<Kit> Selecionar();
        void Atualizar(Kit entidade);
    }
}

using System;

namespace Kzari.MaterialEscolar.Domain
{
    public abstract class Entidade
    {
        protected Entidade()
        {
            DataCriacao = new DateTime();
            Ativo = true;
        }

        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
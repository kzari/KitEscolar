namespace Kzari.MaterialEscolar.Domain.Entities
{
    public class Item
    {
        public int IdKit { get; set; }
        public virtual Kit Kit { get; set; }

        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }

        public int Quantidade { get; set; }
        public int QuantidadeJaComprada { get; set; }
    }
}

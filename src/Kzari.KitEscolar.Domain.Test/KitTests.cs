using FluentValidation.TestHelper;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Validators;
using Kzari.KitEscolar.Infra.Data;
using Kzari.KitEscolar.Infra.Data.DbContexts;
using Kzari.KitEscolar.Infra.Data.Repositories;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Kzari.KitEscolar.Domain.Test
{
    public class KitTests
    {
        private static KitRepository RepositorioMock(Kit kit = null)
        {
            var kitRepositoryMock = new Mock<KitRepository>(new Mock<MEContext>().Object);
            kitRepositoryMock.Setup(s => s.SelecionarAsNoTracking()).Returns(new List<Kit>() { kit ?? new Kit("Kit 99") });
            return kitRepositoryMock.Object;
        }


        [Fact]
        public void KitNomeVazio()
        {
            new KitValidator(RepositorioMock(), null)
                .ShouldHaveValidationErrorFor(kit => kit.Nome, new Kit(nome: ""));
        }

        [Fact]
        public void KitNomeNulo()
        {
            new KitValidator(RepositorioMock(), null)
                .ShouldHaveValidationErrorFor(kit => kit.Nome, new Kit(nome: ""));
        }

        [Fact]
        public void KitNomeNuloVazio_OK()
        {
            new KitValidator(RepositorioMock(), null)
                .ShouldNotHaveValidationErrorFor(kit => kit.Nome, new Kit("Algum nome"));
        }

        [Fact]
        public void KitMesmoNome()
        {
            string nomeKit = "Kit 1";
            var kitRepository = RepositorioMock(new Kit(nomeKit));
            new KitValidator(kitRepository, null).ShouldHaveValidationErrorFor(kit => kit.Nome, new Kit(nomeKit));
        }

        [Fact]
        public void KitMesmoNome_Ok()
        {
            var kitRepository = RepositorioMock(new Kit("Kit 2"));
            new KitValidator(kitRepository, null).ShouldNotHaveValidationErrorFor(kit => kit.Nome, new Kit("Kit 1"));
        }

        [Fact]
        public void KitComNomeMuitoPequeno()
        {
            new KitValidator(RepositorioMock(), null).ShouldHaveValidationErrorFor(kit => kit.Nome, new Kit("K"));
        }

        [Fact]
        public void KitComNomeMuitoPequeno_OK()
        {
            new KitValidator(RepositorioMock(), null).ShouldNotHaveValidationErrorFor(kit => kit.Nome, new Kit("Kit 1"));
        }

        [Fact]
        public void KitComNomeMuitoGrande()
        {
            new KitValidator(RepositorioMock(), null)
                .ShouldHaveValidationErrorFor(kit => kit.Nome, new Kit("dklasj dklas jdklasj dkalsj dasklj dasklj dkalssdaasd sad asdsa "));
        }

        [Fact]
        public void KitComNomeMuitoGrande_Ok()
        {
            new KitValidator(RepositorioMock(), null).ShouldNotHaveValidationErrorFor(kit => kit.Nome, new Kit("Kit 1"));
        }


        [Fact]
        public void KitComItensSemQuantidade()
        {
            var produtoRepositoryMock = new Mock<EntityBaseRepository<Produto>>(new Mock<MEContext>().Object);
            produtoRepositoryMock.Setup(s => s.SelecionarAsNoTracking()).Returns(new List<Produto>() { new Produto { Id = 1 } });

            var kit = new Kit("Kit 1", new List<Item> { new Item { IdProduto = 1 } });
            new KitValidator(RepositorioMock(), produtoRepositoryMock.Object).ShouldHaveValidationErrorFor(kit => kit.Itens, kit);
        }

        [Fact]
        public void KitComItensSemQuantidade_OK()
        {
            var produtoRepositoryMock = new Mock<EntityBaseRepository<Produto>>(new Mock<MEContext>().Object);
            produtoRepositoryMock.Setup(s => s.SelecionarAsNoTracking()).Returns(new List<Produto>() { new Produto { Id = 1 } });

            var kit = new Kit("Kit 1", new List<Item> { new Item { IdProduto = 1, Quantidade = 1 } });
            new KitValidator(RepositorioMock(), produtoRepositoryMock.Object).ShouldNotHaveValidationErrorFor(kit => kit.Itens, kit);
        }

        [Fact]
        public void ProdutosSaoValidos()
        {
            var produtoRepositoryMock = ProdutoTests.ProdutoRepositoryMock(new Produto { Id = 1 });

            var kit = new Kit("Kit 1", new List<Item> { new Item { IdProduto = 2, Quantidade = 1 } });
            new KitValidator(RepositorioMock(), produtoRepositoryMock).ShouldHaveValidationErrorFor(kit => kit.Itens, kit);
        }

        [Fact]
        public void ProdutosSaoValidos_OK()
        {
            var produtoRepositoryMock = ProdutoTests.ProdutoRepositoryMock(new Produto { Id = 1 });

            var kit = new Kit("Kit 1", new List<Item> { new Item { IdProduto = 1, Quantidade = 1 } });

            new KitValidator(RepositorioMock(), produtoRepositoryMock).ShouldNotHaveValidationErrorFor(kit => kit.Itens, kit);
        }
    }
}

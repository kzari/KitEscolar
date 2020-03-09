using FluentValidation.TestHelper;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Validators;
using Kzari.KitEscolar.Infra.Data;
using Kzari.KitEscolar.Infra.Data.DbContexts;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Kzari.KitEscolar.Domain.Test
{
    public class ProdutoTests
    {
        public static EntityBaseRepository<Produto> ProdutoRepositoryMock(Produto produto = null)
        {
            var produtoRepositoryMock = new Mock<EntityBaseRepository<Produto>>(new Mock<MEContext>().Object);
            produtoRepositoryMock
                .Setup(s => s.SelecionarAsNoTracking())
                .Returns(new List<Produto>() { produto ?? new Produto { Id = 1, Nome = "Prod 1" } });
            return produtoRepositoryMock.Object;
        }

        [Fact]
        public void ProdutoNomeVazio()
        {
            new ProdutoValidator(ProdutoRepositoryMock())
                .ShouldHaveValidationErrorFor(kit => kit.Nome, new Produto(nome: ""));
        }

        [Fact]
        public void ProdutoNomeNulo()
        {
            new ProdutoValidator(ProdutoRepositoryMock())
                .ShouldHaveValidationErrorFor(kit => kit.Nome, new Produto(nome: ""));
        }

        [Fact]
        public void ProdutoNomeNuloVazio_OK()
        {
            new ProdutoValidator(ProdutoRepositoryMock())
                .ShouldNotHaveValidationErrorFor(kit => kit.Nome, new Produto("Algum nome"));
        }

        [Fact]
        public void ProdutoMesmoNome()
        {
            string nomeProduto = "Produto 1";
            var kitRepository = ProdutoRepositoryMock(new Produto(nomeProduto));
            new ProdutoValidator(kitRepository).ShouldHaveValidationErrorFor(kit => kit.Nome, new Produto(nomeProduto));
        }

        [Fact]
        public void ProdutoMesmoNome_Ok()
        {
            var kitRepository = ProdutoRepositoryMock(new Produto("Kit 2"));
            new ProdutoValidator(kitRepository).ShouldNotHaveValidationErrorFor(kit => kit.Nome, new Produto("Produto 1"));
        }

        [Fact]
        public void ProdutoComNomeMuitoPequeno()
        {
            new ProdutoValidator(ProdutoRepositoryMock()).ShouldHaveValidationErrorFor(kit => kit.Nome, new Produto("P"));
        }

        [Fact]
        public void ProdutoComNomeMuitoPequeno_OK()
        {
            new ProdutoValidator(ProdutoRepositoryMock()).ShouldNotHaveValidationErrorFor(kit => kit.Nome, new Produto("Produto 1"));
        }

        [Fact]
        public void ProdutoComNomeMuitoGrande()
        {
            new ProdutoValidator(ProdutoRepositoryMock())
                .ShouldHaveValidationErrorFor(kit => kit.Nome, new Produto("dklasj dklas jdklasj dkalsj dasklj dasklj dkalssdaasd sad asdsa "));
        }

        [Fact]
        public void ProdutoComNomeMuitoGrande_Ok()
        {
            new ProdutoValidator(ProdutoRepositoryMock()).ShouldNotHaveValidationErrorFor(kit => kit.Nome, new Produto("Produto 1"));
        }
    }
}

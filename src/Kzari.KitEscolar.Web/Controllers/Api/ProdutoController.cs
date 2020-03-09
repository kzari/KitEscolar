using Kzari.KitEscolar.Application.AppServices.Base;
using Kzari.KitEscolar.Application.Models;
using Kzari.KitEscolar.Domain.Entities;
using Kzari.KitEscolar.Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Kzari.KitEscolar.Web.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IAppServiceBase<Produto, ProdutoModel, ProdutoValidator> _service;
        
        public ProdutoController(IAppServiceBase<Produto, ProdutoModel, ProdutoValidator> appService)
        {
            _service = appService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var produtos = _service.Selecionar(somenteAtivos: true);

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produto = _service.ObterPorId(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Post(ProdutoModel model)
        {
            int id = _service.Criar(model);

            var produto = _service.ObterPorId(id);

            string url = Url.Link("", new { id });

            return Created(url, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProdutoModel model)
        {
            _service.Editar(id, model);

            var produto = _service.ObterPorId(id);

            return Ok(produto);
        }
    }
}

using Kzari.KitEscolar.Application.AppServices.Interfaces;
using Kzari.KitEscolar.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kzari.KitEscolar.Web.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoAppService _appService;
        
        public ProdutoController(IProdutoAppService appService)
        {
            _appService = appService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var produtos = _appService.Selecionar();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produto = _appService.ObterPorId(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Post(ProdutoModel model)
        {
            int id = _appService.Criar(model);

            var produto = _appService.ObterPorId(id);

            string url = Url.Link("", new { id });

            return Created(url, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProdutoModel model)
        {
            _appService.Editar(id, model);

            var produto = _appService.ObterPorId(id);

            return Ok(produto);
        }
    }
}

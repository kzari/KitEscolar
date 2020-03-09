using Kzari.KitEscolar.Application.AppServices.Interfaces;
using Kzari.KitEscolar.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kzari.KitEscolar.Web.Controllers
{
    [Route("api/kit")]
    [ApiController]
    public class KitController : ControllerBase
    {
        private readonly IKitAppService _service;

        public KitController(IKitAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new System.Exception("Alguma coisa");

            var kits = _service.Selecionar<KitExibirModel>(somenteAtivos: true);

            return Ok(kits);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var kit = _service.ObterPorId(id);
            if (kit == null)
                return NotFound();

            return Ok(kit);
        }

        [HttpPost]
        public IActionResult Post(KitModel model)
        {
            int id = _service.Criar(model);

            var kit = _service.ObterPorId(id);

            string url = Url.Link("", new { id });

            return Created(url, kit);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, KitModel model)
        {

            _service.Editar(id, model);

            var kit = _service.ObterPorId(id);

            return Ok(kit);
        }
    }
}
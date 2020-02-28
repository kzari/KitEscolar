using Kzari.MateriaisEscolares.Application.AppServices.Interfaces;
using Kzari.MateriaisEscolares.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kzari.MateriaisEscolares.Web.Controllers
{
    [Route("api/[controller]")]
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
            var kits = _service.ObterTodos();

            return Ok(kits);
        }

        [HttpGet("{id}", Name ="Get")]
        public IActionResult Get(int id)
        {
            var kit = _service.Obter(id);
            if (kit == null)
                return NotFound();

            return Ok(kit);
        }

        [HttpPost]
        public IActionResult Post(KitModel model)
        {
            int id = _service.Criar(model);

            var kit = _service.Obter(id);

            string url = Url.Link("Get", new { id = id });

            return Created(url, kit);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, KitModel model)
        {
            _service.Editar(id, model);

            var kit = _service.Obter(id);

            return Ok(kit);
        }
    }
}
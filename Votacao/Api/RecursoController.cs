using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlterdataVotacao.Interface;
using Microsoft.Extensions.Configuration;
using AlterdataVotacao.Model;
using Microsoft.AspNetCore.Authorization;

namespace AlterdataVotacao.Api
{
    [Produces("application/json")]
    [Route("api/Recurso")]
    public class RecursoController : Controller
    {

        private readonly RecursoRepositorio recursoRepository;

        public RecursoController(IConfiguration configuration)
        {
            recursoRepository = new RecursoRepositorio(configuration);
        }

        // GET: api/Recurso
        [HttpGet("ListarTodos", Name = "ListarTodos")]
        public IEnumerable<Recurso> ListarTodos()
        {
            return recursoRepository.ListarRecursos().ToList();
        }

        // POST: api/Recurso
        [HttpPost("AddRecurso")]
        public IActionResult AddRecurso([FromBody] Recurso Rec)
        {
            if (Rec == null)
            {
                return BadRequest();
            }

            recursoRepository.AddRecurso(Rec);
            return CreatedAtRoute("ListarTodos", new { id = Rec.Id }, Rec);
        }

        // PUT: api/Recurso/5
        [HttpPut("AttRecurso", Name = "AttRecurso")]
        public IActionResult AttRecurso([FromBody] Recurso Rec)
        {
            if (Rec == null)
            {
                return BadRequest();
            }

            var item = recursoRepository.BuscarPorId(Rec.Id);
            if (item == null)
            {
                return NotFound();
            }

            item.Descricao = Rec.Descricao;
            recursoRepository.AttRecurso(item);
            return new NoContentResult();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("DeleteRecurso/{id}", Name = "DeleteRecurso")]
        public IActionResult DeleteRecurso(int id)
        {
            var item = recursoRepository.BuscarPorId(id);
            if (item == null)
            {
                return NotFound();
            }
            recursoRepository.DelRecurso(id);
            return new NoContentResult();
        }
    }
}

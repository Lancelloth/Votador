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
    [Route("api/Resultado")]
    public class ResultadoController : Controller
    {
        private readonly ResultadoRepositorio resultadoRepository;

        public ResultadoController(IConfiguration configuration)
        {
            resultadoRepository = new ResultadoRepositorio(configuration);
        }
        // GET: api/Resultado

        [HttpGet("BuscarTodosResultados", Name = "BuscarTodosResultados")]
        public IEnumerable<ResultadoVotacaoFun> BuscarTodosResultados()
        {
            return resultadoRepository.BuscarTodosResultados().ToList();
        }

        [HttpGet("BuscarPorFuncionario", Name = "BuscarPorFuncionario")]
        public IEnumerable<ResultadoVotacaoFun> BuscarPorFuncionario()
        {
            return resultadoRepository.BuscarPorFuncionario().ToList();
        }
        
        // POST: api/Resultado
        [HttpPost("AdicionarVoto", Name = "AdicionarVoto")]
        public IActionResult AdicionarVoto([FromBody]Resultado Res)
        {
            if (Res == null)
            {
                return BadRequest();
            } 
            resultadoRepository.AddResultado(Res);
            return CreatedAtRoute("BuscarTodosResultados", Res);
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("DeleteResultado/{id}", Name = "DeleteResultado")]
        public IActionResult DeleteResultado(int id)
        {
            resultadoRepository.DelResultado(id);
            return new NoContentResult();
        }
    }
}

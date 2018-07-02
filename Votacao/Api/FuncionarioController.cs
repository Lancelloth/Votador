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
using System.Security.Cryptography;

namespace AlterdataVotacao.Api
{
    [Produces("application/json")]
    [Route("api/Funcionario")]
    public class FuncionarioController : Controller
    {
        private readonly FuncionarioRepositorio funcionarioRepository;

        public FuncionarioController(IConfiguration configuration)
        {
            funcionarioRepository = new FuncionarioRepositorio(configuration);
        }

        // GET api/Funcionario/BuscarTodos
        [HttpGet("BuscarTodos")]
        public IEnumerable<Funcionario> BuscarTodos()
        {
            return funcionarioRepository.BuscarTodos().ToList();
        }

        [HttpGet("BuscarPorId/{id}", Name = "BuscarPorId")]
        public IActionResult BuscarPorId(int id)
        {
            var item = funcionarioRepository.BuscarPorFuncionario(id);
            return new ObjectResult(item);
        }

        [AllowAnonymous]
        [HttpPost("Login/{Email}/{Senha}", Name = "Login")]
        public IActionResult Login(string Email, string Senha)
        {
            var item = funcionarioRepository.BuscarFuncionarioEmail(Email);
            MD5 md5Hash = MD5.Create();
            if (item == null)
            {
                return Ok("");
            }            
            else if(Hash.VerifyMd5Hash(md5Hash, Senha, item.Senha) == true)
            {
                return Accepted(item);
            }
            else
            {
                return BadRequest();
            }
            
        }

        // POST: api/Funcionario/AddFuncionario
        [HttpPost("AddFuncionario")]
        public IActionResult AddFuncionario([FromBody] Funcionario Func)
        {
            if(Func == null)
            {
                return BadRequest();
            }
            MD5 md5Hash = MD5.Create();
            Func.Senha = Hash.GetMd5Hash(md5Hash, Func.Senha);

            funcionarioRepository.AddFuncionario(Func);
            return CreatedAtRoute("BuscarPorId", new { id = Func.Id }, Func);

        }

        // PUT: api/Funcionario/EditFuncionario 
        [HttpPut("AttFuncionario", Name ="AttFuncionario")]
        public IActionResult AttFuncionario(int id, [FromBody] Funcionario Func)
        {
            if(Func == null)
            {
                return BadRequest();
            }

            var item = funcionarioRepository.BuscarPorFuncionario(Func.Id);
            if(item == null)
            {
                return NotFound();
            }

            item.Nome = Func.Nome;
            item.Email = Func.Email;
            item.Senha = Func.Senha;
            item.Role = Func.Role;
            funcionarioRepository.AttFuncionario(item);
            return new NoContentResult();
        }

        // Delete  api/Funcionario/DeleteFuncionario
        [HttpDelete("DeleteFuncionario/{id}")]
        public IActionResult Delete(int id)
        {
            var item = funcionarioRepository.BuscarPorFuncionario(id);
            if (item == null)
            {
                return NotFound();
            }
            funcionarioRepository.DelFuncionario(id);
            return new NoContentResult();

        }
        
    }
}

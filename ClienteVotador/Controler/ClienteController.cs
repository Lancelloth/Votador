using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClienteVotador.Model;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClienteVotador.Controler
{
    public class ClienteController : Controller
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("Principal")]
        public IActionResult Principal()
        {
            return View("Principal");
        }
        [Route("AddVoto")]
        public IActionResult AddVoto()
        {
            ViewBag.Recurso = new SelectList(BuscarRecursos(), "Id", "Descricao");            
            return View("AddVoto");
        }

        [Route("ResultadoTotal")]
        public IActionResult ResultadoTotal()
        {
            ViewBag.TotalVotos = new SelectList(BuscarResultadoTotal(),"NomeFuncionario", "DataHora", "Descricao");
            return View("ResultadoTotal");
        }

        [Route("ResultadoPorVotacao")]
        public IActionResult ResultadoPorVotacao()
        {
            ViewBag.TotalVotos = new SelectList(BuscarResultadoVotacao(), "QtdVotos", "Descricao");
            return View("ResultadoPorVotacao");
        }

        public string BaseUrlFuncionario
        {
            get
            {
                return "http://localhost:60000/api/Funcionario";
            }
        }

        public string BaseUrlResultado
        {
            get
            {
                return "http://localhost:60000/api/Resultado";
            }
        }

        public string BaseUrlRecurso
        {
            get
            {
                return "http://localhost:60000/api/Recurso";
            }
        }

        public IActionResult Login(string Email, string Password)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseUrlFuncionario + "/Login/"+ Email +"/"+ Password);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            Funcionario funcionario = new Funcionario();

            string l = response.Content.ReadAsStringAsync().Result;
            funcionario = JsonConvert.DeserializeObject<Funcionario>(l);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    TempData["Funcionario"] = funcionario.Id;
                }
                catch(Exception ex)
                {
                    TempData["Funcionario"] = 1;
                }
                return View("Principal");
            }
            else
            {
                return BadRequest();
            }

        }

        public List<Funcionario> BuscarTodos()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrlFuncionario + "/BuscarTodos");

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<Funcionario> funcionarios = new List<Funcionario>();


            JArray func = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            foreach (var fun in func)
            {
                funcionarios.Add(new Funcionario() { Nome = fun["nome"].ToString(), Email = fun["email"].ToString() });
            }
            return funcionarios;
        }

        public List<Recurso> BuscarRecursos()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrlRecurso + "/ListarTodos");

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<Recurso> recursos = new List<Recurso>();


            JArray recur = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            foreach (var rec in recur)
            {
                recursos.Add(new Recurso() { Id = Convert.ToInt32(rec["id"].ToString()), Descricao = rec["descricao"].ToString() });
            }

            if(response.IsSuccessStatusCode){
                return recursos;
            }
            else
            {
                return null;
            }

        }

        public List<ResultadoVotacaoFun> BuscarResultadoTotal()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrlResultado + "/BuscarPorFuncionario");

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<ResultadoVotacaoFun> resultado = new List<ResultadoVotacaoFun>();


            JArray aRes = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            foreach (var res in aRes)
            {
                resultado.Add(new ResultadoVotacaoFun() { NomeFuncionario = res["nomeFuncionario"].ToString(), DataHora = Convert.ToDateTime(res["dataHora"]), Descricao = res["Descricao"].ToString()});
            }
            return resultado;
        }

        public List<ResultadoVotacaoFun> BuscarResultadoVotacao()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrlResultado + "/BuscarTodosResultados");

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<ResultadoVotacaoFun> resultado = new List<ResultadoVotacaoFun>();


            JArray aRes = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            foreach (var res in aRes)
            {
                resultado.Add(new ResultadoVotacaoFun() { QtdVotos = Convert.ToInt32(res["qtdVotos"].ToString()), Descricao = res["Descricao"].ToString() });
            }
            return resultado;
        }

    }
}
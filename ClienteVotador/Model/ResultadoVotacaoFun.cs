using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteVotador.Model
{
    public class ResultadoVotacaoFun
    {
        public string NomeFuncionario { get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; }
        public int QtdVotos { get; set; }
    }
}

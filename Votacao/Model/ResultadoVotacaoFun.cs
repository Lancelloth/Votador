using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotacao.Model
{
    public class ResultadoVotacaoFun : BaseEntity
    {
        public string NomeFuncionario { get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; }
        public int QtdVotos { get; set; }
    }
}

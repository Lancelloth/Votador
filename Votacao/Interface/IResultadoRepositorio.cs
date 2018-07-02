using AlterdataVotacao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotacao.Interface
{
    public interface IResultadoRepositorio<T> where T : BaseEntity
    {
        IEnumerable <ResultadoVotacaoFun> BuscarTodosResultados();
        IEnumerable <ResultadoVotacaoFun> BuscarPorFuncionario();
        void AddResultado(Resultado Res);
        void DelResultado(int Id);
    }
}

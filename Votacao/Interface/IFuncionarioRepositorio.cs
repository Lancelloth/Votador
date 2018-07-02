using AlterdataVotacao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotacao.Interface
{
    public interface IFuncionarioRepositorio<T> where T : BaseEntity
    {
        //Buscas no banco 
        Funcionario BuscarPorFuncionario(int IdFuncionario);
        IEnumerable<Funcionario> BuscarTodos();
        Funcionario BuscarFuncionarioEmail(string EmailFuncionario);

        //Adição de objetos
        void AddFuncionario(Funcionario Func);

        //Atualizar ou alterar registros
        void AttFuncionario(Funcionario Func);

        //Deletar Cadastros, recursos e/ou votação
        void DelFuncionario(int IdFuncionario);

    }
}

using AlterdataVotacao.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotacao.Interface
{
    public class ResultadoRepositorio : IResultadoRepositorio <Resultado>
    {
        private string connectionString;
        public ResultadoRepositorio(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("ConnectionString:Conn");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void AddResultado(Resultado Res)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(
                    "INSERT INTO resultado " +
                    "(id_funcionario,id_recurso, comentario, data_hora) " +
                    "VALUES(@IdFuncionario,@IdRecurso,@Comentario, @DataHora)", Res);
            }
        }

        public IEnumerable<ResultadoVotacaoFun> BuscarPorFuncionario()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ResultadoVotacaoFun>(
                    "SELECT funcionario.nome as NomeFuncionario, resultado.data_hora as DataHora, recurso.descricao as Descricao " +
                    "FROM resultado " +
                    "inner join funcionario on " +
                    "funcionario.id = resultado.id_funcionario " +
                    "inner join recurso on " +
                    "recurso.id = resultado.id_recurso");
            }
        }

        public IEnumerable<ResultadoVotacaoFun> BuscarTodosResultados()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ResultadoVotacaoFun>(
                    "SELECT recurso.descricao as Descricao, " +
                    "count(1) as QtdVotos " +
                    "FROM resultado " +
                    "inner join funcionario on " +
                    "   funcionario.id = resultado.id_funcionario " +
                    "inner join recurso on " +
                    "   recurso.id = resultado.id_recurso " +
                    "group by recurso.descricao " +
                    "order by QtdVotos desc");
            }
        }

        public void DelResultado(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM resultado " +
                    " WHERE id = @Id", new { Id = Id });
            }
        }
    }
}

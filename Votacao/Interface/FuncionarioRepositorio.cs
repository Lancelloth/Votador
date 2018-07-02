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
    public class FuncionarioRepositorio : IFuncionarioRepositorio <Funcionario>
    {
        private string connectionString;
        public FuncionarioRepositorio(IConfiguration configuration)
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

        public void AddFuncionario(Funcionario Func)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO funcionario " +
                    "(nome,email,senha, role) " +
                    "VALUES(@Nome,@Email,@Senha, @Role)", Func);
            }
        }

        public void AttFuncionario(Funcionario Func)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE funcionario " +
                    "SET nome = @Nome, email = @Email, senha = @Senha, role = @Role" +
                    "WHERE id = @Id", Func);
            }
        }

        public Funcionario BuscarFuncionarioEmail(string EmailFuncionario)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Funcionario>("SELECT * FROM funcionario " +
                    "WHERE email = @EmailFuncionario", new {EmailFuncionario = EmailFuncionario });
            }
        }

        public Funcionario BuscarPorFuncionario(int IdFuncionario)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Funcionario>("SELECT * FROM funcionario " +
                    "WHERE id = @Id", new { Id = IdFuncionario });
            }
        }

        public IEnumerable<Funcionario> BuscarTodos()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Funcionario>("SELECT id, nome, email, role, id FROM funcionario");
            }
        }

        public void DelFuncionario(int IdFuncionario)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM funcionario " +
                    " WHERE id = @Id", new { Id = IdFuncionario });
            }
        }

    }
}

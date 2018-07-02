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
    public class RecursoRepositorio : IRecursoRepositorio <Recurso>
    {
        private string connectionString;
        public RecursoRepositorio(IConfiguration configuration)
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

        public void AddRecurso(Recurso Rec)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(
                    "INSERT INTO recurso " +
                    "(descricao) " +
                    "VALUES(@Descricao)", Rec);
            }
        }

        public void AttRecurso(Recurso Rec)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query(
                    "UPDATE recurso " +
                    "SET descricao = @Descricao " +
                    "WHERE id = @Id", Rec);
            }
        }

        public Recurso BuscarPorId(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Recurso>(
                    "SELECT id, descricao " +
                    "FROM recurso " +
                    "WHERE id = @Id", new { Id = id }).FirstOrDefault(); ;
            }
        }

        public void DelRecurso(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(
                    "DELETE FROM recurso " +
                    " WHERE id = @Id", new { Id = Id });
            }
        }

        public IEnumerable<Recurso> ListarRecursos()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Recurso>("SELECT id, descricao FROM recurso");
            }
        }
    }
}

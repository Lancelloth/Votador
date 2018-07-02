using AlterdataVotacao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotacao.Interface
{
    public interface IRecursoRepositorio<T> where T : BaseEntity
    {
        IEnumerable<Recurso> ListarRecursos();
        Recurso BuscarPorId(int id);
        void AddRecurso(Recurso Rec);
        void AttRecurso(Recurso Rec);
        void DelRecurso(int Id);
    }
}

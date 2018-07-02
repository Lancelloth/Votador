using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlterdataVotacao.Model
{
    public class Resultado : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public int IdFuncionario { get; set; }
        public int IdRecurso { get; set; }
        public string Comentario { get; set; }
        public DateTime DataHora { get; set; }
    }
}

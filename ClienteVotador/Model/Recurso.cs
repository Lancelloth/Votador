using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteVotador.Model
{
    public class Recurso
    {
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}

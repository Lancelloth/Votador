using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AlterdataVotacao.Model
{
    public class Funcionario : BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        [Required(ErrorMessage = "Insira a senha")]
        public string Senha { get; set; }

    }

    public class TokenConfigurations : BaseEntity
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
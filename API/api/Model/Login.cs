using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trecker.API.api.Model
{
    internal class Login
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public string SenhaHash { get; set; }
    }
}

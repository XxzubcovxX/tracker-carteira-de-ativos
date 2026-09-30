using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using trecker.API.api.Data;
using trecker.func.login;

namespace trecker.API.api.Service
{
    public class LoginService
    {

        public void CriarUsuarioPadrao()
        {
            using (var db = new Data.AppDbContext())
            {
                if (!db.Logins.Any())
                {
                    var usuarioPadrao = new Model.Login
                    {
                        NomeUsuario = "admin",
                        SenhaHash = GerarHash("admin123")
                    };
                    db.Logins.Add(usuarioPadrao);
                    db.SaveChanges();
                }
            }
        }
        public bool ValidarLogin(string username, string senha)
        {
            using (var db = new AppDbContext())
            {
                string hashDigitado = GerarHash(senha);

                return db.Logins.Any(u =>
                    u.NomeUsuario.ToLower() == username.ToLower() &&
                    u.SenhaHash == hashDigitado);
            }
        }
        public bool CadastrarUsuario(string username, string senha)
        {
            using (var db = new AppDbContext())
            {
                // Verifica se já existe um usuário com esse mesmo nome
                bool existe = db.Logins.Any(u => u.NomeUsuario.ToLower() == username.ToLower());
                if (existe) return false;

                var novoUsuario = new Model.Login
                {
                    NomeUsuario = username.Trim(),
                    SenhaHash = GerarHash(senha.Trim())
                };

                db.Logins.Add(novoUsuario);
                db.SaveChanges();
                return true;
            }
        }
        private string GerarHash(string texto)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}

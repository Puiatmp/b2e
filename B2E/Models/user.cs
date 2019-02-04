using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace B2E.Models
{
    public class userParams
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Pass { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public class autenticaParams
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Pass { get; set; }
    }

    public class user
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class userRetorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }

    public class autenticaRetorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public user User { get; set; }
    }

    public class userEstatisticasRetorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<url> Urls { get; set; }
    }
}
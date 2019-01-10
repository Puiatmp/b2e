using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace B2E.Models
{
    public class userParams
    {
        [Required]
        public string User { get; set; }
    }

    public class user
    {
        public string Id { get; set; }
        public string User { get; set; }
    }

    public class userRetorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }

    public class userEstatisticasRetorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<url> Urls { get; set; }
    }
}
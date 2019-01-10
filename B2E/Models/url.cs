using System.ComponentModel.DataAnnotations;

namespace B2E.Models
{
    public class urlParams
    {
        [Required]
        public int User { get; set; }
        [Required]
        public string Url { get; set; }
    }

    public class url
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Url { get; set; }
        public string Shorturl { get; set; }
        public int Hits { get; set; }
    }

    public class urlRetorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }

    public class urlEstatisticasRetorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public url Url { get; set; }
    }
}
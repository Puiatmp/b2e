using System.Collections.Generic;

namespace B2E.Models
{
    public class stats
    {
        public int Hits { get; set; }
        public int UrlCount { get; set; }
        public List<url> TopUrls { get; set; }
    }

    public class statsRetorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public stats Stats { get; set; }
    }
}
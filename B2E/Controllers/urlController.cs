using B2E.Models;
using B2E.Business;
using System.Web.Http;

namespace B2E.Controllers
{
    /// <summary>
    /// Serviço de usuários.
    /// </summary>
    [RoutePrefix("api")]
    public class urlController : ApiController
    {
        /// <summary>
        /// Cadastra uma nova url no sistema 
        /// Requisição:{"user": 1, "url": "http://www.b2egroup.com.br"} 
        /// </summary>
        [HttpPost]
        [Route("url")]
        public urlRetorno IncluirUrl(urlParams parametros)
        {
            //Incluir no log o start de execução (data e hora de início) e o request do processo.
            urlRetorno retorno = new urlRetorno();
            urlBusiness urlBusiness = new urlBusiness();
            retorno.Sucesso = false;
            if (parametros.User == 0)
                retorno.Mensagem = "O campo User não pode ficar em branco.";
            else if (parametros.Url == "" || parametros.Url == null)
                retorno.Mensagem = "O campo Url não pode ficar em branco.";
            else if (parametros.Url.Substring(0,7).ToString().ToUpper() != "HTTP://" && parametros.Url.Substring(0, 8).ToString().ToUpper() != "HTTPS://")
                retorno.Mensagem = "O campo Url deve iniciar com 'http://' ou 'https://'.";
            else
                retorno = urlBusiness.CriarUrl(parametros.User, parametros.Url);
            //Incluir no log o response do processo e o tempo de execução (Elapsed time).
            return retorno;
        }

        /// <summary>
        /// Apaga uma url.
        /// </summary>
        [HttpDelete]
        [Route("url")]
        public urlRetorno ApagarUrl(int id)
        {
            //Incluir no log o start de execução (data e hora de início) e o request do processo.
            urlRetorno retorno = new urlRetorno();
            urlBusiness urlBusiness = new urlBusiness();
            retorno.Sucesso = false;
            if (id == 0)
                retorno.Mensagem = "O campo ID não pode ficar em branco.";
            else
                retorno = urlBusiness.ApagarUrl(id);
            //Incluir no log o response do processo e o tempo de execução (Elapsed time).
            return retorno;
        }

        /// <summary>
        /// Retorna estatísticas das urls de um usuário. O resultado é o mesmo que GET /stats mas 
        /// com o escopo dentro de um usuário.
        /// Caso o usuário não exista o retorno deverá ser com código 404 Not Found.
        /// </summary>
        [HttpGet]
        [Route("url")]
        public urlEstatisticasRetorno EstatisticasUrl(int id)
        {
            //Incluir no log o start de execução (data e hora de início) e o request do processo.
            urlEstatisticasRetorno retorno = new urlEstatisticasRetorno();
            urlBusiness urlBusiness = new urlBusiness();
            retorno.Sucesso = false;
            if (id == 0)
                retorno.Mensagem = "O campo ID não pode ficar em branco.";
            else
                retorno = urlBusiness.EstatisticasUrl(id);
            //Incluir no log o response do processo e o tempo de execução (Elapsed time).
            return retorno;
        }
    }
}
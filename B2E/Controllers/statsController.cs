using B2E.Models;
using B2E.Business;
using System.Web.Http;

namespace B2E.Controllers
{
    /// <summary>
    /// Serviços de status geral.
    /// </summary>
    [RoutePrefix("api")]
    public class statsController : ApiController
    {
        /// <summary>
        /// Retorna estatísticas globais do sistema.
        /// </summary>
        [HttpGet]
        [Route("stats")]
        public statsRetorno EstatisticasUrl()
        {
            //Incluir no log o start de execução (data e hora de início) e o request do processo.
            statsRetorno retorno = new statsRetorno();
            statsBusiness statsBusiness = new statsBusiness();
            retorno.Sucesso = false;
            retorno = statsBusiness.Estatisticas();
            //Incluir no log o response do processo e o tempo de execução (Elapsed time).
            return retorno;
        }
    }
}
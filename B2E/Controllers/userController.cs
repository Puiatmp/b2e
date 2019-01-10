using B2E.Models;
using B2E.Business;
using System.Web.Http;

namespace B2E.Controllers
{
    /// <summary>
    /// Serviço de usuários.
    /// </summary>
    [RoutePrefix("api")]
    public class userController : ApiController
    {
        /// <summary>
        /// Cria um usuário. O conteúdo do request deverá ser com código 201 Created e retornar um 
        /// objeto com o conteúdo no seguinte formato. Caso já exista um usuário com o mesmo id
        /// retornar código 409 Conflict
        /// Requisição: { "id": "jibao" }
        /// </summary>
        [HttpPost]
        [Route("user")]
        public userRetorno IncluirUsuario(userParams parametros)
        {
            //Incluir no log o start de execução (data e hora de início) e o request do processo.
            userRetorno retorno = new userRetorno();
            userBusiness userBusiness = new userBusiness();
            retorno.Sucesso = false;
            if (parametros.User == "" || parametros.User == null)
                retorno.Mensagem = "O campo Usuário não pode ficar em branco.";
            else
                retorno = userBusiness.CriarUsuario(parametros.User);
            //Incluir no log o response do processo e o tempo de execução (Elapsed time).
            return retorno;
        }

        /// <summary>
        /// Apaga um usuário.
        /// </summary>
        [HttpDelete]
        [Route("user")]
        public userRetorno ApagarUsuario(int id)
        {
            //Incluir no log o start de execução (data e hora de início) e o request do processo.
            userRetorno retorno = new userRetorno();
            userBusiness userBusiness = new userBusiness();
            retorno.Sucesso = false;
            if (id == 0)
                retorno.Mensagem = "O campo ID não pode ficar em branco.";
            else
                retorno = userBusiness.ApagarUsuario(id);
            //Incluir no log o response do processo e o tempo de execução (Elapsed time).
            return retorno;
        }

        /// <summary>
        /// Retorna estatísticas das urls de um usuário. O resultado é o mesmo que GET /stats mas 
        /// com o escopo dentro de um usuário.
        /// Caso o usuário não exista o retorno deverá ser com código 404 Not Found.
        /// </summary>
        [HttpGet]
        [Route("user")]
        public userEstatisticasRetorno EstatisticasUsuario(int id)
        {
            //Incluir no log o start de execução (data e hora de início) e o request do processo.
            userEstatisticasRetorno retorno = new userEstatisticasRetorno();
            userBusiness userBusiness = new userBusiness();
            retorno.Sucesso = false;
            if (id == 0)
                retorno.Mensagem = "O campo ID não pode ficar em branco.";
            else
                retorno = userBusiness.EstatisticasUsuario(id);
            //Incluir no log o response do processo e o tempo de execução (Elapsed time).
            return retorno;
        }
    }
}
using B2E.Models;
using B2E.Business;
using System.Web.Http;
using B2E.Data;

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
            else if (parametros.Pass == "" || parametros.Pass == null)
                retorno.Mensagem = "O campo Password não pode ficar em branco.";
            else if (parametros.Name == "" || parametros.Name == null)
                retorno.Mensagem = "O campo Nome não pode ficar em branco.";
            else if (parametros.Email == "" || parametros.Email == null)
                retorno.Mensagem = "O campo E-Mail não pode ficar em branco.";
            else if (!utilData.Valida_EMail(parametros.Email))
                retorno.Mensagem = "O campo E-Mail contém um valor inválido.";
            else
                retorno = userBusiness.CriarUsuario(parametros.User, parametros.Pass, parametros.Name, parametros.Email);
            //Incluir no log o response do processo e o tempo de execução (Elapsed time).
            return retorno;
        }

        [HttpPost]
        [Route("autenticar")]
        public autenticaRetorno AutenticarUsuario(autenticaParams parametros)
        {
            //Incluir no log o start de execução (data e hora de início) e o request do processo.
            autenticaRetorno retorno = new autenticaRetorno();
            userBusiness userBusiness = new userBusiness();
            retorno.Sucesso = false;
            if (parametros.User == "" || parametros.User == null)
                retorno.Mensagem = "O campo Usuário não pode ficar em branco.";
            else if (parametros.Pass == "" || parametros.Pass == null)
                retorno.Mensagem = "O campo Password não pode ficar em branco.";
            else
                retorno = userBusiness.AutenticarUsuario(parametros.User, parametros.Pass);
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
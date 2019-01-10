using B2E.Business;
using System.Net.Http;
using System.Web.Http;

namespace B2E.Controllers
{
    /// <summary>
    /// Redirecionamento de redirecionamento de links curtos - B2e.
    /// Exemplo: api/hit?id=1
    /// </summary>
    [RoutePrefix("api")]
    public class hitController : ApiController
    {
        /// <summary>
        /// Redireciona para o link, pelo id, se existir.
        /// </summary>
        public HttpResponseMessage Get(int id)
        {
            string resposta = @"<!DOCTYPE html>
            <html><head><meta charset='UTF-8'></head><body>                 
            <script>
            setTimeout(function() {
                window.location.href = '@link';
            }, 1000);
            </script>
            </body></html>";
            string link = "";
            hitBusiness hitBusiness = new hitBusiness();
            if (id != 0)
            {
                link = hitBusiness.Link(id);
                resposta = resposta.Replace("@link", link);
            }
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(resposta, System.Text.Encoding.UTF8, "text/html");
            return response;
        }
    }
}
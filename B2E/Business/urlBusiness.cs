using System;
using B2E.Data;
using B2E.Models;

namespace B2E.Business
{
    public class urlBusiness : utilData
    {
        public urlRetorno CriarUrl(int user, string url)
        {
            urlData urlData = new urlData();
            urlRetorno retorno = new urlRetorno();
            try
            {
                retorno.Sucesso = urlData.CriarUrl(user, url, out string Mensagem);
                retorno.Mensagem = Mensagem;
            }
            catch (Exception ex)
            {
                Tratamento(ex.HResult, ex.Message, ex.Source, "urlBusiness.CriarUrl(" + user + "," + url + ")", ex.StackTrace, false, utilData.DB);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao criar url.";
            }
            return retorno;
        }

        public urlRetorno ApagarUrl(int id)
        {
            urlData urlData = new urlData();
            urlRetorno retorno = new urlRetorno();
            try
            {
                retorno.Sucesso = urlData.ApagarUrl(id, out string Mensagem);
                retorno.Mensagem = Mensagem;
            }
            catch (Exception ex)
            {
                Tratamento(ex.HResult, ex.Message, ex.Source, "urlBusiness.ApagarUrl(" + id + ")", ex.StackTrace, false, utilData.DB);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao apagar url.";
            }
            return retorno;
        }

        public urlEstatisticasRetorno EstatisticasUrl(int id)
        {
            urlData urlData = new urlData();
            urlEstatisticasRetorno retorno = new urlEstatisticasRetorno();
            try
            {
                retorno.Sucesso = true;
                retorno.Url = urlData.EstatisticasUrl(id);
                if (retorno.Url.Id != 0)
                    retorno.Mensagem = "Url encontrada com sucesso.";
                else
                    retorno.Mensagem = "Url não encontrada.";
            }
            catch (Exception ex)
            {
                Tratamento(ex.HResult, ex.Message, ex.Source, "urlBusiness.EstatisticasUrl(" + id + ")", ex.StackTrace, false, utilData.DB);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao consultar url.";
            }
            return retorno;
        }
    }
}
using System;
using B2E.Data;
using B2E.Models;

namespace B2E.Business
{
    public class userBusiness : utilData
    {
        public userRetorno CriarUsuario(string user)
        {
            userData usuarioData = new userData();
            userRetorno retorno = new userRetorno();
            try
            {
                retorno.Sucesso = usuarioData.CriarUsuario(user, out string Mensagem);
                retorno.Mensagem = Mensagem;
            }
            catch (Exception ex)
            {
                Tratamento(ex.HResult, ex.Message, ex.Source, "userBusiness.CriarUsuario(" + user + ")", ex.StackTrace, false, utilData.DB);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao criar usuario.";
            }
            return retorno;
        }

        public userRetorno ApagarUsuario(int id)
        {
            userData usuarioData = new userData();
            userRetorno retorno = new userRetorno();
            try
            {
                retorno.Sucesso = usuarioData.ApagarUsuario(id, out string Mensagem);
                retorno.Mensagem = Mensagem;
            }
            catch (Exception ex)
            {
                Tratamento(ex.HResult, ex.Message, ex.Source, "userBusiness.ApagarUsuario(" + id + ")", ex.StackTrace, false, utilData.DB);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao apagar usuario.";
            }
            return retorno;
        }

        public userEstatisticasRetorno EstatisticasUsuario(int id)
        {
            userData usuarioData = new userData();
            userEstatisticasRetorno retorno = new userEstatisticasRetorno();
            try
            {
                retorno.Sucesso = true;
                retorno.Urls = usuarioData.EstatisticasUsuario(id);
                retorno.Mensagem = "Total de Url´s: " + retorno.Urls.Count;
            }
            catch (Exception ex)
            {
                Tratamento(ex.HResult, ex.Message, ex.Source, "userBusiness.EstatisticasUsuario(" + id + ")", ex.StackTrace, false, utilData.DB);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao consultar estatísticas do usuario.";
            }
            return retorno;
        }
    }
}
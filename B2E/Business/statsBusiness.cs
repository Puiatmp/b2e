using System;
using B2E.Data;
using B2E.Models;

namespace B2E.Business
{
    public class statsBusiness : utilData
    {
        public statsRetorno Estatisticas()
        {
            statsData statsData = new statsData();
            statsRetorno retorno = new statsRetorno();
            try
            {
                retorno.Sucesso = true;
                retorno.Stats = statsData.Estatisticas();
                if (retorno.Stats.Hits > 0)
                    retorno.Mensagem = "Estatísitcas encontradas com sucesso.";
                else
                    retorno.Mensagem = "Estatísitcas não encontradas.";
            }
            catch (Exception ex)
            {
                Tratamento(ex.HResult, ex.Message, ex.Source, "statsBusiness.Estatisticas()", ex.StackTrace, false, utilData.DB);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao consultar estatísticas.";
            }
            return retorno;
        }
    }
}
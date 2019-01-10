using System;
using System.Data;

namespace B2E.Data
{
    public class hitData : utilData
    {
        internal string Link(int id)
        {
            string Retorno = "";
            try
            {
                string qryUrl = @"SELECT url FROM tb_urls WHERE id = " + id;
                DataTable reader = RS(qryUrl);
                if (reader.Rows.Count > 0)
                {
                    Executar("UPDATE tb_urls SET hits = hits + 1 WHERE id =" + id);

                    Retorno = reader.Rows[0]["url"].ToString();
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return Retorno;
        }
    }
}
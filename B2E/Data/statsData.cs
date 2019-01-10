using System;
using B2E.Models;
using System.Data;
using System.Collections.Generic;

namespace B2E.Data
{
    public class statsData : utilData
    {
        internal stats Estatisticas()
        {
            stats Retorno = new stats();
            try
            {
                string qryEstatisticas = @"SELECT COUNT(id) AS urls, SUM(hits) AS total FROM tb_urls";
                DataTable reader = RS(qryEstatisticas);
                if (reader.Rows.Count > 0)
                {
                    Retorno.UrlCount = Convert.ToInt16(reader.Rows[0]["urls"]);
                    Retorno.Hits = Convert.ToInt16(reader.Rows[0]["total"]);
                    Retorno.TopUrls = new List<url>();
                    qryEstatisticas = @"SELECT u.id, r.user AS user, url, shorturl, hits FROM tb_urls u INNER JOIN tb_users r ON u.user = r.id ORDER BY hits DESC, u.id LIMIT 10";
                    reader = RS(qryEstatisticas);
                    foreach (DataRow row in reader.Rows)
                    {
                        url url = new url();
                        url.Id = Convert.ToInt32(row["id"].ToString());
                        url.User = row["user"].ToString();
                        url.Url = row["url"].ToString();
                        url.Shorturl = row["shorturl"].ToString();
                        url.Hits = Convert.ToInt32(row["hits"].ToString());
                        Retorno.TopUrls.Add(url);
                    }
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
using System;
using System.Net;
using B2E.Models;
using System.Data;

namespace B2E.Data
{
    public class urlData : utilData
    {
        internal bool CriarUrl(int user, string url, out string mensagem)
        {
            bool Retorno = false;
            try
            {
                string qryUsuario = @"SELECT id FROM tb_users WHERE id = " + user;
                DataTable reader = RS(qryUsuario);
                if (reader.Rows.Count > 0)
                {
                    string qryUrl = @"SELECT id FROM tb_urls WHERE url = " + Aspas(url, false);
                    reader = RS(qryUrl);
                    if (reader.Rows.Count > 0)
                    {
                        Retorno = false;
                        mensagem = "Essa url já existe.";
                    }
                    else
                    {
                        string curto = EncurtadorUrl(new Uri(url));
                        if (Executar("INSERT INTO tb_urls (user, url, shorturl, hits) VALUES (" + user + "," + Aspas(url, false) + ", " + Aspas(curto, false) + ", 0)") > 0)
                        {
                            Retorno = true;
                            mensagem = "Url incluída com sucesso.";
                        }
                        else
                        {
                            Retorno = false;
                            mensagem = "Erro ao inserir url.";
                        }
                    }
                }
                else
                {
                    Retorno = false;
                    mensagem = "Usuário não encontrado. A url não foi criada.";
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return Retorno;
        }

        internal bool ApagarUrl(int id, out string mensagem)
        {
            bool Retorno = false;
            try
            {
                string qryUrl = @"DELETE FROM tb_urls WHERE id = " + id;
                if (Executar(qryUrl) > 0)
                {
                    Retorno = true;
                    mensagem = "Url excluída com sucesso.";
                }
                else
                {
                    Retorno = false;
                    mensagem = "Erro ao excluir url.";
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return Retorno;
        }

        internal url EstatisticasUrl(int id)
        {
            url Url = new url();
            try
            {
                string qryUrl = @"SELECT id, user, url, shorturl, hits FROM tb_urls WHERE id = " + id;
                DataTable reader = RS(qryUrl);
                if (reader.Rows.Count > 0)
                {
                    Url.Id = Convert.ToInt16(reader.Rows[0]["id"]);
                    Url.User = reader.Rows[0]["user"].ToString();
                    Url.Url = reader.Rows[0]["url"].ToString();
                    Url.Shorturl = reader.Rows[0]["shorturl"].ToString();
                    Url.Hits = Convert.ToInt16(reader.Rows[0]["hits"]);
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return Url;
        }

        public static string EncurtadorUrl(string url)
        {
            try
            {
                string urlMigreMe = string.Format("http://tinyurl.com/api-create.php?url={0}", url);
                var client = new WebClient();
                string response = client.DownloadString(urlMigreMe);
                client.Dispose();
                return response;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        public static string EncurtadorUrl(Uri url)
        {
            return EncurtadorUrl(url.AbsoluteUri);
        }
    }
}
using System;
using B2E.Models;
using System.Data;
using System.Collections.Generic;

namespace B2E.Data
{
    public class userData : utilData
    {
        internal bool CriarUsuario(string user, out string mensagem)
        {
            bool Retorno = false;
            try
            {
                string qryUsuario = @"SELECT id FROM tb_users WHERE user = " + Aspas(user, false);
                DataTable reader = RS(qryUsuario);
                if (reader.Rows.Count > 0)
                {
                    Retorno = false;
                    mensagem = "Esse usuário já existe.";
                }
                else
                {
                    if (Executar("INSERT INTO tb_users (user) VALUES (" + Aspas(user, false) + ")") > 0)
                    {
                        Retorno = true;
                        mensagem = "Usuário incluído com sucesso.";
                    }
                    else
                    {
                        Retorno = false;
                        mensagem = "Erro ao inserir usuário.";
                    }
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return Retorno;
        }

        internal bool ApagarUsuario(int id, out string mensagem)
        {
            bool Retorno = false;
            try
            {
                string qryUsuario = @"DELETE FROM tb_users WHERE id = " + id;
                if (Executar(qryUsuario) > 0)
                {
                    Retorno = true;
                    mensagem = "Usuário excluído com sucesso.";
                }
                else
                {
                    Retorno = false;
                    mensagem = "Erro ao excluir usuário.";
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return Retorno;
        }

        internal List<url> EstatisticasUsuario(int id)
        {
            List<url> lista = new List<url>();
            try
            {
                string qryEstatisticas = @"SELECT u.id, r.user, url, shorturl, hits FROM tb_urls u INNER JOIN tb_users r ON u.user = r.id  WHERE r.id = " + id;
                DataTable reader = RS(qryEstatisticas);
                foreach (DataRow row in reader.Rows)
                {
                    url url = new url();
                    url.Id = Convert.ToInt32(row["id"].ToString());
                    url.User = row["user"].ToString();
                    url.Url = row["url"].ToString();
                    url.Shorturl = row["shorturl"].ToString();
                    url.Hits = Convert.ToInt32(row["hits"].ToString());
                    lista.Add(url);
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return lista;
        }
    }
}
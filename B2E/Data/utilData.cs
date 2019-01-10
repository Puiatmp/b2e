using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace B2E.Data
{
    public class utilData
    {
        public static string String_CN = ConfigurationManager.ConnectionStrings["b2e"].ConnectionString;
        public static string DB = "b2e";
        public static MySqlConnection CN;
        public static MySqlCommand CM;
        public static int CodigoUsuario;

        public static void Conectar(string String_Conexao)
        {
            try
            {
                CN = new MySqlConnection(String_Conexao);
                CN.Open();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1040)
                {
                    //MessageBox.Show(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, ex.Number);                
                }
                else
                    Tratamento(ex.Number, ex.Message, ex.Source, "Modulo.Conectar()", ex.StackTrace, false, DB);
            }
        }

        public static void Desconectar()
        {
            try
            {
                CN.Close();
                CN = null;
            }
            catch (MySqlException ex)
            {
                Tratamento(ex.Number, ex.Message, ex.Source, "Modulo.Desconectar()", ex.StackTrace, false, DB);
            }
        }

        public static System.Data.DataTable RS(string String_SQL)
        {
            System.Data.DataTable Retorno = null;
            MySqlDataAdapter DA;
            MySqlCommandBuilder CB;
            try
            {
                Conectar(String_CN);
                Retorno = new System.Data.DataTable();
                DA = new MySqlDataAdapter(String_SQL, CN);
                CB = new MySqlCommandBuilder(DA);
                DA.Fill(Retorno);
                return Retorno;
            }
            catch (MySqlException ex)
            {
                Tratamento(ex.Number, ex.Message, ex.Source, "Modulo.RS(" + String_SQL + ")", ex.StackTrace, true, DB);
                return Retorno;
            }
            finally
            {
                Desconectar();
            }
        }

        public static int Executar(string String_SQL)
        {
            try
            {
                Conectar(String_CN);
                CM = new MySqlCommand(String_SQL, CN);
                if (String_SQL.ToUpper().IndexOf("INSERT") < 0)
                    return CM.ExecuteNonQuery();
                else
                {
                    CM.ExecuteNonQuery();
                    return GetLastID();
                }
            }
            catch (MySqlException ex)
            {
                Tratamento(ex.Number, ex.Message, ex.Source, "Modulo.Executar(" + String_SQL + ")", ex.StackTrace, true, DB);
                Desconectar();
                return 0;
            }
            finally
            {
                Desconectar();
            }
        }

        public static int GetLastID()
        {
            int Retorno = 0;
            System.Data.DataTable RSGetLastID = null;
            MySqlDataAdapter DA;
            MySqlCommandBuilder CB;
            try
            {
                RSGetLastID = new System.Data.DataTable();
                DA = new MySqlDataAdapter("SELECT LAST_INSERT_ID()", CN);
                CB = new MySqlCommandBuilder(DA);
                DA.Fill(RSGetLastID);
                Retorno = Convert.ToInt32(RSGetLastID.Rows[0][0]);
            }
            catch (MySqlException ex)
            {
                Tratamento(ex.Number, ex.Message, ex.Source, "Modulo.GetLastID()", ex.StackTrace, false, DB);
            }
            return Retorno;
        }

        public static string Aspas(string sTexto, bool Data_Completa)
        {
            if (sTexto == "")
                return "NULL";
            else if (IsNumeric(sTexto))
                return sTexto.Replace(".", "").Replace(",", ".");
            else if (IsDate(sTexto) == true)
            {
                if (Data_Completa == true)
                    return (char)39 + Convert.ToDateTime(sTexto).ToString("yyyy/MM/dd HH:mm:ss") + (char)39;
                else if (sTexto.IndexOf("/") == 0)
                    return (char)39 + Convert.ToDateTime(sTexto).ToString("HH:mm:ss") + (char)39;
                else
                    return (char)39 + Convert.ToDateTime(sTexto).ToString("yyyy/MM/dd") + (char)39;
            }
            else if (sTexto.IndexOf((char)39) > -1)
                return (char)39 + sTexto.Replace("\'", "").Replace(@"\", @"\\").Replace("\"", "") + (char)39;
            //return (char)39 + Strings.Replace(Strings.Replace(Strings.Replace(sTexto, (char)39, (char)39 + (char)39), @"\", @"\\"), (char)34, @"\" + (char)34) + (char)39;
            else
                return (char)39 + sTexto.Replace(@"\", @"\\").Replace("\"", "") + (char)39;
        }

        private static bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        private static bool IsDate(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Texto(string sTexto, bool Data_Completa, bool Moeda)
        {
            if (sTexto == null)
                return "";
            else if (IsNumeric(sTexto))
            {
                if (Moeda == true)
                    return Convert.ToDouble(sTexto).ToString("#,###,##0.00");
                else
                    return sTexto.Replace("\r\n", "");
            }
            else if (IsDate(sTexto) == true)
            {
                if (Data_Completa == true)
                    return Convert.ToDateTime(sTexto).ToString("dd/MM/yyyy HH:mm:ss");
                else if (sTexto.IndexOf("/") < 0)
                    return Convert.ToDateTime(sTexto).ToString("HH:mm:ss");
                else
                    return Convert.ToDateTime(sTexto).ToString("dd/MM/yyyy");
            }
            else if (Moeda == true)
                return "0,00";
            else
                return sTexto.Replace("\r\n", "");
        }

        public static void Tratamento(int N, string M, string S, string P, string L, bool Mostra, string DataBase)
        {
            try
            {
                if (Mostra == true)
                    //Interaction.MsgBox(M + Strings.Chr(10) + S + Strings.Chr(10) + P + Strings.Chr(10), MsgBoxStyle.Exclamation, "Erro: " + N);
                    Executar("INSERT INTO " + DataBase + ".tb_erros(sistema, data_erro, maquina, numero, descricao, origem, procedimento, pilha) VALUES(" + Aspas(System.Reflection.Assembly.GetExecutingAssembly().FullName, false) + ", Now(), " + Aspas(System.Net.Dns.GetHostName(), false) + ", " + N + ", " + Aspas(M, false) + ", " + Aspas(S, false) + ", " + Aspas(P, false) + ", " + Aspas(L, false) + ")");
            }
            catch (MySqlException ex)
            {
                //MsgBox(ex.Message + Strings.Chr(10) + "INSERT INTO " + DataBase + ".tb_erros(sistema, data_erro, maquina, numero, descricao, origem, procedimento, pilha) VALUES(" + Aspas(My.Application.Info.AssemblyName, false) + ", Now(), " + Aspas(System.Net.Dns.GetHostName(), false) + ", " + N + ", " + Aspas(M, false) + ", " + Aspas(S, false) + ", " + Aspas(P, false) + ", " + Aspas(L, false) + ")");
            }
        }

        public static void Historico(int Tip, string Cod, long Val, string His, string DataBase)
        {
            Executar("INSERT INTO " + DataBase + ".tb_historicos (cod_usuario, cod_tipo_historico, codigo, valor, data_historico, historico) VALUES (" + CodigoUsuario + ", " + Tip + ", '" + Cod + "', " + Val + ", Now(), " + Aspas(His, false) + ")");
        }

        public static string Encrypt2(string clearText, string encryptionKey)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, encryptor.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt2(string cipherText, string encryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, encryptor.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = System.Text.Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string StripNullCharacters(string vstrStringWithNulls)
        {
            int intPosition;
            string strStringWithOutNulls;
            intPosition = 1;
            strStringWithOutNulls = vstrStringWithNulls;
            while (intPosition > 0)
            {
                intPosition = vstrStringWithNulls.IndexOf("", intPosition);
                if (intPosition > 0)
                    strStringWithOutNulls = strStringWithOutNulls.PadLeft(intPosition - 1) + strStringWithOutNulls.PadRight(strStringWithOutNulls.Length - intPosition);
                if (intPosition > strStringWithOutNulls.Length)
                    break;
            }
            return strStringWithOutNulls;
        }

        //public static void Blob(int Cod, string Tipo, int CodTipo, string Arquivo, bool Grava, bool Abre, string DataBase)
        //{
        //    int FileSize;
        //    byte[] rawData;
        //    System.IO.FileStream FS;
        //    MySql.Data.MySqlClient.MySqlDataReader myData;
        //    try
        //    {
        //        int Maximo = Convert.ToInt32(RS("SHOW VARIABLES LIKE 'max_allowed_packet'").Rows[0][1].ToString());
        //        Conectar(String_CN);
        //        if (Grava == true)
        //        {
        //            FS = new FileStream(Arquivo, FileMode.Open, FileAccess.Read);
        //            FileSize = FS.Length;
        //            rawData = new byte[FileSize + 1];
        //            FS.Read(rawData, 0, FileSize);
        //            FS.Close();
        //            if (FileSize > Maximo)
        //            {
        //                //Interaction.MsgBox("Esse anexo (" + FileSize + ") é maior que o valor máximo permitido (" + Maximo + ") do banco de dados. Diminua o tamanho e tente novamente.", MsgBoxStyle.Exclamation, "Anexo");
        //                return;
        //            }
        //            CM = new MySql.Data.MySqlClient.MySqlCommand("INSERT INTO " + DataBase + ".tb_anexos (cod_usuario, tipo, cod_tipo, nome, anexo, tamanho) VALUES (" + CodigoUsuario + ", '" + Tipo + "', " + CodTipo + ", '" + FileSystem.Dir(Arquivo) + "', ?File, ?Size)", CN);
        //            CM.Parameters.AddWithValue("?File", rawData);
        //            CM.Parameters.AddWithValue("?Size", FileSize);
        //            CM.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            if (Path.GetDirectoryName(Arquivo) != "")
        //                File.Delete(Arquivo);
        //            if (Path.GetDirectoryName(Arquivo) == "")
        //            {
        //                CM = new MySql.Data.MySqlClient.MySqlCommand("SELECT anexo, tamanho FROM " + DataBase + ".tb_anexos WHERE cod_anexo = " + Cod, CN);
        //                myData = CM.ExecuteReader;
        //                if (myData.HasRows)
        //                {
        //                    myData.Read();
        //                    FileSize = myData.GetUInt32(myData.GetOrdinal("tamanho"));
        //                    rawData = new byte[FileSize + 1];
        //                    myData.GetBytes(myData.GetOrdinal("anexo"), 0, rawData, 0, FileSize);
        //                    FS = new System.IO.FileStream(Arquivo, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
        //                    FS.Write(rawData, 0, FileSize);
        //                    FS.Close();
        //                    FS = null;
        //                }
        //                myData.Close();
        //                myData = null/* TODO Change to default(_) if this is not a reference type */;
        //            }
        //            if (Abre == true)
        //                System.Diagnostics.Process.Start(Arquivo, AppWinStyle.NormalFocus);
        //        }
        //        Desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        Tratamento(Information.Err.Number, ex.Message, ex.Source, "Modulo.Blob(" + Cod + ", " + Arquivo + ", " + Grava + ")", ex.StackTrace, false, DB);
        //        myData = null/* TODO Change to default(_) if this is not a reference type */;
        //    }
        //}

        //public static void Excel(ListView L, bool G, string T, string DataBase)
        //{
        //    object oExcl;
        //    object oWrkb;
        //    object oWrks;
        //    object oChrt;
        //    int Linha;
        //    int Coluna;
        //    try
        //    {
        //        oExcl = Interaction.CreateObject("Excel.Application");
        //        oWrks = Interaction.CreateObject("Excel.Sheet");
        //        if (G == true)
        //            oChrt = Interaction.CreateObject("Excel.Chart");
        //        oWrkb = oExcl.Workbooks.Add;
        //        oWrks = oWrkb.Worksheets(1);
        //        oWrks.Rows(1).Font.Bold = true;
        //        for (Coluna = 0; Coluna <= L.Columns.Count - 1; Coluna++)
        //            oWrks.Rows(1).Cells(null/* Conversion error: Set to default value for this argument */, Coluna + 1).Value = L.Columns(Coluna).Text;
        //        for (Linha = 0; Linha <= L.Items.Count - 1; Linha++)
        //        {
        //            oWrks.Cells(Linha + 2, 1).Value = L.Items(Linha).Text;
        //            for (Coluna = 0; Coluna <= L.Columns.Count - 1; Coluna++)
        //                oWrks.Cells(Linha + 2, Coluna + 1).Value = L.Items(Linha).SubItems(Coluna).Text;
        //        }
        //        if (G == true)
        //        {
        //            oChrt = oWrkb.Charts.Add;
        //            oChrt.chartType = 52;
        //            oChrt.HasLegend = false;
        //            oChrt.SetSourceData(Source: oWrks.Range("A2:B2:A" + Linha + ":B" + Linha), PlotBy: 2);
        //            oChrt.Location(Where: 1, Name: "Gráfico");
        //            oChrt.HasTitle = true;
        //            oChrt.ChartTitle.Characters.Text = T;
        //            oChrt.Axes(1, 1).HasTitle = true;
        //            oChrt.Axes(1, 1).AxisTitle.Characters.Text = L.Columns(0).Text;
        //            oChrt.Axes(2, 1).HasTitle = true;
        //            oChrt.Axes(2, 1).AxisTitle.Characters.Text = L.Columns(1).Text;
        //            oChrt.HasTitle = true;
        //            oChrt.ApplyDataLabels(2);
        //            oWrks.Range("A:F").AutoFilter();
        //            oWrks.Range("A:F").Columns.AutoFit();
        //        }
        //        oExcl.Visible = true;
        //        oExcl.UserControl = true;
        //        oExcl = null;
        //        oWrkb = null;
        //        oWrks = null;
        //        if (G == true)
        //            oChrt = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Tratamento(Information.Err.Number, ex.Message, ex.Source, "Modulo.Excel(" + L.Name + ", " + G + ", " + T + ")", ex.StackTrace, true, DataBase);
        //        oExcl = null;
        //        oWrkb = null;
        //        oWrks = null;
        //        if (G == true)
        //            oChrt = null;
        //    }
        //}

        public static bool Valida_CGC(string CGC)
        {
            int[] D = new int[15];
            int I;
            int D13;
            int D14;
            for (I = 1; I <= 14; I++)
                D[I] = Convert.ToInt16(CGC.Substring(I, 1));
            D13 = 11 - ((5 * D[1] + 4 * D[2] + 3 * D[3] + 2 * D[4] + 9 * D[5] + 8 * D[6] + 7 * D[7] + 6 * D[8] + 5 * D[9] + 4 * D[10] + 3 * D[11] + 2 * D[12]) % 11);
            D13 = (D13 == 10 | D13 == 11 ? 0 : D13);
            D14 = 11 - ((6 * D[1] + 5 * D[2] + 4 * D[3] + 3 * D[4] + 2 * D[5] + 9 * D[6] + 8 * D[7] + 7 * D[8] + 6 * D[9] + 5 * D[10] + 4 * D[11] + 3 * D[12] + 2 * D13) % 11);
            D14 = (D14 == 10 | D14 == 11 ? 0 : D14);
            return (D13 == D[13] & D14 == D[14]);
        }

        public static bool Valida_CPF(string CPF)
        {
            int[] D = new int[12];
            int I;
            int D10;
            int D11;
            for (I = 1; I <= 11; I++)
                D[I] = Convert.ToInt16(CPF.Substring(I, 1));
            D10 = 11 - (((D[1] * 10) + (D[2] * 9) + (D[3] * 8) + (D[4] * 7) + (D[5] * 6) + (D[6] * 5) + (D[7] * 4) + (D[8] * 3) + (D[9] * 2)) % 11);
            D10 = (D10 == 10 | D10 == 11 ? 0 : D10);
            D11 = 11 - (((D[1] * 11) + (D[2] * 10) + (D[3] * 9) + (D[4] * 8) + (D[5] * 7) + (D[6] * 6) + (D[7] * 5) + (D[8] * 4) + (D[9] * 3) + (D10 * 2)) % 11);
            D11 = (D11 == 10 | D11 == 11 ? 0 : D11);
            return (D10 == D[10] & D11 == D[11]);
        }

        public static bool Valida_EMail(string E)
        {
            if (E != "")
                return System.Text.RegularExpressions.Regex.IsMatch(E, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            else
                return false;
        }
    }
}
using OfxApplication.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfxApplication.Database
{
    public class Database
    {
        public void SaveData(List<STMTTRN> result)
        {
            foreach (STMTTRN item in result)
            {
                String Ds = @"POSITIVO\SQLEXPRESS";
                String InitialCatalog = "OfxApplication";
                String conStr = "Data Source=" + Ds + ";Initial Catalog=" + InitialCatalog + ";integrated security=True;";

                SqlConnection con = new SqlConnection(conStr);
                String sqlCount = "SELECT COUNT(*) FROM tbl WHERE checksum = CONVERT(uniqueidentifier, '" + item.id + "')";
                sqlCount = "SELECT COUNT(*) FROM tbl WHERE checksum = '" + item.hash + "'";
                SqlCommand cmd = new SqlCommand(sqlCount, con);
                con.Open();
                string count = cmd.ExecuteScalar().ToString();
                int qtd = Int32.Parse(count);
                con.Close();
                if (qtd <= 0)
                {
                    String sql = "INSERT INTO tbl (type, dtPostedm, trnamt, fitid, memo, id, checksum) VALUES " +
                        "('" + item.type + "', '" + item.dtPosted + "', '" + item.trnamt + "', '" + item.fitid + "', '" + item.memo + "', '" + item.id + "', '" + item.hash + "')";
                    cmd = new SqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

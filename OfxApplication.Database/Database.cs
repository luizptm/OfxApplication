using OfxApplication.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfxApplication.Database
{
    class Database
    {
        private void SaveData(List<STMTTRN> result)
        {
            foreach (STMTTRN item in result)
            {
                String Ds = "";
                String InitialCatalog = "";
                String User = "";
                String Password = "";
                String conStr =
                "Data Source=" + Ds + ";" +
                "Initial Catalog=" + InitialCatalog + ";" +
                "User id=" + User + ";" +
                "Password=" + Password + ";";

                SqlConnection con = new SqlConnection(conStr);
                String sqlCount = "SELECT COUNT(*) FROM tbl WHERE id = " + item.id;
                SqlCommand cmd = new SqlCommand(sqlCount);
                con.Open();
                int count = Int32.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                if (count <= 0)
                {
                    String sql = "INSERT INTO tbl (type, dtPostedm trnamt, fitid, memo) VALUES " +
                        "(" + item.type + ", " + item.dtPosted + ", " + item.trnamt + ", " + item.fitid + ", " + item.memo + ")";
                    cmd = new SqlCommand(sql);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

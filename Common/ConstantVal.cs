using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Common
{
    public class ConstantVal
    {

        #region 現在日付取る
        /// <summary>
        /// <returns>日付</returns>
        /// </summary>
        public static DateTime Fu_GetDateTime(MySqlConnection cn)
        {
            DataTable dt_Date = new DataTable();
            string sql = "select now()";
            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql, cn))
            {
                a1.Fill(dt_Date);
            }
            cn.Close();
            return System.Convert.ToDateTime(dt_Date.Rows[0][0].ToString());
        }
        #endregion

        #region 案件コードを取る
        /// <summary>
        /// <returns>コード</returns>
        /// </summary>
        public static String Get_ankenid(MySqlConnection cn)
        {
            DataTable dt_Date = new DataTable();
            string sql = "SELECT case ifnull(max(a.id),'') when '' then 1 else max(rg.id)+1 end as jun from anken a";
            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql, cn))
            {
                a1.Fill(dt_Date);
            }
            cn.Close();
            return dt_Date.Rows[0][0].ToString();
        }
        #endregion
    }
}

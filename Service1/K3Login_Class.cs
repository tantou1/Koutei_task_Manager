using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data;

namespace Service
{
    public class K3Login_Class
    {
        
        public static string Get_Password(string cTantou)
        {
            string pwd = "";
            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            string query = "select sPWD FROM m_j_tantousha where fTAISYA='0' and cTANTOUSHA='" + cTantou + "'";
            DataTable dt = new DataTable();
            con.Close();
            con.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(query, con))
            {
                adap.Fill(dt);
            }
            con.Close();
            if (dt.Rows.Count > 0)
            {
                pwd = dt.Rows[0][0].ToString();
            }

            return pwd;
        }

        public static string Get_tantou(string cTantou)        {            string stantou = "";            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);            string query = "SELECT sTANTOUSHA as stantousha FROM m_j_tantousha WHERE cTANTOUSHA = '" + cTantou + "' AND fTAISYA = 0";            DataTable dt = new DataTable();            con.Open();            using (MySqlDataAdapter a1 = new MySqlDataAdapter(query, con))            {                a1.Fill(dt);            }            con.Close();            if (dt.Rows.Count > 0)            { stantou = dt.Rows[0][0].ToString(); }

            return stantou;        }
    }
}


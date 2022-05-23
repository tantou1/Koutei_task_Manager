using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Service
{
    public class K_ClientConnection_Class
    {
        public DateTime dHENKOU { get; set; }
        public DataTable GetKoutei()
        {
            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            //String qr = "SELECT * FROM sample_np.koutei;";
            String qr = "SELECT DISTINCT ";
            qr += " cSAGYOBUNRUI cBUNRUI";
            qr += " ,ifnull(sSAGYOBUNRUI,'') sBUNRUI";
            qr +=" FROM M_SHIJISHO_SAGYOBUNRUI";
            qr += " WHERE 1=1 ";
            qr += " AND fDEL=0 order by nJUNBAN;";
            DataTable dt = new DataTable();
            con.Close();
            con.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(qr, con))
            {
                adap.Fill(dt);
            }
            con.Close();
            return dt;
        }
        public string Getdhenkou()
        {
            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            dHENKOU = ConstantVal.Fu_GetDateTime(con); 

            return dHENKOU.ToString("yyyyMMddHHmmss");
        }

        public string get_sCUSTOMER_USER_VERSION()
        {
            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
           
            String qr = "SELECT ";
            qr += " ifnull(sCUSTOMER_USER_VERSION,'') sCUSTOMER_USER_VERSION";
            qr += " FROM m_customer";
            qr += " WHERE 1=1 ";
            DataTable dt = new DataTable();
            con.Close();
            con.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(qr, con))
            {
                adap.Fill(dt);
            }
            con.Close();

            return dt.Rows[0][0].ToString();
        }
    }
}

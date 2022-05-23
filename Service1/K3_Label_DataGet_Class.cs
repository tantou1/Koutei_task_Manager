using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data;
using MySql.Data.MySqlClient;

namespace Service
{
    public class K3_Label_DataGet_Class
    {
        public DateTime dHENKOU { get; set; }

        public DataTable Get_Label(bool f_santou,bool fgazou,bool f_old_img_file_ver)
        {
            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            
            String qr = "SELECT";
            qr += " rs.cSHIJISYO cSHIJISYO";
            qr += " ,ifnull(rs.sSHIJISYO,'') sSHIJISYO";
            qr += " ,ifnull(rsb.cBUNRUI,'') cBUNRUI";
            qr += " ,ifnull(rsb.sBUNRUI,'') sBUNRUI";
            qr += " ,ifnull(rsb.nJUMBAN,'0') nJUMBAN";
            qr += " ,DATE_FORMAT(ifnull(rs.dKANRYOUYOTEI,''),'%m/%d') dKANRYOUYOTEI";
            qr += " ,ifnull(rm.sTOKUISAKI,'') sTOKUISAKI";
            qr += " ,ifnull(mjt.sTANTOUSHA,'') sTANTOUSHA";
            qr += " ,ifnull(mjt.SMAIL,'') SMAIL";
            if (fgazou == false)
            {
                qr += " ,'' img";
                qr += " ,'' img_filename";
            }
            else
            {
                if (f_old_img_file_ver == false)
                {
                    qr += " ,ifnull(rsf.path,'') img";
                    qr += " ,ifnull(rsf.hyoujimei,'') img_filename";
                }
                else
                {
                    qr += " ,ifnull(mf.sPATH_SERVER_SOURCE,'') img";
                    qr += " ,ifnull(mf.sFILE,'') img_filename";

                }
            }
            qr += " from r_shijisyo rs";
            qr += " inner join r_shijisyo_bunrui rsb";
            qr += " on rs.cSHIJISYO=rsb.cSHIJISYO";

            if (f_santou == true)
            {
                qr += " inner join(select min(rsb2.nJUMBAN) as minjun,rsb2.cSHIJISYO";
                qr += " from r_shijisyo_bunrui rsb2";
                qr += " where rsb2.fJYOUTAI<>3 group by rsb2.cSHIJISYO) mn";
                qr += " on rsb.nJUMBAN = mn.minjun and rsb.cSHIJISYO=mn.cSHIJISYO";
            }

            qr += " inner join r_shijisyo_mitsu rsm";
            qr += " on rs.cSHIJISYO=rsm.cSHIJISYO";
            qr += " inner join r_mitumori rm";
            qr += " on rsm.cMITUMORI=rm.cMITUMORI";
            qr += " left join m_j_tantousha mjt";
            qr += " on rm.cEIGYOTANTOSYA=mjt.cTANTOUSHA";
            if (fgazou == true)
            {
                if (f_old_img_file_ver == false)
                {
                    qr += " left join r_shiji_file rsf";
                    qr += " on rs.cSHIJISYO=rsf.cSHIJISYO and rsf.shurui='1'";
                }
                else
                {
                    qr += " left join m_file mf";
                    qr += " on rs.cFILE=mf.cFILE";
                }
            }

            qr += " where 1=1";
            qr += " and rs.fJYOUTAI<>'4' and rs.fJYOUTAI<>'0' and rs.fJYOUTAI<>'3'";
            qr += " and rs.fHYOUJI='0' ";
            qr += " and (rsb.fJYOUTAI<>3 or rsb.fJYOUTAI is null or rsb.fJYOUTAI='' )";
            qr += "AND rs.fKAKUTEI='1'";
            if (f_santou == true)
            {
                qr += " group by rs.cSHIJISYO";
            }
            qr += " ORDER BY rs.cSHIJISYO DESC ";
           
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
        public bool EndLable(string cSHIJISYO,string cBUNRUI,bool fend)
        {
            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            dHENKOU = ConstantVal.Fu_GetDateTime(con);

            #region　指示書分類更新
            String updatequery = @"Update";
            updatequery += " r_shijisyo_bunrui set";
            updatequery += " StartTime_YU=IF((fJYOUTAI='0' or fJYOUTAI='' or fJYOUTAI is null),(SELECT dJUUTYU FROM r_shijisyo";
            updatequery += " where cSHIJISYO='" + cSHIJISYO + "'), StartTime_YU) ";

            updatequery += " ,EndTime_YU=IF((fJYOUTAI='0' or fJYOUTAI='' or fJYOUTAI is null),(SELECT dKANRYOUYOTEI FROM r_shijisyo ";
            updatequery += " where cSHIJISYO='" + cSHIJISYO + "'),EndTime_YU)";

            updatequery += " ,StartTime=IF((fJYOUTAI<>'2' or fJYOUTAI='' or fJYOUTAI is null) ,'" + dHENKOU + "',StartTime) ";
            updatequery += " ,EndTime='" + dHENKOU + "' ";
            updatequery += " ,fJYOUTAI = '3' ";
            updatequery += ",dHENKOU = '" + dHENKOU + "'";
            updatequery += " where cSHIJISYO='" + cSHIJISYO + "' and cBUNRUI='"+ cBUNRUI + "';";
            #endregion

            #region 指示書更新
            string start_date_sql = "(select min(rsb.StartTime) as StartTime from r_shijisyo_bunrui rsb where cshijisyo='" + cSHIJISYO + "')";   //add by yamin 20190828
            
            #region 「fend」項目無し場合指示書更新Queryで指示書分類全体完了かどうかチェックSQLも含む
            //updatequery += @"update r_shijisyo set";
            //updatequery += " dKAISHIBI=" + start_date_sql;
            //updatequery += " ,dKANRYOUBI=IF((select count(rsb.cBUNRUI) as kouteicount from r_shijisyo_bunrui rsb";
            //updatequery += " where cshijisyo='"+ cSHIJISYO + "' and rsb.fJYOUTAI<>3)='0',";
            //updatequery += "(select max(rsb.EndTime) as EndTime from r_shijisyo_bunrui rsb where cshijisyo='"+ cSHIJISYO + "'),";
            //updatequery += "null)";
            //updatequery += ",fJYOUTAI=IF((select count(rsb.cBUNRUI) as kouteicount from r_shijisyo_bunrui rsb";
            //updatequery += " where cshijisyo='" + cSHIJISYO + "' and rsb.fJYOUTAI<>3)='0',";
            //updatequery += "3,";
            //updatequery += "2)";
            //updatequery += " where cSHIJISYO='" + cSHIJISYO + "';";
            #endregion

            #region 「fend」項目ある場合指示書更新Query
            updatequery += @"update r_shijisyo set";
            updatequery += " dKAISHIBI=" + start_date_sql;
            if(fend==true)
            {
                updatequery += " ,dKANRYOUBI=";
                updatequery += "(select max(rsb.EndTime) as EndTime from r_shijisyo_bunrui rsb where cshijisyo='" + cSHIJISYO + "')";
                updatequery += ",fJYOUTAI='3'";
            }
            else
            {
                updatequery += ",fJYOUTAI='2'";
            }
            updatequery += " where cSHIJISYO='" + cSHIJISYO + "';";
            #endregion
            #endregion

            con.Open();
            MySqlTransaction tran = con.BeginTransaction();
            try
            {
                MySqlCommand c = new MySqlCommand(updatequery, con);
                c.ExecuteNonQuery();
                tran.Commit();
                con.Close();
            }
            catch
            {
                try
                {
                    tran.Rollback();
                }
                catch
                {
                }
                con.Close();
                return false;
            }

            return true;
        }

        public string Get_Endkoutei_count(string cSHIJISYO)
        {
            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            String qr = "SELECT";
            qr += " count(rsb.cBUNRUI) as kouteicount ";
            qr += " from r_shijisyo_bunrui rsb";
            qr += " where rsb.cshijisyo='" + cSHIJISYO + "' and rsb.fJYOUTAI<>3";

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

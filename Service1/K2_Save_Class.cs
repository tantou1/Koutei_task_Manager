using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Common;
using System.Data;

namespace Service
{
    public class K2_Save_Class
    {
        public DateTime dHENKOU { get; set; }
        public bool DataSave(DataTable dt, string stitle, string photo_path, string filename)
        {
            MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            dHENKOU = ConstantVal.Fu_GetDateTime(con);

            #region 案件コード取るQuery
            string ankenidquery = "";
            ankenidquery += " select t.id";
            ankenidquery += " from(SELECT case ifnull(max(a.id),'') when '' then 1 else max(a.id)+1 end as id from anken a";
            ankenidquery += ") t";
            #endregion

            #region 案件新規Query
            string sql_insert = "";
            sql_insert = @"INSERT INTO";
            sql_insert += " anken";
            sql_insert += " (";
            sql_insert += " id";
            sql_insert += " ,created_at";
            sql_insert += " ,updated_at";
            sql_insert += " )";

            sql_insert += " Values(";
            sql_insert += "(" + ankenidquery + ")";
            sql_insert += ",'" + dHENKOU + "'";
            sql_insert += ",'" + dHENKOU + "'";
            sql_insert += ");";
            #endregion

            #region　ラベルとボート新規Query
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                #region　ラベル新規Query
                sql_insert += @"INSERT INTO";
                sql_insert += " label";
                sql_insert += " (";
                sql_insert += " id";
                sql_insert += " ,anken_id";
                sql_insert += " ,title";
                sql_insert += " ,status";
                sql_insert += " ,created_at";
                sql_insert += " ,updated_at";
                sql_insert += " )";

                sql_insert += " Values(";
                sql_insert += "( select t.id";
                sql_insert += " from(SELECT case ifnull(max(lb.id),'') when '' then 1 else max(lb.id)+1 end as id";
                sql_insert += " FROM label lb";
                sql_insert += ") t)";　//label最大コード取る
                //sql_insert += ",'" + ankenid + "'";
                sql_insert += " ,(select ankent.ankenid";
                sql_insert += " from(SELECT max(a.id) as ankenid";
                sql_insert += " FROM anken a";
                sql_insert += ") ankent)";　//anken最大コード取る
                sql_insert += ",'" + stitle + "'";
                sql_insert += ",'" + 0 + "'";
                sql_insert += ",'" + dHENKOU + "'";
                sql_insert += ",'" + dHENKOU + "'";
                sql_insert += ");";
                #endregion

                #region　ボート新規Query
                sql_insert += @"INSERT INTO";
                sql_insert += " board";
                sql_insert += " (";
                sql_insert += " label_id";
                sql_insert += " ,label_order";
                sql_insert += " ,koutei_id";
                sql_insert += " ,created_at";
                sql_insert += " ,updated_at";
                sql_insert += " )";

                sql_insert += " Values(";
                sql_insert += " (select t.id";
                sql_insert += " from(SELECT max(lb.id) as id";
                sql_insert += " FROM label lb";
                sql_insert += ") t)";　//label最大コード取る
                sql_insert += " ,(select bot.label_order";
                sql_insert += " from(SELECT case ifnull(max(bo.label_order),'') when '' then 1 else max(bo.label_order)+1 end as label_order";
                sql_insert += " from board bo";
                sql_insert += " where bo.koutei_id ='" + dt.Rows[r]["ckoutei"].ToString() + "'";
                sql_insert += ") bot)";　//label_order最大コード取る
                sql_insert += ",'" + dt.Rows[r]["ckoutei"].ToString() + "'";
                sql_insert += ",'" + dHENKOU + "'";
                sql_insert += ",'" + dHENKOU + "'";
                sql_insert += ");";
                #endregion

                #region 画像新規Query
                sql_insert += @"INSERT INTO";
                sql_insert += " m_file";
                sql_insert += " (";
                sql_insert += " cFILE";
                sql_insert += " ,sPATH_SERVER_SOURCE";
                sql_insert += " ,sFILE";
                sql_insert += " ,sPATH_SUB_DIR";
                sql_insert += " ,label_id";
                sql_insert += " )";
                sql_insert += " Values(";
                sql_insert += "( select t.cFILE";
                sql_insert += " from(SELECT case ifnull(max(mf.cFILE),'') when '' then 1 else max(mf.cFILE)+1 end as cFILE";
                sql_insert += " FROM m_file mf";
                sql_insert += ") t)";
                sql_insert += ",'" + photo_path + "'";
                sql_insert += ",'" + filename + "'";
                sql_insert += ",'" + photo_path + "'";
                sql_insert += ",(select tlb.id";
                sql_insert += " from(SELECT max(lb.id) as id";
                sql_insert += " FROM label lb";
                sql_insert += ") tlb)";　//label最大コード取る
                sql_insert += ");";
                #endregion
            }
            #endregion

            con.Open();
            MySqlTransaction tran = con.BeginTransaction();
            try
            {
                MySqlCommand c = new MySqlCommand(sql_insert, con);
                c.ExecuteNonQuery();
                tran.Commit();
                con.Close();
            }
            catch(Exception ex)
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
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Koutei_task_Manager.UserControl
{
    public partial class UC01board : System.Web.UI.UserControl
    {

        #region "工程ボードに付箋データ設定"
        public event EventHandler EndKoutei_All;
        public void SetPendingFusenBoardData(string sbunrui, string cbunrui, string taskcount, string bgcolor)
        {
            //工程タイトルを表示の為
            pnlFusen.CssClass = "UC01FusenInfoDiv PendingBoardDiv";
            divPendingHeader.Style.Add("display", "block");

            lblPendingHeader.Text = sbunrui;
            lblPendingHeader_ID.Text = cbunrui;
            lblcount.Text = taskcount;

            //divFusenList.Style.Add("background-color", "rgb(197,224,245)");
            title_bar.Style.Add("width", "100%");
            title_bar.Style.Add("height", "1%");
            title_bar.Style.Add("background-color", bgcolor);

            if (taskcount == "")
            {
               bt_end.Visible = false;
            }
            else
            {
                bt_end.Visible = true;
            }
        }   

        #endregion

        protected void bt_end_Click(object sender, EventArgs e)
        {
            EndKoutei_All(sender, e);
        }
    }
}
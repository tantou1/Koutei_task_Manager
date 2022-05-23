using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Koutei_task_Manager.UserControl;
using Service;


namespace Koutei_task_Manager.WebFront
{
    public partial class K1_Main : System.Web.UI.Page
    {

        public static string to, tomail;
        protected void Page_Load(object sender, EventArgs e)
        {
            //divLabelSave.Style["display"] = "none";
            //updLabelSave.Update();
            if (!this.IsPostBack)
            {
                //messge_set();
                navbarDropdownMenuLink.InnerText = Session["sTantou"].ToString();
            }
            get_data_DB();
            BindBoard();
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {

        }
        private void BindBoard()
        {

            PinChange();

            //K_ClientConnection_Class test = new K_ClientConnection_Class();
            //DataTable dt = test.GetKoutei();

            //K3_Label_DataGet_Class label = new K3_Label_DataGet_Class();
            //DataTable dtLabel = label.Get_Label(chk_santo.Checked);

            //UC03Main ucmain = (UC03Main)LoadControl("~/UserControl/UC03Main.ascx");
            //ucmain.ID = "UCMain";
            //ucmain.TaskTsuika += this.HandleTaskTsuika;
            //pnlTask.Controls.Add(ucmain);
            //UpdTaskTsuika.Update();

            DataTable dt = Session["dt"] as DataTable;            DataTable dtLabel = Session["dtLabel"] as DataTable;            int color_i = 0;            for (int i = 0; i < dt.Rows.Count; i++)            {                DataRow[] drresult = dtLabel.Select("cBUNRUI = " + dt.Rows[i]["cBUNRUI"].ToString());                DataTable dt_label_koutei = dtLabel.Clone();                if (drresult.Length > 0)                {                    dt_label_koutei = drresult.CopyToDataTable();                }

                //工程ボードを設定する
                UC01board ucBoard = (UC01board)LoadControl("~/UserControl/UC01board.ascx");                ucBoard.ID = "ucPending" + dt.Rows[i]["cBUNRUI"].ToString();                //Session["BoardName"] = dt.Rows[i]["sBUNRUI"].ToString();                //Session["BoardID"] = dt.Rows[i]["cBUNRUI"].ToString();
                string taskcount = "";
                if (dt_label_koutei.Rows.Count > 0)
                {
                    taskcount = dt_label_koutei.Rows.Count.ToString();
                    //Session["TaskCount"] = dt_label_koutei.Rows.Count.ToString();
                }                else
                {
                    taskcount = "";
                    //Session["TaskCount"] = "";
                }                string color = "";                if (color_i == 0)
                {
                    color = "#7CD0FF";
                    color_i++;
                }                else if (color_i == 1)
                {
                    color = "#F65161";
                    color_i++;
                }                else if (color_i == 2)
                {
                    color = "#FDD853";
                    color_i++;
                }                else if (color_i == 3)
                {
                    color = "#FC78B9";
                    color_i++;
                }
                else if (color_i == 4)
                {
                    color = "#36C398";
                    color_i++;
                }
                else if (color_i == 5)
                {
                    color = "#AEDA49";
                    color_i++;
                }
                else if (color_i == 6)
                {
                    color = "#7D5CC1";
                    color_i++;
                }
                else if (color_i == 7)
                {
                    color = "#FF954A";
                    color_i = 0;
                }                ucBoard.SetPendingFusenBoardData(dt.Rows[i]["sBUNRUI"].ToString(), dt.Rows[i]["cBUNRUI"].ToString(), taskcount, color);
                ucBoard.EndKoutei_All += this.HandleEndKoutei_All;
                pnlPending.Controls.Add(ucBoard);

                //工程ボードの中に付箋を設定する
                Panel pnlFusen = (Panel)ucBoard.FindControl("pnlFusen");                if (dt_label_koutei.Rows.Count > 0)                {                    for (int j = 0; j < dt_label_koutei.Rows.Count; j++)                    {                        UC02Label ucLabelJouhou = (UC02Label)LoadControl("~/UserControl/UC02Label.ascx");                        ucLabelJouhou.ID = "uc" + (j + 1);                        ucLabelJouhou.SetFusenJouhou(dt_label_koutei.Rows[j], chk_gaozu.Checked);
                        ucLabelJouhou.EndKoutei += this.HandleEndKoutei;
                        pnlFusen.Controls.Add(ucLabelJouhou);                    }                }            }
            updFusenMain.Update();
        }

        protected void PinChange()
        {
            pnlFusenMain.CssClass = "M01FusenMainDiv";
            pnlPending.CssClass = "M01PendingDiv";
            div_board.Style.Add("display", "none");
            pnlFusenMain.Style.Add("display", "none");
            pnlFusenMain.Controls.Clear();
            pnlPending.Controls.Clear();
        }

        protected void btnFusenTsuika_Click(object sender, EventArgs e)
        {
            //Response.Write("<script language='javascript'>window.alert('確認してください。');</script>");
            //string jscript = "<script>alert('YOUR BUTTON HAS BEEN CLICKED')</script>";
            //System.Type t = this.GetType();
            //ClientScript.RegisterStartupScript(t, "k", jscript);

            //SessionUtility.SetSession("HOME", "Popup");
            //ifShinkiPopup.Src = "K2_Kouteipopup.aspx";
            //mpeShinkiPopup.Show();
            //updShinkiPopup.Update();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);
        }

        protected void btn_ClosePopup_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }

        protected void chk_santo_CheckedChanged(object sender, EventArgs e)
        {

            //get_data_DB();
            //BindBoard();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);
        }

        public void HandleEndKoutei(object sender, EventArgs e)        {
            //divLabelSave.Style["display"] = "flex";
            //updLabelSave.Update();            Button btnDelete = (sender as Button);            UC02Label uc02label = (UC02Label)btnDelete.NamingContainer;            Label lbcSHIJISYO = uc02label.FindControl("lbcSHIJISYO") as Label;            Label lblKouteiId = uc02label.FindControl("lblKouteiId") as Label;            Label lblmail = uc02label.FindControl("lblmail") as Label;
            Label lblsshijisyo = uc02label.FindControl("lblsSHIJISYO") as Label;
            Label lbltokui = uc02label.FindControl("lblsTokuisaki") as Label;            

            //UC01board ucBoard = (UC01board)LoadControl("~/UserControl/UC01board.ascx");

            //Panel pnlFusen = (Panel)ucBoard.FindControl("pnlFusen");
            //pnlFusen.Controls.Remove(uc02label);
            //updFusenMain.Update();

            K3_Label_DataGet_Class label = new K3_Label_DataGet_Class();            string cend_bunrui = label.Get_Endkoutei_count(lbcSHIJISYO.Text);            bool fend = false;            if (cend_bunrui == "1")            {                fend = true;            }            if (label.EndLable(lbcSHIJISYO.Text, lblKouteiId.Text, fend))            {
                //SpVoice spv = new SpVoice();
                //spv.Speak("おめでとうございます。");
                //spv.Rate = 1;
                get_data_DB();                BindBoard();                if (fend == true && lblmail.Text != "")                {
                    string mail_naiyou = "指示書番号：" + lbcSHIJISYO.Text +
                                      "\n件名：" + lblsshijisyo.Text +
                                      "\n得意先名：" + lbltokui.Text +
                                      "\n完了実施者：" + Session["sTantou"].ToString() +
                                      "\n\n上記案件が完了いたしました。";                    mailsend(lblmail.Text,mail_naiyou, lblsshijisyo.Text);                }                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);            }            else            {                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);                return;            }        }

        public void HandleEndKoutei_All(object sender, EventArgs e)
        {          

            Button btnDelete = (sender as Button);
            UC01board board = (UC01board)btnDelete.NamingContainer;
            Label lbcBUNRUI = board.FindControl("lblPendingHeader_ID") as Label;

            DataTable dtLabel = Session["dtLabel"] as DataTable;
            DataRow[] drresult = dtLabel.Select("cBUNRUI = " + lbcBUNRUI.Text);
            DataTable dt_label_koutei = dtLabel.Clone();
            if (drresult.Length > 0)
            {
                dt_label_koutei = drresult.CopyToDataTable();
                if (dt_label_koutei.Rows.Count > 0)                {                    for (int j = 0; j < dt_label_koutei.Rows.Count; j++)                    {
                        K3_Label_DataGet_Class label = new K3_Label_DataGet_Class();
                        string cend_bunrui = label.Get_Endkoutei_count(dt_label_koutei.Rows[j]["cSHIJISYO"].ToString());
                        bool fend = false;
                        if (cend_bunrui == "1")
                        {
                            fend = true;
                        }
                        if (label.EndLable(dt_label_koutei.Rows[j]["cSHIJISYO"].ToString(), dt_label_koutei.Rows[j]["cBUNRUI"].ToString(), fend))
                        {
                            if (fend == true && dt_label_koutei.Rows[j]["SMAIL"].ToString() != "")
                            {
                                string mail_naiyou = "指示書番号："+ dt_label_koutei.Rows[j]["cSHIJISYO"].ToString()+
                                    "\n件名：" +dt_label_koutei.Rows[j]["sSHIJISYO"].ToString() +
                                    "\n得意先名：" + dt_label_koutei.Rows[j]["sTOKUISAKI"].ToString() + 
                                    "\n完了実施者："+ Session["sTantou"].ToString() +
                                    "\n\n上記案件が完了いたしました。";
                                mailsend(dt_label_koutei.Rows[j]["SMAIL"].ToString(),mail_naiyou, dt_label_koutei.Rows[j]["sSHIJISYO"].ToString());
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);
                            return;
                        }                    }
                    get_data_DB();
                    BindBoard();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);
        }
        private void get_data_DB()        {            bool f_old_ver = false;            if( Session["f_old_ver"].ToString() == "true")
            {
                f_old_ver = true;
            }

            K_ClientConnection_Class test = new K_ClientConnection_Class();            DataTable dt = test.GetKoutei();            Session.Add("dt", dt);            K3_Label_DataGet_Class label = new K3_Label_DataGet_Class();            DataTable dtLabel = label.Get_Label(chk_santo.Checked, chk_gaozu.Checked, f_old_ver);            Session.Add("dtLabel", dtLabel);        }
        private void mailsend(string email_add,string mail_naiyou,string kenmei)        {            try            {
                //MailMessage message = Session["message"] as MailMessage;
                //message.To.Add(email_add);
                //SmtpClient smtp = Session["smtp"] as SmtpClient;

                string from, pass, messageBody = "";
                MailMessage message = new MailMessage();
                to = email_add;
                from = ConfigurationManager.AppSettings["SMTP_Sender"];
                pass = ConfigurationManager.AppSettings["SMTP_Password"];
                messageBody = mail_naiyou;
                message.To.Add(to);
                message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]);
                message.Body = messageBody;
                //message.Subject = "工程完了";
                message.Subject = "件名："+ kenmei + "の完了通知";
                 SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SMTP_Host"];
                smtp.Timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_Timeout"]);
                smtp.EnableSsl = true;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                smtp.Credentials = new NetworkCredential(from, pass);
                smtp.EnableSsl = true;                smtp.Send(message);            }            catch (Exception ex)            {                Response.Write("<script>alert('" + ex.Message + "');</script>");            }        }

        #region delete
        //private void messge_set()
        //{

        //    string from, pass, messageBody = "";
        //    string receivemail = ConfigurationManager.AppSettings["SMTP_Sender"];
        //    MailMessage message = new MailMessage();
        //    //to = "minazou0417@gmail.com";
        //    to = receivemail;
        //    from = ConfigurationManager.AppSettings["SMTP_Sender"];
        //    pass = ConfigurationManager.AppSettings["SMTP_Password"];
        //    messageBody = "工程完了しました。";
        //    message.To.Add(to);
        //    message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]);
        //    message.Body = messageBody;
        //    //message.Subject = "工程完了";
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = ConfigurationManager.AppSettings["SMTP_Host"];
        //    smtp.Timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_Timeout"]);
        //    smtp.EnableSsl = true;
        //    smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.UseDefaultCredentials = false;
        //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //    smtp.Credentials = new NetworkCredential(from, pass);
        //    smtp.EnableSsl = true;
        //    Session.Add("message", message);
        //    Session.Add("smtp", smtp);
        //}
        #endregion

        public void HandleTaskTsuika(object sender, EventArgs e)
        {
            //SessionUtility.SetSession("HOME", "Popup");
            //ifShinkiPopup.Src = "K2_Kouteipopup.aspx";
            //mpeShinkiPopup.Show();
            //updShinkiPopup.Update();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);
        }
        protected void chk_gaozu_CheckedChanged(object sender, EventArgs e)        {            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);        }

        protected void BT_LBSaveCross_Click(object sender, EventArgs e)
        {
            //divLabelSave.Style["display"] = "none";
            //updLabelSave.Update();
        }

        protected void LBT_logout_Click(object sender, EventArgs e)        {            Response.Redirect("K3_Login.aspx");        }
    }
}
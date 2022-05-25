using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Service;
namespace Koutei.WebFront
{
    public partial class K3_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.Cookies["cTantou"] != null && Request.Cookies["pwd"] != null)
                    {
                        TB_ctantousha.Text = Request.Cookies["cTantou"].Value;
                        TB_password.Attributes["value"] = Request.Cookies["pwd"].Value;
                        string stantou = K3Login_Class.Get_tantou(TB_ctantousha.Text);
                        if (!String.IsNullOrEmpty(stantou))
                        {
                            TB_stantousha.Text = stantou;
                            TB_password.Focus();

                        }
                        CHK_savepwd.Checked = true;
                    }
                    K_ClientConnection_Class get_ver = new K_ClientConnection_Class();                    string ver = get_ver.get_sCUSTOMER_USER_VERSION();                    if (ver != "")                    {                        int index1 = ver.IndexOf('.', 2);                    }                    if (ver == "2.0" || ver == "2.1" || ver == "2.2" || ver == "2.3")                    {
                        //Session.Add("f_old_ver", "true");
                        SessionUtility.SetSession("f_old_ver", "true");                    }                    else                    {
                        //Session.Add("f_old_ver", "false");
                        SessionUtility.SetSession("f_old_ver", "false");                    }
                }
                catch { Response.Write("<script language='javascript'>window.alert('データベースに接続できません。管理者にお問合せ下さい。');</script>"); }
            }
        }

        protected void BT_login_Click(object sender, EventArgs e)
        {
            string pass = "";
            LB_Code_Error.Text = "";
            LB_Pass_Error.Text = "";
            if(String.IsNullOrEmpty(TB_ctantousha.Text) || String.IsNullOrEmpty(TB_password.Text))
            {
                if (String.IsNullOrEmpty(TB_ctantousha.Text))
                {
                    LB_Code_Error.Text = "ログインIDを入力してください。";
                }
                if (String.IsNullOrEmpty(TB_password.Text))
                {
                    LB_Pass_Error.Text = "パスワードを入力してください。";

                }
            }
            else
            {
                pass = TextUtility.DecryptData_Henkou(K3Login_Class.Get_Password(TB_ctantousha.Text));
                if (pass != "")
                {
                    if (pass == TB_password.Text)
                    {
                        if (CHK_savepwd.Checked)
                        {
                            Response.Cookies["cTantou"].Value = TB_ctantousha.Text;
                            Response.Cookies["pwd"].Value = TB_password.Text;
                            Response.Cookies["cTantou"].Expires = DateTime.Now.AddYears(1);
                            Response.Cookies["pwd"].Expires = DateTime.Now.AddYears(1);
                        }
                        else
                        {
                            Response.Cookies["cTantou"].Expires = DateTime.Now.AddYears(-1);
                            Response.Cookies["pwd"].Expires = DateTime.Now.AddYears(-1);
                        }
                        Session["cTantou"] = TB_ctantousha.Text;
                        Session["sTantou"] = TB_stantousha.Text;
                        Response.Redirect("K1_Main.aspx");

                    }
                    else
                    {
                        LB_Pass_Error.Text = "パスワードが正しくありません。";
                    }

                }
            }
            
        }

        protected void TB_ctantousha_TextChanged(object sender, EventArgs e)        {            LB_Code_Error.Text = "";            LB_Pass_Error.Text = "";            if (!String.IsNullOrEmpty(TB_ctantousha.Text))            {                string ctantou = TB_ctantousha.Text.ToString().PadLeft(4, '0');                TB_ctantousha.Text = ctantou;                string stantou = K3Login_Class.Get_tantou(TB_ctantousha.Text);                if (!String.IsNullOrEmpty(stantou))                {                    TB_stantousha.Text = stantou;                    TB_password.Focus();                }                else                {                    LB_Code_Error.Text = "ログインIDが正しくありません。";                    //TB_ctantousha.Text = string.Empty;                    TB_stantousha.Text = string.Empty;                    TB_ctantousha.Focus();                }            }

        }
    }
}
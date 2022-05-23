using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Service;

namespace Koutei_task_Manager
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DBUtilitycs.Server == "")
                {
                    DBUtilitycs.get_connetion_ifo();
                }
                K_ClientConnection_Class get_ver = new K_ClientConnection_Class();
                string ver= get_ver.get_sCUSTOMER_USER_VERSION();
                if (ver != "")
                {
                    int index1 = ver.IndexOf('.', 2);
                    ver = ver.Substring(0, index1);
                }
               
                if (ver == "2.0" || ver == "2.1" || ver == "2.2" || ver == "2.3")
                {
                    Session.Add("f_old_ver", "true");
                }
                else
                {
                    Session.Add("f_old_ver", "false");
                }
                // ログインメインページへ移動する
                Response.Redirect("WebFront/K3_Login.aspx");
            }
        }
    }
}
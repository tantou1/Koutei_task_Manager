<%@ Page   Language="C#" AutoEventWireup="true" CodeBehind="K1_Main.aspx.cs" Inherits="Koutei_task_Manager.WebFront.K1_Main" 
    EnableEventValidation = "false" MaintainScrollPositionOnPostback="true" ValidateRequest ="False" Async="true"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="ja"><head runat="server">    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>    <link href="../Content/bootstrap.min.css" rel="stylesheet" />    <script src="../Scripts/bootstrap.bundle.min.css"></script>  
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="Scripts/jquery.js"></script>
    <asp:PlaceHolder runat="server">        <%: Styles.Render("~/style/StyleBundle2") %>        <%: Styles.Render("~/style/UCStyleBundle") %>        <%: Scripts.Render("~/scripts/ScriptBundle1") %>       </asp:PlaceHolder>  
       
    
  <script>
      window.onload = function() {		
		//ここで本体を表示にさせる
          document.getElementById('div_board').style.display = 'block';
          
            //alert('ページの読み込みが完了したよ！');   
}
</script>
    <title></title>
    <style>        @font-face {            font-family: 'Inter-Regular';            src: url('../Font/Inter-Regular.ttf') format('truetype');        }         .bigcheck input {                width: 21px;                height: 21px;                vertical-align: middle;                cursor: pointer;                margin-right: 5px;                        }         .navbar-nav li:hover>.dropdown-menu {              display: block;            }         .dropdown-menu-right {             right: 0;             left: auto;         }         .nav-item a:hover        {            color: white;            cursor: pointer;            background-color:#14B0F2;                              }         .dropdown-font{             font:"Inter-Regular";             font-size:20px;         }         .nav-font{             font-family: 'Inter-Regular';             font-size:24px;             font-weight:900;         }                  .btn-font{             font-family: 'Inter-Regular';             font-size:12px;             font-weight:400;         }    </style>
</head>
  
<body style="background-color:#FFFFFF;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <%--Framework Scripts--%>                
                <%-- <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />--%>
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </Scripts>           
        </asp:ScriptManager>

            <nav class="navbar navbar-expand-lg navbar-dark fixed-top" style="background-color:#7CD0FF; height: 45px;">
                <div class="container d-flex justify-content-center""  >
                    <a class="navbar-brand nav-font" href="#" > 工程ボート</a>                    
                </div>
                
                <div style=" z-index: 91;padding:4px 16px 0px 16px;height:40px; margin-bottom:13px;" >                 <div class="collapse navbar-collapse justify-content-end " id="navbarNavDropdown" >                    <ul class="navbar-nav me-auto dropdown-menu-start" >                                            <li class="nav-item dropdown" style=" height: 45px;">                        <a class="nav-link dropdown-toggle dropdown-fon" href="#" id="navbarDropdownMenuLink" runat="server" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="color:#FFFFFF;height:45px;">                            aye                        </a>                        <div class="dropdown-menu dropdown-menu-right text-center" aria-labelledby="navbarDropdownMenuLink">                          <asp:LinkButton ID="LBT_logout" runat="server" class="dropdown-item dropdown-fon" OnClick="LBT_logout_Click">ログアウト</asp:LinkButton>                        </div>                      </li>                    </ul>                  </div>                    </div>
            </nav>
             
        <%-- <asp:UpdatePanel runat="server" ID="updLabelSave" UpdateMode="Conditional">
             <ContentTemplate>
                <div class="success JCSuccess" id="divLabelSave" runat="server" style="position:fixed;display:none;height:30pt;width:100vw; left:0;background-color:#92d050;align-content:center;align-items:center; border-radius:7px;padding-left:10px;margin:2px;" >
                    <asp:Label ID="LB_Save" Text="" runat="server" ForeColor="White" Font-Size="13px"></asp:Label>
                    <asp:Button id="BT_LBSaveCross" Text="✕" runat="server" style="background-color:white;border-style:none;right:10px;position:absolute;" OnClick="BT_LBSaveCross_Click"  />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>

        <div class="container-fluid" >
            <div style="margin-left:12px; margin-top: 55px;">
            <div class="row ">

                <div class="col col-md-auto">
                    <asp:Button ID="btn_SaishinJyouhou" runat="server" Text="最新情報を表示" role="button" CssClass="UC01SaishinjyouhouBtn UC01MobileSaishinjyouhouBtn btn-font"
                    onmousedown="getAllDivScrollPosition('pnlFusenMain','MASTER');"  OnClientClick="displayLoadingModal();" /> <br />
                     
                </div>

                <%--<div class="col col-md-auto">
                    <asp:Button ID="btnFusenTsuika" runat="server" Text="＋タスクを追加" CssClass="UC01FusentSuikaBtn UC01MobileFusentSuikaBtn ml-3" role="button"
                    onmousedown="getAllDivScrollPosition('pnlFusenMain','MASTER');"  OnClientClick="displayLoadingModal();" OnClick="btnFusenTsuika_Click"/> <br />
                     
                </div>--%>
                <div class="col col-md-auto align-content-center mt-1">
                    <asp:CheckBox ID="chk_gaozu" runat="server" AutoPostBack="True" Text="画像を表示" CssClass="bigcheck btn-font" Onchange="displayLoadingModal();"/>
                </div> 

                <div class="col col-md-auto align-content-center mt-1">
                    <asp:CheckBox ID="chk_santo" runat="server" AutoPostBack="True" Text="先頭工程のみ" CssClass="bigcheck ms-3 btn-font" OnCheckedChanged="chk_santo_CheckedChanged" Onchange="displayLoadingModal();"/>
                </div> 

                 <asp:UpdatePanel ID="UpdTaskTsuika" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
                        <asp:Panel ID="pnlTask" runat="server"></asp:Panel>               
                    </ContentTemplate>
                </asp:UpdatePanel>

                 
            </div>
        </div>    
     
       
        <div class="row " id="div_board" runat="server" style="display:none;">
        <asp:UpdatePanel ID="updFusenMain" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:Panel ID="pnlPending" runat="server" class="M01PendingDiv"></asp:Panel>
                <asp:Panel ID="pnlFusenMain" runat="server" class="M01FusenMainDiv"></asp:Panel>               
             </ContentTemplate>
        </asp:UpdatePanel>
            
        </div>
    </div>        
        
        <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                        PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none;height:100%;overflow:hidden;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlShinkiPopup" runat="server" BorderStyle="None">
                            <iframe id="ifShinkiPopup" runat="server" scrolling="yes"  style="height:100vh;width:100vw;border:none;"></iframe>
                            <div style="display:none;">
                                <asp:Button ID="btn_ClosePopup" runat="server" Text="Button" CssClass="" OnClick="btn_ClosePopup_Click" />
                                <asp:Button ID="btn_SavePopup" runat="server" Text="Button" CssClass="" />
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

         
    </form>
</body>
</html>

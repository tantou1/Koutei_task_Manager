<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="K3_Login.aspx.cs" Inherits="Koutei.WebFront.K3_Login" %><!DOCTYPE html><html xmlns="http://www.w3.org/1999/xhtml"><head runat="server"><meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>    <title></title>    <link href="../Content/bootstrap.min.css" rel="stylesheet" />    <script src="../Scripts/bootstrap.bundle.min.js"></script>    <style>
        @font-face {
            font-family: 'Inter-Regular';
            src: url('../Font/Inter-Regular.ttf') format('truetype');
        }        .K3Login {            max-width: 450px !important;            width: 450px !important;            margin-left: auto;            margin-right: auto;        }        @media only screen and (max-width: 1160px) {            .JC01Login {                max-width: 463px !important;                width: 463px !important;                margin-left: auto;                margin-right: auto;            }        }        .LabelFont {            font-family: 'Inter-Regular';            font-weight: 400;            font-size: 20px;            text-align: center;        }        .LoginFont {            font-family: 'Inter-Regular';            font-size: 16px;        }        .textFont{
            font-family: 'Inter-Regular';            font-size: 14px;
        }        .button {            border-radius: 5px;            outline: 0;            cursor: pointer;            border: 1px solid #14B0F2;            background-color: #14B0F2;            color: White;            width:90px;            height:35px;        }        .button:hover, .button:focus {            border-color: #14B0F2;            border-width: 2px;            background-color: #7CD0FF;            color: White;        }                    .bigcheck input {
                width: 21px;
                height: 21px;
                vertical-align: middle;
                cursor: pointer;
                margin-right: 4px; 
                margin-left:3px;
                margin-bottom:3px;
        }

        .error {
            font-family: 'Inter-Regular';
            font-size: 11px;
            color: red;
        }        .col-md-5{
            width:547px;
        }    </style>    <script type='text/javascript'>
    function OnlyNumericEntry() 
    {
        if ((event.keyCode < 48 || event.keyCode > 57)) {
            event.returnValue = false;
        }
    }

    function Login() 
    {
        if (event.keyCode == 13) {

            document.getElementById("BT_Login").click();
            __doPostBack();
        } 
    }
  </script></head><body style="background-color:#F9FCF1;">    <form id="form1" runat="server">        <div class="container-fluid">           <div class="row align-items-center" style="height:100vh;">            <div class="col-md-5 mx-auto">                <div class="login-form bg-white p-4 shadow-lg" style="border-radius:6px;">                    <div class="row justify-content-center align-content-center mt-3">                        <label class="col-form-label LabelFont">工程タスク管理ログイン</label>                        </div>                    <div style="border-bottom: 3px solid #7CD0FF; padding-bottom: 15px;"></div>                    <div class="form-group row  " style="margin-top:30px;">                                                 <label class="col-xl-3 offset-xl-1 col-form-label LoginFont mt-1 " >ログインID</label>                                      <div class="col-xl-2 mt-1">                            <asp:TextBox ID="TB_ctantousha" runat="server" CssClass="form-control LoginFont " TabIndex="1" MaxLength="4" AutoPostBack="True" OnTextChanged="TB_ctantousha_TextChanged" onkeypress="OnlyNumericEntry()" Width="70px" ></asp:TextBox> <%--Width="70"--%>                            </div>                        <div class="col-xl-5 mt-1">                            <asp:TextBox ID="TB_stantousha" runat="server" CssClass="form-control LoginFont" TabIndex="-1"  ReadOnly="true"></asp:TextBox>                            </div>                        <div class="col-xl-7 offset-xl-4 mb-1" style="height:1px;">
                                <asp:Label ID="LB_Code_Error" runat="server" Text=" " CssClass="error"></asp:Label>
                            </div>                         </div>                    <div class="form-group row mt-4">                         <label class="col-xl-3 offset-xl-1 col-form-label LoginFont " >パスワード</label>                         <div class="col-xl-7">                            <asp:TextBox ID="TB_password" runat="server" CssClass="form-control LoginFont " TabIndex="2" TextMode="Password" onkeypress="Login();" MaxLength="8" ></asp:TextBox>                                                        </div>                        <div class="col-xl-7 offset-xl-4 mb-1" style="height:1px;">
                                <asp:Label ID="LB_Pass_Error" runat="server" Text=" " CssClass="error"></asp:Label>
                            </div>                        </div>                    <div class="form-group row mt-4">                        <div class="col-xl-6 offset-xl-4 ">                            <asp:CheckBox ID="CHK_savepwd" runat="server" TabIndex="-1"  Text="パスワード保存する" CssClass="ml-2 bigcheck textFont" />                          </div>                        </div>                    <div class="row justify-content-center align-content-center mt-4">                        <asp:Button ID="BT_login" runat="server" Text="ログイン" type="button" TabIndex="3" CssClass=" button mb-3 textFont"  OnClick="BT_login_Click" /><%--Width="98px" Height="45px"--%>                      </div>                         </div>                </div>               </div>            </div>    </form></body></html>
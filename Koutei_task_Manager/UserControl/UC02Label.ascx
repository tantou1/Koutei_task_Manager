<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC02Label.ascx.cs" Inherits="Koutei_task_Manager.UserControl.UC02Label" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<script>
  function playSound() {
  var audio = new Audio('../Sound/ganbattane.mp3');
      audio.play();
    }    
</script>

<body >
<div class="UC02FusenDiv" id="divFusenJouhou" runat="server">
    <div>
        <div class="row " style="position: relative; top: 0; left:0px;">
            <div class="col-auto me-auto">
                <asp:Label ID="lblSHIJISYO_Hyouji" runat="server" CssClass="UC02AnkenIdLbl" />
            </div>
            <div class="col-auto">
                <asp:Label ID="lbldkanryouyotei" runat="server" CssClass="UC02Lbl" />
            </div>
        </div>
        <div class="row" style="position: relative; top:0; left:0px;">
        <asp:Label ID="lblsSHIJISYO" runat="server" CssClass="UC02Lbl UC02Label" />
        </div>
        <div class="row" style="position: relative; top: 0; left:0px;">
        <asp:Label ID="lblsTokuisaki" runat="server" CssClass="UC02Lbl UC02Label" />
        </div>
        <asp:Label ID="lblKouteiId" runat="server" CssClass="DisplayNone"/>
        <asp:Label ID="lblKouteiName" runat="server" CssClass="DisplayNone"/>
        <asp:Label ID="lblLabelOrder" runat="server" CssClass="DisplayNone"/> 
        <asp:Label ID="lbcSHIJISYO" runat="server" CssClass="DisplayNone" />
        <asp:Label ID="lblmail" runat="server" CssClass="DisplayNone" />
    </div>
    <div id="div_img" class="text-center" runat="server">
            <asp:Image ID="Image" runat="server" Height="79" class="rounded" CssClass="mb-1" />    
    </div>       
        <div style="position: relative; top:-10px; left:0px;">
             <asp:Label ID="lbleigyou" runat="server" CssClass="UC02AnkenIdLbl" />
            <asp:Button ID="bt_end" runat="server" Text="" CssClass="WhiteBackgroundButton1" OnClick="Button1_Click" OnClientClick="playSound();displayLoadingModal();"/>
        </div>
    
</div>
</body>
</html>


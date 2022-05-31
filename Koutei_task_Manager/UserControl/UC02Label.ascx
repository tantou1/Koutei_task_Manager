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
<div class="task" id="divFusenJouhou"  runat="server">    <asp:Label ID="lblSHIJISYO_Hyouji" runat="server" CssClass="scode" />    <asp:Label ID="lbldkanryouyotei" runat="server" CssClass="date" />    <asp:Label ID="lblsSHIJISYO" runat="server" CssClass="kenmei" />    <asp:Label ID="lblsTokuisaki" runat="server" CssClass="sTokuisaki" />    <asp:Label ID="lblKouteiId" runat="server" CssClass="DisplayNone"/>    <asp:Label ID="lblKouteiName" runat="server" CssClass="DisplayNone"/>    <asp:Label ID="lblLabelOrder" runat="server" CssClass="DisplayNone"/>     <asp:Label ID="lbcSHIJISYO" runat="server" CssClass="DisplayNone" />    <asp:Label ID="lblmail" runat="server" CssClass="DisplayNone" />    <div id="div_img" class="image" runat="server">            <asp:Image ID="Image" runat="server"/>             </div>      <asp:Label ID="lbleigyou" runat="server" CssClass="tantou" />     <asp:Button ID="bt_end" runat="server" Text="" CssClass="circle" OnClick="Button1_Click" OnClientClick="displayLoadingModal();"/></div>
</body>
</html>


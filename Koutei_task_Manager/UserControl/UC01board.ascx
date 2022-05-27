<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC01board.ascx.cs" Inherits="Koutei_task_Manager.UserControl.UC01board" %>
<!DOCTYPE html>

<html >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<style>
         
         /*#title_bar{
             width:100%;
             height:1%;
             background-color:lightgreen;
         }*/
         body,html{
             margin:0;
         }
</style>
     <script>
  function playSound() {
  var audio = new Audio('../Sound/ganbattane.mp3');
      audio.play();
    }    
</script>
</head>
<body style="overflow-x:hidden;">
<div id="divFusenList" class="UC01BoardDiv CustomScroll" runat="server">
    <div id="title_bar" runat="server"> <br /></div>
    <div id="divPendingHeader" class="UC01FusenTopDiv DisplayNone" runat="server">
        <div class="UC01InnerTopDiv d-flex justify-content-center " id="div_innertop" runat="server" >
            <asp:Label ID="lblPendingHeader" runat="server" class="headerFont" Text="保留中のボード"></asp:Label>
            <asp:Label ID="lblcount" runat="server" class="UC01TaskCount" Text=""></asp:Label>
            <asp:Label ID="lblPendingHeader_ID" runat="server" class="font-weight-bold" Text="0000" Visible="False"></asp:Label>
            <asp:Button ID="bt_end" runat="server" Text="〇" CssClass="WhiteBackgroundButton1 " OnClientClick="playSound();displayLoadingModal();" OnClick="bt_end_Click" />
           
        </div>
        
</div>
<asp:Panel ID="pnlFusen" runat="server" CssClass="UC01FusenInfoDiv"></asp:Panel>
</div>
</body>

</html>

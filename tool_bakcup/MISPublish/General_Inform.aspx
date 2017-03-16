<%@ page language="C#" autoeventwireup="true" inherits="General_Inform, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="default.css" type="text/css" rel="stylesheet" />
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="bordertable" align="center" cellpadding="4" cellspacing="6" >
    <tr align="center">
    <td style="width: 150px">
    <asp:Button Text="Journal Style" Width="120" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=1','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Btn1"  runat="server" />
    </td>
    <td style="width: 150px">
    <asp:Button Text="Journal Category" Width="130" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=2','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Btn2"  runat="server" />
    </td>
    <td style="width: 150px">
    <asp:Button Text="Cover Material" Width="120" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=3','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Btn3"  runat="server" />
    </td>
    </tr>
    
    <tr align="center">
    <td style="width: 150px">
    <asp:Button Text="Paper Type" Width="120" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=4','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Button1"  runat="server" />
    </td>
    <td style="width: 150px">
    <asp:Button Text="Trim Size/Code" Width="130" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=5','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Button2"  runat="server" />
    </td>
    <td style="width: 150px">
    <asp:Button Text="Cover/Paper GSM" Width="120" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=6','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Button3"  runat="server" />
    </td>
    </tr>
    
    <tr align="center">
    <td style="width: 150px">
    <asp:Button Text="Digital Products" Width="120" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=7','Journal_Style','width=500,height=200,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Button4"  runat="server" />
    </td>
    <td style="width: 150px">
    <asp:Button Text="Typesetting Platforms" Width="130" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=8','Journal_Style','width=500,height=200,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Button5"  runat="server" />
    </td>
    <td style="width: 150px">
    <asp:Button Text="Currency" Width="120" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=9','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Button6"  runat="server" />
    </td>
    </tr>
    
    <tr align="center">
    <td style="width: 150px">
    <asp:Button Text="Country" Width="120" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=10','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Button7"  runat="server" />
    </td>
    <td style="width: 150px">
    <asp:Button Text="Sale Lead Category" Width="130" BorderWidth="0" height="20" 
    OnClientClick="javascript:Journal_Style=window.open('General_PopUp.aspx?id=11','Journal_Style','width=500,height=150,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');Journal_Style.focus()"
    ID="Button8"  runat="server" />
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>

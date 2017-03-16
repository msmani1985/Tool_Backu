<%@ Page Language="C#" AutoEventWireup="true" CodeFile="modulelist.aspx.cs" Inherits="modulelist" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Project and Module List Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Project and Module List    
    </div>
    <div>
        <table align="center" class="bordertable">
        <tr><td>From Date</td><td><asp:TextBox ID="txt_fromdate" runat="server"></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_fromdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td><td>To Date</td><td><asp:TextBox ID="txt_todate" runat="server"></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_todate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td><td>Type</td><td><asp:RadioButtonList ID="rl_type" runat="server" RepeatDirection="horizontal"><asp:ListItem Text="Received" Value="Received" Selected="true"></asp:ListItem><asp:ListItem Text="WIP" Value="WIP"></asp:ListItem><asp:ListItem Text="Despatched" Value="Despatched"></asp:ListItem></asp:RadioButtonList></td>
        <td><asp:Button ID="btn_submit" Text="Submit" runat="server" CssClass="dpbutton" OnClick="btn_submit_Click" /></td>
        </tr>
        </table><br />
        <CR:CrystalReportViewer ID="CV_modulelist" runat="server" AutoDataBind="true" HasToggleGroupTreeButton="false" EnableDatabaseLogonPrompt="false" />
    </div>
    </form>
</body>
</html>

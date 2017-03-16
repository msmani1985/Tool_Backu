f<%@ page language="C#" autoeventwireup="true" inherits="sqlProductivityReport, App_Web_w6b3pav3" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="titlediv" class="dptitle">
        Productivity Report
    </div>
    <div align="center">
        <table width="700px" class="bordertable">
            <tr>
                <td align="right">Type:</td>
                <td>
                    <asp:RadioButtonList ID="rbtn" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtn_SelectedIndexChanged" AutoPostBack="True" >
                    <asp:ListItem Text="Operator" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Team" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td><asp:Label ID="Lblemp" Text="" runat="server"></asp:Label></td>
                <td><asp:DropDownList ID="ddlemp" runat="server" DataTextField="EMP_FULLNAME" DataValueField="EMPLOYEE_ID"></asp:DropDownList>
                </td>
                <td rowspan="2" valign="middle"><asp:Button ID="BtnSubmit" Text="Submit" CssClass="dpbutton" runat="server" OnClick="BtnSubmit_Click" /></td>
            </tr>
            <tr>
                <td align="right">Start Date:</td>
                <td><asp:TextBox ID="Txtsdate" Text="" runat="server"></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td align="right">End Date:</td>
                <td><asp:TextBox ID="Txtedate" runat="server"></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtedate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                
            </tr>
        </table>
    </div>
    <br />
    <div>
        <CR:CrystalReportViewer ID="CRViewerProductivity" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        ReuseParameterValuesOnRefresh="true" DisplayGroupTree="false" HasCrystalLogo="false" HasDrillUpButton="false" HasGotoPageButton="false" 
        HasRefreshButton="false" HasSearchButton="false" HasToggleGroupTreeButton="false" HasViewList="false" HasZoomFactorList="false"   />
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="sqlcoactionstagereport, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Co-Action Report Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="div_title" runat="server">
        Co-Action Publishing Report
    </div>
    <div>
        <table align="center" class="bordertable" width="650px">
            <tr>
                <td align="right">Customer :</td>
                <td colspan="3"><b style="font-size:larger;">Co-Action Publishing</b></td>
                <td rowspan="3"><asp:Button ID="Btn_showrpt" Text="Report" runat="server" CssClass="dpbutton" OnClick="Btn_showrpt_Click" /></td>
            </tr>
            <tr>
                <td align="right">From Date :</td>
                <td><asp:TextBox ID="TxtSDate" Text="" runat="server"></asp:TextBox> 
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=TxtSDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td align="right">To Date :</td>
                <td><asp:TextBox ID="TxtEDate" Text="" runat="server"></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=TxtEDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                
            </tr>
            <tr>
                <td align="right">Journal/ManuscriptId :</td>
                <td colspan="3"><asp:TextBox ID="Txtmanuscriptid" runat="server" Text="" Width="354px" ToolTip="Give ManuscriptId or Journal"></asp:TextBox></td>
                
            </tr>
            <tr><td colspan="4"><div id="div_submission" runat="server"><table><tr><td>Select Submission Due:</td>
            <td><asp:RadioButtonList ID="rb_submisstiondue" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rb_submisstiondue_SelectedIndexChanged"><asp:ListItem Text="Portico" Value="[Portico Submission]"></asp:ListItem><asp:ListItem Text="PMC" Value="[PMC Submission]"></asp:ListItem><asp:ListItem Text="DOAJ" Value="[DOAJ Submission]"></asp:ListItem><asp:ListItem Text="DOI" Value="[DOI Submission]"></asp:ListItem></asp:RadioButtonList></td></tr></table></div></td></tr>
        </table>
    </div>
    <br />
    <div align="center">
        <asp:GridView ID="GVCoActionReport" runat="server" CssClass="lightbackground" 
        HeaderStyle-CssClass="darkbackground" AlternatingRowStyle-CssClass="dullbackground" 
        BorderColor="Black" BorderWidth="1px" width="650px" >
        </asp:GridView>
    </div>
    <div id="div_Error" runat="server" Class="error"></div>
    </form>
</body>
</html>

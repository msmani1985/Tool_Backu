<%@ page language="C#" autoeventwireup="true" inherits="jobreport_team, App_Web_lruasnqi" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Jobs Report - Teamwise </title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Jobs Report - Teamwise
    </div>
    <div align="center">
        <table class="bordertable" border="1px" ><tr><td> Select Team :
        <asp:DropDownList ID="dd_teamlist" runat="server" DataValueField="sales_job_group_id" DataTextField="sales_group_name"></asp:DropDownList>
        </td><td>Job Type :</td><td><asp:DropDownList ID="dd_job_type" runat="server">
        <asp:ListItem Value="6" Text="Issues"></asp:ListItem><asp:ListItem Value="5" Text="Articles"></asp:ListItem>
        <asp:ListItem Value="2" Text="Books"></asp:ListItem><asp:ListItem Value="7" Text="Chapter"></asp:ListItem>
        <asp:ListItem Value="4" Text="Projects"></asp:ListItem><asp:ListItem Value="8" Text="Module"></asp:ListItem>
        </asp:DropDownList></td>
        <td ><asp:RadioButtonList RepeatDirection="horizontal" ID="rb_job_type" runat="server"><asp:ListItem Text="Live Jobs" Value="1" Selected="true"></asp:ListItem> <asp:ListItem Text="Completed Jobs" Value="2"></asp:ListItem><asp:ListItem Text="Invoiced Jobs" Value="3"></asp:ListItem></asp:RadioButtonList></td>
        <td><asp:Button CssClass="dpbutton" ID="btn_jobrpt_teamwise" Text="Submit" runat="server" OnClick="btn_jobrpt_teamwise_Click" /></td>
        </tr></table>
    </div>
    <br />
    <div id="div_jobreport" runat="server" align="center">
    <table width="98%"><tr><td align="right"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel_joblist" OnClick="exportExcel_joblist_Click" /></td></tr>
        <tr><td><asp:GridView ID="gv_reportdetails" runat="server" AutoGenerateColumns="true" OnSorting="gv_reportdetails_Sorting" AllowSorting="true"
        HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" >
        <HeaderStyle CssClass="GVFixedHeader" />
        </asp:GridView></td></tr>
        </table>
        
    </div>
    <div id="div_Error" runat="server" class="error"></div>
    </form>
</body>
</html>

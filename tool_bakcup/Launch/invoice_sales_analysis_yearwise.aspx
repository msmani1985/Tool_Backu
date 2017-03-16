<%@ page language="C#" autoeventwireup="true" inherits="invoice_sales_analysis_yearwise, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Invoice Sales Analysis Yearwise</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Invoice Sales Analysis
    </div>
    <div>
        <table class="bordertable" align="center">
        <tr><td>Customer</td><td><asp:DropDownList ID="dd_customer" runat="server" Width="200px" DataTextField="custname" DataValueField="custno"></asp:DropDownList></td>
        <td>Job Type</td><td><asp:DropDownList ID="dd_jobtype" runat="server"><asp:ListItem Text="--All--" Value="0"></asp:ListItem>
        <asp:ListItem Text="Journal" Value="1"></asp:ListItem><asp:ListItem Text="Book" Value="2"></asp:ListItem>
        <asp:ListItem Text="Project" Value="3"></asp:ListItem></asp:DropDownList></td>
        <td>Month</td><td><asp:DropDownList ID="dd_month" runat="server">
        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
        <asp:ListItem Text="March" Value="3"></asp:ListItem>
        <asp:ListItem Text="Apirl" Value="4"></asp:ListItem>
        <asp:ListItem Text="May" Value="5"></asp:ListItem>
        <asp:ListItem Text="June" Value="6"></asp:ListItem>
        <asp:ListItem Text="July" Value="7"></asp:ListItem>
        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
        </asp:DropDownList></td>
        <td>Year Start</td><td><asp:DropDownList ID="dd_startyear" runat="server"><asp:ListItem Text="2009" Value="2009"></asp:ListItem><asp:ListItem Text="2010" Value="2010"></asp:ListItem><asp:ListItem Text="2011" Value="2011"></asp:ListItem><asp:ListItem Text="2012" Value="2012"></asp:ListItem><asp:ListItem Text="2013" Value="2013"></asp:ListItem></asp:DropDownList></td>
        <td>Year End</td><td><asp:DropDownList ID="dd_endyear" runat="server"><asp:ListItem Text="2009" Value="2009"></asp:ListItem><asp:ListItem Text="2010" Value="2010"></asp:ListItem><asp:ListItem Text="2011" Value="2011"></asp:ListItem><asp:ListItem Text="2012" Value="2012"></asp:ListItem><asp:ListItem Text="2013" Value="2013"></asp:ListItem></asp:DropDownList></td>
        <td><asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="dpbutton" OnClick="btn_submit_Click" /></td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>

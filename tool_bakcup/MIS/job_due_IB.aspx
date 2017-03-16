<%@ Page Language="C#" AutoEventWireup="true" CodeFile="job_due_IB.aspx.cs" Inherits="job_due_IB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Jobs Due</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Jobs Due
    </div>
    <div>
        <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
                <tr><td>Customer</td><td>
                 <asp:DropDownList ID="ddlcustomer" runat="server" DataTextField="CUSTNAME" DataValueField="CUSTNO">
                 </asp:DropDownList>
                </td>
                <td>Type</td><td>
                <asp:DropDownList ID="ddljobtype" runat="server"  >
                    <asp:ListItem Value="6" text="Issue" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="5" text="Article"></asp:ListItem>
                </asp:DropDownList> 
                </td>
                <td align="center" >
                <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="Submit_btn" OnClick="Submit_btn_Click" />
                </td></tr>
            </table>
    </div>
    <br />
    <div style="width:90%;text-align:right;"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  /></div>
    <br />
    <div align="center"><asp:GridView ID="gv_duereport" runat="server" AutoGenerateColumns="true"  width="97%"
    HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" BorderColor="Black" BorderWidth="1px">
    </asp:GridView></div>
    <div id="div_error" runat="server"></div>
    </form>
</body>
</html>

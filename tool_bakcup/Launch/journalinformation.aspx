<%@ page language="C#" autoeventwireup="true" inherits="journalinformation, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
<link href="default.css" type="text/css" rel="stylesheet" />
    <title>Journal Information Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div >
        <table align=center class='bordertable' width="100%" >
            <tr ><td id="trCustomerRow" runat="server" >Select Customer:</td><td id="tdShowCustList" runat="server">
                <asp:DropDownList ID="ddlCustomers" runat="server" DataTextField="Cust_name" DataValueField="Customer_id" AutoPostBack="True">
                </asp:DropDownList>
                <div id="divNameCustomer" style="font-weight:bold;font-size:10pt;" runat="server"></div>
                &nbsp;<asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" Text="Submit" OnClick="btnSubmit_Click"  Visible="false" />
            </td>
            <td id="tdReturnCustList" runat="server"><asp:Button ID="btnReturn" runat="server" Text="Return to Customer List" CssClass="dpbutton" style="width:160px;" OnClick="btnReturn_Click" Visible="false" /></td>
            </tr>
            <tr>
                <td colspan="3" >
                <div id="divjournaldetails"  runat="server" style="float:left;width:65%;font-family:Candara;font-size:12pt;"></div>
                <div style="float:left"><asp:ImageButton ImageUrl="~/images/Excel.jpg" ID="imgExcelExport" runat="server" OnClick="imgExcelExport_Click"  Visible="false" /></div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

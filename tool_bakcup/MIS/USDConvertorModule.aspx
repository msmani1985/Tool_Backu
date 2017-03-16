<%@ Page Language="C#" AutoEventWireup="true" CodeFile="USDConvertorModule.aspx.cs" Inherits="USDConvertorModule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href=default.css type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="dptitle">USD Convertor</div>
   
    <table align="center" width="45%" class="bordertable" id="TABLE1">
    <tr><td>
        <asp:Label ID="Label2" runat="server" Text="Dollar Value" ></asp:Label></td>
        <td><asp:textbox ID="txtDollar"  runat="server" Width="161px"></asp:textbox></td></tr>
     <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Invoice No." ></asp:Label></td>
        <td><asp:textbox ID="txtInvNo"  runat="server" Width="161px"></asp:textbox></td></tr>   
      
        <tr><td colspan="4" align="center">
        <asp:Button ID="btncreate1" CssClass="btn" runat="server" Text="Save" OnClick="btncreate1_Click" />&nbsp;
        <asp:Button ID="btncancel1" runat="server" CssClass="btn" Text="Cancel" OnClick="btncancel1_Click"  />&nbsp;
        </td></tr>
        <tr><td colspan="4" align="center" style="height: 18px"><asp:Label ID="lblresults" runat="server" CssClass="error"  Font-Bold=true></asp:Label></td></tr>
        </table>
    </div>
    </form>
</body>
</html>

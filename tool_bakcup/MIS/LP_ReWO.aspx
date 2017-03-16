<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LP_ReWO.aspx.cs" Inherits="LP_ReWO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href=default.css type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dptitle">ReWork Order Number:</div>
        <div>
             <table align="center" class="bordertable" cellpadding="5" cellspacing="2" border="0" >
                 <tr >
                     <td>
                         JobID :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtJobID" runat="server"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td>
                         PO Number :<asp:TextBox ID="txtPONumber" runat="server"></asp:TextBox>
                     </td>
                 </tr>
                 <tr align="center">
                     <td>
                         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="dpbutton" OnClick="btnSave_Click"/>
                         <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="dpbutton" OnClick="btnClear_Click"/>
                     </td>
                 </tr>
             </table>
        </div>
    </form>
</body>
</html>

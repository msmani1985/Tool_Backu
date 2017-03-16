<%@ page language="C#" autoeventwireup="true" CodeFile="fontslibrarydp.aspx.cs" inherits="fontslibrarydp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Fonts Library Datapage</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Fonts Library
    </div>
    <div id="div_fontlibrary" runat="server" align="center">
        <table class="bordertable"><tr><td>Search Font</td><td><asp:TextBox ID="txt_searchtext" runat="server"></asp:TextBox></td>
        <td>Type</td><td><asp:TextBox ID="txt_fonttype" runat="server"></asp:TextBox></td>
        <td><asp:Button ID="btn_searchfont" runat="server" Text="Search" CssClass="dpbutton" OnClick="btn_searchfont_Click" /></td>
        </tr>
        <tr><td colspan="5">&nbsp;</td></tr>
        </table>
    </div>
    <br />
    <div align="center">
        <asp:GridView ID="gv_fontslibrary" runat="server" AutoGenerateColumns="false" CssClass="lightbackground" 
        AlternatingRowStyle-CssClass="dullbackground" HeaderStyle-CssClass="darkbackground">
        <Columns>
            <asp:BoundField DataField="name" HeaderText="Font Name" />
            <asp:BoundField DataField="filename" HeaderText="File Name" />
            <asp:BoundField DataField="type" HeaderText="Type" />
            <asp:BoundField DataField="path" HeaderText="Path" />
        </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

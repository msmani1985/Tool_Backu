<%@ page language="C#" autoeventwireup="true" inherits="Test, App_Web_xuje0h3i" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
 <body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager2" runat="server">
</asp:ScriptManager>
<asp:CheckBox ID="CheckBox1" runat="server" />
<div style=" width:40%">
   <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
       
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                <ContentTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                </ContentTemplate>
            </cc1:TabPanel>
           
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <ContentTemplate>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
                    <asp:Button ID="Button2" runat="server" Text="Button" />
                </ContentTemplate>
            </cc1:TabPanel>
                      
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                <ContentTemplate>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
                    <asp:Button ID="Button3" runat="server" Text="Button" />
                </ContentTemplate>
            </cc1:TabPanel>
           
        </cc1:TabContainer>
</div>
</form>
</body>
</html>


<%@ page language="C#" autoeventwireup="true" inherits="email_group, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email Group</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Create Email Address</div>
        </div>
        <table align="center" border="0" cellpadding="1" cellspacing="1" class="bordertable">
            <tr>
                <td style="width: 100px">
                    Name:<span style="color: #ff0000">*</span></td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtName" runat="server" CssClass="TxtBox" Width="308px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Email Address:<span style="color: #ff0000">*</span></td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="TxtBox" Width="308px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td align="right">
                    <asp:Button ID="btnEmailSave" runat="server" CssClass="dpbutton" OnClick="btnEmailSave_Click"
                        Text="Save" /></td>
            </tr>
        </table>
        <br />
        <div class="dptitle">
            Create Email Group</div>
        <table align="center" border="0" cellpadding="1" cellspacing="1" class="bordertable">
            <tr>
                <td colspan="3">
                    Email Group Name:<span style="color: #ff0000">*</span>
                    <asp:DropDownList ID="drpEmailGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpEmailGroup_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;<asp:TextBox ID="txtEmailGroupName" runat="server" CssClass="TxtBox"
                        Width="250px" BackColor="#FFFFC0"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnNew" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/tools/add.png"
                        OnClick="imgbtnNew_Click" ToolTip="New" /></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:ListBox ID="lstEmail" runat="server" Height="148px" Width="342px" SelectionMode="Multiple">
                    </asp:ListBox>
                </td>
                <td style="width: 36px">
                    <asp:Button ID="btnEmailAdd" runat="server" CssClass="dpbutton" Font-Bold="True"
                        Text=">" Width="54px" OnClick="btnEmailAdd_Click" />
                    <asp:Button ID="btnEmailDel" runat="server" CssClass="dpbutton" Font-Bold="True"
                        Text="<" Width="54px" OnClick="btnEmailDel_Click" /></td>
                <td style="width: 100px">
                    <asp:ListBox ID="lstEmailGroup" runat="server" Height="148px" Width="342px" SelectionMode="Multiple">
                    </asp:ListBox></td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 36px">
                </td>
                <td align="right">
                    <asp:Button ID="btnSaveGroup" runat="server" CssClass="dpbutton" OnClick="btnSaveGroup_Click"
                        Text="Save" /><span id="divClose" runat="server">
                        <input id="Button1" type="button" value="Close" onclick="javascript:self.close();"
                            class="dpbutton" />
                    </span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="ConnInfo, App_Web_xuje0h3i" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .darkbackground1 {
                background: linear-gradient(to bottom, #B4DDB4 0%, #83C783 1%, #008A00 57%, #007014 100%) repeat scroll 0% 0% transparent;
                color: #FFF;
                text-align: left;
                height: 20px;
                font-family: Segoe UI;
                font-size:12px;
                font-weight:bold;
            }

    </style>
    </head>
<body class="LightBackGound" style="background-repeat:no-repeat; width:100%; height:100%">
    <form id="form1" runat="server">
    <div class="darkbackground1" height="100px" id="divTitle" align="left" runat="server">Contact Information</div>
    <div>
        <table align="center">
            <tr>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                     &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                     &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                     &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">
                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="Top">
                    <asp:Label ID="lblCategory" Font-Size="11px" Font-Names="Arial" Text="Category :" runat="Server"></asp:Label>
                    <asp:Label ID="lblImportant" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                    <asp:DropDownList ID="drpCategory" Font-Size="11px" Font-Names="Arial" Width="228px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                     <asp:Label ID="lblCompany" Font-Size="11px" Font-Names="Arial" Text="Company :" runat="Server"></asp:Label>
                    <asp:Label ID="lblImportant3" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                    <asp:DropDownList ID="drpCompany" Font-Size="11px" Font-Names="Arial" Width="300px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td valign="Top">

                </td>
            </tr>
            <tr>
                <td valign="Top">
                    <asp:Label ID="lblName" Font-Size="11px" Font-Names="Arial" Text="Name :" runat="Server"></asp:Label>
                    <asp:Label ID="lblImportant0" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                    <asp:DropDownList ID="drpName" Font-Size="11px" Font-Names="Arial" Width="228px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpName_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                     <asp:Label ID="lblDisplay" Font-Size="11px" Font-Names="Arial" Text="Display Name :" runat="Server"></asp:Label>

                    <asp:Label ID="lblImportant4" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                    <asp:TextBox ID="txtDisplayName"  Font-Size="11px" Font-Names="Arial"  runat="server" Width="220px"></asp:TextBox>
                </td>
                <td valign="Top">

                </td>
            </tr>
            <tr>
                <td valign="Top">
                    <asp:Label ID="lblName0" Font-Size="11px" Font-Names="Arial" Text="First Name :" runat="Server"></asp:Label>
                    <asp:Label ID="lblImportant1" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                    <asp:TextBox ID="txtFirstName" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                     <asp:Label ID="lblDisplay0" Font-Size="11px" Font-Names="Arial" Text="Sur Name :" runat="Server"></asp:Label>

                    <asp:Label ID="lblImportant5" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                    <asp:TextBox ID="txtSurName" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>
                </td>
                <td valign="Top">

                </td>
            </tr>
            <tr>
                <td valign="Top">
                    <asp:Label ID="lblName1" Font-Size="11px" Font-Names="Arial" Text="Phone 1 :" runat="Server"></asp:Label>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                    <asp:TextBox ID="txtPhone1" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                     <asp:Label ID="lblDisplay1" Font-Size="11px" Font-Names="Arial" Text="Phone 2 :" runat="Server"></asp:Label>
                </td>
                <td valign="Top">

                </td>
                <td valign="Top">
                    <asp:TextBox ID="txtPhone2" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>
                </td>
                <td valign="Top">

                </td>
            </tr>
            <tr>
                <td valign="Top" >
                    <asp:Label ID="lblName2" Font-Size="11px" Font-Names="Arial" Text="Fax 1" runat="Server"></asp:Label>
                </td>
                <td valign="Top" >

                </td>
                <td valign="Top" >
                    <asp:TextBox ID="txtFax1" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>
                </td>
                <td valign="Top" >

                </td>
                <td valign="Top" >
                     <asp:Label ID="lblDisplay2"  Font-Size="11px" Font-Names="Arial" Text="Fax 2 :" runat="Server"></asp:Label>
                </td>
                <td valign="Top" >

                </td>
                <td valign="Top" >
                    <asp:TextBox ID="txtFax2" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>
                </td>
                <td valign="Top" >

                </td>
            </tr>
            <tr>
                <td valign="Top">

                    <asp:Label ID="lblName3" Font-Size="11px" Font-Names="Arial" Text="Mobile :" runat="Server"></asp:Label>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                    <asp:TextBox ID="txtMobile" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                     <asp:Label ID="lblDisplay3" Font-Size="11px" Font-Names="Arial" Text="Email :" runat="Server"></asp:Label>

                    <asp:Label ID="lblImportant6" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                    <asp:TextBox ID="txtEmail" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>

                </td>
                <td valign="Top">

                </td>
            </tr>
            <tr>
                <td valign="Top">

                    <asp:Label ID="lblName4" Font-Size="11px" Font-Names="Arial" Text="Invoice Display Name :" runat="Server"></asp:Label>

                    <asp:Label ID="lblImportant2" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                    <asp:TextBox ID="txtInvDisplayName" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                     <asp:Label ID="lblDisplay4" Font-Size="11px" Font-Names="Arial" Text="Invoice Email :" runat="Server"></asp:Label>

                    <asp:Label ID="lblImportant7" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                    <asp:TextBox ID="txtInvEmail" Font-Size="11px" Font-Names="Arial" runat="server" Width="220px"></asp:TextBox>

                </td>
                <td valign="Top">

                </td>
            </tr>
            <tr>
                <td valign="Top">

                    <asp:Label ID="lblName5" Font-Size="11px" Font-Names="Arial" Text="Address :" runat="Server"></asp:Label>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                    <asp:TextBox ID="txtAddress" Font-Size="11px" Font-Names="Arial" runat="server" Height="150px" TextMode="MultiLine" Width="226px"></asp:TextBox>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                     <asp:Label ID="lblDisplay5" Font-Size="11px" Font-Names="Arial" Text="Responsibility :" runat="Server"></asp:Label>

                    <asp:Label ID="lblImportant8" Font-Size="11px" Font-Names="Arial" Text="*" runat="Server" ForeColor="#FF3300"></asp:Label>

                </td>
                <td valign="Top">

                </td>
                <td valign="Top">

                    <asp:ListBox ID="lstResponsiblity" Font-Size="11px" Font-Names="Arial" Width="230px" Height="150px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                    l</td>
                <td valign="Top">

                </td>
            </tr>
            <tr>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">

                    <asp:Label ID="lblError" Font-Size="11px" Font-Names="Arial" runat="Server" ForeColor="#FF3300"></asp:Label>

                </td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">

                     &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
                <td valign="Top">

                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" valign="Top" colspan="7">

                    <asp:Button ID="btnSave" CssClass="dpbutton"  runat="server" OnClick="btnSave_Click" Text="Save" />

                    <asp:Button ID="btnNew" CssClass="dpbutton"  runat="server" OnClick="btnNew_Click" Text="Create as new contact" Width="150px" />

                    <asp:Button ID="btnClear" CssClass="dpbutton"  runat="server" OnClick="btnClear_Click" Text="Clear" />

                </td>
            </tr>
        </table>
    </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>

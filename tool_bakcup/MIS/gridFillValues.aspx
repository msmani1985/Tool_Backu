<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gridFillValues.aspx.cs" Inherits="gridFillValues" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
      
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Received date</td>
                <td>
                <asp:TextBox  ID="txtReceived" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender TargetControlID="txtReceived"  PopupButtonID="txtReceived"  ID="CalendarExtender1" runat="server" />
                    </td>
                
            </tr>
            <tr>
                <td>
                    Due date</td>
                <td>
                    <asp:TextBox ID="txtDue" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender TargetControlID="txtDue"  PopupButtonID="txtDue"  ID="CalendarExtender2" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Task</td>
                <td>
                <asp:DropDownList ID="drpTask" runat="server">
                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="DOI" Value="DOI"></asp:ListItem>
                    <asp:ListItem Text="ePUB" Value="ePUB"></asp:ListItem>
                    <asp:ListItem Text="ePUB, eMobi, ePDF" Value="ePUB, eMobi, ePDF"></asp:ListItem>
                    <asp:ListItem Text="ePUB, eMobi" Value="ePUB, eMobi"></asp:ListItem>
                    <asp:ListItem Text="NLM 2.3" Value="NLM 2.3"></asp:ListItem>
                    <asp:ListItem Text="NLM 3.0" Value="NLM 3.0"></asp:ListItem>
                    <asp:ListItem Text="NLM JATS" Value="NLM JATS"></asp:ListItem>
                    <asp:ListItem Text="Atypon" Value="Atypon"></asp:ListItem>
                    <asp:ListItem Text="XHTML" Value="XHTML"></asp:ListItem>
                    <asp:ListItem Text="HTML" Value="HTML"></asp:ListItem>
                    <asp:ListItem Text="OJS, DOI" Value="OJS, DOI"></asp:ListItem>
                </asp:DropDownList></td>
            </tr>

            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                        onclick="btnSubmit_Click" />
                </td>
            </tr>

        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="projectonhold, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Project Onhold</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validation()
        {
            if(document.getElementById("moduleholdDDList").value==0)
            {
                alert("Please Select Module");
                document.getElementById("moduleholdDDList").focus();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="titlediv" class="dptitle" runat="server">Project Onhold</div>
    <div align="center">
    <table width="600px" class="bordertable">
        <tr>
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text="Project"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="projectholdDDList" runat="server" DataValueField="project_id" DataTextField="project_name" OnSelectedIndexChanged="projectholdDDList_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>    
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label2" runat="server" Text="Module"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="moduleholdDDList" runat="server" DataValueField="module_id" DataTextField="module_name" OnSelectedIndexChanged="moduleholdDDList_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label3" runat="server" Text="Module Bugs"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="bugholdDDList" runat="server" DataTextField="bug_name" DataValueField="bug_id" AutoPostBack="True" OnSelectedIndexChanged="bugholdDDList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label5" runat="server" Text="Hold Date"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="holddateTxt" runat="server"></asp:TextBox><img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=holddateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label4" runat="server" Text="Description"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="holddesTxt" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 22px">
                <asp:Label ID="Label6" runat="server" Text="End Hold"></asp:Label>
            </td>
            <td align="left" style="height: 22px">
                <asp:CheckBox ID="holdendChkBox" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="AddBtn" runat="server" Text="Add" OnClientClick="return validation();" OnClick="AddBtn_Click" />&nbsp;
                <asp:Button ID="UpdateBtn" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>


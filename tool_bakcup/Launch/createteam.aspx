<%@ page language="C#" autoeventwireup="true" inherits="createteam, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Create Team</title>
    <link href="default.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server" style="text-align:center ">
    <div id="runTitle" class="dptitle" >Create Team</div>
    <div id="errMsg" class="errorMsg" runat="server" ></div>
    <table width="500px">
        <tr>
            <td>Name:&nbsp;<asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
            <td>Department:&nbsp;<asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label></td>
            <td>Team Name:&nbsp;<asp:Label ID="lblTeamName" runat="server" Text=""></asp:Label> </td>
        </tr>
    </table>
    <div >
    <table class="bordertable" width="500px">
        <tr><td class="HeaderText">List of Employees</td><td>&nbsp;</td><td class="HeaderText">Team Members</td></tr>
        <tr><td>
        <asp:ListBox Rows="20" SelectionMode="Multiple" DataTextField="EMP_FULLNAME" 
        DataValueField="EMPLOYEE_ID" runat="server" ID="lstEmployee" ></asp:ListBox>
        </td><td style="vertical-align:middle">
            <asp:ImageButton ID="ImageButton1"  Width="35px" runat="server" ImageUrl="~/images/tools/arrow1.jpg" AlternateText="Go" OnClick="ImageButton1_Click" /><br />
            </td><td>
        <asp:ListBox Rows="20" SelectionMode="Multiple" DataTextField="EMP_FULLNAME" 
        DataValueField="EMPLOYEE_ID" runat="server" ID="lstTeamemployee" ></asp:ListBox>
        </td></tr>
        
    
    </table>
    </div>
    </form>
</body>
</html>

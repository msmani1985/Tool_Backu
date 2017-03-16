<%@ page language="C#" autoeventwireup="true" inherits="changepassword, App_Web_vlobbbje" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form style="text-align:center" id="form1" runat="server">
    <div style="text-align:center;width:100%">
        <table cellpadding="3" cellspacing="3" class="borderdiv" width="300px" style="text-align:left" >
            <tr><td colspan="2" class="dpGreenHeader">Change Password</td></tr>
            <tr><td>Old Password:</td><td><asp:TextBox TextMode="Password" ID="oldpassword" runat="server" ></asp:TextBox></td></tr>
            <tr><td>New Password:</td><td><asp:TextBox TextMode="Password" ID="newpass" runat="server" ></asp:TextBox></td></tr>
            <tr><td>Confirm New Password:</td><td><asp:TextBox TextMode="Password"  ID="confirmpass" runat="server" ></asp:TextBox></td></tr>        
            <tr><td align="center" colspan="2"><asp:Button CssClass="dpbutton" ID="submit" Text="Submit" runat="server" OnClick="submit_Click" /></td></tr>
        </table>
    </div>
    <div style="margin-top:20px;width:600px;text-align:center;color:red" id="divmessage" runat="server" ></div>
    <div style="margin-top:20px;width:600px;text-align:center;color:green" id="meaningmsg">Datapage MIS Users are advised to change their password at your first login, and also in a frequent interval</div>    
    </form>
</body>
</html>

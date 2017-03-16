<%@ page language="C#" autoeventwireup="true" inherits="Sales_Calender_Events_Update, App_Web_xuje0h3i" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Calendar Events</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <style>
        A.url1:link    {font-family: Verdana, Tahoma, arial;
        color: Green; text-decoration: none; font-size:8pt; font-weight:bold;}
        A.url1:visited {font-family: Verdana, Tahoma, arial;
        color: Green; text-decoration: none; font-size:8pt;font-weight:bold;}
        A.url1:active  {font-family: Verdana, Tahoma, arial;
        color: Green; text-decoration: none; font-size:8pt;font-weight:bold;}
        A.url1:hover   {font-family: Verdana, Tahoma, arial;
        cursor:pointer; color: red ; text-decoration: none; font-size:8pt;font-weight:bold;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Calendar Event</div>
            <br />
            <table cellpadding="0" cellspacing="0" width="600" border="0" align="center" class="bordertable">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="1" border="0" align="center" style="width: 600px">
                            <tr>
                                <td height="30PX" align="center" style="background-color:Honeydew" valign="middle">
                                    <a id="lnkPrev" class="url1" href="#" runat="server" title="Previous">&laquo; Prev</a>
                                    &nbsp;
                                    <asp:Label ID="lblCurrentDate" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                    &nbsp; 
                                    <a id="lnkNext" class="url1" href="#" runat="server" title="Next">Next &raquo;</a>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color:Honeydew" align="center">                                    
                                    <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Height="296px" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="background-color: honeydew">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" style="background-color:Honeydew">
                                    <asp:Button ID="btnUpdateCal" runat="server" CssClass="dpbutton" Text="Update Calendar"
                                        Width="178px" OnClick="btnUpdateCal_Click" /></td>
                            </tr>
                            <tr>
                                <td height="30px" align="center" style="background-color:Honeydew">
                                    <a id="lnkBack" class="url1" href="#" runat="server"><b>Back to the Calendar</b></a>                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

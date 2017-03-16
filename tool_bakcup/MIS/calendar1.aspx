<%@ page language="C#" autoeventwireup="true" CodeFile="calendar1.aspx.cs" inherits="calendar1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Calendar</title>
    <script language="javascript">
    function resizeWin(){
    var win=navigator.userAgent;if(win.indexOf('Firefox')!=-1)window.resizeTo(200, 260);else if(win.indexOf('Chrome')!=-1)window.resizeTo(200, 260);
	//else if(win.indexOf('MSIE 7')!=-1)window.resizeTo(200, 280);else window.resizeTo(200, 210);
	}
    </script>
</head>
<body topmargin="2" leftmargin="5" onload="javascript:resizeWin();">
    <form id="frmCalander" method="post" runat="server">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 89px">
                        <asp:DropDownList ID="drpMonth" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Maroon"
                            Width="96px" AutoPostBack="True" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged" BackColor="#E0E0E0">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpYear" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Maroon"
                            Width="69px" AutoPostBack="True" OnSelectedIndexChanged="drpYear_SelectedIndexChanged" BackColor="#E0E0E0">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100px">
                        <%--<asp:Calendar Font-Size="9pt" SelectionMode="Day" DayNameFormat="FirstTwoLetters"
                            FirstDayOfWeek="Monday" Font-Names="Tahoma, Arial" DayHeaderStyle-ForeColor="maroon"
                            OtherMonthDayStyle-ForeColor="lightgrey" SelectedDayStyle-ForeColor="green" TitleStyle-ForeColor="white"
                            TitleStyle-BackColor="green" NextPrevStyle-Font-Bold="true" NextPrevStyle-ForeColor="white"
                            BorderColor="green" ID="calDate" OnSelectionChanged="Change_Date" runat="server"
                            Width="166px">--%>
                            <asp:Calendar ID="calDate" runat="server" ondayrender="Calendar1_DayRender" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="170px" OnSelectionChanged="Change_Date"  ShowGridLines="True" Width="166px" >
   
                            <TodayDayStyle BackColor="Green" Font-Bold="False" ForeColor="White" />
                            <SelectedDayStyle BackColor="Green" Font-Bold="False" ForeColor="White" />
                            <OtherMonthDayStyle ForeColor="LightGray" />
                            <NextPrevStyle Font-Bold="True" ForeColor="White" />
                            <DayHeaderStyle ForeColor="Maroon" />
                            <TitleStyle BackColor="Green" ForeColor="White" />
                        </asp:Calendar>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>

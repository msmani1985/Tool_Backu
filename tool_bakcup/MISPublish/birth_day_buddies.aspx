<%@ page language="C#" autoeventwireup="true" inherits="birth_day_buddies, App_Web_mjsvsc11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Birthday Buddies</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <h1>
                            <u><i>Happy Birthday Buddies!!!!!!!!!!!!!!!!!!</i></u></h1>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="height: 198px">
                        <img src="images/Bday cake.gif" style="width: 192px; height: 194px" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <h3>
                            <i>Let us wish a very happy birthday to all these staffs celebrating their birthdays
                                in the month of
                                <%Response.Write(DateTime.Now.ToString("MMMM"));%>
                                :</i></h3>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvBuddies" runat="server" AutoGenerateColumns="False"
                            Width="588px" Font-Italic="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <%=id++ %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="E.Code" DataField="emp_no" ReadOnly="True" />
                                <asp:BoundField HeaderText="Name" DataField="emp_name" ReadOnly="True" />
                                <asp:BoundField HeaderText="Date" DataField="dob1" ReadOnly="True" />
                                <asp:BoundField HeaderText="Division" DataField="employee_team_name" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="height: 198px">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

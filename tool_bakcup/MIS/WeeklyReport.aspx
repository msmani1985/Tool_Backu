<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeeklyReport.aspx.cs" Inherits="WeeklyReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Weekly Production Summary</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="invreport" class="dptitle">
                Weekly Production Summary</div>
            <table align="center" class="bordertable">
                <tr>                    
                    <td>
                        <asp:Label runat="server" ID="lblStartDate" Text="Start Date :" Font-Size="10pt"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" />
                        <img style="cursor: pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=TextBox1','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                            src="images/Calendar.jpg" height="20px" border="0" />
                    </td>
                    <td>
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date :" Font-Size="10pt"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" />
                        <img style="cursor: pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=TextBox2','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                            src="images/Calendar.jpg" height="20px" border="0" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton ID="Radio5" runat="server" GroupName="radioGroup" Text="Daily" /><asp:RadioButton
                            ID="Radio6" runat="server" GroupName="radioGroup" Text="Weekly" /><asp:RadioButton
                                ID="chkLiveData" runat="server" Checked="True" GroupName="radioGroup" Text="Live Summary" /></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" Width="98px" OnClick="btnShowReport_Click"
                            CssClass="dpbutton" /></td>
                    <td>
                        <asp:Button ID="btnExport" runat="server" CssClass="dpbutton" Text="Export to Excel" Width="113px" OnClick="btnExport_Click" /></td>
                </tr>
                <tr>
                    <td colspan="8" align="right">
                        <!--<a href="timesheetreport.aspx" target="_blank" style="border:none" title="Click here to view the Time Sheet."></a>-->
                        &nbsp;</td>
                </tr>
            </table>
            <br />
            <table align="center">
                <tr>
                    <td colspan="8" align="center">
                        <!--<a href="timesheetreport.aspx" target="_blank" style="border:none" title="Click here to view the Time Sheet."></a>-->
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="Ivory"
                            BorderColor="DarkGreen" BorderStyle="Double" GridLines="Vertical" Height="120px"
                            Width="920px">
                            <SelectedItemStyle BorderColor="#FFE0C0" />
                            <AlternatingItemStyle BackColor="White" />
                            <HeaderStyle BackColor="DarkGreen" Font-Bold="True" Font-Size="20pt" ForeColor="White"
                                Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="WeekNo" HeaderText="Week" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ArticlesRecd" HeaderText="Articles Recd" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ArticlesDesp" HeaderText="Articles Desp" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ArticlesInHand" HeaderText="Articles In Hand" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IssuesRecd" HeaderText="Issues Recd" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IssuesDesp" HeaderText="Issues Desp" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FinalIssueDesp" HeaderText="Final Issues Desp" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FinalIssuePageDesp" HeaderText="Final Issue Pages Desp"
                                    ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IssuesInHand" HeaderText="Issues In Hand" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BooksRecd" HeaderText="Books Recd" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BooksDesp" HeaderText="Books Desp" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BookFinalDesp" HeaderText="Final Books Desp" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BookFinalPageDesp" HeaderText="Final Book Pages Desp"
                                    ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BooksInHand" HeaderText="Books In Hand" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProjectsRecd" HeaderText="Projects Recd" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProjectsDesp" HeaderText="Final Projects Desp" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProjectsInHand" HeaderText="Projects In Hand" ReadOnly="True">
                                    <HeaderStyle Font-Size="8pt" />
                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

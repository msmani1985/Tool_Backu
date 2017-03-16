<%@ Page Language="C#" AutoEventWireup="true" CodeFile="job_costing.aspx.cs" Inherits="job_costing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Job Costing</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript">
    function popjob(jname,cname,pages,value){
        //alert(jname);
        jname = jname.replace('&','%26');
        cname = cname.replace('&','%26');
        window.open("job_costing_summary.aspx?jname="+jname+"&cname="+cname+"&invval="+value,"JOB_COST_SUMMARY","top=1,left=1,width=680,height=650,status=yes, scrollbars=yes");
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="invreport" class="dptitle">
                <asp:Label ID="lblTitle" runat="server">Job Costing</asp:Label></div>
        </div>
        <table align="center" cellpadding="2" cellspacing="2" class="bordertable" style="width: 480px">
            <tr>
                <td>
                    Customer</td>
                <td>
                    <asp:DropDownList ID="drpCustomer" runat="server" DataTextField="CUSTNAME" DataValueField="CUSTNO">
                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Taylor and Francis" Value="2556"></asp:ListItem>
                        <asp:ListItem Text="Taylor and Francis Scandivia" Value="10037"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Type</td>
                <td>
                    <asp:DropDownList ID="drpJobType" runat="server">
                        <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Month</td>
                <td>
                    <%--<asp:TextBox id="fromdate" runat="server" />
                <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=fromdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />--%>
                    <asp:DropDownList ID="drpMonths" runat="server">
                        <asp:ListItem Value="0">--All--</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Year</td>
                <td>
                    <asp:DropDownList ID="drpYears" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" OnClick="btnSubmit_Click"
                        Text="Submit" /></td>
            </tr>
            <tr>
                <td align="right" colspan="10" rowspan="2">
                    <div id="divMonthly" runat="server">                        
                        &nbsp;<asp:ImageButton ID="cmd_Excel_Export" runat="server" ImageUrl="~/images/tools/j_excel.png"
                            OnClick="cmd_Excel_Export_Click" ToolTip="Export Excel" /><asp:GridView ID="gvJobCosting"
                                runat="server" AutoGenerateColumns="False" CssClass="lightbackground" Width="100%"
                                OnRowDataBound="gvJobCosting_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Job No.">
                                        <ItemTemplate>
                                            <%# id++%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice No.">
                                        <ItemTemplate>
                                            <%# Eval("IINNO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <%# Eval("CNAME")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Job Type">
                                        <ItemTemplate>
                                            <%# Eval("CATEGORY")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Title">
                                        <ItemTemplate>
                                            <a class="link1" style="text-decoration:underline" href="javascript:void(0);" onclick="javascript:popjob('<%# Eval("JOBTITLE")%>','<%# Eval("CNAME")%>','<%# Eval("TOTALPAGES_FINAL")%>','<%# Eval("EUROVALUE")%>');"><%# Eval("JOBTITLE")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Pages" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Eval("TOTALPAGES_FINAL")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoiced Value (€)" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Eval("EUROVALUE")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Man-Hours Spent(min)" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Eval("MANHOURS")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Cost(€)" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Eval("JOBCOST")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profit/Loss(€)" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDifference" runat="server" Text='<%# Eval("DIFFERENCE")%>' Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                        No records found.</div>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="darkbackground" />
                                <AlternatingRowStyle CssClass="dullbackground" />
                            </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="10">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

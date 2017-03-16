<%@ page language="C#" autoeventwireup="true" inherits="job_costing_summary, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job Costing Summary</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript">
    function popEmplog(jname,empid,empname,loc){
        window.location="job_costing_summary.aspx?jname="+jname+"&empid="+empid+"&empname="+empname+"&loc="+loc;        
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
        <div id="divSummary" runat="server">
            <div class="dptitle">
                Job Costing Summary</div>
            <table align="center" style="width: 616px">
                <tr>
                    <td colspan="3" align="right"><input id="Button2" type="button" value="Close[x]" class="dpbutton" onclick="javascript:self.close();" />
                    </td>                    
                </tr>
                <tr>
                    <td colspan="3">
                        Customer:
                        <asp:Label ID="lblCustomer" runat="server" ForeColor="Blue"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        Job Name:
                        <asp:Label ID="lblJobName" runat="server" ForeColor="Blue"></asp:Label>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        Invoiced Value:
                        <asp:Label ID="lblInvValue" runat="server" ForeColor="Blue"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvJobCostingSummary" runat="server" AutoGenerateColumns="False"
                            CssClass="lightbackground" Width="100%" ShowFooter="true" OnRowDataBound="gvJobCostingSummary_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.no.">
                                    <ItemTemplate>
                                        <%=id++%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee">
                                    <ItemTemplate>
                                    <a class="link1" href="javascript:void(0);" onclick="javascript:popEmplog('<%=Request.QueryString["jname"].ToString()%>','<%# Eval("employee_id") %>','<%# Eval("fname")%>','<%# Eval("location_name")%>');" style="text-decoration: underline">
                                            <%# Eval("fname")%></a>                                        
                                        <asp:HiddenField ID="hfgvSummaryEmpID" runat="server" Value='<%# Eval("employee_id") %>'/>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div align="right"><b>Total:</b></div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <%# Eval("location_name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time Spent (mins)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSummaryMins" runat="server" Text='<%# Eval("tot_mins")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvSummaryTotal" runat="server" Text="0" Font-Bold="true"></asp:Label>                                    
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                    No records found.</div>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="darkbackground" />
                            <AlternatingRowStyle CssClass="dullbackground" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divSplitUp" runat="server">
            <div class="dptitle">
                Job Costing Break Down Sheet</div>            
            <table align="center" style="width: 616px">
                <tr>
                    <td colspan="3" align="right"><input id="btnBack" type="button" value="< Back to Summary" class="dpbutton" onclick="javascript:history.go(-1);" style="width: 112pt" /><input id="Button1" type="button" value="Close[x]" class="dpbutton" onclick="javascript:self.close();" /></td>
                    
                </tr>
                <tr>
                    <td colspan="3">
                        Employee:
                        <asp:Label ID="lblEmpName" runat="server" ForeColor="Blue"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        Job Name:
                        <asp:Label ID="lblJobName1" runat="server" ForeColor="Blue"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        Location:
                        <asp:Label ID="lblLocation" runat="server" ForeColor="Blue"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                       <asp:GridView ID="gvJobCostingSplit" runat="server" AutoGenerateColumns="False"
                            CssClass="lightbackground" Width="100%" ShowFooter="True" OnRowDataBound="gvJobCostingSplit_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.no.">
                                <ItemTemplate>
                                    <%=id++%>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Job ID">
                                <ItemTemplate>
                                    <%# Eval("child_name")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Task Name">
                                <ItemTemplate>
                                    <%# Eval("task_name")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <%# Eval("start_time")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <%# Eval("end_time")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div align="right"><b>Total:</b></div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time Spent (mins)">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblgvSplitMins" runat="server" Text='<%# Eval("tot_mins")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvSplitTotal" runat="server" Text="0" Font-Bold="true"></asp:Label>                                    
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                No records found.</div>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="darkbackground" />
                        <AlternatingRowStyle CssClass="dullbackground" />
                    </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

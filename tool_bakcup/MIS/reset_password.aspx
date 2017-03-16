<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reset_password.aspx.cs" Inherits="reset_password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript">
        function confirmReset(emp){
            return confirm ('Do you want to Reset Password for '+emp+'?');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Reset Password</div>
        </div>
        <table align="center" class="bordertable" cellpadding="2" cellspacing="2" style="width: 650px">
            <tr>
                <td colspan="4" style="height: 18px">
                    <strong>Team Owner: </strong>
                    <asp:Label ID="lblTeamOwner" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 18px">
                    <strong>Team Members:</strong></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                         width="100%" OnRowCommand="gvEmployees_RowCommand" OnRowDataBound="gvEmployees_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# id++%>                                    
                                    <asp:HiddenField ID="hfgvEmpID" runat="server" Value='<%#Eval("EMPLOYEE_ID") %>' />
                                    <asp:HiddenField ID="hfgvLastVisit" runat="server" Value='<%#Eval("LAST_VISIT") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                        
                            <asp:TemplateField HeaderText="Emp Code">
                                <ItemTemplate>
                                    <%# Eval("ECODE")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp Name">
                                <ItemTemplate>
                                    <%# Eval("EMP_FULLNAME")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <%# Eval("LOCATION_NAME")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <%# Eval("DESIGNATION")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reset Password">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtngvReset" OnClientClick='<%# Eval("EMP_FULLNAME","javascript:return confirmReset(\"{0}\");")%>'
                                    ImageUrl="~/images/tools/reload.png" runat="server" ToolTip="Reset" CommandName="pwdreset" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
    </form>
</body>
</html>

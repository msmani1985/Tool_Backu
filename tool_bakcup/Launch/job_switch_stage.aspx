<%@ page language="C#" autoeventwireup="true" inherits="job_switch_stage, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stage Edit</title>
    <link href="default.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server"><br />
        <div id="divTitle" runat="server" class="dptitle">
            Stage Edit</div>
        <div id="divReport" align="center" runat="server">
            <table style="width: 95%"><tr><td align="right"><input id="Submit1" class="dpbutton" type="submit" value="Close[x]" onclick="javascript:self.close()" /></td></tr></table>
            <table class="bordertable" style="width: 95%">
                <tr>
                    <td align="left" class="dpJobGreenHeader">
                        <b>Job:</b> <asp:Label ID="lblJob" Text="" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvStageEdit" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="2" CssClass="lightbackground" Width="100%" OnRowCommand="gvStageEdit_RowCommand">
                            <RowStyle HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <HeaderStyle ForeColor="white" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.no">
                                    <ItemTemplate>
                                        <%# id++ %>
                                        <asp:HiddenField ID="hfgvStageID" Value='<%#Eval("job_stage_id") %>' runat="server" />
                                        <asp:HiddenField ID="hfTAT" Value='<%#Eval("tat") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Stage" DataField="job_stage_name" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ImageUrl="~/images/tools/yes.png" ToolTip="Re-assign Stage" CommandName="re-assign" runat="server" ImageAlign="Middle" OnClientClick='<%# Eval("job_stage_name", "return confirm(\"Do you want to re-assign this job to the {0} stage?\");") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="darkbackground" />
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                    No records found.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Label ID="lbldummy" runat="server"></asp:Label>
    </form>
</body>
</html>

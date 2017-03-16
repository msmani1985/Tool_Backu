<%@ Page Language="C#" AutoEventWireup="true" CodeFile="job_unassigned.aspx.cs" Inherits="job_unassigned" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Articles Unassigned</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        &nbsp;<div class="dptitle">
            Articles Unassigned Summary</div>
        <br />
        <div id="divArticlesList" runat="server">
            <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="width: 700px">
                <tr>
                    <td align="right"><asp:Button ID="btnBack" runat="server" CssClass="dpbutton" OnClick="btnBack_Click"
                            Text="< Back" Width="73px" />&nbsp;
                        <asp:Button ID="btnArticleAssigned" runat="server" CssClass="dpbutton" OnClick="btnArticleAssigned_Click"
                            Text="Duplicate Articles" Width="151px" />
                        <asp:Button ID="btnAssign" runat="server" CssClass="dpbutton" OnClick="btnAssign_Click"
                            Text="Assign Selected" Width="130px" />
                        <input id="Button2" class="dpbutton" type="button" value="Cancel" onclick="javascript:self.close()" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvArticlesList" CssClass="lightbackground" runat="server" Width="100%"
                            AutoGenerateColumns="False" OnRowDataBound="gvArticlesList_RowDataBound">
                            <RowStyle HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <HeaderStyle CssClass="darkbackground" />
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                    No records found.</div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="S.no">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobID" runat="server" Text='<%# Eval("job_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Doc Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDocType" runat="server" Text='<%# Eval("document_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Article ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobName" runat="server" Text='<%# Eval("job_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Article Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobTitle" runat="server" Text='<%# Eval("job_title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkgvSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;<asp:HiddenField ID="hfDuplicateJobID" runat="server" />
                        <asp:LinkButton ID="btnArticleDuplicate" runat="server" OnClick="btnArticleDuplicate_Click"></asp:LinkButton>
                        &nbsp;<asp:Button ID="btnAssign1" runat="server" CssClass="dpbutton" OnClick="btnAssign_Click"
                            Text="Assign Selected" Width="130px" />
                        <input id="Button3" class="dpbutton" type="button" value="Cancel" onclick="javascript:self.close()" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

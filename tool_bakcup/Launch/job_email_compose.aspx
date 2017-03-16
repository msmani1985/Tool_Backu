<%@ page language="C#" autoeventwireup="true" inherits="job_email_compose, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Email Compose</div>
        </div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable">
            <tr>
                <td colspan="5" align="center">
                    Customer:<span style="color: #ff0000">*</span><strong> </strong>
                    <asp:DropDownList ID="drpCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged"
                        Width="220px">
                    </asp:DropDownList><span style="color: #ff0000">&nbsp; </span>Job Type:<span style="color: #ff0000">*</span><strong>
                    </strong>
                    <asp:DropDownList ID="drpJobtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpJobtype_SelectedIndexChanged">
                        <asp:ListItem Value="6">Issues</asp:ListItem>
                        <asp:ListItem Value="5">Articles</asp:ListItem>
                        <asp:ListItem Value="2">Books</asp:ListItem>
                        <asp:ListItem Value="4">Projects</asp:ListItem>
                    </asp:DropDownList>
                    Job:<span style="color: #ff0000">* </span>
                    <asp:DropDownList ID="drpParentJob" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpParentJob_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                <td id="tdJobNo" colspan="1" runat="server">
                    &nbsp;Job No:<span style="color: #ff0000">*</span>
                    <asp:TextBox ID="txtJobNo" runat="server" CssClass="TxtBox" Width="82px"></asp:TextBox>
                </td>
                <td colspan="1">
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" OnClick="btnSearch_Click"
                        Text="Search" /></td>
            </tr>
        </table>
        <br />
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable" id="divConfig" runat="server">
            <tr>
                <td>
                    <asp:GridView ID="gvEmailCompose" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                        Width="820px" OnRowCommand="gvEmailCompose_RowCommand">
                        <RowStyle HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sno.">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblsno" runat="server" Text="<%# id=id+1 %>"></asp:Label>
                                    <asp:HiddenField ID="gvhfJobEvent" runat="server" Value='<%# Eval("job_event_id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Config Type">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblEmailConfigType" runat="server" Text='<%# Eval("job_event_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Type">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblEmailType" runat="server" Text='<%# Eval("iscontributed") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Letter">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblEmailLetter" runat="server" Text='<%# Eval("email_letter_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Has Attachments">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblHasAttachment" runat="server" Text='<%# Eval("hasattachment") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Sent">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblEmailSent" runat="server" Text='<%# Eval("emailsent") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnCompose" runat="server" CommandName="compose" CssClass="link1" Font-Bold="True">Compose</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                        
                        <HeaderStyle CssClass="darkbackground" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="dullbackground" />
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                No records found.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

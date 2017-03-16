<%@ page language="C#" autoeventwireup="true" inherits="job_email_report, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job Summary Report</title>
    <link href="default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divTitle" class="dptitle" runat="server">
                Job Summary Report</div>
        </div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable">
            <tr>
                <td align="left" colspan="5">
                    Customer:<span style="color: #ff0000">*</span></td>
                <td align="left" colspan="5">
                    <asp:DropDownList ID="drpCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged"
                        Width="220px">
                    </asp:DropDownList></td>
                <td align="left" colspan="5">
                    Job Type:<span style="color: #ff0000">*</span>
                </td>
                <td align="left" colspan="5">
                    <asp:DropDownList ID="drpJobtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpJobtype_SelectedIndexChanged">
                        <asp:ListItem Value="1">Journal</asp:ListItem>
                        <asp:ListItem Value="2">Books</asp:ListItem>
                        <asp:ListItem Value="4">Projects</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" colspan="5">
                    Job:<span style="color: #ff0000"> </span>
                </td>
                <td align="left" colspan="5">
                    <asp:DropDownList ID="drpParentJob" runat="server">
                    </asp:DropDownList></td>
                <td align="left" colspan="5">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5">
                    From Date</td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtfDate" runat="server" CssClass="" MaxLength="10" onclick="javascript:this.value='';"
                        Width="70px"></asp:TextBox><img align="absMiddle" alt="Calendar" border="0" height="20"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtfDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                            src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                            border-left-style: none; border-bottom-style: none" /></td>
                <td align="left" colspan="5">
                    To Date</td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txteDate" runat="server" CssClass="" MaxLength="10" onclick="javascript:this.value='';"
                        Width="70px"></asp:TextBox><img align="absMiddle" alt="Calendar" border="0" height="20"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txteDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                            src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                            border-left-style: none; border-bottom-style: none" /></td>
                <td align="left" colspan="5">
                </td>
                <td align="left" colspan="5">
                </td>
                <td align="left" colspan="5">
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" OnClick="btnSearch_Click"
                        Text="Search" /></td>
            </tr>
        </table>
        <br />
        <div id="divReport" align="center" runat="server">
            <table class="bordertable" style="width: 97%">
                <tr>
                    <td>
                        <asp:GridView ID="gvEmailSent" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                            Width="100%" OnRowCommand="gvEmailSent_RowCommand" CellPadding="2" OnRowDataBound="gvEmailSent_RowDataBound">
                            <RowStyle HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.no">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Job Name" ItemStyle-Wrap="false" >
                                    <ItemTemplate>
                                        <a href="jobbag.aspx?jobid=<%# Eval("jobbag_jobid")%>&jobtypeid=<%# Eval("jobbag_jobtypeid")%>&print=1" target="_blank"><%# Eval("job_name") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Job Title" DataField="job_name" ItemStyle-Wrap="false" visible="false" ></asp:BoundField>
								<asp:BoundField HeaderText="Contact Name" DataField="email_sent_to"></asp:BoundField>
                                <asp:BoundField HeaderText="Title" DataField="topics"></asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="email_letter_name"></asp:BoundField>
                                <asp:TemplateField HeaderText="Email Header" visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmailHeader" runat="server" Text='<%#Eval("email_header") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Sent On" DataField="email_sent_date" ItemStyle-Wrap="false" ></asp:BoundField>                                
                                <asp:BoundField HeaderText="Attachments" DataField="hasattachments" HeaderImageUrl="img/attachment.jpg" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:TemplateField visible="false">
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lnkbtnPreview" runat="server" CommandName="preview" CssClass="link1"
                                            Font-Bold="True" ToolTip="Preview of Email Sent">view</asp:LinkButton>--%>
                                        <strong><a id="lnkPreview" class="link1" href="#" runat="server">View</a></strong>
                                        <asp:HiddenField ID="hfEmailSentID" runat="server" Value='<%# Eval("email_sent_id") %>' />
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
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>

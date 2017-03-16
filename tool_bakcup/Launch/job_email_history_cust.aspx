<%@ page language="C#" autoeventwireup="true" inherits="job_email_history_cust, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email History</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script language="javascript">    
    function previewJob(jobid, jobtypeid){
        var width = 1024,height = 600;
        var left = (screen.width  - width)/2;
        var top = (screen.height - height)/2;
        var w=window.open('job_email_history_cust_preview.aspx?jobid=' + jobid + '&jobtypeid='+jobtypeid+'&print=1','Preview','width='+width+',height='+height+',left='+left+',top='+top+',toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divTitle" runat="server" class="dptitle">
            Email Summary</div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="font-weight: bold;
            background-color: honeydew">
            <tr>
                <td align="right" colspan="12">
                    &nbsp; Journal:&nbsp;<asp:DropDownList ID="drpJournal" runat="server">
                    </asp:DropDownList><span style="color: #ff0000"></span></td>
                <td align="right" colspan="1">
                    &nbsp;&nbsp;Stage:
                    <asp:DropDownList ID="drpJobStage" runat="server">
                    </asp:DropDownList></td>
                <td align="left" colspan="1">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="13">
                    Start Date:
                    <asp:TextBox ID="txtfDate" runat="server" CssClass="" MaxLength="10" onclick="javascript:this.value='';"
                        Width="70px" BackColor="#EFEFEF"></asp:TextBox><img align="absMiddle" alt="Calendar" border="0" height="20"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtfDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                            src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                            border-left-style: none; border-bottom-style: none" />
                    &nbsp;&nbsp;End Date:
                    <asp:TextBox ID="txteDate" runat="server" CssClass="" MaxLength="10" onclick="javascript:this.value='';"
                        Width="70px" BackColor="#EFEFEF"></asp:TextBox><img align="absMiddle" alt="Calendar" border="0" height="20"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txteDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                            src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                            border-left-style: none; border-bottom-style: none" />
                </td>
                <td align="left" colspan="1">
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" OnClick="btnSearch_Click"
                        Text="Submit" /></td>
            </tr>
        </table>
        <br />
        <div id="divReport" align="center" runat="server">
            <table class="bordertable" style="width: 95%">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="imgbtnExport" runat="server" ImageUrl="~/images/tools/j_excel.png"
                            OnClick="imgbtnExport_Click" ToolTip="Export to Excel" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvEventHistory" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="2" CssClass="lightbackground" Width="100%" OnSorting="gvEventHistory_Sorting" OnRowDataBound="gvEventHistory_RowDataBound">
                            <RowStyle HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <HeaderStyle ForeColor="white" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.no">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="journal_code" HeaderText="Journal" SortExpression="journal_code" />                                
                                <asp:TemplateField HeaderText="ID" SortExpression="name">
                                    <ItemTemplate>
                                        <a href="#preview" onclick="javascript:previewJob('<%#Eval("job_id")%>','<%#Eval("job_type_id")%>')">
                                            <%#Eval("name")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Article Title" SortExpression="title">
                                    <ItemTemplate>
                                        <div style="width: 200px; word-wrap: break-word;"
                                            title='<%#Eval("title")%>'>
                                            <%#Eval("title")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Email To" DataField="display_name" SortExpression="display_name" />
                                <asp:BoundField HeaderText="Email Address" DataField="email" SortExpression="email" />
                                <asp:BoundField DataField="job_stage_name" HeaderText="Stage" SortExpression="job_stage_name" />
                                <asp:BoundField DataField="email_letter_name" HeaderText="Status" SortExpression="email_letter_name" />                                
                                <asp:BoundField DataField="email_sent_date" HeaderText="Sent on" SortExpression="email_sent_date" />                                
                                <asp:BoundField DataField="hasattachments" HeaderText="Attachment" SortExpression="hasattachments" />                                
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
        </div>
    </form>
</body>
</html>

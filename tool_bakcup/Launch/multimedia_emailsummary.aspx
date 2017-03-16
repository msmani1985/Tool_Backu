<%@ page language="C#" autoeventwireup="true" inherits="multimedia_emailsummary, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
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
    <div id="div_Title" runat="server" class="dptitle">
    Email Summary
    </div>
    <div id="div_emailsummary" runat="server">
    <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="font-weight: bold;
            background-color: honeydew">
        <tr><td align="right">TV Station:&nbsp;<asp:DropDownList ID="drpJournal" runat="server">
                        </asp:DropDownList><span style="color: #ff0000"></span></td>
            <td align="right" colspan="1">
              &nbsp;&nbsp;<asp:Label id="lbl_jobstatus" runat="server" Text="Status:"></asp:Label>
                        <asp:DropDownList ID="drpJobStatus" runat="server">
                        </asp:DropDownList></td>
        <td align="right">
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
        <td align="left"><asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" 
                        Text="Submit" OnClick="btnSearch_Click" /></td>
        </tr>                    
    </table>
    </div>
    <div id="divReport" align="center" runat="server">
        <table class="bordertable" style="width: 95%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgbtnExport" runat="server" ImageUrl="~/images/tools/j_excel.png"
                         ToolTip="Export to Excel" OnClick="imgbtnExport_Click" /></td>
            </tr>
            <tr><td>
            <asp:GridView ID="gv_email_summary" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="lightbackground" Width="100%" OnSorting="gv_email_summary_sorting">
            <RowStyle HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="dullbackground" />
            <HeaderStyle ForeColor="white" />
            <Columns>
            <asp:TemplateField HeaderText="S.no">
                <ItemTemplate>
                    <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="TVSTATION" HeaderText="TV Station" SortExpression="TVSTATION" />  
             <asp:TemplateField HeaderText="Speaker" SortExpression="name">
                <ItemTemplate>
                    <a href="#preview" onclick="javascript:previewJob('<%#Eval("job_id")%>','<%#Eval("job_type_id")%>')">
                        <%#Eval("display_name")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
          <%--<asp:BoundField DataField="display_name" HeaderText="Email To" SortExpression="display_name" />                   --%>
          
          <asp:BoundField DataField="email" HeaderText="Email Address" SortExpression="email" />
          <asp:BoundField DataField="sanlucas_phoneno" HeaderText="Tel No." SortExpression="sanlucas_phoneno" />
          <asp:BoundField DataField="sanlucas_faxno" HeaderText="Fax No." SortExpression="sanlucas_faxno" />
          <asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />                   
          <asp:BoundField DataField="sanlucas_interviewdate" HeaderText="Intervie wDate" SortExpression="sanlucas_interveiwdate" DataFormatString="{0:MM/dd/yyyy}" />
          <asp:BoundField DataField="sanlucas_interviewtime" HeaderText="Interview Time" SortExpression="sanlucas_interviewtime" />
          <asp:BoundField DataField="email_sent_date" HeaderText="Sent on" SortExpression="email_sent_date" /> 
          <asp:BoundField DataField="hasattachments" HeaderText="Attachment" SortExpression="hasattachments" /> 
            </Columns>
            <HeaderStyle CssClass="darkbackground" />
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                    No records found.</div>
                            </EmptyDataTemplate>
            </asp:GridView>
            </td></tr>
        </table>
    </div>
    <div id="div_error" runat="server" class="error"></div>
    </form>
</body>
</html>

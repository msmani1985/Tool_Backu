<%@ page language="C#" autoeventwireup="true" inherits="bookinxml, App_Web_25d24vps" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Booking - In XML</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Booking - In XML
    </div>
    <div id="div_bookinginxmldetails" runat="server" align="center">
        <table class="bordertable"><tr><td>Job&nbsp;&nbsp;</td>
        <td>
        <asp:RadioButtonList ID="rb_jobtype" RepeatDirection="horizontal" runat="server"><asp:ListItem Text="Live" Value="1"></asp:ListItem><asp:ListItem Text="Dispatch" Value="2"></asp:ListItem></asp:RadioButtonList>
        </td>
        <td><asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="dpbutton" OnClick="btn_submit_Click" /></td>
        </tr></table>
    </div>
    <br />
    <div id="div_xmljobdetails" runat="server" align="center" >
    <table><tr><td align="right"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" /></td></tr>
    <tr><td><asp:GridView ID="gv_xmljob" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
               HeaderStyle-CssClass="darkbackground" Font-Names="Segoe UI" Font-Size="11px" 
            >
        <AlternatingRowStyle backcolor="#F0FFF0"></AlternatingRowStyle>

            <Columns>
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="title" HeaderText="Title" >
                <ItemStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField DataField="received_date" HeaderText="Received Date" 
                    DataFormatString="{0:dd/MM/yyyy}" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="due_date" HeaderText="Due Date" 
                    DataFormatString="{0:dd/MM/yyyy}" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="despatch_date" HeaderText="Dispatch Date" 
                    DataFormatString="{0:dd/MM/yyyy}" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="job_stage_name" HeaderText="Job Stage Name" >
                <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderTemplate><asp:ImageButton ImageUrl="~/images/tools/j_save.png" ID="ibtn_dispatch" runat="server" OnClick="ibtn_dispatch_click" Height="25px" Width="25px" /></HeaderTemplate>
                    <ItemTemplate><asp:CheckBox ID="cb_dispatch" runat="server" /><asp:HiddenField ID="hf_job_id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_history_id") %>' /></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate><asp:ImageButton ImageUrl="~/images/tools/delete.png" ID="btn_delete" runat="server" Height="25px" Width="25px" OnClick="ibtn_delete_click" /></HeaderTemplate>
                    <ItemTemplate><asp:CheckBox ID="cb_delete" runat="server" /><asp:HiddenField ID="hf_jobid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' /></ItemTemplate>
                </asp:TemplateField>
            </Columns>

<HeaderStyle CssClass="darkbackground"></HeaderStyle>
        </asp:GridView></td></tr>
    </table>
        
    </div>
    <div id="div_error" class="errorMsg" runat="server" 
        style="font-family: 'Segoe UI'; font-size: 11px">No Record's Found</div>
    </form>
</body>
</html>

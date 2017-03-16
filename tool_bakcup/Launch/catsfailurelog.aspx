<%@ page language="C#" autoeventwireup="true" inherits="catsfailurelog, App_Web_w6b3pav3" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <div class="dptitle">
        CATs Failure Log
    </div>
    <div>
        <table align="center" class="bordertable" width="70%">
            <tr>
                <td><asp:RadioButtonList ID="rbt_cats" runat="server" RepeatDirection="horizontal"><asp:ListItem Text="CATs Due Date" Value="0"></asp:ListItem><asp:ListItem Text="Failure Date" Value="1"></asp:ListItem></asp:RadioButtonList> </td>
                <td>Select Date</td>
                <td><asp:TextBox ID="txt_catsdate" runat="server"></asp:TextBox><img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_catsdate','calendar_window','width=180,height=200,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" /></td>
                <td><asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="dpbutton" OnClick="btn_submit_Click" /></td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width:90%;text-align:right;"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" /></div>
    <div>
    <table width="90%" align="center"><tr><td><asp:GridView ID="gv_catsdetails" runat="server" AutoGenerateColumns="false"
    CssClass="lightbackground" HeaderStyle-CssClass="darkbackground" AlternatingRowStyle-CssClass="dullbackground" 
     ShowHeader="true">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" />
        <asp:BoundField DataField="ZIP_FILE_NAME" HeaderText="ZIP File Name" />
        <asp:BoundField DataField="JOURCODE" HeaderText="Journal Code" />
        <asp:BoundField DataField="JOB_NAME" HeaderText="Job Name" />
        <asp:BoundField DataField="JOB_TYPE" HeaderText="Job Type" />
        <asp:BoundField DataField="FAILURE_DATE" HeaderText="Failure Date" />
        <asp:BoundField DataField="CATS_DUE_DATE" HeaderText="CATS Due Date" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="REASON" ItemStyle-Width="150px" ItemStyle-Wrap="true" HeaderText="Reason" />
        <asp:BoundField DataField="EMP_NAME" HeaderText="Verified" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="ch_catssave" runat="server" /><asp:HiddenField ID="hf_catsid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ID") %>' />
            </ItemTemplate>
        <HeaderTemplate>
            <asp:ImageButton ID="hcmd_Save_CATs" ImageUrl="~/images/tools/j_save.png" runat="server"
             ToolTip="Save Book" OnClick="cmd_Save_CATs_Click" />
        </HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    </td></tr></table>
    </div>
    <div id="div_error" runat="server"></div>
    </form>
</body>
</html>

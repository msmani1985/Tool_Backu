<%@ page language="C#" autoeventwireup="true" inherits="sqltaskmgr, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Event Log</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" >
        function Validation(source,args)
        {
            var elem=document.getElementById('txtJobNumber').value;
            if(isNaN(elem))
                args.IsValid=false;
            else
                args.IsVaild=true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" runat="server" id="divheader">Event Logger of</div>
    <div >
        <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:800px;">
            <tr>
                <td>Job Type</td>
                <td>
                    <asp:DropDownList ID="ddljobtype" runat="server">
                        <asp:ListItem Text="Issue" Value="6" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Article" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Value="7" text="Chapter"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Module" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Job No.:</td>
                <td><asp:TextBox ID="txtJobNumber" runat="server" Text=""></asp:TextBox>
                    
                </td>
                <td>Task Name:</td>
                <td><asp:DropDownList ID="ddlstyles" DataTextField="task_name" DataValueField="task_id" runat="server"></asp:DropDownList>
                    <asp:DropDownList Visible="false"  ID="ddltask_type" DataTextField="task_id" DataValueField="task_type_id" runat="server"></asp:DropDownList>
                 </td>
                 <%--<td>Status:</td>
                 <td>
                 <asp:DropDownList ID="ddl_WorkStatus" runat="server">
                    <asp:ListItem Text = "--Select--" Value = "0" Selected= "True" ></asp:ListItem>
                    <asp:ListItem Text = "Completed" Value = "1" ></asp:ListItem>
                    <asp:ListItem Text = "Pending" Value = "2" ></asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
                <td id="Td2" align="center" runat="server">
                    <asp:Button Text="Log Now" CssClass="dpbutton" runat="server" ID="Button1" OnClick="btnSubmit_click" UseSubmitBehavior="false" />
                </td>
            </tr>
            <tr><td colspan="3">&nbsp;</td>
            <td colspan="4"><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Enter Number only" ClientValidationFunction="Validation" ControlToValidate="txtJobNumber"></asp:CustomValidator></td></tr>   
        </table>
    </div>
    <div runat="server" id="errMessage" class="errorMsg"></div>
    <%--<div>
    <table align ="right">
    <tr>
    <td>Status:</td>
         <td>
         <asp:DropDownList ID="ddl_WorkStatus" runat="server">
            <asp:ListItem Text = "--Select--" Value = "0" Selected= "True" ></asp:ListItem>
            <asp:ListItem Text = "Completed" Value = "1" ></asp:ListItem>
            <asp:ListItem Text = "Pending" Value = "2" ></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    </table>
    </div>--%>
    <div id="divEvents" style="overflow:auto;padding-top:20px;" runat="server" >
        <asp:GridView ID="agvLogEvents" HorizontalAlign="center" runat="server" Width="95%" 
        AutoGenerateColumns="false" HeaderStyle-CssClass="darkbackground" AlternatingRowStyle-CssClass="dullbackground" RowStyle-CssClass="lightbackground"
         CellPadding="4"  GridLines="horizontal" 
         OnRowDataBound="agvLogEvents_RowDataBound"  OnRowCommand="agvLogEvents_RowCommand" >
         
            <Columns>
                <asp:BoundField DataField="Job_Type_Name" HeaderText="Job Type" />
                <%--<asp:BoundField DataField="NAME" HeaderText="Job Ref" />--%>
                <asp:TemplateField HeaderText="Job Ref">
                    <ItemTemplate>
                        <a href='<%# "jobbag.aspx?jobid="+Eval("job_id") + "&jobtypeid=" + Eval("job_type_id") %>'  runat="server"><%# DataBinder.Eval(Container.DataItem,"NAME") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="task_name" HeaderText="Process" />
                <asp:BoundField DataField="Start_Time" HeaderText="Start Time" />
                <asp:BoundField DataField="End_Time" HeaderText="End Time" />
                <asp:BoundField DataField="Pending_End_Time" HeaderText="End Time(Pending)" />
                <asp:TemplateField HeaderText="Pages">
                    <ItemTemplate>
                        <asp:TextBox ID="txtpages" Width="30px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"achived_value") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate>
                        <asp:TextBox ID="txtcomments" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"comments") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText ="Status">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_WorkStatus" runat="server">
                            <asp:ListItem Text = "--Select--" Value = "0" Selected= "True" ></asp:ListItem>
                            <asp:ListItem Text = "Completed" Value = "1" ></asp:ListItem>
                            <asp:ListItem Text = "Pending" Value = "2" ></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:Button ID="btnEndLog" runat="server" Text="End" CssClass="dpbutton"
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem,"loggedevent_id") %>' CommandName="EndLogEvent" />
                        <asp:HiddenField ID="hf_logid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"loggedevent_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <br /><br />
    <div id="div_comment" runat="server">
    <asp:GridView ID="gv_comment" runat="server" AutoGenerateColumns="false"
    HeaderStyle-CssClass="darkbackground" AlternatingRowStyle-CssClass="dullbackground" RowStyle-CssClass="lightbackground"
    OnRowDataBound="gv_comment_RowDataBound">
    <Columns>
        <asp:BoundField DataField="name" SortExpression="name" HeaderText="Name" />
        <asp:BoundField DataField="comments" SortExpression="comments" HeaderText="Comments" />
        <asp:BoundField DataField="created_on" HeaderText="Created On" DataFormatString="{0:MM/dd/yyyy}" />
        <asp:TemplateField>
        <HeaderTemplate>
            <asp:ImageButton ID="ibtn_assign" runat="server" OnClick="ibtn_assign_click" ImageUrl="~/images/tools/j_save.png" />
        </HeaderTemplate>
        <ItemTemplate>
            <asp:CheckBox ID="chk_assignalert" runat="server" />
            <asp:HiddenField ID="hf_comment_id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"comment_id") %>' />
        </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    </div>
    </form>
</body>
</html>

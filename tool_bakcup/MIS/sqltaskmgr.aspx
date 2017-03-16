<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sqltaskmgr.aspx.cs" Inherits="sqltaskmgr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Event Log</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="jquery/jquery-ui.css" rel="stylesheet" type="text/css" />  
    <script src="jquery/jquery.min.js" type="text/javascript"></script>  
    <script src="jquery/jquery-ui.min.js" type="text/javascript"></script>  
      
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $("#txtArtCode").autocomplete({
                source: function (request, response) {
                    var obj = {};
                    obj.ID = $.trim($("[id*=txtArtCode]").val());
                    obj.JobTypeid = $.trim($("[id*=ddljobtype]").val());
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "sqltaskmgr.aspx/Article?",
                        data: JSON.stringify(obj),//"{'ID':'" + document.getElementById('txtArtCode').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        }
                    });
                }
            });
        }
    </script>   
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
        <div>
            <table class="bordertable" align="center" cellpadding="2" cellspacing="2">
                <tr>
                    <td><asp:Label ID="lblJobCodeName" runat="server" Text="Article Code :"></asp:Label></td>
                    <td><asp:TextBox ID="txtArtCode" runat="server" Text=""></asp:TextBox></td>
                    <td id="Td1" align="center" runat="server">
                    <asp:Button Text="Search" CssClass="dpbutton" runat="server" ID="btnSearch" OnClick="btnSearch_Click"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvArticles" runat="server" Width="100%" OnRowCommand="gvArticles_RowCommand" 
                            AutoGenerateColumns="false" Font-Size="8pt" CssClass="lightbackground" AllowSorting="True">
                            <HeaderStyle CssClass="darkbackground" />
                            <AlternatingRowStyle BackColor="#F2F2F2" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job ID" SortExpression="job_id" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EmpNoId" runat="server" CommandName="Display" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "JOB_id") %>'>
						                    <%# DataBinder.Eval(Container.DataItem, "JOB_id")%>
						                </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stage" SortExpression="job_stage_name" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobStage" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job Name" SortExpression="JobCode" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvArticleNo" runat="server" Text='<%# Eval("JobCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    <div >
        <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:800px;">
            <tr>
                <td>Job Type</td>
                <td>
                    <asp:DropDownList ID="ddljobtype" runat="server" 
                        OnSelectedIndexChanged="ddljobtype_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Article" Value="5" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Issue" Value="6" ></asp:ListItem>
                        <asp:ListItem Text="Book" Value="7" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Job No.:</td>
                <td><asp:TextBox ID="txtJobNumber" runat="server" Width="70px" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblchapName" runat="server" Text="Chapters:" Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpChapName" runat="server" Visible="false"></asp:DropDownList>
                </td>
                <td>
                    Type:
                </td>
                <td>
                    <asp:DropDownList ID="drpType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                        <asp:ListItem Selected="True">Common</asp:ListItem>
                        <asp:ListItem Value="Input">Input</asp:ListItem>
                        <asp:ListItem Value="Copyediting">Copyediting</asp:ListItem>
                        <asp:ListItem Value="Preediting">Preediting</asp:ListItem>
                        <asp:ListItem Value="Artwork">Artwork</asp:ListItem>
                        <asp:ListItem Value="Quality">Quality</asp:ListItem>
                        <asp:ListItem Value="EP">EP</asp:ListItem>
                        <asp:ListItem Value="Pagination">Pagination</asp:ListItem>
                    </asp:DropDownList>
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
                <asp:BoundField DataField="ACode" HeaderText="Job ID" />
                <asp:BoundField DataField="task_name" HeaderText="Process" />
                <asp:BoundField DataField="LSTARTDATE" HeaderText="Start Time" />
                <asp:BoundField DataField="LENDDATE" HeaderText="End Time" />
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
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uno") %>' CommandName="EndLogEvent" />
                        <asp:HiddenField ID="hf_logid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"leno") %>' />
                         <asp:HiddenField ID="hf_Plogid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PID") %>' />
                        <asp:HiddenField ID="hf_jobid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"JOB_TYPE_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

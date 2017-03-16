<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LaunchWIP.aspx.cs" Inherits="LaunchWIP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/common.js"></script>
   

</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <div class="dptitle">Launch WIP</div>
    <table>
        
    <tr>
    <td align="center">
        <asp:RadioButtonList ID="rdWIP" runat="server" AutoPostBack="True"  RepeatDirection="Horizontal" OnSelectedIndexChanged="rdWIP_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="P">Pending</asp:ListItem>
            <asp:ListItem>WIP</asp:ListItem>
        </asp:RadioButtonList>
        </td>
    </tr>
             <tr>
         <td align="center" >
            Month&nbsp;
                    <asp:DropDownList ID="DDMonthList" runat="server"  OnSelectedIndexChanged="DDMonthList_SelectedIndexChanged">
                    <asp:ListItem Value="0">---All---</asp:ListItem>
                        <asp:ListItem Selected="True" Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem  Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;&nbsp;Year&nbsp;
                    <asp:DropDownList ID="DDYearList" runat="server"  OnSelectedIndexChanged="DDYearList_SelectedIndexChanged">
                        <asp:ListItem Value="0">---All---</asp:ListItem>
                        <asp:ListItem  Value="2014">2014</asp:ListItem>
                        <asp:ListItem Selected="True"  Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem  Value="2018">2018</asp:ListItem>
                    </asp:DropDownList>
             <asp:Button CssClass="dpbutton" ID="submit" Text="Submit" runat="server" OnClick="submit_Click" />  
                </td>
         </tr>
    <tr>
    <td>
    <table>
                                       <tr>
                                       <td align="left">
                                        <asp:GridView ID="GvProject" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                      CssClass="lightbackground" width="100%" OnRowDataBound="GvProject_RowDataBound" 
                                      OnRowCommand="GvProject_RowCommand" HorizontalAlign="Center" >
                                            <HeaderStyle CssClass="darkbackground"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                 <asp:TemplateField SortExpression="parent_job_id" HeaderText="Sl.No" >
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("Sl") %>' ></asp:Label>
                                                               <br />
                                                        <%--<asp:HiddenField ID="hfgvProjectID" runat="server" Value='<%# Eval("pro_id") %>' />
                                                        <asp:HiddenField ID="hfgvProjectname" runat="server" Value='<%# Eval("projectname") %>' />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" >
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"pro_id") %>'></asp:Label>
                                                      </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField SortExpression="jobid" HeaderText="Job ID"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobid" runat="server" Text='<%# Eval("Jobid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="title" HeaderText="Job Name" HeaderStyle-Width="150" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("ProjectName") %>' Width="130px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="cust_name" HeaderText="Customer"  HeaderStyle-Width="100" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="cust_Loc" HeaderText="Customer Contact"  HeaderStyle-Width="80" ItemStyle-Width="80">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgveditor" runat="server" Text='<%# Eval("Projecteditor") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stage"    ItemStyle-HorizontalAlign="Center" >
                                                     <ItemTemplate>
                                                         <asp:DropDownList ID="DropStage"  runat="server">
                                                         </asp:DropDownList>
                                                     </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="task" HeaderText="Task"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvtask" runat="server" Text='<%# Eval("task") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Platform" HeaderText="Platform"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPlatform" runat="server" Text='<%# Eval("Platform") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Pages" HeaderText="Pages"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPages" runat="server" Text='<%# Eval("Pages") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="received_date"  HeaderText="Rec. Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("RecivDate") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField SortExpression="due_dateFrom" HeaderText="Due Date From" HeaderStyle-Width="80" ItemStyle-Width="80" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueDateFrom" runat="server" Text='<%# Eval("DueDateFrom") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_dateTo" HeaderText="Due Date To" HeaderStyle-Width="80" ItemStyle-Width="80">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueDateTo" runat="server" Text='<%# Eval("DueDateTo") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_Timefrom" HeaderText="Due Time From" HeaderStyle-Width="80" ItemStyle-Width="80" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueTimeFrom" runat="server" Text='<%# Eval("DueTimeFrom") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_Timeto" HeaderText="Due Time To" HeaderStyle-Width="80" ItemStyle-Width="80">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueTimeTo" runat="server" Text='<%# Eval("DueTimeTo") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField SortExpression="due_TimeFromIST" HeaderText="Due Time From(IST)" HeaderStyle-Width="80" ItemStyle-Width="80">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueTimeFromIST" runat="server" Text='<%# Eval("DueTimeFrom_IST") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_TimetoIST" HeaderText="Due Time To(IST)" HeaderStyle-Width="80" ItemStyle-Width="80">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueTimeToIST" runat="server" Text='<%# Eval("DuetimeTo_IST") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Del_Date" HeaderText="Delivery Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DelDate" Width="70px"  runat="server" CssClass="auto-feature" Text='<%#Eval("DeliveryDate")%>'></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender99" ClientIDMode="AutoID" Format="MM/dd/yyyy" TargetControlID="DelDate" runat="server">
                                                        </asp:CalendarExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField SortExpression="Del_Time" HeaderText="Delivery Time(IST)" >
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="DelTime" runat="server" Width="70" Text='<%#Eval("DeliveryTime")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Status" HeaderText="Status" >
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DropStatus"  runat="server">
                                                         </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="Remarks" HeaderText="Remarks" HeaderStyle-Width="200" ItemStyle-Width="200">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRemarks" runat="server" Text='<%# Eval("QuoteRemarks") %>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField>
                                               <ItemTemplate>
                                                    <asp:ImageButton ID="BtnSave" AlternateText="Save" ToolTip="Save" ImageUrl="~/images/tools/yes.png" runat="server" 
                                                     CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Pro_id") %>' CommandName="Save"
                                                      />
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                       </td>
                                       </tr>
                                       </table>
    </td>
    </tr></table>
    </div>
    </form>
</body>
</html>

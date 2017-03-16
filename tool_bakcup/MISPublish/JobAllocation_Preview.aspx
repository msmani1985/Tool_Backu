<%@ page language="C#" autoeventwireup="true" inherits="JobAllocation_Preview, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="divTitle" align="left" runat="server">Allocated Jobs</div>
                        <ol id="toc">
                        
                            <li id="LstMyJobsList" runat="server">
                                <asp:LinkButton ID="LnkMyJobsLst" runat="server"  OnClick="lnkMyJobList_Click" TabIndex="5" CommandName="LnkMyJobLst">My Job(s) List</asp:LinkButton></li>
                                <asp:HiddenField ID="hf_empiid" runat="server" />
                            <li id="LstMyTeamJobsList" runat="server">
                                <asp:LinkButton ID="LnkMyTeamJobsLst" runat="server"  OnClick="lnkMyTeamJobList_Click" CommandName="LnkMyTeamJobLst"
                                    TabIndex="6" >My Team Job(s) List</asp:LinkButton></li>
                                <asp:HiddenField ID="hf_empteamid" runat="server" />
                        </ol>  
                        
                        
                        <div class="content" id="tabGeneral" runat="server">
                        
                        <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                        
                        <tr>
        <td colspan="2" >
        <%--<div style="width:100%; height:300; overflow:auto;">--%>

       <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="400" Width="100%">
        
        
        <asp:GridView ID="gv_job_allocation" CaptionAlign="Left" runat="server" 
        AutoGenerateColumns="False" ForeColor="#333333" 
        GridLines="Vertical" AllowSorting="True" OnSorting="gv_general_Sorting" Width="100%" 
               OnRowDataBound="gv_joballocation_preview_RowDataBound" 
               Font-Names="Segoe UI" Font-Size="11px" >
         
        <HeaderStyle CssClass="header" />
        <rowstyle backcolor="white" Height="22px"/>
        <alternatingrowstyle backcolor="#F0FFF0" Height="22px"/>
        <Columns>
        <asp:TemplateField HeaderText="S.No.">

            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ROWID") %>'></asp:Label>
                <br />
                <%--<asp:HiddenField ID="hfgvInvoiceID" runat="server" Value='<%# Eval("ROWID") %>' />--%>
            </ItemTemplate>
        </asp:TemplateField>
        
        <%--<asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME">--%>
        
        
        
        <asp:BoundField DataField="NAME" HeaderText="Name"/>
       
        
        
        <%--</asp:BoundField>--%>
              
        <asp:BoundField DataField="cust_name" HeaderText="Customer Name" />
        <asp:BoundField DataField="job_type_name" HeaderText="Job Type" />
        <asp:BoundField DataField="document_item_name" HeaderText="Document Type Item" />
        <asp:BoundField DataField="job_stage_name" HeaderText="Job Stage Name" />
        <asp:BoundField DataField="cats_due_date" HeaderText="Cats Due date" /> 
        <asp:BoundField DataField="username" HeaderText="Last Accessed User" />
        <%--<asp:BoundField DataField="work_status" HeaderText="Job stage" ItemStyle-ForeColor=OrangeRed/>--%>
        <%-- <asp:BoundField DataField="invoice_date" HeaderText="INVOICE_DATE" SortExpression="invoice_date"/>--%>
        
       <%-- <asp:TemplateField>
        <ItemTemplate>
        <asp:CheckBox ID="CheckBox_MoveTask"  runat="server"/>
        <asp:HiddenField id="hiddenMoveTask" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' runat="server">
        </asp:HiddenField> 
        <asp:HiddenField id="hf_job_type_id" Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' runat="server">
        </asp:HiddenField> 
        </ItemTemplate>
        </asp:TemplateField>--%>
         <%--<asp:TemplateField>
        <HeaderTemplate>
        <asp:DropDownList OnSelectedIndexChanged="job_process_OnSelectedIndexChanged" AutoPostBack="true" ID="dd_jobstage" runat="server" >
         <asp:ListItem  Value="1">All Status</asp:ListItem>
                    <asp:ListItem  Value="2">Completed</asp:ListItem>
                    <asp:ListItem  Value="3">WIP</asp:ListItem>
                 
        </asp:DropDownList>
        
        </HeaderTemplate>
        <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem,"[JOB STAGE]") %>
        </ItemTemplate>
        </asp:TemplateField>--%>
        
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:DropDownList OnSelectedIndexChanged="job_process_OnSelectedIndexChanged" AutoPostBack="true" ID="dd_jobstage" runat="server" >
        <asp:ListItem  Value="0" >All Status</asp:ListItem>
                    <asp:ListItem  Value="1">Completed</asp:ListItem>
                    <asp:ListItem  Value="2">WIP</asp:ListItem>
        </asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem,"[work_status]") %>
        </ItemTemplate>
        </asp:TemplateField>
        
          
        </Columns>
        

        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
        
        </asp:GridView>
       </asp:Panel>
        </td>
        </tr>
       
                                
      </table>
            </div>            
                      
    </form>
</body>
</html>

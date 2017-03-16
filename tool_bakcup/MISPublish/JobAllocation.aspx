<%@ page language="C#" autoeventwireup="true" inherits="JobAllocation, App_Web_vlobbbje" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
</head>
 <style type="text/css"> 
      .header
        { 
            font-weight:bold;
             
   position:absolute; 
       background-color:White;
         } 
     
 </style> 

 <script type ="text/javascript">
     function tabFocus(e) {
         document.getElementById("HiddenField1").value = e.id;
     }

     function onKeyPress(evt) {
         evt = (evt) ? evt : window.event;
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode > 31 && (charCode < 48 || charCode > 57)) {
             return false;
         }
         return true;
     }
    </script>
<body>
    <form id="form1" runat="server">
       
    <div class="dptitle" id="divTitle" align="left" runat="server">Job Allocation</div>
    
                        <ol id="toc">
                        
                            <li id="LstGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral" runat="server" CommandName="General"  OnClick="lnkGeneral_Click" 
                                TabIndex="1" >General</asp:LinkButton></li>
                                <li id="LstAMO" runat="server">
                                <asp:LinkButton ID="lnkAMO" runat="server" OnClick="lnkAMO_Click"
                                    TabIndex="2">AMO</asp:LinkButton></li>
                                     <li id="LstAMOEP" runat="server">
                                <asp:LinkButton ID="lnkAMOEP" runat="server" OnClick="lnkAMOEP_Click"
                                    TabIndex="3">AMO-EP</asp:LinkButton></li>
                             <li id="LstPreEditing" runat="server">
                                <asp:LinkButton ID="lnkPE" runat="server" OnClick="lnkPE_Click"
                                    TabIndex="4">Pre-editing</asp:LinkButton></li>
                                       <li id="LstCollation" runat="server">
                                <asp:LinkButton ID="lnkCollation" runat="server" 
                                    TabIndex="5" OnClick="lnkCollation_Click">Collation</asp:LinkButton></li>
                            <li id="LstCE" runat="server">
                                <asp:LinkButton ID="lnkCE" runat="server"   OnClick="lnkCE_Click"
                                    TabIndex="6">Copy Editing</asp:LinkButton></li>
                           
                            <li id="LstDeft" runat="server">
                                <asp:LinkButton ID="lnkDEFT" runat="server"  OnClick="lnkDEFT_Click"
                                    TabIndex="7" >Tagging</asp:LinkButton></li>
                            <li id="LstArtWork" runat="server">
                                <asp:LinkButton ID="lnkArtWork" runat="server"  OnClick="lnkArtWork_Click"
                                    TabIndex="8">Artwork</asp:LinkButton></li>
                            
                            <li id="LstPagination" runat="server">
                                <asp:LinkButton ID="lnkPagination" runat="server"  OnClick="lnkPagination_Click" TabIndex="9">Pagination</asp:LinkButton></li>
                            <li id="LstQC" runat="server">
                                <asp:LinkButton ID="lnkQC" runat="server"  OnClick="lnkQC_Click" TabIndex="10">QC</asp:LinkButton></li>
                            <li id="LstEPD" runat="server">
                                <asp:LinkButton ID="lnkEPD" runat="server" OnClick="lnkEPD_Click" TabIndex="11">EP</asp:LinkButton></li>
                            
                         </ol>
                         
                        
                        <div id="div_Moveselection" runat = "server">
                        </div>
    <%--<asp:Panel CssClass="content" ID="mypanel" runat="server">--%>
                        <div class="content" id="tabGeneral" runat="server">
                        
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td align="right" >
                                    
                            <asp:Label ID="lbl_selection" runat="server" Text="Selection" Font-Bold = "True" Font-Size="12px" 
                                            ForeColor ="Green" Font-Name="Segoe UI"></asp:Label>
                            <asp:DropDownList ID="ddl_MoveSelection" runat="server" AutoPostBack="true"  
                                            OnSelectedIndexChanged="ddl_MoveSelection_SelectedIndexChanged" 
                                            Font-Names="Segoe UI" Font-Size="11px">
                                <asp:ListItem Text ="Move by Department" Value = "1" ></asp:ListItem>
                                <asp:ListItem Text ="Move by Employee" Value = "2"   Selected ="True" ></asp:ListItem>
                            </asp:DropDownList>
                                    
                                    <asp:Label ID="lbl_department" runat="server" Text="Department" Font-Bold = "True" Font-Size="12px" 
                                            ForeColor ="Green" Font-Name="Segoe UI"></asp:Label>
                                        <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true" 
                                            Font-Names="Segoe UI" Font-Size="11px">
                                    </asp:DropDownList>
                                    
                                    <asp:Label ID="lbl_employees" runat="server" Text="Employees"></asp:Label>
                                        <asp:DropDownList ID="ddl_employees" runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                    
                                    <asp:Label ID="lbl_task" runat="server" Text="Task"></asp:Label>
                                        <asp:DropDownList ID="ddl_task" runat="server">
                                    </asp:DropDownList>
                                         
                                        
                                        <asp:Button ID="btn_Move" CssClass="dpbutton" runat="server" Text="Move"  
                                            ToolTip="Move" OnClick="btnMove_Click" Width="90px"/>
                                        
                                        <asp:ImageButton ID="cmd_Excel_Export" ImageUrl="images/Excel.jpg" 
                                            runat="server" OnClick="ibtnExcel_Export_Click"
                                            ToolTip="Export Excel" Height="20px" />
                         
                                    </td>
                                </tr>
                                
        <tr>
        <td colspan="2" >
            <%--<asp:ListItem Selected="True" Value="0">--select--</asp:ListItem>--%>

       <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="340px" Width="100%">
        
        
           <asp:GridView ID="gv_job_allocation" runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Left" 
               CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
               OnRowCommand="gv_job_allocation_RowCommand" 
               OnRowDataBound="gv_job_allocation_RowDataBound" OnSorting="gv_general_Sorting" 
               Width="100%" Font-Names="Segoe UI" Font-Size="11px">
               <rowstyle backcolor="white"/>
               <alternatingrowstyle backcolor="#F0FFF0"/>
               <Columns>
                   <asp:TemplateField HeaderText="S.No.">
                       <ItemTemplate>
                           <asp:Label ID="Label1" runat="server" Text='<%# Eval("ROWID") %>'></asp:Label>
                           <br />
                           <%--<asp:HiddenField ID="hfgvInvoiceID" runat="server" Value='<%# Eval("ROWID") %>' />--%>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME" />
                   <asp:BoundField DataField="cust_name" HeaderText="Customer" 
                       SortExpression="cust_name" />
                   <asp:BoundField DataField="job_type_name" HeaderText="Job Type" 
                       SortExpression="job_type_name" />
                   <asp:BoundField DataField="document_item_name" HeaderText="Document Type Item" 
                       SortExpression="document_item_name" />
                   <asp:BoundField DataField="job_stage_name" HeaderText="Job Stage Name" 
                       SortExpression="job_stage_name" />
                   <asp:BoundField DataField="cats_due_date" HeaderText="Cats Due Date" 
                       SortExpression="cats_due_date" />
                   <asp:BoundField DataField="username" HeaderText="Last Accessed User" 
                       SortExpression="username" />
                   <asp:BoundField DataField="job_status_id" HeaderText="Job Status" 
                       SortExpression="job_status" Visible="False" />
                   <asp:TemplateField>
                       <ItemTemplate>
                           <asp:CheckBox ID="CheckBox_MoveTask" runat="server" />
                           <asp:HiddenField ID="hiddenMoveTask" runat="server" 
                               Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />
                           <asp:HiddenField ID="hf_job_type_id" runat="server" 
                               Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' />
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
               <HeaderStyle BackColor="Green" CssClass="darkbackground" Font-Bold="True" 
                   ForeColor="White" />
           </asp:GridView>
       </asp:Panel>
       
       <%--<div style="width:100%; height:300; overflow:auto;">--%>
       
        
       
       

        </td>
        </tr>
       
                                
      </table>
      
      <table>
            <tr>
                <td >
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="340px" Width="770px">
                        <asp:GridView ID="gv_job_allocation_Employee"  
                            CaptionAlign="Left" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" 
                            ForeColor="#333333" BorderStyle="Solid"  
                            GridLines="Vertical" AllowSorting="True" 
                            OnSorting="gv_general_Sorting_employee" Width="100%" 
                            OnRowDataBound="gv_job_allocation_employee_RowDataBound" 
                            OnRowCommand="gv_job_allocation_RowCommand" Font-Names="Segoe UI" Font-Size="11px">
                            <HeaderStyle CssClass="header" />
                            <rowstyle backcolor="white"/>
                            <alternatingrowstyle backcolor="#F0FFF0"/>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="Label1" runat="server" Text="<%# iRowId++ %>"></asp:Label>--%>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ROWID") %>' Width="30px"></asp:Label>
                                        <br />
                                        <%--<asp:HiddenField ID="hfgvInvoiceID" runat="server" Value='<%# Eval("ROWID") %>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME">
                                <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cust_name" HeaderText="Customer" 
                                    SortExpression="cust_name">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="job_type_name" HeaderText="Job Type" 
                                    SortExpression="job_type_name">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="document_item_name" HeaderText="Doc. Item" 
                                    SortExpression="document_item_name">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="job_stage_name" HeaderText="Stage" 
                                    SortExpression="job_stage_name">
                                    <ItemStyle Width="60px" />
                                   </asp:BoundField>
                                <asp:BoundField DataField="cats_due_date" HeaderText="Cats Due Date" 
                                    SortExpression="cats_due_date">
                                    <ItemStyle Width="100px" />
                                   </asp:BoundField>
                                <asp:BoundField DataField="username" HeaderText="Cur. Employee" 
                                    SortExpression="username">
                                <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="current_department" HeaderText="Current Department" 
                                    SortExpression="Current Department" Visible ="false"/>
                                <asp:BoundField DataField="job_status_id" HeaderText="JOB STATUS" 
                                    SortExpression="job_status" Visible="false" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox_MoveTask"  runat="server" />
                                        <asp:HiddenField id="hiddenMoveTask" 
                                    Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="hf_job_type_id" 
                                    Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' 
                                    runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="hf_job_history_id" 
                                    Value='<%# DataBinder.Eval(Container.DataItem,"job_history_id") %>' 
                                    runat="server">
                                        </asp:HiddenField>
                                    </ItemTemplate>
                                    <ItemStyle Width="35px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                            </asp:Panel>
                </td>
                <td>
                
                </td>
                <td >
                    <asp:Panel ID="pnlAllocatedEmp" runat="server" ScrollBars="Both" Height="340px" Width="400px">       
                        <asp:GridView ID="grdEmplyeeTask"  CaptionAlign="left" runat="server" 
                            AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" BorderStyle="Solid"  
                            GridLines="Vertical" AllowSorting="True" Width="100%" 
                            onrowdatabound="grdEmplyeeTask_RowDataBound" Font-Names="Segoe UI" Font-Size="11px" >
        
                        <HeaderStyle CssClass="header" />
                        <rowstyle backcolor="white"/>
                        <alternatingrowstyle backcolor="#F0FFF0"/>
                        <Columns>
        
                        <asp:TemplateField HeaderText="S.No.">

                            <ItemTemplate>
                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("ROWID") %>' Width="20px"></asp:Label>
                                <br />
                            </ItemTemplate>
                        </asp:TemplateField>
        
       
                        <asp:BoundField DataField="EMPLOYEE" HeaderText="Employee" SortExpression="EMPLOYEE" Visible="true"/>
                        
                      <%--  <asp:TemplateField HeaderText="Task">
                                <ItemTemplate>
                                    <asp:TextBox style="border:0px;" ID="txtEmployee" Text='<%# Eval("JOB_ORDER") %>' runat="server" Width="100px">
                                    </asp:TextBox>
                                </ItemTemplate>
                        </asp:TemplateField>--%>
                           
                        <asp:TemplateField HeaderText="Task">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_EmpTask" runat="server" Font-Names="Segoe UI" 
                                        Font-Size="11px" >
                                    </asp:DropDownList>
                                </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Order">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOrder" onKeyPress="return onKeyPress(event)" Text='<%# DataBinder.Eval(Container.DataItem,"JOB_ORDER") %>' runat="server" Width="20px" MaxLength="2">
                                    </asp:TextBox>
                                </ItemTemplate>
                        </asp:TemplateField>
          
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_Emp" runat="server" />
                                    <asp:HiddenField ID="hiddenMoveTask" runat="server" 
                                        Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />
                                    <asp:HiddenField ID="hf_job_type_id" runat="server" 
                                        Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' />
                                    <asp:HiddenField ID="hf_job_AEID" runat="server" 
                                        Value='<%# DataBinder.Eval(Container.DataItem,"AEID") %>' />
                                    <asp:HiddenField ID="hf_emp_id" runat="server" 
                                        Value='<%# DataBinder.Eval(Container.DataItem,"EMPLOYEE_ID") %>' />
                                    <asp:HiddenField ID="hf_task_id" runat="server" 
                                        Value='<%# DataBinder.Eval(Container.DataItem,"TASK_ID") %>' />
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
     
    <%--<asp:HiddenField ID="hfgvInvoiceID" runat="server" Value='<%# Eval("ROWID") %>' />--%>
     <div id="div_Error" runat="server" class="error">
         
    </div>

    
                        
     
     </form>
</body>
</html>

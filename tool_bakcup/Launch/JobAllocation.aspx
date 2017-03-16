<%@ page language="C#" autoeventwireup="true" inherits="JobAllocation, App_Web_wct36enf" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
    
    <title>Untitled Page</title>
    
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    
    <script type ="text/javascript">
    function tabFocus(e) {
    document.getElementById("HiddenField1").value = e.id;
    }
    
    function onKeyPress(evt)
    {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    </script>
    
    <%-- <script language="javascript">
    
    function removeOptions()
            {
                var i;
                for(i=selectbox.options.length-1;i>=0;i--)
                  {
                     if(selectbox.options[i].selected)
                     selectbox.remove(i);
                  }
                  
                  var myTab =parseInt( $get('lnkGeneral').value);
                  

var ActiveTabIndex = $get('lnkGeneral').control;

            }
    </script>
 <%--   <style>
    table 
{ 
  table-layout:fixed; 
} 
td 
{ 
  word-wrap:break-word; 
}
    </style>--%>
    
    <style type="text/css"> 
      .header
        { 
            font-weight:bold;
             
   position:absolute; 
       background-color:White;
         } 
         
         <%--.sssss tr td
        {
            border-top: 1px solid red;
            border-bottom: 1px solid red;
            
        }--%>

 </style> 
 
</head>
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
                         
                        
                        <div class="dptitle" id="div1" align="left" runat="server" style="height: 5px"></div>
                        
                        <div id="div_Moveselection" runat = "server">
                            <asp:Label ID="lbl_selection" runat="server" Text="Selection" Font-Bold = "true"  ForeColor ="green"></asp:Label>
                            <asp:DropDownList ID="ddl_MoveSelection" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddl_MoveSelection_SelectedIndexChanged">
                                <asp:ListItem Text ="Move by Department" Value = "1" ></asp:ListItem>
                                <asp:ListItem Text ="Move by Employee" Value = "2"   Selected ="True" ></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                            <%--<asp:Panel CssClass="content" ID="mypanel" runat="server">--%>
                        <div class="content" id="tabGeneral" runat="server">
                        
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td align="right" style="height: 22px">
                                    
                                    <asp:Label ID="lbl_department" runat="server" Text="Department"></asp:Label>
                                        <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true">
                                        <%--<asp:ListItem Selected="True" Value="0">--select--</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    
                                    <asp:Label ID="lbl_employees" runat="server" Text="Employees"></asp:Label>
                                        <asp:DropDownList ID="ddl_employees" runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                    
                                    <asp:Label ID="lbl_task" runat="server" Text="Task"></asp:Label>
                                        <asp:DropDownList ID="ddl_task" runat="server">
                                    </asp:DropDownList>
                                         
                                        
                                        <asp:Button ID="btn_Move" CssClass="dpbutton" runat="server" Text="Move"  ToolTip="Move" OnClick="btnMove_Click"/>
                                        
                                        <asp:ImageButton ID="cmd_Excel_Export" ImageUrl="images/Excel.jpg" runat="server" OnClick="ibtnExcel_Export_Click"
                                            ToolTip="Export Excel" />
                         
                                    </td>
                                </tr>
                                
        <tr>
        <td colspan="2" >
        <%--<div style="width:100%; height:300; overflow:auto;">--%>

       <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="470" Width="100%">
        
        
        <asp:GridView ID="gv_job_allocation"  CaptionAlign="Left" runat="server" 
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderStyle="Solid"  
        GridLines="Vertical" AllowSorting="True" OnSorting="gv_general_Sorting" Height="300px" Width="100%" OnRowDataBound="gv_job_allocation_RowDataBound" OnRowCommand="gv_job_allocation_RowCommand">
        
        <Columns>
        
        <asp:TemplateField HeaderText="S.No.">

            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text="<%# iRowId++ %>"></asp:Label>
                <br />
                <%--<asp:HiddenField ID="hfgvInvoiceID" runat="server" Value='<%# Eval("ROWID") %>' />--%>
            </ItemTemplate>
        </asp:TemplateField>
        
        
        
        <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME"/>
       
       
        
        <asp:BoundField DataField="cust_name" HeaderText="CUSTOMER NAME" SortExpression="cust_name"/>
        <asp:BoundField DataField="job_type_name" HeaderText="JOB TYPE" SortExpression="job_type_name"/>
        <asp:BoundField DataField="document_item_name" HeaderText="DOCUMENT TYPE ITEM" SortExpression="document_item_name"/>
       <asp:BoundField DataField="job_stage_name" HeaderText="JOB STAGE NAME" SortExpression="job_stage_name"/>
        <asp:BoundField DataField="cats_due_date" HeaderText="CATS DUE DATE" SortExpression="cats_due_date"/> 
        <asp:BoundField DataField="username" HeaderText="Last Accessed User" SortExpression="username"/>
        <asp:BoundField DataField="job_status_id" HeaderText="JOB STATUS" SortExpression="job_status" Visible = "False"/>
        <asp:TemplateField>
        <ItemTemplate>
        <asp:CheckBox ID="CheckBox_MoveTask"  runat="server" />
        <asp:HiddenField id="hiddenMoveTask" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' runat="server">
        </asp:HiddenField> 
        <asp:HiddenField id="hf_job_type_id" Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' runat="server">
        </asp:HiddenField> 
        
        </ItemTemplate>
        </asp:TemplateField>
          
        </Columns>
        

        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
        
        </asp:GridView>
       </asp:Panel>
       
       <%--Grid view for Employee allocation--%>
       
        <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="470" Width="100%">       
       <asp:GridView ID="gv_job_allocation_Employee"  CaptionAlign="left" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" BorderStyle="Solid"  
        GridLines="Vertical" AllowSorting="True" OnSorting="gv_general_Sorting_employee" Height="300" Width="100%" OnRowDataBound="gv_job_allocation_employee_RowDataBound" OnRowCommand="gv_job_allocation_employee_RowDataCommand">
        
        <HeaderStyle CssClass="header" />
        
        <Columns>
        
        <asp:TemplateField HeaderText="S.No.">

            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text="<%# iRowId++ %>"></asp:Label>
                <br />
                <%--<asp:HiddenField ID="hfgvInvoiceID" runat="server" Value='<%# Eval("ROWID") %>' />--%>
            </ItemTemplate>
        </asp:TemplateField>
        
        <%--<asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME">--%>
        
        
        
        <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME"/>
       
        
        
        <%--</asp:BoundField>--%>
       
       
        
        <asp:BoundField DataField="cust_name" HeaderText="CUSTOMER NAME" SortExpression="cust_name"/>
        <asp:BoundField DataField="job_type_name" HeaderText="JOB TYPE" SortExpression="job_type_name"/>
        <asp:BoundField DataField="document_item_name" HeaderText="DOCUMENT TYPE ITEM" SortExpression="document_item_name"/>
       <asp:BoundField DataField="job_stage_name" HeaderText="JOB STAGE NAME" SortExpression="job_stage_name"/>
        <asp:BoundField DataField="cats_due_date" HeaderText="CATS DUE DATE" SortExpression="cats_due_date"/> 
        <asp:BoundField DataField="username" HeaderText="Last Accessed User" SortExpression="username"/>
        <asp:BoundField DataField="current_department" HeaderText="Current Department" SortExpression="Current Department" Visible ="false"/>
        <asp:BoundField DataField="job_status_id" HeaderText="JOB STATUS" SortExpression="job_status" Visible="false" />
        <%-- <asp:BoundField DataField="invoice_date" HeaderText="INVOICE_DATE" SortExpression="invoice_date"/>--%>
        <asp:TemplateField HeaderText="NO OF TASK">
                <ItemTemplate>
                    <asp:TextBox ID="txtProcess" onKeyPress="return onKeyPress(event)" MaxLength="1" Text='<%# DataBinder.Eval(Container.DataItem,"no_of_process") %>' Visible="true" Width="50px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <asp:CheckBox ID="CheckBox_MoveTask"  runat="server" />
        <asp:HiddenField id="hiddenMoveTask" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' runat="server">
        </asp:HiddenField> 
        <asp:HiddenField id="hf_job_type_id" Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' runat="server">
        </asp:HiddenField> 
        <asp:HiddenField id="hf_job_history_id" Value='<%# DataBinder.Eval(Container.DataItem,"job_history_id") %>' runat="server">
        </asp:HiddenField>
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
     <%--</asp:Panel>--%>
     <div id="div_Error" runat="server" class="error"></div>
     </form>
</body>
</html>

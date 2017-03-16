<%@ page language="C#" autoeventwireup="true" inherits="JobInvoicePattern, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Job Invoice Patterns</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/common.js"></script>
     <script type="text/javascript">
     
     function validInvoiceTypeItem(){
        if(document.form1.drpCostInvoiceType!=null && document.form1.drpCostInvoiceType.value !="0" && document.form1.drpCostInvoiceType.value =="4"){
//            alert(document.getElementById ('divPopBCostInvTypeItem'));
//            alert(document.getElementById ('divMasked'));
            document.getElementById ('divPopBCostInvTypeItem').style.visibility='visible';
            document.getElementById ('divPopBCostInvTypeItem').style.display='';       
            document.getElementById ('divPopBCostInvTypeItem').style.top= '150px';
            document.getElementById ('divPopBCostInvTypeItem').style.left='248px'; 
            if (typeof document.body.style.maxHeight == "undefined")
            {
                var layer = document.getElementById ('divPopBCostInvTypeItem');
                layer.style.display = 'block';
                var iframe = document.getElementById('iframetop');
                iframe.style.display = 'block';
                iframe.style.visibility = 'visible';
                iframe.style.top= layer.offsetTop-10;
                iframe.style.left= layer.offsetLeft-10;
                iframe.style.width=  layer.offsetWidth+10;
                iframe.style.height= layer.offsetHeight+10;                
            }else
            {
                document.getElementById ('divMasked').style.display='';
                document.getElementById ('divMasked').style.visibility='visible';
                document.getElementById ('divMasked').style.top='0px';
                document.getElementById ('divMasked').style.left='0px';
                document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
                document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px'; 
            }  
            document.getElementById ('txtBCpopInvTypeItem').select();         
        }
        else {alert("Select Invoice Type to Additional Cost");}
    }
    </script>
     
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="divTitle" align="left" runat="server">Job Invoice Pattern </div>
    <div>
    
        <table align="center" class="bordertable" width="70%"  id="TABLE1">
            <tr style="border-bottom:solid 1px green;border-bottom-width:thick;">
            <td>
                    <asp:Label ID="Label2" runat="server" Text="Customer Name:" ></asp:Label>
                    
            </td>
          <td><asp:DropDownList  ID="ddl_CustomerName" runat="server" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="ddl_Customer_SelectedIndexedChanged" >
          <%--<asp:ListItem Selected="True" Value="-1">--select--</asp:ListItem>--%>
          </asp:DropDownList>
          <%--<asp:HiddenField ID="hfB_ID" runat="server" />--%>
          </td>
          <td>
          <asp:Label ID="Label1" runat="server" Text="Job Type:"  ></asp:Label>
          </td>
           <td><asp:DropDownList  ID="ddl_JobType" runat="server" OnSelectedIndexChanged="ddl_JobType_SelectedIndexedChanged" AutoPostBack="true">
                    <asp:ListItem Selected="True" Value="0">--select--</asp:ListItem>
                    <asp:ListItem Value="2">Book</asp:ListItem>
                    <asp:ListItem  Value="4">Project</asp:ListItem>
                    <asp:ListItem  Value="1">Journal</asp:ListItem>
                </asp:DropDownList></td>
                </tr>
            <tr id=trJournal runat="server">    
            <td>
                <asp:Label ID="Label3" runat="server" Text="Journals:" ></asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="ddl_Journals" runat="server" Width="307px" OnSelectedIndexChanged="ddl_Journal_selectedIndexedChanged" AutoPostBack="true">
            <%--<asp:ListItem Selected="True" Value="0">--select--</asp:ListItem> --%>
            </asp:DropDownList>
            </td>
           </tr>

          
        </table>
    </div>
    <div class="content" id="tabBookAddCost" runat="server">
        <table id="Table2" border="0" width="100%" cellpadding="2" cellspacing="0">
        <tr>
        <td colspan="4" class="dpJobGreenHeader" style="width: 657px">
        <img id="ImgBookCost" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
         <asp:Label ID="lblHeader" runat="server" ></asp:Label>    
         <td class="dpJobGreenHeader">
                                        <asp:ImageButton ID="cmd_Cost_new" ImageUrl="~/images/tools/j_new.png" runat="server"
                                            ToolTip="New" OnClick="cmd_Cost_new_Click" />
                                        <asp:ImageButton ID="cmd_Save_Cost" ImageUrl="~/images/tools/j_save.png" runat="server"
                                             ToolTip="Save" text="Save" OnClick="cmd_Save_Cost_Click" />
                                        <%--<asp:ImageButton ID="cmd_Cost_orderindex" ImageUrl="~/images/tools/j_index.png" runat="server"
                                            ToolTip="Order Index" OnClick="cmd_Cost_orderindex_Click" />--%>
        </td>
        </tr>
        
        <tr id="trBCCtrls" runat="server"> 
                                   <td colspan="5">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 137px; height: 21px;">
                                                    Invoice Type: <span style="font-size: 9pt; color: #ff0000">*</span>
                                                </td>
                                                <td style="width: 141px; height: 21px;">
                                                    <asp:DropDownList ID="drpCostInvoiceType" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="drpCostInvoiceType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">-- select --</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="height: 21px; width: 146px;">
                                                    &nbsp; Invoice Type Item: <span style="font-size: 9pt; color: #ff0000">*</span>&nbsp;</td>
                                                <td style="font-size: 8pt; height: 21px;">
                                                    <asp:DropDownList ID="drpCostInvoiceTypeItem" runat="server" Width="200px">
                                                    <asp:ListItem Selected="True" Value="0">-- select --</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<img id="imgbtnBCAddInvTypeItem" align="absMiddle" src="images/tools/add.png" style="cursor: pointer"
                                                        title="New Invoice Type Item" onclick="javascript:return validInvoiceTypeItem();"
                                                        runat="server" />--%>
                                                    </td>
                                                
                                               
                                            </tr>
                                          
                                            <tr style="font-size: 8pt">
                                                <td style="width: 137px">
                                                    Type of Cost: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                <td style="width: 141px; font-size: 8pt;">
                                                    <asp:DropDownList ID="drpCostType" runat="server" Width="200px">
                                                     <asp:ListItem Selected="True" Value="0">-- select --</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="font-size: 8pt; width: 146px">
                                                    <span style="color: #000000">&nbsp; Order Index:</span></td>
                                                <td style="font-size: 8pt">
                                                    <asp:TextBox ID="txtOrderIndex" runat="server" Width="50px" CssClass="TxtBox" onkeypress="javascript: return OnlyAllowNumbers(this,event);"></asp:TextBox>&nbsp;
                                                    Price Code: <span style="font-size: 9pt; color: #ff0000">*
                                                      <asp:TextBox ID="txtCostPriceCode" runat="server" Width="50px" CssClass="TxtBox" onkeypress="javascript: return OnlyAllowNumbers(this,event);" BackColor="#FFFFC0"></asp:TextBox></span></td>
                                            </tr>
                                            
                                            
                                            <tr>
                                            
                                             <td style="width: 137px">
                                                    Service Type: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                <td style="width: 141px; font-size: 8pt;">
                                                    <asp:DropDownList ID="drpServiceType" runat="server" Width="200px">
                                                     <asp:ListItem Selected="True" Value="0">-- select --</asp:ListItem>
                                                    </asp:DropDownList></td>
                                    
                                            <td>
                                                &nbsp;
                                                    Item Description</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtCostItemdesc" runat="server" CssClass="TxtBox" MaxLength="100"
                                                        ToolTip="Eg: 40204.PY9781420073669.PBHRD.XXXX" Width="400px"></asp:TextBox></td>
                                            </tr>
                                            
                                        </table>
                                        
                                        </td>
                                        </tr>
                               
                                
                                <tr>
                                    <td colspan="5">
                                    <asp:HiddenField ID="hfCostInvTypeItemID" runat="server" />
                                        <asp:GridView ID="gvCostInvoicePattern" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                            Width="100%" OnRowCommand="gvCostInvoicePattern_RowCommand">
                                            <Columns>
                                <asp:TemplateField HeaderText="Invoice Type Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInvTypeItem" runat="server" Text='<%# Eval("InvoiceType_item_Name") %>'></asp:Label>
                                        <%--<asp:HiddenField ID="hdn_invoicetypeitem" runat="server" Value='<%# Eval("job_invoice_type_item_id") %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type of Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTypeofCost" runat="server" Text='<%# Eval("cost_type_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvServiceType" runat="server" Text='<%# Eval("service_type_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPriceCode" runat="server" Text='<%# Eval("price_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesc" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Index">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderIndex" runat="server" Text='<%# Eval("order_index") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtngvEdit" runat="server" CommandName="pedit" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/tools/edit.png" ToolTip="Edit" />
                                        <asp:ImageButton ID="imgbtngvDelete" runat="server" CommandName="pdelete" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/tools/delete.png" OnClientClick="javascript:return confirm('Confirm delete?');" ToolTip="Delete" />
                                        <asp:HiddenField ID="hfgvPatternID" runat="server" Value='<%# Eval("job_cost_pattern_id") %>' />
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                                            <EmptyDataTemplate>
                                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="darkbackground" />
                                            <AlternatingRowStyle CssClass="dullbackground" />
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfCostPatternID" runat="server" />
                                    </td>
                                </tr>
        </table>
    </div>
    </form>
</body>
</html>

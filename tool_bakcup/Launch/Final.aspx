<%@ page language="C#" autoeventwireup="true" inherits="Final, App_Web_w6b3pav3" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Final Invoice</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
        
   <script type="text/javascript">
       function changeGreenImage(id)
        {
//            
//          if(document.getElementById('hfEmID').value!='1')//If software team then it has not change
//           {
//                document.getElementById(id).src'images/te-proofgreen.gif';
//                id.src='images/te-proofgreen.gif';
//                             
//            }
      }
        </script>
         
</head>
<body>
    <form id="form1" runat="server">
     <div id="div_header" class="dptitle" runat="server">
        Final Invoice
    </div>
    <div>
        <table class="bordertable" align="center">
            <tr><td>Customer</td><td><asp:DropDownList ID="ddlcustomer" runat="server" DataValueField="customer_id" DataTextField="cust_name"></asp:DropDownList></td>
                <td>Type</td><td>
                <asp:DropDownList ID="ddltype" runat="server">
                    <asp:ListItem Text="Issue" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Project" Value="4"></asp:ListItem>
                      <asp:ListItem Text="WIP_Article" Value="4"></asp:ListItem>
                      
                </asp:DropDownList></td>
                <td><asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="dpbutton" OnClick="btnsubmit_Click" /></td>
            </tr>
           </table>
           <div>&nbsp;
        &nbsp;&nbsp;</div>
        
           <div id ="diverror" runat="server" align="center">
           <asp:Label ID ="lblerror" Text="" runat="server" style="font-size: 8pt; background-color: Yellow;color: Black;"></asp:Label>
           </div>
    </div>
    <br />
    <div>
        &nbsp;
        &nbsp;&nbsp;
     
    </div>
    
      <div id="viewdiv" runat="server">
    <table  align="center" width="75%" border="0" ><tr><td >
        <asp:GridView Width="100%" ID="gv_invoicablejobs" runat="server" OnRowCommand="gv_invoice_row_command" AutoGenerateColumns="false" OnRowDataBound="gv_invoice_row_bound"   >
        <HeaderStyle BackColor="#EAFEE2" ForeColor="Green" Font-Bold="true"   Font-Size="Larger" />
            <Columns>
                <asp:BoundField HeaderText="Customer Name" DataField="cust_name" />
                <asp:BoundField HeaderText="Title" DataField="Title" />
                <asp:BoundField HeaderText="Dispatch" DataField="despatch_date" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:BoundField HeaderText="Type" DataField="job_type_name" />
                <asp:BoundField HeaderText="" DataField="INDIA_PREVIEW" Visible="false" />
                 <asp:BoundField HeaderText="" DataField="DUBLIN_PREVIEW" Visible="False" />
                
                 <asp:TemplateField >
                    <ItemTemplate>
                <%--     India Invoice--%>
                  <asp:ImageButton ID="imgindiabutton" ToolTip="Click to view IndiaInvoice" OnClientClick="javascript:changeGreenImage(this);" ImageUrl="images/te-proof.gif" runat="server" style="cursor:pointer;border:none" Height="28" CommandName="i"   />
                 
               <asp:HiddenField ID="hf_cusid1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"customer_id") %>' />
                 <asp:HiddenField ID="hf_jobid1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />
                  <asp:HiddenField ID="hf_jobtypeid1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' />
                 <asp:HiddenField ID="hf_locationid1" runat="server" Value="2" /> 
           
                  <asp:CheckBox ID="chkjobtest" runat="server" Visible="false" /> 
                           
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField>
                    <ItemTemplate>
                    <%--Dublin Invoice--%>
                 <asp:ImageButton ID="imgdublinbutton"  ToolTip="Click to view dublinInvoice" OnClientClick="javascript:changeGreenImage(this);" ImageUrl="images/te-proof.gif" runat="server" style="cursor:pointer;border:none" Height="28" CommandName="d"   />  
                        
                <asp:HiddenField ID="hf_cusid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"customer_id") %>' />
                 <asp:HiddenField ID="hf_jobid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />
                  <asp:HiddenField ID="hf_jobtypeid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' />
                 <asp:HiddenField ID="hf_locationid" runat="server" Value="1" />
  
                    </a></ItemTemplate>
                    
                </asp:TemplateField>
               <%-- <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_SavePrint" name="btn_SavePrint" runat="server" AlternateText="Save&Print" 
                        ToolTip="Save&Print" height="28"  ImageUrl="~/images/tools/j_save.png" AccessKey="S" 
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "job_id")%>' CommandName="saveprint" OnClientClick="javascript:return disablefn(this);" />
                    </ItemTemplate>              
                </asp:TemplateField>--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a target="_blank" id="a_firstmail" title="Click to Email Invoice" href="emailpreviewsql.aspx?location=d&iinvoiceno=<%# DataBinder.Eval(Container.DataItem,"invoice_no") %>&custno=<%# DataBinder.Eval(Container.DataItem,"customer_id") %>&job_id=<%# DataBinder.Eval(Container.DataItem,"JOB_ID") %>">
                            <img id="img_Fristmail" style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" 
                            alt="Email to Financial Contact" title="Email to Financial Contact" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <a target="_blank" id="a_secondmail" title="Click to Email Invoice" href="">
                        <img id="img_secondmail" style="cursor:pointer;border:none;" runat="server" visible='<%#(DataBinder.Eval(Container.DataItem,"customer_id").ToString()=="68" && DataBinder.Eval(Container.DataItem,"job_type_id").ToString()=="6")?true:false%>' src="images/temail.jpg" height="20"
                        alt="Email to Financial Contact" title="Email to Financial Contact" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Inv. No" DataField="invoice_no" />
                <asp:BoundField HeaderText="Inv. Date" DataField="invoice_date" />
                <asp:BoundField HeaderText="PE Name" DataField="CONTACT_EDITOR" />
            </Columns>
        </asp:GridView>
        &nbsp;
                 <asp:HiddenField ID="hfEmID" runat="server"     />  
    </td></tr>
    <tr>
        <asp:Label ID="lblmess" runat="server" Text=" "></asp:Label>
    </tr>
    </table>
    </div>
    </form>
    
</body>
</html>

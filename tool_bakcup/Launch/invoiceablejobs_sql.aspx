<%@ page language="C#" autoeventwireup="true" inherits="invoiceablejobs_sql, App_Web_w6b3pav3" enableeventvalidation="true" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>India Invoice</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
        
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
        <style type="text/css">
        .error
        {
        color: Red;
        font-weight:bold;
        font-size:10pt;
        text-align:center;
       }        </style>
 <%--  <script language="javascript">
       function changeGreenImage(id)
       {
           if(document.getElementById('hfEmID').value!='1')//If software team then it has not change
           {
             id.src='images/te-proofgreen.gif';
         }s
         }

    </script> 
         --%>
</head>

<body>
    <form id="form1" runat="server">
     <div id="div_header" class="dptitle" runat="server">
      India Invoice
    </div>
    <div id="div_error" class="error" runat="server"></div>
    <div>
        <table class="bordertable" align="center">
            <tr><td>Customer</td><td><asp:DropDownList ID="ddlcustomer" runat="server" DataValueField="customer_id" DataTextField="cust_name"></asp:DropDownList></td>
                <td>Type</td><td>
                <asp:DropDownList ID="ddltype" runat="server">
                    <asp:ListItem Text="Issues" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Project" Value="4"></asp:ListItem>
                     <asp:ListItem Text="WIP_Article" Value="5"></asp:ListItem>
                </asp:DropDownList></td>
                <td><asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="dpbutton" OnClientClick="showdiv();" OnClick="btnsubmit_Click" /></td>
               
            </tr>
        </table>
    </div>
    
    <br />
    <div>
        &nbsp;
        &nbsp;&nbsp;
    </div>

    <div id="divErrorfirst" runat="server" align="center">
  <asp:Label ID="lblerror" runat="server" Text="" style="font-size: 8pt; background-color: Yellow;color: Black;"></asp:Label>
  </div>
     <div>
        &nbsp;
        &nbsp;&nbsp;
    </div>
  <%-- <div id ="divgrid" style="width:100%; height: 600px; position: relative; overflow: scroll; z-index:0; border: 1px solid #CCC;">--%>
    <table  width="100%"  align=left >
      <tr>
      <td width="50%" valign=top style="vertical-align:top; ">
      <div style="vertical-align:top; ">
        <asp:GridView  ID="gv_invoicablejobs" runat="server" OnRowCreated="gv_rowcreated"
         OnRowCommand="gv_invoice_row_command" AutoGenerateColumns="false"
          OnRowDataBound="gv_invoice_row_bound"   >
        <HeaderStyle BackColor="#EAFEE2" ForeColor="Green" Font-Bold="true"   />
            <Columns>
                <asp:BoundField HeaderText="Customer" DataField="cust_name" />
                <asp:BoundField HeaderText="NAME" DataField="TITLE" />
                <asp:BoundField HeaderText="INDIA PREVIEW" DataField="INDIA_PREVIEW" Visible="false" />
                <asp:BoundField HeaderText="DUBLIN PREVIEW" DataField="DUBLIN_PREVIEW"  Visible="false"/>
                <asp:TemplateField >
                    <ItemTemplate>
                <%--     India Invoice--%>
               
               <asp:ImageButton ID="imgindiabutton" ToolTip="Click to view IndiaInvoice"  ImageUrl="images/te-proof.gif"  runat="server" style="cursor:pointer;border:none" Height="28" CommandName="i"    />
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
               
                <asp:ImageButton ID="imgdublinbutton" ToolTip="Click to view DublinInvoice"  ImageUrl="images/te-proof.gif"  runat="server" style="cursor:pointer;border:none" Height="28" CommandName="d"   />  
                <asp:HiddenField ID="hf_cusid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"customer_id") %>' />
                <asp:HiddenField ID="hf_jobid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />
                <asp:HiddenField ID="hf_jobtypeid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' />
                <asp:HiddenField ID="hf_locationid" runat="server" Value="1" />
                </a></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_SavePrint" runat="server" AlternateText="Save&Print" 
                        ToolTip="Save&Print" height="28"  ImageUrl="~/images/tools/j_save.png" AccessKey="S" 
                          CommandName="saveprint" />
               </ItemTemplate>              
                </asp:TemplateField>
                           
            </Columns>
        </asp:GridView>
        </div>
        &nbsp;
                 <asp:HiddenField ID="hfEmID" runat="server" />  
                 <asp:HiddenField ID="hfEMPID" runat="server" />  
                 
    </td>
    
   <td width="50%" valign=top  style="vertical-align:top; " > 

 <div id ="viewreport" style="vertical-align:top;"   runat=server>
 
   <%-- <div runat="server"  id="errormsgCRPT" style="font-size: 8pt; background-color: Yellow;color: Black; font-weight:bold">No Records Found!</div>--%>
     <asp:ImageButton ID="exportreport" ToolTip="ExportPDF" ImageUrl="images/pdf_icon.gif"  runat="server" style="cursor:pointer;border:none" Height="30" CommandName="tepdf" OnClick="exportpdf_click"   />
     
     <%--
        <input id="btnClose" type="button" onclick="javascript:self.close();" value="Close[x]"/>--%>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" DisplayGroupTree=False HasCrystalLogo=False HasSearchButton=False
     HasZoomFactorList=True EnableToolTips=true BestFitPage=True Width="50%" HasToggleGroupTreeButton=true 
     EnableViewState="true" DisplayPage="true" OnNavigate="CrystalReportViewer1_Navigate" 
     ReuseParameterValuesOnRefresh="True" HasExportButton=True    />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server"></CR:CrystalReportSource>
<%--    <asp:Button ID="btnexport" runat="server" Text="Export" CssClass="dpbutton" OnClick="btnexport_Click"   />--%>
    
   </div>
    </td>
 </tr>
 
    </table>
 <%--   </div> --%>
 <%--<div id="diverror" runat="server" align="center">
  <asp:Label ID="lblerr" runat="server" Text="" style="font-size: 8pt; background-color: Yellow;color: Black;"></asp:Label>
  </div>--%>
    
   
    </form>
    
</body>
</html>
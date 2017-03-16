<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueInvoiceCorrection.aspx.cs" Inherits="IssueInvoiceCorrection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript" >
        function getScrollBottom(p_oElem)
        {
         return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
        }
        function changeGreenImage(id)
        {
            if(document.getElementById('hfEmID').value!='1')//If software team then it has not change
            {
                //document.getElementById(id).src='images/te-proofgreen.gif';
                id.src='images/te-proofgreen.gif';
                
            }
        }
        function PopupWidow(url)
        {
            try
            {
                window.open(url);
                return true;
            }
            catch(err)
            {
                alert(err.description);
                return false;
            }
        }
        function testfunction()
        {
            alert("Test");
            return true;
        }
        function disablefn(obj)
        {
           if(confirm("Are you sure you want to save this invoice?"))
           {
                obj.style.visibility="hidden";
                obj.style.cursor='pointer';
                document.body.style.cursor = 'wait';
           }
           else
                 return false;         
           return true;
        }
        function cursor_clear() { document.body.style.cursor = 'auto';  }
        
    </script>
    
    <script type="text/javascript">
function TextAreaVisibility(){

if (document.getElementById('ddl_crossref').onclick)//only display the value when user select 1
document.getElementById('lbl_crossref').style.display='none';

}

</script>
    
</head>

<script lang="C#" runat=server>
    protected void Page_Unload(Object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["invDS"] = null;
        }
    }
</script>        
<body>
    <form id="form1" style="text-align:left" runat="server">
   <div class="dptitle" id="invtitle" runat="server" ></div>
   <div>
        <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
            <tr>
                <td></td><td style="width: 114px">Customer</td><td></td>
                <td style="width: 310px"><asp:DropDownList ID="ddlcustomer" runat="server" DataTextField="CUSTNAME" DataValueField="CUSTNO" Width="178px"></asp:DropDownList></td><td style="width: 5px"></td>
                </tr>
                <tr>
                <td></td><td>Type</td><td></td>
                <td style="width: 310px">
                <asp:DropDownList ID="ddljobtype" runat="server" OnSelectedIndexChanged="ddljobtype_SelectedIndexChanged" Width="178px" >
                    <asp:ListItem Value="0" Text="---------- Select ----------"></asp:ListItem>
                    <asp:ListItem Value="1" text="Issue"></asp:ListItem>
                    <asp:ListItem Value="2" text="Book"></asp:ListItem>
                    <asp:ListItem Value="3" text="Project"></asp:ListItem>
                    <asp:ListItem  Selected="True" Value="4" Text="WIP"></asp:ListItem>                    
                </asp:DropDownList> 
                </td>
                <td style="width: 5px"></td>
                </tr>
                <tr><td></td><td style="width: 114px">Job Number</td><td></td>
                <td style="width: 310px"><asp:TextBox ID="txtJobNumber" runat="server"  Width="249px" Text="CJRS130003" ></asp:TextBox></td>
                <td style="width: 5px"></td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                <td></td>
                <td style="width: 310px" >
                <asp:Button Text="Submit" CssClass="dpbutton" OnClientClick="form1.target='right'" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click" />
                </td>
                <td style="width: 5px"></td>
                </tr>
            </table>
            
    <div style="width:90%;text-align:right;">
        <%--<asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel"/>--%>
        <asp:ImageButton ID="btn_SaveArticle" runat="server" AlternateText="Save_Article"  OnClick="btn_Save_Article"  ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" />
    </div>
    
    <br />
    </div>
     
    <div>
     <table align="center">
       <tr>
         <td>
         <div style="width:100%">
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="gvIssueCorr" AllowPaging="False"  
      EnableViewState="true" runat="server" AutoGenerateColumns="false" 
            OnRowDataBound="gvIssueCorr_DataBound">
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
                
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="AARTICLECODE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>
                 
                 <asp:TemplateField HeaderText="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_ADNO" runat="server">
                            
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 
                 <asp:BoundField HeaderText="TYPENO" DataField="STYPENO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                <asp:BoundField HeaderText="INVOICE NO." DataField="INVNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />                    
                
                <%--<asp:TemplateField HeaderText="INVOICE NO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:Label ID="lbl_INVNO" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                --%>
                
                <asp:TemplateField HeaderText="COPY EDIT" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_ExtraCopyEdit" runat="server" Width="60px">
                            <asp:ListItem Text="Y" Value="Y" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText="CROSS REF" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_crossref" runat="server" Width="60px">
                            <asp:ListItem Text="Y" Value="Y" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="EPUB" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_epub" runat="server" Width="60px">
                            <asp:ListItem Text="Y" Value="Y" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
                
               <asp:TemplateField HeaderText="HIGH LEVEL COPYEDIT" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_highlevel_copyedit" runat="server" Width="60px">
                            <asp:ListItem Text="Y" Value="Y" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
              
              <asp:TemplateField HeaderText="CE PAGES" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                 <ItemTemplate>
                     <asp:TextBox ID="txtCE_PAGES" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CE_PAGES")%>' Width=50%  onkeypress="javascript: return OnlyAllowNumbers(this,event);"></asp:TextBox>
                 </ItemTemplate>
              </asp:TemplateField>
           
              <asp:TemplateField HeaderText="SAM PAGES" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                 <ItemTemplate>
                     <asp:TextBox ID="txtSAM_PAGES" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SAM_PAGES")%>' Width=50% onkeypress="javascript: return OnlyAllowNumbers(this,event);" ></asp:TextBox>
                 </ItemTemplate>
              </asp:TemplateField>
           
              <asp:TemplateField HeaderText="WIP PAGES" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                  <ItemTemplate>
                      <asp:TextBox ID="txtWIP_AREALNOOFPAGES" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WIP_AREALNOOFPAGES")%>' Width=50%  ></asp:TextBox>
                  </ItemTemplate>
              </asp:TemplateField>
           
              <asp:TemplateField HeaderText="ARTICLE PAGES" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                  <ItemTemplate>
                       <asp:TextBox ID="txtAREALNOOFPAGES" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AREALNOOFPAGES")%>' Width=50% ></asp:TextBox>
                  </ItemTemplate>
              </asp:TemplateField>
                     <asp:TemplateField HeaderText="PREEDIT" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Preedit" runat="server" Width="60px">
                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            <asp:ListItem  Selected="True" Text="N" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
              
               <asp:TemplateField HeaderText="REMOVE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_REMOVE" runat="server" Width="60px" AutoPostBack="TRUE" OnSelectedIndexChanged="ddl_REMOVE_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
              
              <%--<asp:TemplateField>
                     <ItemTemplate>
                           <asp:ImageButton ID="imgbtngvDelete" runat="server" CommandName="pdelete" ImageAlign="AbsMiddle"
                                ImageUrl="~/images/tools/delete.png" OnClientClick="javascript:return confirm('Confirm delete?');" ToolTip="Delete" />
                           <%--<asp:HiddenField ID="hfgvPatternID" runat="server" Value='<%# Eval("job_cost_pattern_id") %>' />--%>
                    <%--</ItemTemplate>
              </asp:TemplateField>--%>
              
              
              
         </Columns>
         
         
       </asp:GridView>
       </div>
        <br />
         <div style="width:90%;text-align:right;">
        <%--<asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel"/>--%>
        <asp:ImageButton ID="btn_SaveIssue" runat="server" AlternateText="SaveIssue"  OnClick="btn_Save_issue"  ToolTip="SaveIssue" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" />
         </div>
          <br />
        <div  style="width:100%">
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="gvIssues" AllowPaging="True"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false" 
            OnRowDataBound="gvIssues_DataBound">
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
                
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="INO" DataField="INO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ISSUE NUMBER" DataField="IISSUENO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="JOURNAL" DataField="JOURCODE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="TYPENO" DataField="STYPENO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="INVOICE NO" DataField="IINVOICENO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                    
                <asp:TemplateField HeaderText="CE Journal" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_COPYEDIT" runat="server" Width="60px"  >
                            <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                            <asp:ListItem Text="N" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="SAM Journal" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_SAM" runat="server" Width="60px" >
                            <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                            <asp:ListItem Text="N" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                    
                <asp:TemplateField HeaderText="CE_SECTIONDISPLAY" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_CEDISPLAY" runat="server" Width="60px">
                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="SAM_SECTIONDISPLAY" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_SAMDISPLAY" runat="server" Width="60px">
                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="CO_ACTION EPUB" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_epubSplit" runat="server" Width="60px" >
                            <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                            <asp:ListItem Text="N" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="IS ARTICLE BASED" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_ArticleBased" runat="server" Width="60px" >
                            <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                            <asp:ListItem Text="N" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="IS FPM" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_FPM" runat="server" Width="60px" >
                            <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                            <asp:ListItem Text="N" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>

         </Columns>
       </asp:GridView>   
       </div>     
    </td>
    </tr>
    </table>
        &nbsp;<br />
        &nbsp;</div>     
    <div id="divmessage" runat="server" >
    &nbsp;&nbsp;
        
        &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp;
        </div>
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:HiddenField ID="hfEmID" runat="server" />
        <div id="div_Error" runat="server" class="error"></div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="cw_InvoiceCorrection, App_Web_vlobbbje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Invoice Correction</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" >        
            

    </script>

    
<script lang="C#" runat=server>
    protected void Page_Unload(Object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["invDS"] = null;
        }
    }
</script>  

     

    
    <script type="text/javascript">
        function TexstAreaVisibility() {

            if (document.getElementById('ddl_crossref').onclick)//only display the value when user select 1
                document.getElementById('lbl_crossref').style.display = 'none';

        }

</script>
    
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
        .auto-style2 {
            width: 114px;
            height: 25px;
        }
        .auto-style3 {
            width: 310px;
            height: 25px;
        }
        .auto-style4 {
            width: 5px;
            height: 25px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <div class="dptitle" id="invtitle" runat="server" ></div>
          <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
            
                <td class="auto-style1"></td><td class="auto-style2">Customer</td><td class="auto-style1" style="color: #CC3300">*</td>
                <td class="auto-style3"><asp:DropDownList ID="ddlcustomer" runat="server" DataTextField="CUSTNAME" DataValueField="CUSTNO" Width="178px"></asp:DropDownList></td><td class="auto-style4"></td>
                </tr>
                <tr>
                <td></td><td>Type</td><td>&nbsp;</td>
                <td style="width: 310px">
                <asp:DropDownList ID="ddljobtype" runat="server" Width="178px" >
                    <asp:ListItem Value="0" Text="---------- Select ----------"></asp:ListItem>
                    <asp:ListItem Value="1" text="Issue" Selected="True"></asp:ListItem>
                  <%--  <asp:ListItem Value="2" text="Book"></asp:ListItem>
                    <asp:ListItem Value="3" text="Project"></asp:ListItem>
                    <asp:ListItem Value="4" Text="WIP"></asp:ListItem>        --%>            
                </asp:DropDownList> 
                </td>
                <td style="width: 5px"></td>
                </tr>
                <tr><td></td><td style="width: 114px">Job Number</td><td></td>
                <td style="width: 310px"><asp:TextBox ID="txtJobNumber" runat="server"  Width="249px" ></asp:TextBox></td>
                <td style="width: 5px"></td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                <td></td>
                <td style="width: 310px" >
                <asp:Button Text="Submit" CssClass="dpbutton" OnClientClick="form1.target='right'" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click"/>
                </td>
                <td style="width: 5px"></td>
                </tr>
            </table>
        </div>
    <div id="divNature" runat="server">

        <table style="width:100%">
              <tr>
                  <td align="right">
                    <asp:ImageButton ID="btn_SaveArticle" runat="server" AlternateText="Save_Article"  OnClick="btn_Save_Article"  ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" />
                  </td>
              </tr>
            <tr>
                <td>
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="gvIssueCorrCW" AllowPaging="False"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvIssueCorrCW_RowDataBound" width="100%" >
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
                
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO0" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="AARTICLECODE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>
                 
                 <asp:TemplateField HeaderText="Page Count" HeaderStyle-HorizontalAlign="Center"   ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <ItemTemplate>
                        <asp:TextBox ID="txtPagecnt" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "AREALNOOFPAGES")%>' ></asp:TextBox>     
                    </ItemTemplate>
                </asp:TemplateField>
                
                 
                 <asp:BoundField HeaderText="Pre-Process" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                <asp:BoundField HeaderText="CopyEdit"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />                    
                
                <asp:TemplateField HeaderText="First Proof" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_FirstProof" runat="server" Width="60px">
                            <asp:ListItem Text="CW" Value="CW"></asp:ListItem>
                            <asp:ListItem Text="DP" Value="DP"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText="Revises" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Revises" runat="server" Width="60px">
                             <asp:ListItem Text="CW" Value="CW"></asp:ListItem>
                            <asp:ListItem Text="DP" Value="DP"  Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="Press" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Press" runat="server" Width="60px">
                               <asp:ListItem Text="CW" Value="CW"></asp:ListItem>
                            <asp:ListItem Text="DP" Value="DP" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
                
               <asp:TemplateField HeaderText="Online" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Online" runat="server" Width="60px">
                              <asp:ListItem Text="CW" Value="CW"></asp:ListItem>
                            <asp:ListItem Text="DP" Value="DP"  Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
              
              <asp:TemplateField HeaderText="CW-Revision" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                 
              </asp:TemplateField>
           
              <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                  <ItemTemplate>
                      <asp:TextBox ID="txtPrice" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                 </ItemTemplate>
              </asp:TemplateField>
                   <asp:TemplateField HeaderText="Keying in KB" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                  <ItemTemplate>
                      </ItemTemplate>

                   </asp:TemplateField>
           
              <asp:TemplateField HeaderText="Total USD" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                  <ItemTemplate>
                      <asp:TextBox ID="txtTotal" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "USD")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                  </ItemTemplate>
              </asp:TemplateField>
         </Columns>         
       </asp:GridView>
                    </td>
                </tr>
        </table>
        
    </div>

         <div id="divSGM" runat="server">

        <table style="width:100%">
              <tr>
                  <td align="right">
                    <asp:ImageButton ID="btn_SGM_SaveArticle" runat="server" AlternateText="Save_Article"  OnClick="btn_SGM_SaveArticle_Click"  ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" />
                  </td>
              </tr>
            <tr>
                <td>
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="grdSGM" AllowPaging="False"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false"  width="100%" OnRowDataBound="grdSGM_RowDataBound" >
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
                
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO0" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="AARTICLECODE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>
                 
                 <asp:TemplateField HeaderText="Page" HeaderStyle-HorizontalAlign="Center"   ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <ItemTemplate>
                        <asp:TextBox ID="txtPagecnt" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "PageCount")%>' ></asp:TextBox>     
                    </ItemTemplate>
                </asp:TemplateField>
                
                                                  
                <asp:TemplateField HeaderText="First" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_FirstProof" runat="server" Width="80px">
                            <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText="Revises" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Revises" runat="server" Width="80px">
                             <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                            
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="Press" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Press" runat="server" Width="80px">
                               <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Online" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_Online" runat="server" Width="80px">
                            <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                        <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
              
            <asp:TemplateField HeaderText="PDF QA" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                     <asp:TextBox ID="txtPDFQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PDFQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >      
                <ItemTemplate>
                    <asp:TextBox ID="txtPDFPriceQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>           
            </asp:TemplateField>
                <asp:TemplateField HeaderText="XML QA" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >    
                    <ItemTemplate>
                    <asp:TextBox ID="txtXMLQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "XMLQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>               
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >     
                            <ItemTemplate>
                    <asp:TextBox ID="txtPriceXMLQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceXMLQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>              
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Vendor Management" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >     
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Vendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>     
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >    
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorPriceMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceVendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>             
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Customer Service" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >        
                  <ItemTemplate>
                    <asp:TextBox ID="txtCustService" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "CustService")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate> 
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtCustServicePrice" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceCustService")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtPricecode" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Pricecode")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                    </ItemTemplate>

                </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtTotal" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "USD")%>' Width="60%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>         
       </asp:GridView>
                    </td>
                </tr>
        </table>
        
    </div>

          <div id="divBJMBR" runat="server">

        <table style="width:100%">
              <tr>
                  <td align="right">
                    <asp:ImageButton ID="btn_BJMBR_SaveArticle" runat="server" AlternateText="Save_Article"    ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" OnClick="btn_BJMBR_SaveArticle_Click" />
                  </td>
              </tr>
            <tr>
                <td>
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="grdBJMBR" AllowPaging="False"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false"  width="100%" OnRowDataBound="grdBJMBR_RowDataBound"  >
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
                
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO0" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="AARTICLECODE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>
                 
                 <asp:TemplateField HeaderText="Page" HeaderStyle-HorizontalAlign="Center"   ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <ItemTemplate>
                        <asp:TextBox ID="txtPagecnt" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "PageCount")%>' ></asp:TextBox>     
                    </ItemTemplate>
                </asp:TemplateField>
                
                                                  
                <asp:TemplateField HeaderText="First" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_FirstProof" runat="server" Width="80px">
                            <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText="Revises" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Revises" runat="server" Width="80px">
                             <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                            
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="Press" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Press" runat="server" Width="80px">
                               <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Online" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_Online" runat="server" Width="80px">
                            <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                        <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
              
            <asp:TemplateField HeaderText="PDF QA" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                     <asp:TextBox ID="txtPDFQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PDFQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >      
                <ItemTemplate>
                    <asp:TextBox ID="txtPDFPriceQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>           
            </asp:TemplateField>
                <asp:TemplateField HeaderText="XML QA" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >    
                    <ItemTemplate>
                    <asp:TextBox ID="txtXMLQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "XMLQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>               
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >     
                            <ItemTemplate>
                    <asp:TextBox ID="txtPriceXMLQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceXMLQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>              
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOI Insertion" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >     
                 <ItemTemplate>
                    <asp:TextBox ID="txtDoi" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Doi")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>     
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >    
                 <ItemTemplate>
                    <asp:TextBox ID="txtDoiPrice" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceDOI")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>             
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Vendor Management" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >     
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Vendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>     
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >    
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorPriceMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceVendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>             
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Customer Service" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >        
                  <ItemTemplate>
                    <asp:TextBox ID="txtCustService" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "CustService")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate> 
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtCustServicePrice" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceCustService")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                
           
            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >
                <ItemTemplate>
                    <asp:TextBox ID="txtTotal" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "USD")%>' Width="100%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>         
       </asp:GridView>
                    </td>
                </tr>
        </table>
        
    </div>

        <div id="divANS" runat="server">

        <table style="width:100%">
              <tr>
                  <td align="right">
                    <asp:ImageButton ID="btn_ANS_SaveArticle" runat="server" AlternateText="Save_Article"    ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" OnClick="btn_ANS_SaveArticle_Click" />
                  </td>
              </tr>
            <tr>
                <td>
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="grdANS" AllowPaging="False"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false"  width="100%"  >
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
                
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO0" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="AARTICLECODE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>
                 
                 <asp:TemplateField HeaderText="Page" HeaderStyle-HorizontalAlign="Center"   ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <ItemTemplate>
                        <asp:TextBox ID="txtPagecnt" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "PageCount")%>' ></asp:TextBox>     
                    </ItemTemplate>
                </asp:TemplateField>
                
                                                  
                <asp:TemplateField HeaderText="First" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_FirstProof" runat="server" Width="80px">
                            <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText="Revises" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Revises" runat="server" Width="80px">
                             <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                            
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="Press" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Press" runat="server" Width="80px">
                               <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Online" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_Online" runat="server" Width="80px">
                            <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                        <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
              
                

             <asp:TemplateField HeaderText="Vendor Management" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >     
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Vendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>     
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >    
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorPriceMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceVendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>             
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Customer Service" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >        
                  <ItemTemplate>
                    <asp:TextBox ID="txtCustService" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "CustService")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate> 
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtCustServicePrice" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceCustService")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtPricePage" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Pricecode")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>     
           
            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >
                <ItemTemplate>
                    <asp:TextBox ID="txtTotal" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "USD")%>' Width="80%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>         
       </asp:GridView>
                    </td>
                </tr>
        </table>
        
    </div>


        <div id="divCRM" runat="server">

        <table style="width:100%">
              <tr>
                  <td align="right">
                    <asp:ImageButton ID="btn_CRM_SaveArticle" runat="server" AlternateText="Save_Article"    ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" OnClick="btn_CRM_SaveArticle_Click" />
                  </td>
              </tr>
            <tr>
                <td>
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="grdCRM" AllowPaging="False"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false"  width="100%" OnRowDataBound="grdCRM_RowDataBound"  >
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
                
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO0" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="AARTICLECODE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>
                 
                 <asp:TemplateField HeaderText="Page" HeaderStyle-HorizontalAlign="Center"   ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <ItemTemplate>
                        <asp:TextBox ID="txtPagecnt" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "PageCount")%>' ></asp:TextBox>     
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField HeaderText="Pre-Process" HeaderStyle-HorizontalAlign="Center"  DataField="Preprocess" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80"  />
                <asp:BoundField HeaderText="CopyEdit"  HeaderStyle-HorizontalAlign="Center"  DataField="CE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80"  />                    
                                                  
                <asp:TemplateField HeaderText="First" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_FirstProof" runat="server" Width="80px">
                            <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText="Revises" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Revises" runat="server" Width="80px">
                             <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend" Selected="True"></asp:ListItem>
                            
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="Press" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_Press" runat="server" Width="80px">
                               <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                            <asp:ListItem Text="Reverend" Value="Reverend" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
              </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Online" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_Online" runat="server" Width="80px">
                            <asp:ListItem Text="OKS" Value="OKS"></asp:ListItem>
                        <asp:ListItem Text="Reverend" Value="Reverend" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="PDF QA" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                     <asp:TextBox ID="txtPDFQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PDFQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >      
                <ItemTemplate>
                    <asp:TextBox ID="txtPDFPriceQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>           
            </asp:TemplateField>
                

             <asp:TemplateField HeaderText="Vendor Management" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >     
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Vendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>     
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >    
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorPriceMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceVendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>             
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Customer Service" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >        
                  <ItemTemplate>
                    <asp:TextBox ID="txtCustService" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "CustService")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate> 
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtCustServicePrice" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceCustService")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtPricePage" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Pricecode")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>     
           
            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >
                <ItemTemplate>
                    <asp:TextBox ID="txtTotal" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "USD")%>' Width="80%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>         
       </asp:GridView>
                    </td>
                </tr>
        </table>
        
    </div>

           <div id="divNASP" runat="server">

        <table style="width:100%">
              <tr>
                  <td align="right">
                    <asp:ImageButton ID="btn_NASP_SaveArticle" runat="server" AlternateText="Save_Article"    ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" OnClick="btn_NASP_SaveArticle_Click" />
                  </td>
              </tr>
            <tr>
                <td>
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="grdNASP" AllowPaging="False"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false"  width="100%" OnRowDataBound="grdNASP_RowDataBound"  >
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
                
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO0" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="Articles" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>
                 
                 <asp:TemplateField HeaderText="Page" HeaderStyle-HorizontalAlign="Center"   ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <ItemTemplate>
                        <asp:TextBox ID="txtPagecnt" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "PageCount")%>' ></asp:TextBox>     
                    </ItemTemplate>
                </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtPricePage" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Pricecode")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>     
           
            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >
                <ItemTemplate>
                    <asp:TextBox ID="txtTotal" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "USD")%>' Width="80%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>         
       </asp:GridView>
                    </td>
                </tr>
        </table>
        
    </div>

        <div id="divMaryAnn" runat="server">

        <table style="width:100%">
              <tr>
                  <td align="right">
                    <asp:ImageButton ID="btn_MaryAnn_SaveArticle" runat="server" AlternateText="Save_Article"    ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" OnClick="btn_Mary_SaveArticle_Click" Visible="false" />
                  </td>
              </tr>
            <tr>
                <td>
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="grdMaryAnn" AllowPaging="False"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false"  width="100%" OnRowDataBound="grdNASP_RowDataBound"  >
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO0" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="Articles" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>
                 
                
                
            <asp:TemplateField HeaderText="Price/Article rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtPricePage" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Pricecode")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>     
           
            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >
                <ItemTemplate>
                    <asp:TextBox ID="txtTotal" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "USD")%>' Width="80%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>         
       </asp:GridView>
                    </td>
                </tr>
        </table>
        
    </div>


        <div id="divUkBooks" runat="server">

        <table style="width:100%">
              <tr>
                  <td align="right">
                    <asp:ImageButton ID="btn_UkBooks_SaveArticle" runat="server" AlternateText="Save_Article"    ToolTip="SaveArticle" ImageUrl="~/images/tools/j_save.png" AccessKey="S" CommandName="saveprint" OnClick="btn_UkBooks_SaveArticle_Click" Visible="false" />
                  </td>
              </tr>
            <tr>
                <td>
            <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="grdUkBooks" AllowPaging="False"  
            EnableViewState="true" runat="server" AutoGenerateColumns="false"  width="100%">
                <HeaderStyle CssClass="GVFixedHeader" />
                <FooterStyle CssClass="GVFixedFooter" /> 
               <Columns>
               
               <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                    <HeaderTemplate>
                        <asp:Label ID="lblSNO0" Text="SLNO" runat="server"></asp:Label>
                    </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                    </ItemTemplate >
               </asp:TemplateField>
           
                 <asp:BoundField HeaderText="ANO" DataField="ANO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <asp:BoundField HeaderText="ARTICLE" DataField="TITLE" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />
                 <%--<asp:BoundField HeaderText="ADNO" DataField="ADNO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" />--%>

                   <asp:TemplateField HeaderText="First Proof" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_First" runat="server" Width="80px">
                        <asp:ListItem Text="Exemplarr" Value="Exemplarr" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="PDF QA" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                     <asp:TextBox ID="txtPDFQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PDFQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >      
                <ItemTemplate>
                    <asp:TextBox ID="txtPDFPriceQA" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceQA")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>           
            </asp:TemplateField>
                

             <asp:TemplateField HeaderText="Vendor Management" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >     
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Vendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>     
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >    
                 <ItemTemplate>
                    <asp:TextBox ID="txtVendorPriceMgt" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "PriceVendor")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>             
            </asp:TemplateField>
                
                
            <asp:TemplateField HeaderText="Price/page rate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" >
                <ItemTemplate>
                    <asp:TextBox ID="txtPricePage" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Pricecode")%>' Width="50%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>     
           
            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >
                <ItemTemplate>
                    <asp:TextBox ID="txtTotal" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "USD")%>' Width="80%" ReadOnly="true" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>         
       </asp:GridView>
                    </td>
                </tr>
        </table>
        
    </div>

       <div id="div_Error" runat="server" class="error"></div>
    </form>
</body>
</html>

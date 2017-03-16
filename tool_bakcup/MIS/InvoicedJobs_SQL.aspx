<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoicedJobs_SQL.aspx.cs" Inherits="InvoicedJobs_SQL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript" >
        function getScrollBottom(p_oElem) {
            return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
        }
        function changeGreenImage(id) {
            if (document.getElementById('hfEmID').value != '1')//If software team then it has not change
            {
                //document.getElementById(id).src='images/te-proofgreen.gif';
                id.src = 'images/te-proofgreen.gif';

            }
        }
        function PopupWidow(url) {
            try {
                window.open(url);
                return true;
            }
            catch (err) {
                alert(err.description);
                return false;
            }
        }
        function testfunction() {
            alert("Test");
            return true;
        }
        function disablefn(obj) {
            if (confirm("Are you sure you want to save this invoice?")) {
                obj.style.visibility = "hidden";
                obj.style.cursor = 'pointer';
                document.body.style.cursor = 'wait';
            }
            else
                return false;
            return true;
        }
        function cursor_clear() { document.body.style.cursor = 'auto'; }
        
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
    <div style="margin-bottom:20px;">
            <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
                <tr><td>Customer</td><td>
                 <asp:DropDownList ID="ddlcustomer" runat="server" DataTextField="CUSTNAME" DataValueField="CUSTNO">
                 </asp:DropDownList>
                </td>
                <td>Type</td><td>
                <asp:DropDownList ID="ddljobtype" runat="server" OnSelectedIndexChanged="ddljobtype_SelectedIndexChanged" >
                    <asp:ListItem Value="1" text="Journal" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" text="Book"></asp:ListItem>
                    <asp:ListItem Value="3" text="Project"></asp:ListItem>
                    <asp:ListItem Value="4" Text="WIP"></asp:ListItem>                    
                </asp:DropDownList> 
                </td>
                <td align="center" >
                <asp:Button Text="Submit" CssClass="dpbutton" OnClientClick="form1.target='right'" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click" />
                </td></tr>
            </table>
    </div>      
    <asp:Panel runat="server" ID="pnlContainer" ScrollBars="Auto" style="display:none " Height="300px" Width="90%" >
    </asp:Panel>
    <div style="width:90%;text-align:right;"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  /></div>
    <br />
    <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="adgdispatchedlist" 
    AllowSorting="True" width="97%" runat="server" AutoGenerateColumns="False"
		AllowPaging="False" CellPadding="1" OnRowCommand="Grid_RowCommand" OnRowDataBound="Grid_RowDataBound"  
		OnSorting="Grid_Sorting">
        <HeaderStyle CssClass="GVFixedHeader" />
        <FooterStyle CssClass="GVFixedFooter" />         
		<Columns>
			<asp:BoundField DataField="CNAME" SortExpression="CNAME" ItemStyle-Width="170" HeaderStyle-HorizontalAlign="left" HeaderText="Customer" />
			<%--<asp:TemplateField SortExpression="CNAME" ItemStyle-Width="170" HeaderStyle-HorizontalAlign="left"  HeaderText="Customer">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "CNAME") %>
				</ItemTemplate>
			</asp:TemplateField>--%>
			<asp:BoundField DataField="JOURCODE" ItemStyle-Width="60" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderText="Journal" />
			<%--<asp:TemplateField SortExpression="JOURCODE" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  HeaderText="Journal">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "JOURCODE") %>
				</ItemTemplate> 
			</asp:TemplateField>--%>
			<asp:TemplateField ItemStyle-Width="100" HeaderText="Issue" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
			    <ItemTemplate>
			         <%# DataBinder.Eval(Container.DataItem,"JOURCODE") %><%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>
			    </ItemTemplate>
			</asp:TemplateField>
			<%--<asp:TemplateField SortExpression="IISSUENO" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  HeaderText="Issue" >
				<ItemTemplate>
				    <!--<a target="_blank" title="Click Here to Preview Indian format" href="invoicepreview.aspx?location=i&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO")%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE")%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO")%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO")%>">
					<%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>
					</a>-->
					<%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>
				</ItemTemplate>
			</asp:TemplateField>--%>
			<asp:TemplateField ItemStyle-Width="90" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  HeaderText="Job Number">
				<ItemTemplate>
				    <!--<a target="_blank" title="Click Here to Preview Dublin format" href="invoicepreview.aspx?location=d&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO")%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE")%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO")%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO")%>">
					<%# DataBinder.Eval(Container.DataItem, "IJOBNUMBER") %>
					</a>-->
					<%# DataBinder.Eval(Container.DataItem, "IJOBNUMBER") %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField SortExpression="ICREATEDATE" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  HeaderText="Received Date">
				<ItemTemplate>
					<%--<%# DataBinder.Eval(Container.DataItem, "ICREATEDATE").ToString().Substring(0, DataBinder.Eval(Container.DataItem, "ICREATEDATE").ToString().IndexOf(" ")  )%>--%>
					<%# (DataBinder.Eval(Container.DataItem,"ICREATEDATE")!=DBNull.Value && DataBinder.Eval(Container.DataItem,"ICREATEDATE").ToString()!="")? Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ICREATEDATE")).ToShortDateString():""%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField SortExpression="IDUEDATE" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  HeaderText="Due Date" >
				<ItemTemplate>
                    <%--<%# DataBinder.Eval(Container.DataItem, "IDUEDATE").ToString().Substring(0, DataBinder.Eval(Container.DataItem, "IDUEDATE").ToString().IndexOf(" ")) %>						--%>
                    <%# (DataBinder.Eval(Container.DataItem, "IDUEDATE") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IDUEDATE").ToString() != "") ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "IDUEDATE")).ToShortDateString() : ""%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField SortExpression="LEDATE" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  HeaderText="Dispatched Date">
				<ItemTemplate>
                    <%--<%# DataBinder.Eval(Container.DataItem, "LEDATE").ToString().Substring(0, DataBinder.Eval(Container.DataItem, "LEDATE").ToString().IndexOf(" ")) %>--%>		
               		<%# (DataBinder.Eval(Container.DataItem,"LEDATE")!=DBNull.Value && DataBinder.Eval(Container.DataItem,"LEDATE").ToString()!="")?Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "LEDATE")).ToShortDateString() : ""%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="">
				<ItemTemplate>
				    <%--<asp:ImageButton ID="imgbtn_IndiaPreview" CommandName="India_Preview" style="cursor:pointer" OnClick="Journal_India_preview" ImageUrl="images/TE-proof.gif" OnClientClick="javascript:changeGreenImage(this);" Height="28" runat="server" />--%>
				    
				<a target="_blank" style="border:none"  href="previewinvoice.aspx?location=i&category=1&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO").ToString().Trim()%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO").ToString().Trim()%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE").ToString().Trim()%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO").ToString().Trim()%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO").ToString().Trim()%>&invoiceno=<%# DataBinder.Eval(Container.DataItem,"IINNO") %>">
				    <img alt="Click to View Indian Invoice" onclick="javascript:changeGreenImage(this);" runat="server" name="Journal_imgIndiaPDF" id="Journal_imgIndiaPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
			    </a>
			    
			   <%-- <a target="_blank" style="border:none" title="Click Here to Preview India format" href="invoicepreview.aspx?location=i&category=1&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO")%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE")%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO")%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO")%>">
				    <img alt="test" id="img1" style="cursor:pointer; border:none " src="images/TE-proof.jpg" height="20"  />
			    </a>--%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="">
				<ItemTemplate>
				    <asp:ImageButton AlternateText="Approve" ImageUrl="images/approve.gif" id="btnApprove" height="28" AccessKey="A" style="cursor :pointer" runat="server" />  
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="">
				<ItemTemplate>
				<%--<a target="_blank" style="border:none" title="Click Here to Preview Dublin format" href="invoicepreview.aspx?location=d&category=1&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO").ToString().Trim()%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO").ToString().Trim()%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE").ToString().Trim()%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO").ToString().Trim()%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO").ToString().Trim()%>&invoiceno=<%# DataBinder.Eval(Container.DataItem,"IINNO") %>">
				    <img alt="Click to View Dublin Invoice" id="imgDublinPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
			    </a>--%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="">
				<ItemTemplate>
				    <%--<asp:ImageButton ID="imgbtn_DublinPreview" OnClientClick="javascript:changeGreenImage(this);" CommandName="Dublin_Preview" style="cursor:pointer" ImageUrl="images/TE-proof.gif" Height="28" runat="server" />--%>
				
				<a target="_blank" style="border:none"  title="Click Here to Preview Dublin format" href="previewinvoice.aspx?location=d&category=1&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO").ToString().Trim()%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO").ToString().Trim()%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE").ToString().Trim()%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO").ToString().Trim()%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO").ToString().Trim()%>&invoiceno=<%# DataBinder.Eval(Container.DataItem,"IINNO") %>">
				    <img alt="Click to View Dublin Invoice" onclick="javascript:changeGreenImage(this);" runat="server" name="Journal_imgDublinPDF" id="Journal_imgDublinPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
			    </a>
				<%--<a target="_blank" style="border:none" title="Click Here to Preview Dublin format" href="invoicepreview.aspx?location=d&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO")%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE")%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO")%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO")%>">
				<!--<a target="_blank" style="border:none" title="Click Here to View PDF" href="pdfpreview.aspx?iissueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURCODE")%>">-->
				    <img alt="test" id="imgDublinPDF" style="cursor:pointer ;border:none" src="images/TE-proof.jpg" height="20" />
			    </a>--%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField>
			    <ItemTemplate>
			        <asp:ImageButton AlternateText="Save&Print" name="btnsaveprint" ToolTip="Save&Print"  height="28"  ImageUrl="~/images/tools/j_save.png" id="btnsaveprint" AccessKey="S" 
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "INO")%>' CommandName="saveprint"  runat="server" OnClientClick="javascript:return disablefn(this);"  />  
			    </ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="PE Mail">
				<ItemTemplate>
				    <%--<a target="_blank" id="firstanchor"  title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=1&iinvoiceno=<%# DataBinder.Eval(Container.DataItem,"IINNO") %>&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO").ToString().Trim()%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO").ToString().Trim()%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE").ToString().Trim()%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO").ToString().Trim()%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO").ToString().Trim()%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "INVDNAME").ToString().Trim()%>&Mailflg=1">				
					    <img id="Img1" style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Click to Email Invoice" title="Click to Email Invoice" />
					</a>--%>
					<a target="_blank" id="firstanchor"  title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=1&iinvoiceno=<%# DataBinder.Eval(Container.DataItem,"IINNO") %>&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO").ToString().Trim()%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO").ToString().Trim()%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE").ToString().Trim()%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO").ToString().Trim()%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO").ToString().Trim()%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "INVDNAME").ToString().Trim()%>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO") %>&finsiteno=<%# DataBinder.Eval(Container.DataItem,"FINNO")%>&Mailflg=1">				
					    <img id="Img1"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email to Financial Contact" title="Email to Financial Contact" />
					</a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Second Mail">
				<ItemTemplate>
				    <a target="_blank" id="secondanchor" title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=1&iinvoiceno=<%# DataBinder.Eval(Container.DataItem,"IINNO") %>&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO").ToString().Trim()%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO").ToString().Trim()%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE").ToString().Trim()%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO").ToString().Trim()%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO").ToString().Trim()%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "INVDNAME").ToString().Trim()%>&Mailflg=2&finsiteno=<%# DataBinder.Eval(Container.DataItem,"FINNO")%>">				
					    <img id="SecondImg1" style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="T&F Common Email" title="T&F Common Email" />
					</a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Journal Name">
			    <ItemTemplate>
			        <asp:Label ID="Id_Jourcode" runat="server" Text='<%# Bind("JOURCODE")%>'></asp:Label>
			        <asp:Label ID="Id_Issue" runat="server" Text='<%# Bind("IISSUENO")%>'></asp:Label>
			        <asp:Label ID="Id_custno" runat="server" Text='<%# Bind("CNO") %>'></asp:Label>
			        <asp:Label ID="Id_journo" runat="server" Text='<%# Bind("JOURNO") %>'></asp:Label>
			        <asp:Label ID="Id_ino" runat="server" Text='<%# Bind("INO") %>'></asp:Label>
			    </ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Inv. No" SortExpression="IINNO">
			    <ItemTemplate>
			        <%# DataBinder.Eval(Container.DataItem, "IINNO")%>
			    </ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" HeaderText="Inv.Date" DataField="IDATE" SortExpression="IDATE" />
			<%--<asp:TemplateField HeaderText="Inv. Date">
			    <ItemTemplate>
			        <%# DataBinder.Eval(Container.DataItem,"IDATE") %>
			    </ItemTemplate>
			</asp:TemplateField>--%>
			
			<asp:TemplateField HeaderText="PE Name">
			    <ItemTemplate>
			        <%--<asp:Button Text='<%# DataBinder.Eval(Container.DataItem,"INEMAIL")%>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CONNO")%>'
			         CommandName="Email" runat="server" />--%>
			         <%--<a id="hrf_Eamil" target="_blank" title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=1&iinvoiceno=<%# DataBinder.Eval(Container.DataItem,"IINNO") %>&issueno=<%# DataBinder.Eval(Container.DataItem, "IISSUENO").ToString().Trim()%>&custno=<%# DataBinder.Eval(Container.DataItem, "CNO").ToString().Trim()%>&jourcode=<%# DataBinder.Eval(Container.DataItem, "JOURCODE").ToString().Trim()%>&journo=<%# DataBinder.Eval(Container.DataItem, "JOURNO").ToString().Trim()%>&ino=<%# DataBinder.Eval(Container.DataItem, "INO").ToString().Trim()%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "INVDNAME").ToString().Trim()%>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO") %>&finsiteno=<%# DataBinder.Eval(Container.DataItem,"FINNO")%>" >--%>
			            <%# DataBinder.Eval(Container.DataItem, "INVDNAME")%>
			        <%-- </a>--%>
			    </ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
    <div id="divmessage" runat="server" >
    &nbsp;&nbsp;
        
        &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp;
        </div>
        &nbsp;&nbsp;
        <asp:GridView ID="BooksInvoiceList" HorizontalAlign="Center" runat="server" 
        Width="97%" AutoGenerateColumns="False" CellPadding="1" GridLines="horizontal"  
         OnRowCommand="Grid_RowCommand" OnRowDataBound="Grid_RowDataBound" 
         AllowSorting="True" OnSorting="Grid_Sorting"  
         >
        <HeaderStyle CssClass="GVFixedHeader" />
        <FooterStyle CssClass="GVFixedFooter" />         
            <Columns>
                <asp:BoundField DataField="CUSTNAME1" HeaderText="Customer Name" SortExpression="CUSTNAME1" />
                <asp:BoundField DataField="BNUMBER1" HeaderText="CATNO" SortExpression="BNUMBER1" />
                <asp:BoundField DataField="BTITLE1" HeaderText="Title" SortExpression="BTITLE1" />
                <asp:TemplateField HeaderText="Dispatch Date" SortExpression="BDISPATCH1">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "BDISPATCH1").ToString().Substring(0, DataBinder.Eval(Container.DataItem, "BDISPATCH1").ToString().IndexOf(" "))%>
                    </ItemTemplate>
                </asp:TemplateField>                
                <%--<asp:BoundField DataField="CUSTNO1" HeaderText="Custno" SortExpression="CUSTNO1" Visible="False" />--%>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <%--<asp:ImageButton AlternateText="Click to View Indian Invoice" OnClientClick="javascript:changeGreenImage(this);" ID="imgbtn_IndiaPreview" CommandName="India_Preview" style="cursor:pointer" ImageUrl="images/TE-proof.gif" Height="28" runat="server" />--%>
                        <a target="_blank" style="border:none"  title="Click to View Indian Invoice" href="previewinvoice.aspx?location=i&custno=<%# DataBinder.Eval(Container.DataItem, "CUSTNO1")%>&BNO=<%# DataBinder.Eval(Container.DataItem, "BNO1")%>&category=2&invoiceno=<%# DataBinder.Eval(Container.DataItem,"BINVOICENO1") %>">
                        <img alt="Click to View Indian Invoice" onclick="javascript:changeGreenImage(this);" runat="server" name="Book_imgIndiaPDF" id="Book_imgIndiaPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
			    </a>
                      
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <%--<asp:ImageButton ID="imgbtn_DublinPreview" OnClientClick="javascript:changeGreenImage(this);" AlternateText="Click to View Dublin invoice" CommandName="Dublin_Preview" style="cursor:pointer" ImageUrl="images/TE-proof.gif" Height="28" runat="server" />--%>
                        <a target="_blank" style="border:none"  title="Click to View Dublin Invoice" href="previewinvoice.aspx?location=d&custno=<%# DataBinder.Eval(Container.DataItem, "CUSTNO1")%>&BNO=<%# DataBinder.Eval(Container.DataItem, "BNO1")%>&category=2&invoiceno=<%# DataBinder.Eval(Container.DataItem,"BINVOICENO1") %>">
                        <img alt="Click to View Dublin invoice" onclick="javascript:changeGreenImage(this);" runat="server" name="Book_imgDublinPDF" id="Book_imgDublinPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
			    </a>
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="BINVOICENO1" HeaderText="Inv. No." Visible="False" />--%>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton AlternateText="Click to Approve Invoice" height="28" ImageUrl="images/approve.gif" id="btnApprove" AccessKey="A" style="cursor :pointer"
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BNO1")%>' CommandName="Approve"  runat="server"  />  
                    </ItemTemplate>
                </asp:TemplateField>
               
               <%-- <asp:BoundField DataField="INVCONEMAIL" HeaderText="INVCONEMAIL" SortExpression="INVCONEMAIL" Visible="False" />
                <asp:BoundField DataField="CONFULLNAME1" HeaderText="DISPLAYNAME" SortExpression="CONFULLNAME1" Visible="False" />--%>
                <asp:TemplateField>
			    <ItemTemplate>
			        <asp:ImageButton AlternateText="Save" name="btnsaveprint" ToolTip="Save" height="28"   ImageUrl="~/images/tools/j_save.png" id="btnsaveprint" AccessKey="S" 
                                 CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BNO1")%>' CommandName="saveprint"  runat="server" OnClientClick="javascript:return disablefn(this);"  />  
			    </ItemTemplate>
			</asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <%--<a target="_blank" style="border:none" title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=2&binvoiceno=<%# DataBinder.Eval(Container.DataItem,"BINVOICENO1") %>&custno=<%# DataBinder.Eval(Container.DataItem, "CUSTNO1")%>&BNO=<%# DataBinder.Eval(Container.DataItem, "BNO1")%>&BNUMBER=<%# DataBinder.Eval(Container.DataItem, "BNUMBER1")%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INVCONEMAIL")%>&pename=<%# DataBinder.Eval(Container.DataItem, "CONFULLNAME1")%>">
                        <img alt="send invoice" id="imgEmailPDF" style="cursor:pointer; border:none " src="images/temail.jpg" height="20" title="Click to Email Invoice"  />
		            </a>--%>
		            <a target="_blank" style="border:none" title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=2&binvoiceno=<%# DataBinder.Eval(Container.DataItem,"BINVOICENO1") %>&custno=<%# DataBinder.Eval(Container.DataItem, "CUSTNO1").ToString().Trim()%>&BNO=<%# DataBinder.Eval(Container.DataItem, "BNO1")%>&BNUMBER=<%# DataBinder.Eval(Container.DataItem, "BNUMBER1")%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INVCONEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "CONFULLNAME1").ToString().Trim()%>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO1") %>&finsiteno=<%# DataBinder.Eval(Container.DataItem,"FINSITENO1") %>&btitle=<%#DataBinder.Eval(Container.DataItem,"BTITLE1") %>">
                        <img alt="send invoice" id="imgEmailPDF" style="cursor:pointer; border:none " src="images/temail.jpg" height="20" title="Click to Email Invoice"  />
		            </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
			    <ItemTemplate>
			        <asp:Label ID="Id_Bnumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BNUMBER1")%>'></asp:Label>
			        <asp:Label ID="Id_bookcustno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CUSTNO1") %>'></asp:Label>
			        <asp:Label ID="Id_Bno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BNO1") %>'></asp:Label>
			        
			    </ItemTemplate>
			</asp:TemplateField>
			
			<asp:BoundField DataField="BINVOICENO1" HeaderText="Inv. No" SortExpression="BINVOICENO1" />
            <asp:BoundField DataField="BINVOICEDATE1" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false"  HeaderText="Inv. Date" SortExpression="BINVOICEDATE1" />
            <asp:TemplateField>
			    <ItemTemplate>
			        <%--<asp:Button Text='<%# DataBinder.Eval(Container.DataItem,"INEMAIL")%>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CONNO")%>'
			         CommandName="Email" runat="server" />--%>
			         <%--<a id="hrf_BookEamil" target="_blank" title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=2&iinvoiceno=<%# DataBinder.Eval(Container.DataItem,"BINVOICENO1") %>&custno=<%# DataBinder.Eval(Container.DataItem, "CUSTNO1").ToString().Trim()%>&BNO=<%# DataBinder.Eval(Container.DataItem, "BNO1")%>&BNUMBER=<%# DataBinder.Eval(Container.DataItem, "BNUMBER1")%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INVCONEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "CONFULLNAME1").ToString().Trim()%>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO1") %>&finsiteno=<%# DataBinder.Eval(Container.DataItem,"FINSITENO1") %>" >
			            <%# DataBinder.Eval(Container.DataItem, "CONFULLNAME1")%>
			         </a>--%>
			         <%# DataBinder.Eval(Container.DataItem, "CONFULLNAME1")%>
			    </ItemTemplate>
			</asp:TemplateField>
            </Columns>
        </asp:GridView>
        &nbsp;&nbsp;
        <asp:GridView ID="ProjectInvoiceList" HorizontalAlign="Center" runat="server" 
        AutoGenerateColumns="False" Width="97%" CellPadding="1" GridLines="Horizontal" 
         OnRowCommand="Grid_RowCommand"  OnRowDataBound="Grid_RowDataBound"
         OnSorting="Grid_Sorting"  AllowSorting="True" >
        <HeaderStyle CssClass="GVFixedHeader" />
        <FooterStyle CssClass="GVFixedFooter" />         
            <Columns>
                <asp:BoundField DataField="CNAME" HeaderText="Customer Name" SortExpression="cname" />
                <asp:BoundField DataField="PCODE" HeaderText="Project Code" SortExpression="pcode" />
                <asp:BoundField DataField="PTITLE" HeaderText="Project Title" SortExpression="ptitle" />
                
                <%--<asp:BoundField DataField="PROJECTNO" HeaderText="PROJECTNO" SortExpression="PROJECTNO"
                    Visible="False" />--%>
                <asp:TemplateField HeaderText="Dispatch Date" SortExpression="PDISPATCHDATE">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PDISPATCHDATE").ToString().Substring(0, DataBinder.Eval(Container.DataItem, "PDISPATCHDATE").ToString().IndexOf(" "))%>
                    </ItemTemplate>
                </asp:TemplateField>                 
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <%--<asp:ImageButton AlternateText="Click to View Indian Invoice" OnClientClick="javascript:changeGreenImage(this);" ID="imgbtn_IndiaPreview" CommandName="India_Preview" style="cursor:pointer" ImageUrl="images/TE-proof.gif" Height="28" runat="server" />--%>
                        <a target="_blank" style="border:none"  title="Click to View Indian Invoice" href="previewinvoice.aspx?location=i&projectno=<%# DataBinder.Eval(Container.DataItem, "projectno").ToString().Trim()%>&category=3&custno=<%# DataBinder.Eval(Container.DataItem, "cno").ToString().Trim()%>&pcode=<%# DataBinder.Eval(Container.DataItem, "pcode").ToString().Trim()%>&invoiceno=<%# DataBinder.Eval(Container.DataItem,"INVNO") %>">
                            <img alt="Click to View Indian Invoice" onclick="javascript:changeGreenImage(this);" runat="server" name="Project_imgIndiaPDF" id="Project_imgIndiaPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
        			    </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <%--<asp:ImageButton ID="imgbtn_DublinPreview" AlternateText="Click to View Dublin invoice" OnClientClick="javascript:changeGreenImage(this);" CommandName="Dublin_Preview" style="cursor:pointer" ImageUrl="images/TE-proof.gif" Height="28" runat="server" />--%>
                        
                        <a target="_blank" style="border:none"  title="Click to View Dublin Invoice" href="previewinvoice.aspx?location=d&projectno=<%# DataBinder.Eval(Container.DataItem, "projectno").ToString().Trim()%>&category=3&custno=<%# DataBinder.Eval(Container.DataItem, "cno").ToString().Trim()%>&pcode=<%# DataBinder.Eval(Container.DataItem, "pcode").ToString().Trim()%>&invoiceno=<%# DataBinder.Eval(Container.DataItem,"INVNO") %>">
                        <img alt="Click to View Dublin Invoice" runat="server" onclick="javascript:changeGreenImage(this);" name="Project_imgDublinPDF" id="Project_imgDublinPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
			            </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton AlternateText="Click to Approve Invoice" ImageUrl="images/approve.gif" height="28" id="btnApprove" AccessKey="A" style="cursor :pointer"
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "projectno")%>' CommandName="Approve"  runat="server"  />  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
			    <ItemTemplate>
			        <asp:ImageButton AlternateText="Save" name="btnsaveprint" ToolTip="Save"  height="28"  ImageUrl="~/images/tools/j_save.png" id="btnsaveprint" AccessKey="S" 
                                 CommandArgument='<%# DataBinder.Eval(Container.DataItem, "projectno")%>' CommandName="saveprint"  runat="server" OnClientClick="javascript:return disablefn(this);"  />  
			    </ItemTemplate>
			</asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <%--<a target="_blank" style="border:none" title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=3&invno=<%# DataBinder.Eval(Container.DataItem,"INVNO") %>&custno=<%# DataBinder.Eval(Container.DataItem, "cno").ToString().Trim()%>&projectno=<%# DataBinder.Eval(Container.DataItem, "projectno").ToString().Trim()%>&pcode=<%# DataBinder.Eval(Container.DataItem, "pcode").ToString().Trim()%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INVCONEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "DISPLAYNAME").ToString().Trim()%>">
                        <img alt="Click to Email Invoice" id="imgEmailPDF" style="cursor:pointer; border:none " src="images/temail.jpg" height="20" title="Click to Email Invoice"  />
		            </a>--%>
		            <a target="_blank" id="Project_Anchor" style="border:none" title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=3&invno=<%# DataBinder.Eval(Container.DataItem,"INVNO") %>&custno=<%# DataBinder.Eval(Container.DataItem, "cno").ToString().Trim()%>&projectno=<%# DataBinder.Eval(Container.DataItem, "projectno")%>&pcode=<%# DataBinder.Eval(Container.DataItem, "pcode").ToString().Trim()%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INVCONEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "DISPLAYNAME").ToString().Trim()%>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO") %>&finsiteno=<%# DataBinder.Eval(Container.DataItem,"FINSITENO") %>&ptitle=<%#DataBinder.Eval(Container.DataItem,"PTITLE").ToString().Trim() %>">
                        <img id="imgEmailPDF" alt="Click to Email Invoice" runat="server"  style="cursor:pointer; border:none " src="images/temail.jpg" height="20" title="Click to Email Invoice"  />
		            </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
			    <ItemTemplate>
			    <asp:Label ID="Id_Pcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PCODE")%>'></asp:Label>
			    <asp:Label ID="Id_projectno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"projectno") %>'></asp:Label>
			    <asp:Label ID="Id_custno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"cno") %>'></asp:Label>
			    </ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="INVNO" HeaderText="Inv. No" SortExpression="INVNO" />
            <asp:BoundField DataField="PINVDATE" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" HeaderText="Inv. Date" SortExpression="PINVDATE" />
            <asp:TemplateField>
			    <ItemTemplate>
			         <%--<a id="hrf_BookEamil" target="_blank" title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=3&invno=<%# DataBinder.Eval(Container.DataItem,"INVNO") %>&custno=<%# DataBinder.Eval(Container.DataItem, "cno").ToString().Trim()%>&projectno=<%# DataBinder.Eval(Container.DataItem, "projectno")%>&pcode=<%# DataBinder.Eval(Container.DataItem, "pcode")%>&pemail=<%# DataBinder.Eval(Container.DataItem, "INVCONEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "DISPLAYNAME").ToString().Trim()%>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO") %>&finsiteno=<%# DataBinder.Eval(Container.DataItem,"FINSITENO") %>" >
			            <%# DataBinder.Eval(Container.DataItem, "DISPLAYNAME")%>
			         </a>--%>
			          <%# DataBinder.Eval(Container.DataItem, "DISPLAYNAME")%>
			    </ItemTemplate>
			</asp:TemplateField>
            </Columns>
        </asp:GridView>
        &nbsp;&nbsp;
         <asp:GridView ID="WIPInvoiceList" HorizontalAlign="center" runat="server" 
        AutoGenerateColumns="false" Width="97%" CellSpacing="1" GridLines="horizontal"
        OnRowCommand="Grid_RowCommand" OnRowDataBound="Grid_RowDataBound"
        >
        <HeaderStyle CssClass="GVFixedHeader" />
        <FooterStyle CssClass="GVFixedFooter" />
        <Columns>
            <asp:BoundField DataField="title" HeaderText="Title" />
            <asp:TemplateField HeaderText="">
				<ItemTemplate>
				<a target="_blank" style="border:none"  href="previewinvoice.aspx?location=i&category=4&custno=<%# DataBinder.Eval(Container.DataItem,"WCUSTNO") %>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO") %>&invno=<%# DataBinder.Eval(Container.DataItem,"INVNO") %>&pename=<%# DataBinder.Eval(Container.DataItem,"INVDNAME") %>">
				    <img alt="Click to View Indian Invoice" onclick="javascript:changeGreenImage(this);" runat="server" name="WIP_imgIndiaPDF" id="WIP_imgIndiaPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
			    </a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="">
				<ItemTemplate>
				
				<a target="_blank" style="border:none"  title="Click Here to Preview Dublin format" href="previewinvoice.aspx?location=d&category=4&custno=<%# DataBinder.Eval(Container.DataItem,"WCUSTNO") %>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO") %>&invno=<%# DataBinder.Eval(Container.DataItem,"INVNO") %>&pename=<%# DataBinder.Eval(Container.DataItem,"INVDNAME") %>">
				    <img alt="Click to View Dublin Invoice" onclick="javascript:changeGreenImage(this);" runat="server" name="WIP_imgDublinPDF" id="WIP_imgDublinPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />
			    </a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField>
			    <ItemTemplate>
			        <asp:ImageButton AlternateText="Save&Print" name="btnsaveprint" ToolTip="Save&Print"  height="28"  ImageUrl="~/images/tools/j_save.png" id="btnsaveprint" AccessKey="S" 
                               OnClientClick="javascript:return disablefn(this);"   CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CONNO")%>' CommandName="saveprint"  runat="server"  />  
			    </ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="PE Mail">
				<ItemTemplate>
					<a target="_blank" id="firstanchor"  title="Click to Email Invoice" href="emailpreview.aspx?location=d&category=4&iinvoiceno=<%# DataBinder.Eval(Container.DataItem,"INVNO") %>&custno=<%# DataBinder.Eval(Container.DataItem, "WCUSTNO").ToString().Trim()%>&pemail=<%# DataBinder.Eval(Container.DataItem, "PEMAIL").ToString().Trim()%>&pename=<%# DataBinder.Eval(Container.DataItem, "PENAME").ToString().Trim()%>&conno=<%# DataBinder.Eval(Container.DataItem,"CONNO") %>&finsiteno=470&Mailflg=1">				
					    <img id="Img1"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email to Financial Contact" title="Email to Financial Contact" />
					</a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="PE Name">
			    <ItemTemplate>
			            <%# DataBinder.Eval(Container.DataItem, "INVDNAME")%>
			    </ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Invoice Date">
			    <ItemTemplate>
			            <%# DataBinder.Eval(Container.DataItem, "INVDATE")%>
			    </ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Invoice No.">
			    <ItemTemplate>
			            <%# DataBinder.Eval(Container.DataItem, "INVNO")%>
			    </ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="EVNO">
			    <ItemTemplate>
			            <%# DataBinder.Eval(Container.DataItem, "EVNO")%>
			    </ItemTemplate>
			</asp:TemplateField>
				<asp:TemplateField HeaderText="CONNO">
			    <ItemTemplate>
			            <%# DataBinder.Eval(Container.DataItem, "CONNO")%>
			    </ItemTemplate>
			</asp:TemplateField>
        </Columns>
        </asp:GridView>
        <asp:HiddenField ID="hfEmID" runat="server" />
    </form>
</body>
</html>

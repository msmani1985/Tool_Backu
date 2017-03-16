<%@ page language="C#" autoeventwireup="true" inherits="invoicedreport, App_Web_w6b3pav3" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Invoiced Report Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div >
    <div id="invreport" class="dptitle" >Invoiced Report</div>
    <div >
            <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
                <tr><td>Customer</td><td>
                <asp:DropDownList ID="ddlcustomer" DataTextField="CUSTNAME" DataValueField="CUSTNO" runat="server" >
                    <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Taylor and Francis" Value="2556"></asp:ListItem>
                    <asp:ListItem Text="Taylor and Francis Scandivia" Value="10037"></asp:ListItem>
                </asp:DropDownList>
                </td>
                <td>Type</td><td>
                    <asp:DropDownList ID="ddljobtype" runat="server" >
                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="3"></asp:ListItem>
                    </asp:DropDownList> 
                </td>           </tr>
                <tr><td>From</td><td>
                <asp:TextBox id="fromdate" runat="server" />
                <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=fromdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td id="Td1" rowspan="2" colspan="2" valign="bottom" runat="server"   >
                    <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click" />
                </td></tr>
                <tr><td>To</td><td><asp:TextBox id="todate" runat="server" />
                <img style="cursor:pointer; border:none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=todate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td></tr>            
            </table>
    </div> 
    <br />
    <div style="width:75%;text-align:right;"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" /></div>
    </div>  
    <br />   
    <div id="ErrorDiv" align="center" style="background-color:White" >
        <asp:Label runat="server" ID="lblMessage" BorderColor="red" Text="" ></asp:Label>
    </div> 
    <div>
        <CR:CrystalReportViewer ID="PDFTandFCReportViewer" runat="server" AutoDataBind="true" />
    </div>
    <div style="text-align:center;width:100%; z-index:10;">
            
            <asp:DataGrid ID="adgdispatchedlist" Visible="True" CaptionAlign="Left" 
            AllowSorting="True" width="90%" runat="server" AutoGenerateColumns="False"
				AllowPaging="False" DataKeyField="INO" GridLines="Both" CellPadding="3" OnItemDataBound="adgdispatchedlist_ItemDataBound" 
				ShowFooter="True" 
 
				 >
				<FooterStyle CssClass="lightbackground"></FooterStyle>
				<AlternatingItemStyle CssClass="lightbackground"></AlternatingItemStyle>
				<ItemStyle CssClass="lightbackground"></ItemStyle>
				<HeaderStyle Font-Names="Tahoma" Font-Bold="True" CssClass="darkbackground"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn  HeaderText="Invoice Date">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Customer" Visible=False>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "CNAME") %>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="4&nbsp;Letter Acronym" >
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "JOURCODE")%>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Journal" >
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "JOURTITLE")%>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Production Editor" >
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "INVDNAME")%>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Supplier Invoice No.">
						<ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "IINNO")%>												
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="CATs ID No." >
						<ItemTemplate>
							&nbsp;<%# DataBinder.Eval(Container.DataItem, "IISSUENO")%>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Format: L=Large; S=Small" >
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="No. Article/Pages; P=Pages; A=Articles" >
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Invoice Total (INR)">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Invoice Total (&euro;)">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Invoice Total ($)">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Invoice Total (&pound;)">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate >&nbsp;</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="India">
						<ItemTemplate>
						   <%-- <a target="_blank" style="border:none" title="Click Here to Preview India format" href="previewinvoice.aspx?location=i&custno=<%# DataBinder.Eval(Container.DataItem, "CUSTNO1")%>&BNO=<%# DataBinder.Eval(Container.DataItem, "INO")%>&category=2">
                        <img alt="Click Here to Preview India format" id="imgIndiaPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />                           --%>
                        <asp:HyperLink ID="IndiaPreviewHL" ToolTip="Click to view Indian Invoice" ImageUrl="images/TE-proof.gif" runat="server" NavigateUrl=""  CssClass="CursorAdd" >
                        </asp:HyperLink>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Dublin">
						<ItemTemplate>
						   <%-- <a target="_blank" style="border:none" title="Click Here to Preview India format" href="previewinvoice.aspx?location=i&custno=<%# DataBinder.Eval(Container.DataItem, "CUSTNO1")%>&BNO=<%# DataBinder.Eval(Container.DataItem, "INO")%>&category=2">
                        <img alt="Click Here to Preview India format" id="imgIndiaPDF" style="cursor:pointer; border:none " src="images/TE-proof.gif" height="28"  />                           --%>
                        <asp:HyperLink ID="DublinPreviewHL" ToolTip="Click to view Dublin Invoice" ImageUrl="images/TE-proof.gif" runat="server" NavigateUrl=""  CssClass="CursorAdd" >
                        </asp:HyperLink>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Comments ">
					<ItemTemplate>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</ItemTemplate>
					</asp:TemplateColumn>
					
				</Columns>
			</asp:DataGrid>
			
			</div>
			<p>&nbsp;</p>

    </form>
</body>
</html>

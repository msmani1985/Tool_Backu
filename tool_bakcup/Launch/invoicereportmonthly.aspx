<%@ page language="C#" autoeventwireup="true" inherits="invoicereportmonthly, App_Web___drxint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="invreport" class="dptitle" >Monthly Invoiced Report</div>
    <div id="div_monthlyreport" runat="server" >
            <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
                <tr><td>Customer</td><td>
                <asp:DropDownList ID="ddlcustomer" Width="200px" DataTextField="CUSTNAME" DataValueField="CUSTNO" runat="server" >
                    <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Taylor and Francis" Value="2556"></asp:ListItem>
                    <asp:ListItem Text="Taylor and Francis Scandivia" Value="10037"></asp:ListItem>
                </asp:DropDownList>
                </td>
                <td>Type</td><td>
                    <asp:DropDownList ID="ddljobtype" runat="server" >
                       <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="3"></asp:ListItem>
                    </asp:DropDownList> 
                </td> <td>Month&nbsp;
                <%--<asp:TextBox id="fromdate" runat="server" />
                <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=fromdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />--%>
                    <asp:DropDownList ID="DDMonthList" runat="server">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Year&nbsp;
                <%--<asp:TextBox id="todate" runat="server" />
                <img style="cursor:pointer; border:none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=todate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />--%>
                    <asp:DropDownList ID="DDYearList" runat="server">
                        <%--<asp:ListItem>2002</asp:ListItem>
                        <asp:ListItem>2003</asp:ListItem>
                        <asp:ListItem>2004</asp:ListItem>
                        <asp:ListItem>2005</asp:ListItem>
                        <asp:ListItem>2006</asp:ListItem>
                        <asp:ListItem>2007</asp:ListItem>--%>
                        <asp:ListItem>2008</asp:ListItem>
                        <asp:ListItem>2009</asp:ListItem>
                        <asp:ListItem>2010</asp:ListItem>
                        <asp:ListItem>2011</asp:ListItem>
                        <asp:ListItem>2012</asp:ListItem>
                        <asp:ListItem>2013</asp:ListItem>
                        <asp:ListItem Selected="True">2014</asp:ListItem>
                    </asp:DropDownList>
                </td></tr> <tr>
                <td colspan="2" align="center" >
                    <asp:RadioButtonList ID="RB_location" runat="server" RepeatDirection="Horizontal" BorderStyle="Solid" BorderWidth="1px">
                        <asp:ListItem Text="All" Value="0" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Chennai" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Coimbatore" Value="3"></asp:ListItem></asp:RadioButtonList>
               </td><td colspan="3">
                        <asp:RadioButtonList ID="RB_invoiceval" runat="server" RepeatDirection="horizontal" BorderStyle="solid" BorderWidth="1px"><asp:ListItem Text="India Value" Value="i" Selected="true"></asp:ListItem><asp:ListItem Text="Dublin Value" Value="d"></asp:ListItem></asp:RadioButtonList>
                </td>         
                <td id="Td1"  align="left" valign="bottom" runat="server"   >
                    <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click" />
                </td></tr>
                
            </table>
    </div> 
    <br />
    <div style="width:85%;text-align:right;"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" /></div>
    </div>  
    <div id="ErrorDiv" align="center" class="errorMsg" style="background-color:White" >
        <asp:Label runat="server" ID="lblMessage" BorderColor="red" Text="" ></asp:Label>
    </div> 
    <div style="text-align:center;width:100%; z-index:10;">
            <asp:DataGrid ID="adgdispatchedlist" Visible="True" CaptionAlign="Left" 
            AllowSorting="True" width="90%" runat="server" AutoGenerateColumns="False"
				AllowPaging="False" DataKeyField="INO" GridLines="Both" CellPadding="3" OnSortCommand="Grid_SortCommand" OnItemDataBound="adgdispatchedlist_ItemDataBound" 
				ShowFooter="True" 
				 >
				<FooterStyle CssClass="lightbackground"></FooterStyle>
				<AlternatingItemStyle CssClass="lightbackground"></AlternatingItemStyle>
				<ItemStyle CssClass="lightbackground"></ItemStyle>
				<%--<HeaderStyle Font-Names="Tahoma" Font-Bold="True" CssClass="darkbackground"></HeaderStyle>--%>
				<HeaderStyle CssClass="GVFixedHeader" />
				<Columns>
				    <asp:TemplateColumn HeaderText="Job No" >
				        <ItemTemplate >
				            <%# DataBinder.Eval(Container.DataItem, "JOBNO")%>
				        </ItemTemplate>
				    </asp:TemplateColumn>
				    <asp:TemplateColumn HeaderText="Invoice No" SortExpression="IINNO">
				        <ItemTemplate>
				            <%# DataBinder.Eval(Container.DataItem, "IINNO")%>
				        </ItemTemplate>
				    </asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Invoice Date" SortExpression="IDATE">
						<ItemTemplate> &nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Customer" SortExpression="CNAME" >
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "CNAME") %>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-Width="130px"  HeaderText="Job Title" >
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "JOBTITLE")%>
						</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Type" SortExpression="CATEGORY">
					    <ItemTemplate>
					        <%# DataBinder.Eval(Container.DataItem, "CATEGORY")%>
					    </ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-Width="100px"   HeaderText="No. Article/Pages; P=Pages;A=Articles" >
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
					</asp:TemplateColumn>
					<%--<asp:TemplateColumn HeaderText="Invoice Total (INR)">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>--%>
					<asp:BoundColumn HeaderText="Invoice Total (INR)" DataField="InrVal"  DataFormatString="{0:0.00}"></asp:BoundColumn>
					<%--<asp:TemplateColumn  HeaderText="Invoice Total (&euro;)">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>--%>
					<asp:BoundColumn HeaderText="Invoice Total (&euro;)" DataField="EuroVal"  DataFormatString="{0:0.00}"></asp:BoundColumn>
					<%--<asp:TemplateColumn  HeaderText="Invoice Total ($)">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
					</asp:TemplateColumn>--%>
					<asp:BoundColumn HeaderText="Invoice Total ($)" DataField="DollarVal" DataFormatString="{0:0.00}"></asp:BoundColumn>
					<%--<asp:TemplateColumn  HeaderText="Invoice Total (&pound;)">
						<ItemTemplate>&nbsp;</ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate >&nbsp;</FooterTemplate>
					</asp:TemplateColumn>--%>
					<asp:BoundColumn HeaderText="Invoice Total (&pound;)" DataField="PoundVal"  DataFormatString="{0:0.00}"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
 
    </div>
    <p>&nbsp;</p>
    </form>
</body>
</html>


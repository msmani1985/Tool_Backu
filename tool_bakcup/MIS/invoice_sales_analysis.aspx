<%@ Page Language="C#" AutoEventWireup="true" CodeFile="invoice_sales_analysis.aspx.cs"
    Inherits="invoice_sales_analysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice Sales Analysis</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
     <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">
    
    

<%-- <script type="text/javascript">
     $(function () {
         $("input[type=submit], a, button")
.button()
.click(function (event) {
    event.preventDefault();
});
     });
</script>
--%>
<%-- <script type="text/javascript">
 $(function() {
     var dlg = $("#dialog").dialog({
                         draggable: true,
                         resizable: true,
                         show: 'Transfer',
                         hide: 'Transfer',
                         color : 'Green',
                         width: 320,
                         autoOpen: false,
                         minHeight: 10,
                         minwidth: 10
                         
                     });
    dlg.parent().appendTo(jQuery("form:first"));
});
</script>--%>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="invreport" class="dptitle">
                <asp:Label ID="lblTitle" runat="server"></asp:Label></div>
        </div>
        <table align="center" cellpadding="2" cellspacing="2" class="bordertable" style="width: 480px">
            <tr>
                <td>
                    Customer</td>
                <td>
                    <asp:DropDownList ID="drpCustomer" runat="server" DataTextField="CUSTNAME" DataValueField="CUSTNO">
                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Taylor and Francis" Value="2556"></asp:ListItem>
                        <asp:ListItem Text="Taylor and Francis Scandivia" Value="10037"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Type</td>
                <td>
                    <asp:DropDownList ID="drpJobType" runat="server">
                       <%-- <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="3"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    Month</td>
                <td>
                    <%--<asp:TextBox id="fromdate" runat="server" />
                <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=fromdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />--%>
                    <asp:DropDownList ID="drpMonths" runat="server">
                        <asp:ListItem Value="0">--All--</asp:ListItem>
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
                <td>
                    Year</td>
                <td>
                    <asp:DropDownList ID="drpYears" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                    <%--<div id="dialog" style="text-align: left;display: none;">--%>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" OnClick="btnSubmit_Click"
                        Text="Submit" /></td>
                        <%--</div>--%>
            </tr>
            <tr>
                <td align="right" colspan="10" rowspan="2">
                    <div id="divMonthly" runat="server">
                        &nbsp;<asp:ImageButton ID="cmd_Excel_Export" runat="server" ImageUrl="~/images/tools/j_excel.png"
                            OnClick="cmd_Excel_Export_Click" ToolTip="Export Excel" />
                            <asp:GridView ID="gvSalesAnalysis"
                                runat="server" AutoGenerateColumns="False" CssClass="lightbackground" Width="100%"
                                OnRowDataBound="gvSalesAnalysis_RowDataBound" OnRowCommand="gvSalesAnalysis_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Job No.">
                                        <ItemTemplate>
                                            <%# Eval("JOBNO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice No.">
                                        <ItemTemplate>
                                            <%# Eval("IINNO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <%# Eval("CNAME")%> - <%# Eval("SALES_SPLIT")%> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Title">
                                        <ItemTemplate>
                                            <%# Eval("JOBTITLE")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Pages">
                                        <ItemTemplate>
                                            <%# Eval("TOTALPAGES_FINAL")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Total (€)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbleuro" runat="server" Text='<%# Eval("EUROVALUE")%>' Visible="false"></asp:Label>
                                            <%# Eval("EUROVALUE")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cumulative">
                                        <ItemTemplate>
                                            <%# Eval("CUMULATIVE")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Total (&#163;)">
                                        <ItemTemplate>
                                            <%# Eval("POUNDVALUE")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Total ($)">
                                        <ItemTemplate>
                                            <%# Eval("USDVALUE")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Total (CAD$)">
                                        <ItemTemplate>
                                            <%# Eval("CADVALUE")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField><HeaderTemplate>Indian Invoice</HeaderTemplate>  <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="preview_indiainvoice" style="cursor:pointer; border:none " ImageUrl="images/pdf_icon.gif" 
                                        height="28" AlternateText="Click to View Indian Invoice" CommandName="India"  />
                                    </ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField><HeaderTemplate>Dublin Invoice</HeaderTemplate><ItemTemplate>
                                        <asp:ImageButton runat="server" ID="preview_dublininvoice" style="cursor:pointer; border:none " ImageUrl="images/pdf_icon.gif" 
                                        height="28" AlternateText="Click to View Dublin Invoice" CommandName="Dublin"  />
                                    </ItemTemplate></asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                        No records found.</div>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="darkbackground" />
                                <AlternatingRowStyle CssClass="dullbackground" />
                            </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="10">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>




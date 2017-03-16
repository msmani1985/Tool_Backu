<%@ page language="C#" autoeventwireup="true" inherits="YTD_sales_report, App_Web_w6b3pav3" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel=stylesheet />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle" id="divtitle" runat="server" ></div>
       <table align="center" cellpadding="2" cellspacing="2" class="bordertable" style="width: 480px">
            <tr>
                <td>
                    Customer</td>
                <td>
                    <asp:DropDownList ID="drpCustomer" runat="server" DataTextField="CUST_NAME" DataValueField="CUSTOMER_ID">
                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Taylor and Francis" Value="2556"></asp:ListItem>
                        <asp:ListItem Text="Taylor and Francis Scandivia" Value="10037"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Type</td>
                <td>
                    <asp:DropDownList ID="drpJobType" runat="server">
                        <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Month</td>
                <td>
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
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" OnClick="btnSubmit_Click"
                        Text="Submit" /></td>
            </tr>
        </table>
        <br />
        <div id="diverror" runat="server"></div>
        <br />
        <div style="width:auto">
            <CR:CrystalReportViewer BorderStyle="Solid" BorderColor="green" ID="YTD_Reports" runat="server" AutodataBind="true" 
                HasRefreshButton="False" HyperlinkTarget="_new" 
                HasCrystalLogo="False" ToolbarStyle-BackColor="LightGreen"  
                ToolbarStyle-BorderColor="Green" DisplayGroupTree="False" EnableDrillDown="False" 
                HasDrillUpButton="False" HasToggleGroupTreeButton="False" HasSearchButton="False" HasViewList="False"
                 HasZoomFactorList="False" HasGotoPageButton="False" 
                />            
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server"></CR:CrystalReportSource>
        </div>
    </div>
    </form>
</body>
</html>

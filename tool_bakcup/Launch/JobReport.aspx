<%@ page language="C#" autoeventwireup="true" inherits="JobReport, App_Web_opij0lkt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div  class="dptitle" style="padding-bottom:10px;width:90%;" id="divTitle" runat="server">Job History Report</div>
    <div style="padding-top:10px;width:90%">
        <table  align="center" width="650px" style="border:solid 1px green;" >
            <tr bordercolor="green" >
              
                <td width="60px" align="right">Job Type : </td>
                <td width="125px"><asp:DropDownList ID="ddjobtype" runat="server">
                    <asp:ListItem Text="Issue" Value="6" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Article" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Chapter" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Project" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Module" Value="8"></asp:ListItem>
                </asp:DropDownList></td>
                <td align="right">Customer Name : </td><td><asp:Label Font-Bold="true" Font-Size="Larger" ID="Lbl_custname" runat="server" Text="" Visible="false"></asp:Label><asp:DropDownList ID="ddcustname" DataValueField="customer_id" DataTextField="cust_name" runat="server"></asp:DropDownList></td>
                <td rowspan="3" valign="middle"><asp:Button ID="Btnviewrpt" Width="100px" CssClass="dpbutton" Text="View Report" runat="server" OnClick="Btnviewrpt_Click"  /></td>
            </tr>
            <tr><td colspan="4" height="2px" style="background-image:url('Images/line.gif');background-repeat:repeat-x;"></td></tr>
            <tr >
                <td width="60px" align="right" >From : </td>
                <td width="125px"><asp:TextBox ID="Txtfrmdate" runat="server" Text="" Width="100px"></asp:TextBox><img style="cursor:pointer; border: none" onclick="javascript:window.open('calendar.aspx?formname=Txtfrmdate','Calendar_window','width=180,height=190,left=350,top=300,toolbars=no,scrollbars=no,status=no,resizable=no')" alt="Calendar" src="Images/calendar.jpg" height="20px" border="0" /></td>
                <td align="right">To : </td><td><asp:TextBox ID="Txttodate" runat="server" Width="100px" Text=""></asp:TextBox><img style="cursor:pointer;border:none;" onclick="javascript:window.open('calendar.aspx?formname=Txttodate','Calendar_window','width=180,height=190,left=350,top=300,toolbars=no,scrollbars=no,status=no,resizable=no')" alt="Calendar" src="Images/calendar.jpg" height="20px" border="0" /></td>            
            </tr>
        </table>
    </div>
    <br />
    <div id="DivError" class="errorMsg" runat="server"></div>
    <div id="divbtnshow" runat="server"  style="margin-left:10px;display:none;width:90%" >
        <table  style="border-top:solid 1px green;width:90%;">
            <tr><td style="color:Crimson;font-weight:bold;font-size:10pt;" align="right" ><asp:ImageButton ToolTip="GridView Details" ImageUrl="~/Images/grid6.jpg" AlternateText="GridView Details" ID="ibtngridview" runat="server" OnClick="ibtngridview_Click" />&nbsp;
                <asp:ImageButton ID="ibtngraphdetails" AlternateText="View Chart Details" ImageUrl="~/Images/Graph1.jpg" runat="server" OnClick="ibtngraphdetails_Click" />
                &nbsp; <asp:ImageButton ID="ibtnExcel_Export" runat="server" AlternateText="Excel Export" ImageUrl="~/images/Excel.jpg" OnClick="ibtnExcel_Export_Click" />
                </td>
            </tr>
        </table>
    </div>
   <%-- <div style="margin-left:20px;"><table><tr><td style="height: 23px"><asp:Button Text="View Details" ID="Btnviewdetails" CssClass="buttonstyle" runat="server" /></td>
                    <td style="height: 23px"><asp:Button Text="View Graph" ID="BtnViewGraph" CssClass="buttonstyle" runat="server" OnClick="BtnViewGraph_Click" /></td>
                </tr>
        </table></div>--%>
    <br />
    <div id="divcrystalrpt" runat="server" style="display:none;margin-left:10px;width:90%" >
        <table width="90%">
             <tr bgcolor=AntiqueWhite><td style="height: 21px;color:Green;font-weight:bold;">Chart Type : 
            <asp:DropDownList ID="ddlcharttype" runat="server" OnSelectedIndexChanged="ddlcharttype_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Text="BarChart" Value="0"></asp:ListItem>
                    <asp:ListItem Text="PieChart" Value="1"></asp:ListItem>
                    <asp:ListItem Text="RadarChart" Value="2"></asp:ListItem>
                </asp:DropDownList>
             </td></tr>
             <tr><td>
                    <CR:CrystalReportViewer ID="CReportView" runat="server" AutoDataBind="true" HasCrystalLogo="false" HasDrillUpButton="false" HasExportButton="false" HasGotoPageButton="false" HasPageNavigationButtons="false" HasSearchButton="false" HasToggleGroupTreeButton="false" HasViewList="false" HasZoomFactorList="false" DisplayGroupTree="false" DisplayToolbar="false" EnableDatabaseLogonPrompt="False" />
        </td></tr></table>
        
    </div>
    <div id="divgrid" runat="server" style="width:90%;margin-left:20px; display:none;">
        <table width="100%">
        <tr  ><td colspan="2" style="padding-left:75px;" >
        <asp:GridView Width="100%" ID="gvhistoryrpt" CaptionAlign="left" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Vertical" BorderColor="green" >
            <HeaderStyle BackColor="Green" Font-Bold="True" ForeColor="White" />
        </asp:GridView></td></tr></table>
    </div>
    </form>
</body>
</html>

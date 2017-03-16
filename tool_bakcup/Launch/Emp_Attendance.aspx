<%@ page maintainscrollpositiononpostback="true" language="C#" autoeventwireup="true" inherits="Emp_Attendance, App_Web_uqa4niiw" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle">Attendance Details</div>
    <div class="content" id="divReports" runat="server" >
       <table class="bordertable" style="width: 900px; height: 60px">
           <tr>
               <td align="center" style="height: 2px; width: 126px;">
                   Start Date:</td>
               <td style="height: 2px; width: 202px;">
                   <asp:TextBox ID="txtsdate" runat="server" Text=""></asp:TextBox>
                   <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" /></td>
               <td align="center" style="height: 2px; width: 118px;">
                   End Date:</td>
               <td style="height: 2px; width: 207px;">
                   <asp:TextBox ID="txtedate" runat="server"></asp:TextBox>
                   <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtedate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" />
               </td>
               <td rowspan="1" valign="middle" style="height: 2px">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                       Text="View" OnClick="BtnSubmit_Click"   /></td>
            
          </tr>
                   
                                <tr>
                                <td colspan="8">
                                    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" EnableDatabaseLogonPrompt="False" 
        ReuseParameterValuesOnRefresh="true" DisplayGroupTree="true" HasCrystalLogo="false" HasDrillUpButton="false" HasGotoPageButton="true" 
        HasRefreshButton="false" HasSearchButton="false" HasToggleGroupTreeButton="false" HasViewList="true" HasZoomFactorList="false" ShowAllPageIds="True" ></cr:crystalreportviewer>
                                </td>
                                </tr>
               
          
       </table>
    </div>
    </div>
    </form>
</body>
</html>

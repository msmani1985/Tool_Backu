<%@ page language="C#" autoeventwireup="true" inherits="Online_Request_Report, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     
<title>Online Request Report</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    
     <style type="text/css">
  .error
{
	color: Red;
	font-weight: bold;
	font-size: 10pt;
	text-align: center;
}
 </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="divTitle" align="left" runat="server">Online Request Report </div>
    <div>
            <table align="center" class="bordertable" width="60%"  id="TABLE1">
            <tr style="border-bottom:solid 1px green;border-bottom-width:thick;">
            <td >
                  <asp:Label ID="Label1" runat="server" Text="Employees:"></asp:Label>
            </td>
            
            <td colspan="2"><asp:DropDownList  ID="ddl_employees" DataTextField="employeewithteam" DataValueField="employee_id"  runat="server" >     </asp:DropDownList></td>
            <td>&nbsp;</td>
               <td rowspan="3" valign="middle">
                    <asp:Button ID="ViewReport" Width="100px"  CssClass="dpbutton" Text="View Report" runat="server" OnClick="ViewReport_Click" /></td>     
            <tr>
            <td colspan="4" align="right" style="background-image:url('Images/line.gif');background-repeat:repeat-x;" >
            </td>
            </tr>
            <tr>
            <td>
                    <asp:Label ID="Label3" runat="server" Text="From:"></asp:Label>
            </td>
            <td>
                    <asp:TextBox ID="FromDate" runat="server" ></asp:TextBox>
                    <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" ID="ImageButton1" Height="24px" Width="25px" ImageAlign="Middle" OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=FromDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
            </td>
            <td>
                    <asp:Label ID="Label4" runat="server" Text="To:"></asp:Label>
                    
            </td>
            <td>
                    <asp:TextBox ID="ToDate" runat="server" ></asp:TextBox>
                    
                    <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" ID="ImageButton2" Height="24px" Width="25px" ImageAlign="Middle" OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=ToDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
             </td>
             
             </tr>
            </table></div>
         <br />
         
        <div>
           <table  width="100%" >
            <tr><td style="color:Crimson;font-weight:bold;font-size:10pt;" align="right">
             <asp:ImageButton ID="ibtnExcel_Export" runat="server" ImageUrl="~/images/Excel.jpg" OnClick="ibtnExcel_Export_Click" ToolTip="Export To Excel" />
             <br />
             <asp:Label ID="lblmessage" CssClass="error" runat="server" Text=""></asp:Label>
             </td>
                
            </tr>
        </table></div>
        
        <div id=div_Online_Grid runat="server">
        <table width="100%">
        <tr> 
        <td colspan="2" >
        <asp:GridView Width="100%" ID="gv_online_request_report" CaptionAlign="left" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
        GridLines="Vertical" BorderColor="green" OnRowDataBound=gv_ONLINE_REQUEST_REPORT_datarowbound >
         <%--OnRowDataBound=gv_ONLINE_REQUEST_REPORT_datarowbound--%>
        
        <HeaderStyle BackColor="Green" Font-Bold="True"  ForeColor="White" />
       
        <Columns>
        
        <asp:BoundField DataField="task_name" HeaderText="JOB NAME"/>
        <asp:TemplateField>
        <HeaderTemplate>
       <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="WORK_STATUS_OnSelectedIndexChanged"   ID="ddL_WORK_STATUS" runat="server" >
                    <asp:ListItem  Value="1">All Status</asp:ListItem>
                    <asp:ListItem  Value="2">Completed</asp:ListItem>
                    <asp:ListItem  Value="3">WIP</asp:ListItem>
                    <asp:ListItem Value="4">Hold </asp:ListItem>
                    
        </asp:DropDownList>
        
       </HeaderTemplate>
       <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem,"[work_status]") %>
        </ItemTemplate> 
        </asp:TemplateField>
       <asp:BoundField DataField="due_date" HeaderText="DUE DATE" DataFormatString="{0:MM/dd/yyyy}"/>
        <asp:BoundField DataField="start_date" HeaderText="START DATE" DataFormatString="{0:MM/dd/yyyy}"/>
        <asp:BoundField DataField="completed_date" HeaderText="COMPLETED DATE" DataFormatString="{0:MM/dd/yyyy}"/>
        <asp:BoundField DataField="WorkingDays" HeaderText="NO OF WORKING DAYS" /> 
        <asp:BoundField DataField="HoldedDays" HeaderText="NO OF HOLDED DAYS"/>  
        <asp:BoundField DataField="hold_start_date" HeaderText="HOLD DATE" DataFormatString="{0:MM/dd/yyyy}"/>
        <asp:BoundField DataField="Release_Date" HeaderText="RELEASED DATE" />
        
        <asp:BoundField DataField="hold_reason" HeaderText="HOLD REASON"/>
         
         
        </Columns>
        <HeaderStyle CssClass="darkbackground" />
        </asp:GridView>
        </td>
        </tr> 
        </table>
        </div>
    
      
        <div id="div_Error" runat="server" class="error"></div>
         
    </form>
</body>
</html>

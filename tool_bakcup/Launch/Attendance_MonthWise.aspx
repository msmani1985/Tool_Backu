<%@ page language="C#" autoeventwireup="true" inherits="Attendance_MonthWise, App_Web_2fnjoyyb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="default.css" type="text/css" rel="stylesheet" />
     <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<style type="text/css">
.FixedHeader {
            font-weight:bold; color:White; background-color:Green;
        }  
.sample
{
background-color:#DC5807;
border:1px solid black;
border-collapse:collapse;
color:White;
} 
</style>
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=grvrpt.ClientID %>').Scrollable({
        ScrollHeight: 350
    });
});

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">Attendance Details</div>
    <div id="DisableDiv"> </div>
    <div class="content" id="divReports" runat="server" >
       <table class="bordertable" >
           <tr>
               <td align="center" colspan="4" >
                   Month\Year:
                   <asp:DropDownList ID="DropMonth" runat="server">
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
                   <asp:DropDownList ID="DropYear" runat="server">
                    <asp:ListItem Value="2014">2014</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                   </asp:DropDownList>
                   </td>
               
               <td rowspan="1" valign="middle" style="height: 2px">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                       Text="View" OnClick="BtnSubmit_Click"  />
                       <asp:Button ID="btnClear" runat="server" CssClass="dpbutton"
                       Text="Clear" OnClick="btnClear_Click"   /></td>
               <td rowspan="1" valign="middle" style="height: 2px">
            <asp:ImageButton ID="imgbtnEventExport" runat="server" ImageUrl="~/images/QMS/excel.png" 
            ToolTip="Export Excel" Width="25px" OnClick="imgbtnEventExport_Click" /></td>
          </tr>
                    <tr >
                        
                        <td align="center" colspan="2" >
                            <asp:RadioButton ID="EmpnoRBtn" runat="server" GroupName="Employee" Text="ID"  OnCheckedChanged="EmpnoRBtn_CheckedChanged" AutoPostBack="true" Width="69px"/>
                          
                          <asp:RadioButton ID="EmpNameRBtn2" runat="server" GroupName="Employee" Text="Name" OnCheckedChanged="EmpnoRBtn_CheckedChanged"  AutoPostBack="true" Width="87px" />
                                </td>
                                <td  align="center" >
                            <asp:Label ID="EmployeeLbl" runat="server" Text="Employee No." Width="111px"></asp:Label>
                        </td>
                        <td   > 
                            <asp:TextBox ID="EmpNoNameTxt" runat="server"></asp:TextBox>
                        </td>
                                <td>
                                    <asp:Button ID="btnSync"  CssClass="dpbutton" runat="server" Text="Sync" OnClick="btnSync_Click" Visible="False" />
                                </td>
                                
                                 </tr>
                                 <tr>
                                 <td colspan="6" ></td>
                                 </tr>
                                <tr >
                                <td colspan="6" align="center" >
                                    <asp:Label ID="lblTotalHrs"  runat="server" Text="Total Working Hours:" Visible="False" Height="21px" Width="141px" Font-Bold="True"></asp:Label>
                                
                                    <asp:Label ID="txtTotalHrs" runat="server" Visible="False" Width="72px" Font-Bold="True" Height="23px" ></asp:Label>
                               <asp:Label ID="lblLateEarly"  runat="server" Text="Total Late In/Early Out:" Visible="False" Height="21px"   Font-Bold="True" Width="161px"></asp:Label>
                                <asp:Label ID="txtInOut"  runat="server"  Visible="False" Height="21px" Width="69px" Font-Bold="True"></asp:Label></td>
                                </tr>
                   <tr >
                                <td colspan="8" align="center" >
                                <table>
                                <tr>
                                <td align="left"  >
                                <asp:GridView ID="grvrpt" runat="server"  autogeneratecolumns="false" 
                                    EmptyDataText="No data available."  HeaderStyle-CssClass="FixedHeader" Font-Size="9pt" >
                                     
                                     <AlternatingRowStyle Width="100" BackColor="#F2F2F2" />
                                     <Columns>
                 <asp:TemplateField HeaderText="UserID"  ItemStyle-Width="50px"   ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UserID") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UserName"  ItemStyle-Width="100px"  >
                 <ItemTemplate>
                     <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UserName") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="Designation"  ItemStyle-Width="225px"   >
                 <ItemTemplate>
                     <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Designation") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department"  ItemStyle-Width="75px"   >
                 <ItemTemplate>
                     <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Department") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Processdate"  ItemStyle-Width="50px"   ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProcessDate") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>                        
                                        
                                        
                                       
                                        <asp:boundfield datafield="Punch1_time"  headertext="In"/>
                                        <asp:boundfield datafield="OutPunch"  headertext="Out"/>
                                        <asp:boundfield datafield="Total_Wrk_Time"  headertext="Total Hrs"/>
                                        <asp:boundfield datafield="Outtime"  headertext="Break"/>
                                        <asp:boundfield datafield="WorkTIme_HHMM"  headertext="Working Hrs"/>
                                        <asp:boundfield datafield="Status"  headertext="Status"/>
                                        <asp:boundfield datafield="OT"  headertext="Overtime"/>
                                        <asp:boundfield datafield="LateIn_HHMM"  headertext="LateIn"/>
                                        <asp:boundfield datafield="EarlyOut_HHMM"  headertext="EarlyOut"/>
                                         <asp:boundfield datafield="Status1"  headertext="Late/Per" />
                                          <asp:boundfield datafield="Designation"   headertext="Designation"/>
                                    </Columns>
                                    </asp:GridView>
                                </td>
                                </tr>
                                </table>
                                    </td>
                                </tr>
       </table>
    </div>
    <div id="testdiv"></div>
    </form>
     <script type="text/javascript">
$(function() {
$('#btnClick').click(function() {
$('#DisableDiv').fadeTo('slow', .6);
$('#DisableDiv').append('<div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="loading.gif" style="background-color:Aqua;position:fixed; top:40%; left:46%;"/></div>');
setTimeout(function() { GetData() }, 1000)
})
});
function GetData()
{
$.ajax({
type: "POST",
contentType: "application/json; charset=utf-8",
url: "ShowLoadingImageonButtonClick.aspx/BindDatatable",
data: "{}",
dataType: "json",
success: function(data) {
var theHtml = data.d;
$('#testdiv').html(theHtml)
$('#DisableDiv').html("");
},
error: function(result) {
alert("Error");
}
});
}
</script>
</body>
</html>

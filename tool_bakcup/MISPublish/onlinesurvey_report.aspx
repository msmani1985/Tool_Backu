<%@ page language="C#" autoeventwireup="true" inherits="onlinesurvey_report, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Online Survey Report Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
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
       <div class="dptitle" id="divTitle" align="left" runat="server">Online Survey Report </div>
    <div id="divempdetails" runat="server">    <div align="center">
            <table class="bordertable"><tr><td>Select Team:</td><td><asp:DropDownList ID="dd_employeeteam" runat="server" DataValueField="EMPLOYEE_ID" DataTextField="EMP_FULLNAME" OnSelectedIndexChanged="dd_employeeteam_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td><td style="width: 64px"><asp:Label Text="Employee:" ID="lbl_emp" runat="server" Visible="false"></asp:Label></td><td><asp:DropDownList ID="dd_teammember" Visible="false" runat="server" DataTextField="EMP_FULLNAME" DataValueField="EMPLOYEE_ID"  AutoPostBack="True" OnSelectedIndexChanged="dd_teammember_SelectedIndexChanged1"></asp:DropDownList><asp:DropDownList ID="dd_emplocation" runat="server" DataValueField="LOCATION_ID" Visible="false" DataTextField="LOCATION_NAME"></asp:DropDownList></td><td rowspan="2"><asp:Button ID="btn_submit" Text="Submit" CssClass="dpbutton" runat="server" OnClick="btn_submit_Click" /></td></tr>
            <tr><td>Employee Number:</td><td colspan="3" align="left"><asp:TextBox ID="txt_employeenumber" runat="server"></asp:TextBox></td></tr>
            </table></div>
        
    </div>
    <br />
    <div id="errormsg" align="center" runat="server">
  <asp:Label ID="lblerr" CssClass="error" runat="server" Text=""></asp:Label>
    </div>
     <div id="divexcelbutton" runat="server">
           <table  width="100%" >
            <tr><td style="color:Crimson;font-weight:bold;font-size:10pt;" align="right">
             <asp:ImageButton ID="ibtnExcel_Export" runat="server" ImageUrl="~/images/Excel.jpg"  ToolTip="Export To Excel" OnClick="ibtnExcel_Export_Click" /></td> </tr></table></div> 

    <div align="center">
    <asp:GridView ID="gv_employeesurvey_report" runat="server"  Width="95%"
         HeaderStyle-CssClass="darkbackground" 
         AlternatingRowStyle-CssClass="dullbackground" 
         RowStyle-CssClass="lightbackground" CellPadding="4" GridLines="Horizontal" 
         AutoGenerateColumns="false" CssClass="lightbackground" ><%--OnRowDataBound=gv_ONLINE_SURVEY_REPORT_datarowbound >--%>
         
    <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground" />
    <RowStyle HorizontalAlign="left" />
    <Columns>
 
    <asp:BoundField DataField="Questions" HeaderText="QUESTIONS" />
    <asp:BoundField DataField="Answers" HeaderText="ANSWERS" />
    </Columns>
	</asp:GridView>
    </div>
    </form>
</body>
</html>

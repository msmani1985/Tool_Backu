<%@ page language="C#" autoeventwireup="true" inherits="sqlProductivityReport, App_Web_znvsjrxn" %>
 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="titlediv" class="dptitle">
        Productivity Report
    </div>
    <div align="center">
        <table width="700px" class="bordertable">
            <tr>
                <td align="right">Type:</td>
                <td>
                    <asp:RadioButtonList ID="rbtn" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtn_SelectedIndexChanged" AutoPostBack="True" >
                    <asp:ListItem Text="Operator" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Team" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td><asp:Label ID="Lblemp" Text="" runat="server"></asp:Label></td>
                <td><asp:DropDownList ID="ddlemp" runat="server" DataTextField="EMP_FULLNAME" DataValueField="EMPLOYEE_ID"></asp:DropDownList>
                </td>
                <td rowspan="2" valign="middle"><asp:Button ID="BtnSubmit" Text="Submit" CssClass="dpbutton" runat="server" OnClick="BtnSubmit_Click" /></td>
            </tr>
            <tr>
                <td align="right">Start Date:</td>
                <td><asp:TextBox ID="Txtsdate" Text="" runat="server"></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td align="right">End Date:</td>
                <td><asp:TextBox ID="Txtedate" runat="server"></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtedate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                
            </tr>
        </table>
    </div>
                                                <asp:ImageButton ID="cmd_Excel_Export" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                                    ToolTip="Export Excel" OnClick="cmd_Excel_Export_Click" />
        <br />
     <div align="center">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
 
        <asp:GridView ID="grdProdReport" runat="server" OnPageIndexChanging="grdProdReport_PageIndexChanging"  AutoGenerateColumns="false" Font-Size="8pt" CssClass="lightbackground" AllowPaging="True" PageSize="15"   emptydatatext="No data available." >

         <rowstyle backcolor="white" Height="22px"/>
                <HeaderStyle CssClass="darkbackground" />
        <alternatingrowstyle backcolor="#F0FFF0" Height="22px"/>
            <Columns>
                <asp:BoundField HeaderText="Name" DataField="empname"/>
                <asp:BoundField HeaderText="Date‎" DataField="workingday" />
                <asp:BoundField HeaderText="Cust Name‎"  DataField="cust_name"/>
                <asp:BoundField HeaderText="Job Id‎"  DataField="Code"/>
                <asp:BoundField HeaderText="Process‎"  DataField="task_name"/>
                <asp:BoundField HeaderText="Pages‎"  DataField="pages" />
                <asp:BoundField HeaderText="Start Time‎"  DataField="lstartdate"/>
                <asp:BoundField HeaderText="End Time"  DataField="lenddate"/>
                <asp:BoundField HeaderText="Total time‎(‎Min‎)‎"  DataField="mins"/>
                <asp:BoundField HeaderText="Comments‎"  DataField="Comments"/>


            </Columns>
                <EmptyDataTemplate>
        <label style="color:Red;font-weight:bold">No records found !</label>
        </EmptyDataTemplate>
         
        </asp:GridView>
 
     </div>
       
        <br />
    <br />
 
    <div runat="server" id="errMessage" class="errorMsg" ></div>
    </form>
</body>
</html>

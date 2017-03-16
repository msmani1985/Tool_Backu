<%@ page language="C#" autoeventwireup="true" inherits="consolidateLeaveDetails, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="ConsolidateGrid" runat="server" AutoGenerateColumns="False"
         OnRowCreated="ConsolidateGrid_RowCreated" AlternatingRowStyle-CssClass="dullbackground" HeaderStyle-CssClass="darkbackground"
         CssClass="lightbackground"
        >
            <Columns>
                <asp:BoundField DataField="FNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="JOINDATE" HeaderText="JoinDate" SortExpression="JOINDATE" />
                <asp:BoundField DataField="CONFIRMDATE" HeaderText="Confirmed Date" SortExpression="CONFIRMDATE" />
                <asp:BoundField DataField="DATEOFBIRTH" HeaderText="Date_of_Birth" SortExpression="DATEOFBIRTH" />
                <asp:BoundField DataField="PL_TOTAL_LEAVE" HeaderText="PL" SortExpression="PL_TOTAL_LEAVE" />
                <asp:BoundField DataField="SL_TOTAL_LEAVE" HeaderText="SL" SortExpression="SL_TOTAL_LEAVE" />
                <asp:BoundField DataField="PL_LEAVE_DAYS" HeaderText="PL" SortExpression="PL_LEAVE_DAYS" />
                <asp:BoundField DataField="SL_LEAVE_DAYS" HeaderText="SL" SortExpression="SL_LEAVE_DAYS" />
                <asp:BoundField DataField="PL_AVAILABLE_LEAVE" HeaderText="PL" SortExpression="PL_AVAILABLE_LEAVE" />
                <asp:BoundField DataField="SL_AVAILABLE_LEAVE" HeaderText="SL" SortExpression="SL_AVAILABLE_LEAVE" />
            </Columns>
        </asp:GridView>
        <asp:DataGrid ID="adgFullList" runat="server" AutoGenerateColumns="false" 
        AlternatingItemStyle-CssClass="dullbackground" HeaderStyle-CssClass="darkbackground" 
        CssClass="lightbackground"
        OnItemCreated="adgFullList_onRowCreated" 
        >
            <Columns>
                <asp:BoundColumn ReadOnly="true" DataField="EMPLOYEE_NUMBER" HeaderText="Emp. Code" SortExpression="EMPLOYEE_NUMBER" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="FNAME" HeaderText="Name" SortExpression="FNAME"></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="JOINDATE" HeaderText="Date of Joined" SortExpression="JOINDATE" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="CONFIRMDATE" HeaderText="Date of Confirmed" SortExpression="CONFIRMDATE" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="DESIGNATION_NAME" HeaderText="Designation" SortExpression="DESIGNATION" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="EMPLOYEE_TEAM_NAME" HeaderText="Department" SortExpression="DEPARTMENT" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="PL_TOTAL_LEAVE" HeaderText="PL" SortExpression="PL_TOTAL_LEAVE" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="SL_TOTAL_LEAVE" HeaderText="SL" SortExpression="SL_TOTAL_LEAVE" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="PL_LEAVE_DAYS" HeaderText="PL" SortExpression="PL_LEAVE_DAYS" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="SL_LEAVE_DAYS" HeaderText="SL" SortExpression="SL_LEAVE_DAYS" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="COMP_OFF" HeaderText="COMP_OFF" SortExpression="COMP_OFF" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="LOSS_OF_PAY" HeaderText="LOSS_OF_PAY" SortExpression="LOSS_OF_PAY" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="PL_AVAILABLE_LEAVE" HeaderText="PL" SortExpression="PL_AVAILABLE_LEAVE" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="SL_AVAILABLE_LEAVE" HeaderText="SL" SortExpression="SL_AVAILABLE_LEAVE" ></asp:BoundColumn> 
                <asp:BoundColumn ReadOnly="true" DataField="LEAVEDAYS" HeaderText="LEAVE DAYS" SortExpression="LEAVEDAYS" ></asp:BoundColumn> 
                
            </Columns>
        </asp:DataGrid> 
    </div>
    </form>
</body>
</html>

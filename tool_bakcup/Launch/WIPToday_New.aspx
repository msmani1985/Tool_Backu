<%@ page language="C#" autoeventwireup="true" inherits="WIPToday_New, App_Web_vf14wvzj" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center ">
&nbsp;<table style="text-align:left;" border="1" cellspacing="2" cellpadding="2" >
<tr><td colspan="8"><b><i>Apply Filters(s)</i></b></td></tr>
<tr><td>Customer: 
<asp:DropDownList ID="ddlcustomer" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlcustomer_SelectedIndexChanged">
<asp:ListItem Text="--NO FILTER--" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="YES" Value="1" ></asp:ListItem>
<asp:ListItem Text="NO" Value="2" ></asp:ListItem>
</asp:DropDownList>
</td><td>Jobs OnHold:&nbsp;&nbsp;<asp:DropDownList ID="ddlonhold" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlonhold_SelectedIndexChanged">
<asp:ListItem Text="--NO FILTER--" Value="0" Selected="true" ></asp:ListItem>
<asp:ListItem Text="YES" Value="1" ></asp:ListItem>
<asp:ListItem Text="NO" Value="2" ></asp:ListItem>
</asp:DropDownList></td></tr>
<tr><td>Job Type:&nbsp;&nbsp;<asp:DropDownList ID="ddlJobType" runat="server" 
        AutoPostBack="true" onselectedindexchanged="ddlJobType_SelectedIndexChanged">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList></td><td>Job Stage&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:DropDownList ID="ddlstage" runat="server" 
        AutoPostBack="true" onselectedindexchanged="ddljobstage_SelectedIndexChanged">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList></td></tr>
<tr><td>Assigned:&nbsp;&nbsp;<asp:DropDownList ID="ddlparentjobid" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlparentjobid_SelectedIndexChanged">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList></td><td>Department:&nbsp;&nbsp;<asp:DropDownList ID="ddldept" runat="server"  AutoPostBack="true" onselectedindexchanged="ddldept_SelectedIndexChanged">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList></td></tr>
<tr>
<td>Due Date:&nbsp;&nbsp;<asp:DropDownList ID="ddlcatsduedate" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlcatsduedate_SelectedIndexChanged">
</asp:DropDownList>
</td>
<td>Is SAM:&nbsp;&nbsp;<asp:DropDownList ID="ddlIsSAM" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlIsSAM_SelectedIndexChanged" >
</asp:DropDownList>
</td>
</tr>
<tr>
<td>Is Copyedit:&nbsp;&nbsp;<asp:DropDownList ID="ddlIsCopyEdit" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlIsCopyEdit_SelectedIndexChanged" >
</asp:DropDownList>
</td>
 
</tr>
</table><br />
    </div>
    
<table>
<tr>
<td>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
     
<asp:Button ID="btnExcelExpt" runat="server" Text="ExportToExcel" OnClick="btnExcelExpt_Click" Visible="false" />
<asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  />

<asp:ImageButton ImageUrl="images/icon-excel2010.gif" runat="server" ID="exportExcel_selectedcolumns" OnClick="exportExcel_selectedcolumns_Click"  />
</td>

</tr>
<tr>
<td>

<asp:GridView runat="server" ID="gvWIPtoday" 
        onrowdatabound="gvWIPtoday_RowDataBound" EnableViewState="true" AutoGenerateColumns="false" AllowSorting="true" 
        OnSorting="gvWIPtoday_OnSort">
        <Columns>
        <asp:BoundField HeaderText="S. No." DataField="SNO" />
        <asp:BoundField HeaderText="JOB NUMBER" DataField="JOB_NUMBER" />
        <asp:BoundField HeaderText="STAGE" DataField="JOB_STAGE" SortExpression="JOB_STAGE" />
        <asp:BoundField HeaderText="NAME" DataField="NAME" SortExpression="NAME" />
        <asp:BoundField HeaderText="SAM JOB" DataField="SAM_JOURNAL" SortExpression="SAM_JOURNAL" />
        <asp:BoundField HeaderText="COPYEDIT" DataField="CE_JOURNAL" SortExpression="CE_JOURNAL" />
        <asp:BoundField HeaderText="ASSIGNED" DataField="ASSIGNED" SortExpression="ASSIGNED" />
        <asp:BoundField HeaderText="DEPARTMENT" DataField="DEPARTMENT" SortExpression="DEPARTMENT" />
        <asp:BoundField HeaderText="EMPLOYEE" DataField="EMPLOYEE" SortExpression="EMPLOYEE" />
        <asp:BoundField HeaderText="REC. DATE" DataField="RECEIVED_DATE" SortExpression="RECEIVED_DATE" />
        <asp:BoundField HeaderText="CATS DUE DATE" DataField="CATS_DUE_DATE" SortExpression="CATS_DUE_DATE" />
        <asp:BoundField HeaderText="HOLD DETAILS" DataField="ONHOLD" SortExpression="ONHOLD" />
        <asp:BoundField HeaderText="PRE-EDIT" DataField="TAGGEDWORDDATE" SortExpression="TAGGEDWORDDATE" />
        <asp:BoundField HeaderText="EARLY XML" DataField="EARLYXMLDATE" SortExpression="EARLYXMLDATE" />
        <asp:BoundField HeaderText="TYPESETTING" DataField="TYPESETTINGDATE" SortExpression="TYPESETTINGDATE" />
        <asp:BoundField HeaderText="AMOUPLOADTIME" DataField="AMORECIVEDTIME" SortExpression="AMORECIVEDTIME" />
        <asp:BoundField HeaderText="ACTUAL CATSDUEDATE" DataField="ACTUAL_CATSDUEDATE" SortExpression="ACTUAL_CATSDUEDATE" />
        </Columns>
</asp:GridView>

</td>
</tr>
</table>
    </form>
</body>
</html>

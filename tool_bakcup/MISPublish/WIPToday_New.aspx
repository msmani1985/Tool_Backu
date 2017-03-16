<%@ page language="C#" autoeventwireup="true" inherits="WIPToday_New, App_Web_mjsvsc11" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="divTitle" align="left" runat="server">Daily WIP</div>
<table style="border:1px solid green;align:center;" align="center" >
    <tr>
            <td align="center" class="darkTitle" colspan="8">
                Apply Filters</td>
    </tr>
    <tr>
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text="Customer :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            </td>
            <td> 
<asp:DropDownList ID="ddlcustomer" runat="server"  AutoPostBack="true" 
                    onselectedindexchanged="ddlcustomer_SelectedIndexChanged" Width="150px">
<asp:ListItem Text="--NO FILTER--" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="YES" Value="1" ></asp:ListItem>
<asp:ListItem Text="NO" Value="2" ></asp:ListItem>
</asp:DropDownList>
            </td>
            <td></td>
            <td align="right">
            
                <asp:Label ID="Label2" runat="server" Text="Jobs On Hold :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td><asp:DropDownList ID="ddlonhold" runat="server"  AutoPostBack="true" 
                    onselectedindexchanged="ddlonhold_SelectedIndexChanged" Width="150px">
<asp:ListItem Text="--NO FILTER--" Value="0" Selected="true" ></asp:ListItem>
<asp:ListItem Text="YES" Value="1" ></asp:ListItem>
<asp:ListItem Text="NO" Value="2" ></asp:ListItem>
</asp:DropDownList></td>
            <td></td>
            <td align="right">
            
                <asp:Label ID="Label3" runat="server" Text="Job Type :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td>
            
                <asp:DropDownList ID="ddlJobType" runat="server" 
        AutoPostBack="true" onselectedindexchanged="ddlJobType_SelectedIndexChanged" 
                    Width="150px">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList>
            
            </td>
    </tr>
    <tr>
            <td align="right">
            
                <asp:Label ID="Label8" runat="server" Text="Job Stage :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td>
<asp:DropDownList ID="ddlstage" runat="server" 
        AutoPostBack="true" onselectedindexchanged="ddljobstage_SelectedIndexChanged" 
                    Width="150px">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList></td>
            <td></td>
            <td align="right">
            
                <asp:Label ID="Label7" runat="server" Text="Assigned :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td><asp:DropDownList ID="ddlparentjobid" runat="server"  AutoPostBack="true" 
                    onselectedindexchanged="ddlparentjobid_SelectedIndexChanged" Width="150px">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList></td>
            <td></td>
            <td align="right">
            
                <asp:Label ID="Label4" runat="server" Text="Department :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td>
            
                <asp:DropDownList ID="ddldept" runat="server"  AutoPostBack="true" 
                    onselectedindexchanged="ddldept_SelectedIndexChanged" Width="150px">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList>
            
            </td>
    </tr>

    <tr>
            <td align="right">
            
                <asp:Label ID="Label9" runat="server" Text="Due Date :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td><asp:DropDownList ID="ddlcatsduedate" runat="server"  AutoPostBack="true" 
                    onselectedindexchanged="ddlcatsduedate_SelectedIndexChanged" Width="150px">
</asp:DropDownList>
            </td>
            <td></td>
            <td align="right">
            
                <asp:Label ID="Label6" runat="server" Text="Is SAM :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td><asp:DropDownList ID="ddlIsSAM" runat="server"  AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlIsSAM_SelectedIndexChanged" Width="150px" >
</asp:DropDownList>
            </td>
            <td></td>
            <td align="right">
            
                <asp:Label ID="Label5" runat="server" Text="Is Copyedit :" 
                    Font-Names="Segoe UI" Font-Size="12px"></asp:Label>
            
            </td>
            <td>
            
                <asp:DropDownList ID="ddlIsCopyEdit" runat="server"  AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlIsCopyEdit_SelectedIndexChanged" Width="150px" >
</asp:DropDownList>
            
            </td>
    </tr>

    <tr>
            <td align="right">
            
                <asp:Label ID="Label10" runat="server" Text="Vendor Name :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td><asp:DropDownList ID="ddlVendorname" runat="server"  AutoPostBack="true" 
                    onselectedindexchanged="ddlcatsduedate_SelectedIndexChanged" Width="150px">
</asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td align="right">
            
                <asp:Label ID="Label11" runat="server" Text="Is Preedit :" Font-Names="Segoe UI" 
                    Font-Size="12px"></asp:Label>
            
            </td>
            <td><asp:DropDownList ID="ddlIsPreEdit" runat="server"  AutoPostBack="true" 
                    onselectedindexchanged="ddlIsPreedit_SelectedIndexChanged" Width="150px">

                   
</asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td align="right">
            
                &nbsp;</td>
            <td>
            
                &nbsp;</td>
    </tr>
</table>
    
<table width="100%">
<tr>
<td align="right" style="padding-right:30px;">
   
<asp:Button ID="btnExcelExpt" runat="server" Text="ExportToExcel" OnClick="btnExcelExpt_Click" Visible="false" />
<asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" 
        OnClick="exportExcel_Click" Height="20px"  />

<asp:ImageButton ImageUrl="images/icon-excel2010.gif" runat="server" 
        ID="exportExcel_selectedcolumns" OnClick="exportExcel_selectedcolumns_Click" 
        Height="20px"  visible= "false" />
</td>

</tr>
<tr>
<td>
 <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="490px" Width="100%">
<asp:GridView runat="server" ID="gvWIPtoday" 
        onrowdatabound="gvWIPtoday_RowDataBound" AutoGenerateColumns="False" AllowSorting="True" 
        OnSorting="gvWIPtoday_OnSort" Font-Names="Segoe UI" Font-Size="11px" 
         Width="100%">
        <rowstyle backcolor="white" Height="22px"/>
        <alternatingrowstyle backcolor="#F0FFF0" Height="22px"/>
        <Columns>
        <asp:BoundField HeaderText="S. No." DataField="SNO" />
        <asp:BoundField HeaderText="Job Number" DataField="JOB_NUMBER" />
        <asp:BoundField HeaderText="Stage" DataField="JOB_STAGE" 
                SortExpression="JOB_STAGE" />
        <asp:BoundField HeaderText="Name" DataField="NAME" SortExpression="NAME" />
        <asp:BoundField HeaderText="Sam Job" DataField="SAM_JOURNAL" 
                SortExpression="SAM_JOURNAL"  Visible="false" />
        <asp:BoundField HeaderText="Copy Edit" DataField="CE_JOURNAL" 
                SortExpression="CE_JOURNAL" Visible="false" />
        <asp:BoundField HeaderText="Assigned" DataField="ASSIGNED" 
                SortExpression="ASSIGNED" />
        <asp:BoundField HeaderText="Department" DataField="DEPARTMENT" 
                SortExpression="DEPARTMENT" />
        <asp:BoundField HeaderText="Employee" DataField="EMPLOYEE" 
                SortExpression="EMPLOYEE" >
            <ItemStyle Width="70px" />
            </asp:BoundField>
        <asp:BoundField HeaderText="Rec. Date" DataField="RECEIVED_DATE" 
                SortExpression="RECEIVED_DATE" />
        <asp:BoundField HeaderText="Internal Due Date" DataField="CATS_DUE_DATE" 
                SortExpression="CATS_DUE_DATE" />
             <asp:BoundField HeaderText="Due Date" DataField="ACTUAL_CATSDUEDATE" 
                SortExpression="ACTUAL_CATSDUEDATE" />
        <asp:BoundField HeaderText="Hold Details" DataField="ONHOLD" 
                SortExpression="ONHOLD" />
        <asp:BoundField HeaderText="Pre-Edit" DataField="Ispreedit" 
                SortExpression="Ispreedit" />
            <asp:BoundField HeaderText="Pre-Edit" DataField="TAGGEDWORDDATE" 
                SortExpression="TAGGEDWORDDATE" Visible="false"/>
        <asp:BoundField HeaderText="Early Xml" DataField="EARLYXMLDATE" 
                SortExpression="EARLYXMLDATE" Visible="false"/>
        <asp:BoundField HeaderText="Type Setting" DataField="TYPESETTINGDATE" 
                SortExpression="TYPESETTINGDATE" Visible="false"/>
        <asp:BoundField HeaderText="Amo Upload Time" DataField="AMORECIVEDTIME" 
                SortExpression="AMORECIVEDTIME" />
				<asp:BoundField HeaderText="Rec. Time" DataField="Mail_Received_Time" 
                SortExpression="Mail_Received_Time" />
				<asp:BoundField HeaderText="Upload Time" DataField="Mail_Received_Time" 
                SortExpression="Mail_Received_Time" />
				<asp:BoundField HeaderText="CUST. NAME" DataField="CUSTOMER_NAME_Fiter" 
                SortExpression="CUSTOMER_NAME_Fiter" />
            <asp:BoundField HeaderText="VENDOR NAME" DataField="VENDOR_NAME" 
                SortExpression="VENDOR_NAME" />
         <asp:BoundField HeaderText="Status" DataField="Status" 
                SortExpression="Status" />
             <asp:BoundField HeaderText="ZipName" DataField="ZipName" 
                SortExpression="ZipName" />
             <asp:BoundField HeaderText="DOI" DataField="DOI" 
                SortExpression="DOI" />
       
        </Columns>
         <HeaderStyle CssClass="darkbackground" Font-Bold="True" 
                                    ForeColor="White" Height="25px" />
</asp:GridView>
</asp:Panel>
</td>
</tr>
</table>
    </form>
</body>
</html>

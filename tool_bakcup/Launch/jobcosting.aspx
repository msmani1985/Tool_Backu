<%@ page language="C#" autoeventwireup="true" inherits="jobcosting, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />     
</head>
<body>
    <form id="form1" runat="server">
       <div class="dptitle" id="invtitle" runat="server" >Job Costing</div>
    <table width="550px" style="vertical-align:middle" align="center" class="bordertable"><tr>
        <td>Job Type:</td><td>
        <asp:DropDownList ID="ddljobtype" runat="server" >
            <asp:ListItem Value="1" text="Journal" Selected="True"></asp:ListItem>
            <asp:ListItem Value="2" text="Book"></asp:ListItem>
            <asp:ListItem Value="3" text="Project"></asp:ListItem>                    
        </asp:DropDownList>                 
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;<asp:TextBox style="display:none" id="txtJobNumber" Text="" runat="server" ></asp:TextBox> </td>
        </tr>
            <tr >
                <td>
                    Start&nbsp;Date:</td>
                <td >
                    <asp:TextBox ID="txtStartdate" runat="server"></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td>
                <td >
                    End&nbsp;Date:</td>
                <td >
                    <asp:TextBox ID="txtEnddate" runat="server"></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtEnddate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td>
            </tr>        
        <tr><td id="Td1" align="center" colspan="4" valign="bottom" runat="server"   >
            <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click" />
        </td></tr>
    </table>  
    <br />
    <div id="errMessage" class="errorMsg" style="display:none" runat="server" >No records found, please check Job Number</div>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" />
        <asp:GridView ID="JobCostDetails" runat="server" AutoGenerateColumns="False" 
        HorizontalAlign="Center"
        CellPadding="3"
        HeaderStyle-CssClass="darkbackground"  
        AlternatingRowStyle-CssClass="dullbackground" 
        CssClass="lightbackground" 
        BorderColor="Black" BorderWidth="1px" Width="800px"
          OnRowDataBound="JobCostDetails_OnRowDataBound" 
          OnRowCreated="JobCostDetails_OnRowCreated">
            <Columns>
                <asp:BoundField DataField="CustName" HeaderText="Customer Name" SortExpression="CustName" />
                <asp:BoundField DataField="JourCode" HeaderText="Code" SortExpression="JourCode" />
                <asp:BoundField DataField="INo" HeaderText="INO" SortExpression="INo"  />                
                <asp:BoundField DataField="iIssueNo" HeaderText="Issue" SortExpression="iIssueNo" />
                <asp:BoundField DataField="COLUMN7" HeaderText="Pages" SortExpression="Column1" />
                <asp:BoundField DataField="iinvoiceno" HeaderText="Inv. No" SortExpression="iinvoiceno" />
               
                <asp:TemplateField HeaderText="ManHours">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="CalHours"  Text="" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="COLUMN1" HeaderText="(Euro)"/>
                <asp:BoundField DataField="COLUMN2" HeaderText="(INR)"  />
                <asp:BoundField DataField="COLUMN3" HeaderText="(Euro)"   />                
                <asp:BoundField DataField="COLUMN4" HeaderText="(INR)" />
                <asp:BoundField DataField="COLUMN5" HeaderText="(Euro)"  />
                <asp:BoundField DataField="COLUMN6" HeaderText="(INR)" />

            </Columns>
            <HeaderStyle CssClass="darkbackground" />
            <AlternatingRowStyle CssClass="dullbackground" />
        </asp:GridView>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="JobReceivedReport, App_Web_zfrrxy20" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
 
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="dptitle" id="divTitle" align="left" runat="server">Job Received Report </div>

    <div>
    
        <table align="center" class="bordertable" width="70%"  id="TABLE1">
            <tr style="border-bottom:solid 1px green;border-bottom-width:thick;"><td >
            
                <asp:Label ID="Label1" runat="server" Text="Job Type:"  ></asp:Label>
                
                </td>
                <td><asp:DropDownList  ID="JobType" runat="server">
                    <asp:ListItem Selected="True" Value="6">Issue</asp:ListItem>
                    <asp:ListItem  Value="5">Article</asp:ListItem>
                    <asp:ListItem  Value="2">Book</asp:ListItem>
                    <asp:ListItem  Value="7">Chapter</asp:ListItem>
                    <asp:ListItem  Value="4">Project</asp:ListItem>
                    <asp:ListItem  Value="8">Module</asp:ListItem>                
                </asp:DropDownList></td>
                
                <td >
                    <asp:Label ID="Label2" runat="server" Text="Customer Name:" ></asp:Label>
                    
                </td>
                <td><asp:DropDownList  ID="CustomerName" runat="server" AutoPostBack="true" EnableViewState="true" >
                    </asp:DropDownList></td>
                    <%--<td><asp:Button ID="ViewReport" runat="server" Text="View Report" CssClass="dpbutton" OnClick="ViewReport_Click" /></td>--%>
                    <td rowspan="3" valign="middle"><asp:Button ID="ViewReport" Width="100px"  CssClass="dpbutton" Text="View Report" runat="server" OnClick="ViewReport_Click"/></td>
                </tr>
                               
                <tr>
                <td colspan="4" align="right" style="background-image:url('Images/line.gif');background-repeat:repeat-x;" >
                </td>
                </tr>
                <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="From:"></asp:Label>
                </td>
                <td><asp:TextBox ID="FromDate" runat="server" ></asp:TextBox>
                    <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" ID="ImageButton1" Height="24px" Width="25px" ImageAlign="Middle" OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=FromDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" /></td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="To:"></asp:Label>
                    
                </td>
                <td> <asp:TextBox ID="ToDate" runat="server" ></asp:TextBox>
                    
                    <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" ID="ImageButton2" Height="24px" Width="25px" ImageAlign="Middle" OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=ToDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
                    </td>
                
                </tr>
                
            
        </table>
         <br />
        
 
        <table  width="100%" >
            <tr><td style="color:Crimson;font-weight:bold;font-size:10pt;" align="right">
             <asp:ImageButton ID="ibtnExcel_Export" runat="server" AlternateText="Excel Export" ImageUrl="~/images/Excel.jpg" OnClick="ibtnExcel_Export_Click" />
             </td>
            </tr>
        </table>
        
        <table width="100%">
        <tr> 
        <td colspan="2" >
            <asp:GridView Width="100%" ID="jobreceived" CaptionAlign="left" runat="server"
                AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333"
                GridLines="Vertical" BorderColor="green" OnRowDataBound="gv_jobreceived_datarowbound" OnSelectedIndexChanged="jobreceived_SelectedIndexChanged">
                <headerstyle backcolor="Green" font-bold="True" forecolor="White" />
               
                <columns>
        
        <asp:BoundField DataField="JOB_ID" HeaderText="JOB ID"/>
       
        <asp:BoundField DataField="CUSTOMER NAME" HeaderText="CUSTOMER NAME"/>
        <asp:BoundField DataField="JOURNAL CODE" HeaderText="JOURNAL CODE"/>
        <asp:BoundField DataField="JOB NAME" HeaderText="JOB NAME"/>
  <%--      <asp:BoundField DataField="Work Flow" HeaderText="WORK FLOW"/>--%>
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:DropDownList OnSelectedIndexChanged="job_stage_OnSelectedIndexChanged" AutoPostBack="true" ID="dd_jobstage" runat="server" ></asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem,"[JOB STAGE]") %>
        </ItemTemplate>
        </asp:TemplateField>
       
        <asp:BoundField DataField="PRINT PAGES" HeaderText="PRINT PAGES"/>
        <asp:BoundField DataField="RECEIVED DATE" HeaderText="RECEIVED DATE"/>
 
        <asp:BoundField DataField="DUE DATE" HeaderText="DUE DATE"/>
      
        <asp:BoundField DataField="CATS DUE DATE" HeaderText="CATS DUE DATE"/>
        <asp:BoundField DataField="DESPATCH DATE" HeaderText="DESPATCH DATE"/>
        <asp:BoundField DataField="INVOICE DATE" HeaderText="INVOICED DATE" />
        <asp:BoundField DataField="DELIVERY" HeaderText="DELIVERY" />
            <asp:BoundField  HeaderText="Supervisor Name" />
<%--    <asp:BoundField DataField="Cat_Received_Time" HeaderText="Cats Received Time" />
        <asp:BoundField DataField="IST" HeaderText="Upload Time (IST)" />
        <asp:BoundField DataField="GMT" HeaderText="Upload Time (GMT)" />--%>
        </columns>
                <headerstyle cssclass="darkbackground" />
            </asp:GridView>
        </td>
        </tr> 
        </table>
    </div>
       
        <div id="div_Error" runat="server" class="error"></div>
        
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="OnlineRequestStatus, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 
    <title>Online Status</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
    .btn
    {
    background-color: GREEN;
    font-color: GREEN;
	font-weight: bold;
	font-size: 8pt;
	width: 60pt;
	color: white;
	height: 16pt;
	text-align: center;
	cursor: pointer;
	z-index: 1000;
    }
    </style>
    
    <style type="text/css">
    .log
    {
    background-color: White;
    font-color: GREEN;
	font-weight: bold;
	font-size: 8pt;
    </style>
    
    <style type="text/css">
  .error
{
	font-color: Red;
	font-weight: bold;
	font-size: 10pt;
	text-align: center;
}
 </style>
    
    </head>
<body>

    <form id="form1" runat="server"  >
    <div class="dptitle" id="divTitle" align="left" runat="server">Online Request Allocated</div>
   <%-- <div class="dptitle" id="divlog" align="left" runat="server" onload="logger();" ></div>--%>
 <%--<table>
    <tr> 
    <td>  
    <asp:Label ID="lbllogername" runat="server" CssClass="log" OnLoad="log();"></asp:Label>
    </td>
    </tr>
    </table> --%>
                        <ol id="toc">
                        
                            <li id="LstGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral" runat="server" TabIndex="5" OnClick="lnkGeneral_Click1">General</asp:LinkButton></li>
                            <li id="LstPending" runat="server">
                                <asp:LinkButton ID="lnkPending" runat="server" TabIndex="6" OnClick="lnkPending_Click1">Pending</asp:LinkButton></li>
                            <li id="LstCompleted" runat="server">
                                <asp:LinkButton ID="lnkcomplete" runat="server" TabIndex="7" OnClick="lnkcomplete_Click" >Completed</asp:LinkButton></li>
                           </ol>
                                            
                            <div id="tabGeneral" runat="server"> 
                         
                                <table id="Table4"  align ="center" class="bordertable" style="width:100%"> <%--cellpadding="2" cellspacing="0" width="">--%>
                                <tr  align="center" class="dpJobGreenHeader">
                                <td align="center">
                                <asp:Label ID="Label3" runat="server" Text="Task"></asp:Label>
                                <asp:DropDownList ID="ddl_task" DataTextField="task_name" DataValueField="task_id" runat="server">
                                </asp:DropDownList>
                                                                                       
                                <asp:Label ID="Label1" runat="server" Text="Assigned_To"></asp:Label>&nbsp;
                                <asp:DropDownList ID="ddl_assignto" DataTextField="employeewithteam" DataValueField="employee_id" runat="server">
                                </asp:DropDownList></td> 
                               
                                </tr>
                                                               
                                <tr  align="center" class="dpJobGreenHeader">      
                                <td align="center" style="height: 20px">       
                                <asp:Label ID="Label4" runat="server" Text="Assigned Date"></asp:Label>
                                <asp:TextBox ID="txtassigndate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                
                                                         
                                <asp:Label ID="Label2" runat="server" Text="Redirect To"></asp:Label>&nbsp;
                                <asp:DropDownList ID="ddl_redirect" DataTextField="employeewithteam" DataValueField="employee_id" runat="server">
                                </asp:DropDownList></td>
                                
                                <tr  align="center" class="dpJobGreenHeader">      
                                <td align="center" style="height: 20px">       
                                <asp:Label ID="Label5" runat="server" Text="Due Date"></asp:Label>
                                <asp:TextBox ID="txtDueDate" runat="server" ></asp:TextBox>
                                <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" ID="ImageButton2" Height="24px" Width="25px" ImageAlign="Middle" OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
                                              
                                 
                                <tr align="center" class="dpJobGreenHeader">
                                <td align="center" style="height: 20px">
                                <asp:Label ID="Label6" runat="server" Text="Comments"></asp:Label> 
                                <asp:TextBox ID="txtcomment" runat="server" CssClass="TxtBox" Width="427px" TabIndex="30" BackColor="#F1F1F1" TextMode="MultiLine" Height="65px"></asp:TextBox>
                                
                                <tr align="center"  class="dpJobGreenHeader">
                                <td align="center">
                                <asp:Button ID="btnsubmit" CssClass="btn" runat="server" Text="Submit" OnClick="btnsubmit_Click"/>
                                <asp:Button ID="btnclear" CssClass="btn" runat="server" Text="clear" OnClick="btnclear_Click"/>
                                </td>
                                </tr>
                                <tr><td align="center">
                                    <asp:Label ID="lbmsg" runat="server" CssClass="error" Text=""></asp:Label></td></tr>
                                                          
      
        <tr>
        <td> 
        <br />
        <asp:label ID="lblerr" runat="server" text=""></asp:label>  </table></div>      
        <br />
        
        
         <div id="griddiv" runat="server">
        <%-- <table id="Table1"  align ="center" class="bordertable" style="width:100%"> 
                                <tr  align="center" >
                                <td align="center">--%> 
       <%-- <asp:Panel ID="Panel1" runat="server" ScrollBars="vertical" Height="430" Width="100%">--%>
        
        <asp:GridView ID="gv_online_status" CaptionAlign="left" runat="server" Visible="true"
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
        GridLines="Vertical" BorderColor="green" AllowSorting="True" OnSorting="gv_GENERAL_sorting" >
        
        <Columns>
        <asp:BoundField DataField="request_title" HeaderText="TITLE"  SortExpression="request_title"/>
        <asp:BoundField DataField="task_name" HeaderText="TASK" SortExpression="TASK"/>
        <asp:BoundField DataField="to_request_teamname" HeaderText=" REQUEST TO TEAM" SortExpression="Request_to_team_id"/>
        <asp:BoundField DataField="assigned_to" HeaderText="ASSIGN TO"  />
        <asp:BoundField DataField="from_request_team" HeaderText="REQUEST FROM TEAM NAME" SortExpression="REQUEST FROM"/>  
        <asp:BoundField DataField="request_from" HeaderText=" REQUEST FROM EMPLOYEE NAME"/>  
        <asp:BoundField DataField="Request_date" HeaderText="REQUEST DATE" SortExpression="REQUEST DATE"/>
        <asp:BoundField DataField="assigned_date" HeaderText="ASSIGN DATE" SortExpression="ASSIGNED DATE"/>
        <asp:BoundField DataField="due_date" HeaderText="DUE DATE" SortExpression="DUE DATE"/>
        <asp:BoundField DataField="start_date" HeaderText="START DATE" />
        <asp:BoundField DataField="priority_name" HeaderText="PRIORITY" SortExpression="PRIORITY" />
        <asp:BoundField DataField="completed_date" HeaderText="COMPLETION DATE" SortExpression="COMPLETED DATE"/>
        <asp:BoundField DataField="hold_status" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="red" HeaderText="Hold Status" SortExpression="hold_status" />
        <asp:BoundField DataField="hold_reason" HeaderText="Hold Reason" SortExpression="hold_reason" />
        <asp:TemplateField>
        <ItemTemplate>
    
        <asp:CheckBox ID="chkRequest"  runat="server"/>
        
        <asp:HiddenField id="hidenrequid" Value='<%# DataBinder.Eval(Container.DataItem,"online_request_id") %>' runat="server">
        </asp:HiddenField>
       
        
        </ItemTemplate>
            
        </asp:TemplateField>
        </Columns>
         
        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
           <%-- </asp:Panel></div>--%>
       <%-- </td>
        </tr>
       
        </table>--%>
    </div> 
     <div id="div_Error" runat="server" class="error">
         &nbsp;</div>
     </form>
</body>
</html>

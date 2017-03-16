<%@ page language="C#" autoeventwireup="true" inherits="online_request_start_stop, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>ONLINE REQUEST VIEW</title>
    <link href=default.css type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    
   
    <style type="text/css">
    .btn
    {
    background-color: GREEN;
	font-weight: bold;
	font-size: 8pt;
	width: 60pt;
	color: white;
	height: 16pt;
	text-align: center;
	cursor: pointer;
	z-index: 1000;
    }
 
    .tab
{
	z-index: 1000;
	font-size: 8pt
}
 
   
  .error
{
	color: Red;
	font-weight: bold;
	font-size: 10pt;
	text-align: center;
}
  
  
 .lblhead
 {
 color:green;
 font-weight:bold;
 } 
 
    
 .log
    {
    background-color: White;
    font-color: GREEN;
	font-weight: bold;
	font-size: 8pt;
	
   </style>
    
    <style type="text/css">
     div.ModalPopup {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 11px;
        font-style: normal;
        background-color: #fff;
        position:absolute;
        /* set z-index higher than possible */
        z-index:10000;
        visibility: hidden;
        color: Black;
        border-style: solid;
        border-color: #999999;
        border-width: 1px;
        width: 300px;
        height :auto;
        }
        
        div.divMasked 
        {
        visibility: hidden;
        position:absolute;
        left:0px;
        top:0px;
        font-family:verdana;
        font-weight:bold;
        padding:40px;
        z-index:100;        
        background-image:url(images/tools/Mask.png);
        /* ieWin only stuff */
        _background-image:none;
        _filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true, sizingMethod=scale src='Mask.png');
        opacity:0.4;
        filter:alpha(opacity=70)
        }
        
        
        </style>
      
       </head>
    
  <script language="javascript">
function showDiv()
{
  //alert(document.getElementById('divOnHold').style.visibility);
  document.getElementById('divOnHold').style.display=""; 
  
}
function closeDiv()
 {
    refID = document.getElementById('divOnHold');
	refID.style.display = "none";
 }
	
	

</script> 
 

<body>
<form id="form1" runat="server">
<div class="dptitle" id="divTitle" align="left" runat="server">Online Request View</div>

  <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
        </div>
        <iframe width="0" scrolling="no" height="0" frameborder="0" class="divMasked" id="iframetop">
        </iframe>
       <div><asp:Label ID="lname" runat="server" Text=""></asp:Label></div>
     <ol id="toc">
      <li id="LstGeneral" runat="server">
     <asp:LinkButton ID="lnkGeneral" runat="server" TabIndex="6" OnClick="lnkGeneral_Click">General</asp:LinkButton></li>
     <li id="LstPending" runat="server">
     <asp:LinkButton ID="lnkPending" runat="server" TabIndex="6" OnClick="lnkPending_Click">Pending</asp:LinkButton></li>
     <li id="LstCompleted" runat="server">
     <asp:LinkButton ID="lnkcomplete" runat="server" TabIndex="7" OnClick="lnkcomplete_Click1">Completed</asp:LinkButton></li>
     </ol>
                            
      <div id="tabGeneral" runat="server"> 
                       
    
    <div>
    <div>
       
    <table>
    <tr>
    <td style="height: 23px">
       &nbsp;<asp:Button ID="btnstart" CssClass="btn" runat="server" Text="Start" OnClick="btnstart_Click" />
    </td>
     
    <td style="height: 23px"> 
    <asp:Button ID="btnend" OnClick="btnend_Click1"  CssClass="btn" runat="server" Text="End" />
    </td>
     
    <td style="height: 23px">
    <input type=button ID="btnhold" runat="server"  Class="btn" value="Hold" OnClick ="showDiv()"/>
    </td>
    </tr>
    </table>
     <table>
   <tr>
   <td style="height: 15px">
   
       <div id="divOnHold" style="display:none;background-color:Honeydew;">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
       <tr>
       <td align="left" style="background-color: green; color: White; font-weight: bold;width: 163px;">
        &nbsp;Request On Hold
       </td>
       <td align="right" style="background-color: green; color: White; font-weight: bold">
       <a href="#" onclick="closeDiv(divOnHold);" title="Close" style="color: White;">
       [x]</a>
       </td>
       </tr>
       <tr>
       <td style="width: 163px">
       &nbsp;Reason for Hold:<span style="font-size: 9pt; color: #ff0000">*</span></td>
       <td>
       <asp:TextBox ID="txtholdreason" runat="server" CssClass="TxtBox" Width="180px"
       MaxLength="300"></asp:TextBox></td>
       </tr>
       <tr bgcolor="Honeydew">
       <td colspan="2" align="center" style="height: 19px">
       
       <asp:Button ID="btnsubmit"  CssClass="btn" runat="server" Text="Job Hold" OnClick="btnsubmit_Click" />
       
       &nbsp; 
    <asp:Button ID="btnunhold"  CssClass="btn" runat="server" Text="Job Release" OnClick="btnunhold_Click"/>
       
                                                     </td>
                                                    </tr>
                                                    
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="center">
                                    <asp:Label ID="lblmsg1" CssClass="error" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblmsg2"  CssClass="error" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblmsg3" CssClass="error" runat="server" Text=""></asp:Label>
                                     <asp:Label ID="lblmsg4" CssClass="error" runat="server" Text=""></asp:Label>
                                    </td>
                                    </tr>
                                    
                                 
                                </table>
    
  <%--  Div for ONHOLD--%>
        
  <%--  <div id=divhold runat="server" style="display:none;" >
        <asp:TextBox ID="txtholdreason" runat="server" TextMode="MultiLine"></asp:TextBox> <br />
        <asp:Button ID="btnsubmit" CssClass="btn" runat="server" Text="submit"/></div>--%>
     
    
    <asp:Panel ID="Panel1" runat="server" ScrollBars="vertical" Height="430" Width="100%">
        
        <asp:GridView ID="gv_online_status_allocation" CaptionAlign="left" runat="server"
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
        GridLines="Vertical" BorderColor="green" AllowSorting="True" OnSorting="gv_general_sorting" >
        
        <Columns>
            
         <asp:BoundField DataField="Description" HeaderText="REQUEST"  SortExpression="DESCRIPTION"/>
         <asp:BoundField DataField="task_name" HeaderText="TASK" SortExpression="task_name"/>
         <asp:BoundField DataField="Assigned_to" HeaderText="ASSIGN TO" SortExpression="Assigned_to"/>
         <asp:BoundField DataField="assigned_date" HeaderText="REQUEST RECEIVED  DATE" />
         <asp:BoundField DataField="due_date" HeaderText="DUE DATE" />
         <asp:BoundField DataField="start_date" HeaderText="START DATE" SortExpression="START DATE" />
         <asp:BoundField DataField="completed_date" HeaderText="REQUEST COMPLETED DATE" />
         <asp:BoundField DataField="priority_name" HeaderText="PRIORITY" SortExpression="priority_name" />
         <asp:BoundField DataField="hold_status" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="red" HeaderText="Hold Status" SortExpression="hold_status" />
         <asp:BoundField DataField="hold_reason" HeaderText="Hold Reason" SortExpression="hold_reason" />
        <asp:TemplateField>
        <ItemTemplate>
       
        <asp:CheckBox ID="chkallocated"  runat="server"/>
       
        <asp:HiddenField id="hidenallocated" Value='<%# DataBinder.Eval(Container.DataItem,"online_request_id") %>' runat="server">
        </asp:HiddenField>  
        </ItemTemplate>
            
        </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
                 
       </asp:Panel>
        </div>
        </div>
        </div>
      
    </form>
</body>
</html>

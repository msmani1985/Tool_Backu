<%@ page language="C#" autoeventwireup="true" inherits="taskmgr, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Event Log</title>
    <link href="default.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="divheader" runat="server" >Event Logger of </div>
    <div class="dptitle">
            <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:700px;" >
                <tr><td>Job Type</td><td>
                <asp:DropDownList ID="ddljobtype" runat="server" >
                    <asp:ListItem Value="1" text="Issue" Selected=True></asp:ListItem>
                    <asp:ListItem Value="2" text="Article"></asp:ListItem>
                    <asp:ListItem Value="3" text="Book"></asp:ListItem>
                    <%--<asp:ListItem Value="4" text="Chapter"></asp:ListItem>--%>
                    <asp:ListItem Value="5" text="Project"></asp:ListItem> 
                    <asp:ListItem Value="6" text="Module"></asp:ListItem>
                </asp:DropDownList> 
                </td>
                <td>Barcode:</td>
                <td><asp:TextBox ID="txtJobNumber" runat="server" Text="" ></asp:TextBox> </td>
                <td>Task Name:</td>
                <td>
                <asp:DropDownList ID="ddlStyles" runat="server" DataTextField="SNAME" DataValueField="SNO"></asp:DropDownList>
                </td>                              
                  <td id="Td2" align=center runat="server"   >
                    <asp:Button Text="Log Now" CssClass="dpbutton" runat="server"  ID="Button1" OnClick="btnSubmit_Click" UseSubmitBehavior="False" />
                </td>  
                </tr>
        </table>
    </div> 
    <div runat="server" id="errMessage" class="errorMsg"></div>
    <div style="display:">
    <input runat="server" type="hidden" id="slenohidden" />
    <input runat="server" type="hidden" id="sLogTypehidden" />
    <input runat="server" type="hidden" id="sCommentshidden" />
    <input runat="server" type="hidden" id="sPageshidden" />            
    
    </div>
    <div id="divEvents" style="overflow:auto;height:300px;" runat="server">
        <asp:GridView ID="agvLogEvents" HorizontalAlign="center" runat="server" 
        Width="95%" AutoGenerateColumns="False" 
        HeaderStyle-CssClass="darkbackground" 
        AlternatingRowStyle-CssClass="dullbackground" 
        RowStyle-CssClass="lightbackground" CellPadding="4" GridLines="Horizontal" 
        OnRowDataBound="agvLogEvents_RowDataBound" 
        OnRowCommand="agvLogEvents_RowCommand">
            <Columns>
                <asp:BoundField DataField="COLUMN1" HeaderText="Job Type" />                
                <asp:BoundField DataField="COLUMN3" HeaderText="Job Ref" />
                <asp:BoundField DataField="COLUMN4" HeaderText="Start Time" />
                <asp:BoundField DataField="COLUMN5" HeaderText="End Time" />
                <asp:BoundField DataField="SNAME" HeaderText="Process" />
                 <asp:TemplateField HeaderText="Pages">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtpages" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "COLUMN6")%>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="test" Width="100px"  Text='<%# DataBinder.Eval(Container.DataItem, "COLUMN7")%>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button id="btnEndLog" name="btnEndLog" CssClass="dpbutton" Text="End" runat="server" 
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LENO")%>' CommandName="EndLogEvent"  />  
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>    
    </div>
    </form>
</body>
</html>

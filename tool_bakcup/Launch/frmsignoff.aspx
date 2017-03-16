<%@ page language="C#" autoeventwireup="true" inherits="frmsignoff, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SignOff Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />    
    <script language="javascript">
        function validation()
        {
            var sdate=document.signofffrm.SDateTxt.value.toUpperCase();
            var edate=document.signofffrm.EDateTxt.value.toUpperCase();
            
            if((sdate.value!="") && (edate.value!=""))
            {
                if((sdate.indexOf("AM")>-1)||(sdate.indexOf("PM")>-1) || (edate.indexOf("AM")>-1)||(edate.indexOf("PM")>-1) )
                {
                    alert("Please remove AM or PM and give 24 hours format");
                    return false;
                }
            }
            
            return true; 
        }
        
        function updatevalidation()
        {
            var sdate=document.signofffrm.USDateTxt.value.toUpperCase();
            var edate=document.signofffrm.UEDateTxt.value.toUpperCase();
            if((sdate.value!="") && (edate.value!=""))
            {
                if((sdate.indexOf("AM")>-1)||(sdate.indexOf("PM")>-1) || (edate.indexOf("AM")>-1)||(edate.indexOf("PM")>-1) )
                {
                    alert("Please remove AM or PM and give 24 hours format");
                    return false;
                }
            }
           
            return true;
        }
        function InsertValidation()
        {
            var sdate=document.signofffrm.USDateTxt.value.toUpperCase();
            var edate=document.signofffrm.UEDateTxt.value.toUpperCase();
            if((sdate.value!="") && (edate.value!=""))
            {
                if((sdate.indexOf("AM")>-1)||(sdate.indexOf("PM")>-1) || (edate.indexOf("AM")>-1)||(edate.indexOf("PM")>-1) )
                {
                    alert("Please remove AM or PM and give 24 hours format");
                    return false;
                }
            }
            sdate=document.signofffrm.ISDateTxt.value.toUpperCase();
            edate=document.signofffrm.IEDateTxt.value.toUpperCase();
            if((sdate.value!="") && (edate.value!=""))
            {
                if((sdate.indexOf("AM")>-1)||(sdate.indexOf("PM")>-1) || (edate.indexOf("AM")>-1)||(edate.indexOf("PM")>-1) )
                {
                    alert("Please remove AM or PM and give 24 hours format");
                    return false;
                }
            }    
            return true;
        }
    </script>
</head>
<body>
    <form id="signofffrm" runat="server" method="post" action="">
    <div class="dptitle" id="invtitle" runat="server" >
        Time Sheet Sign Off
    </div>
        <table width="700px" class="bordertable"  style="margin-bottom:15px" align="center" >
            <tr>
                <td colspan="7">&nbsp;</td>
            </tr>
            <tr>
            
                <td align="right">
                    Employee 
                </td>
                <td >
                    <asp:DropDownList ID="Employeeddlist" DataTextField="COLUMN1" DataValueField="EMPNO" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
                 <td align="right" valign="middle">
                    Start Time
                </td>
                <td>
                    <asp:TextBox ID="SDateTxt" runat="server"></asp:TextBox>&nbsp;
                    <img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=SDateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" />
                </td>
                <td align="right">
                    End Time
                </td>
                <td>
                    <asp:TextBox ID="EDateTxt" runat="server"></asp:TextBox>&nbsp;
                    <img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=EDateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" />
                </td>
                
                
            </tr>
           <tr>
            <td colspan="6" align="center">
                        <asp:Button ID="Submit_Btn" runat="server"  CssClass="dpbutton" Text="Submit" OnClick="Submit_Btn_Click" OnClientClick="return validation();" />&nbsp;
                        <asp:HiddenField ID="LenNoHField" runat="server" />
                </td>
           </tr>
        </table>
        <div id=Errormsg runat="server" class="errorMsg">
        </div>
        
        
        <div runat="server" style="border:1px; overflow:auto; width:100%; height:100%;">
            <div class="divwithborder" id="UpdateDiv"  runat="server" align="left" style="width:400px;">
            <table width="300px" id="UpdateTable" align="left"  >
                <tr>
                    <td align="left" >
                        Start Time
                    </td>
                    <td  colspan=2>
                        <asp:TextBox ID="USDateTxt" runat="server"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                     <td align="left" >
                        End Time
                    </td>
                    <td  >
                        <asp:TextBox ID="UEDateTxt" runat="server"></asp:TextBox>
                    </td>
                    <td  >
                        <asp:Button ID="Update_Btn" CssClass="dpbutton" runat="server" Text="Update" OnClick="Update_Btn_Click" OnClientClick="return updatevalidation();" />
                    </td>
                </tr>
            </table>
            </div>
            
        <br />
       <div class="divwithborder" id="InsertDiv"  runat="server" align="left" style="width:450px;padding-top:15px; ">
            <table style="width:350px" id="InsertTable">
                <tr>
                    <td  align="left">
                        Stage
                    </td>
                    <td  colspan=2>
                        <asp:DropDownList ID="StageListDD" DataTextField="SNAME" DataValueField="SNO" runat="server">
                        </asp:DropDownList>
                    </td>
                     
                   
                </tr>
                <tr>
                    <td align="left" >
                        Start Time
                    </td>
                     <td colspan=2 >
                        <asp:TextBox ID="ISDateTxt" runat="server"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td align="left">
                        End Time
                    </td>
                    <td >
                        <asp:TextBox ID="IEDateTxt" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="InsertBtn" runat="server" CssClass="dpbutton" Text="Insert" OnClick="InsertBtn_Click" OnClientClick="return updatevalidation();"  />
                        <asp:HiddenField ID="JobIdHField" runat="server" />
                    </td>
                </tr>
                
            </table>
       </div>
            <table  > 
            <tr>
                <td align="right">
                    <asp:Button ID="SignOff_Btn" runat="server" Text="Sign Off" OnClick="SignOff_Btn_Click" CssClass="dpbutton" />
                </td>
           </tr>
            <tr>
            <td align="center"  style="padding-top:20px;">
            
            
                <asp:GridView ID="EmployeeDetails" runat="server" Width="800px" RowStyle-Height="23" AutoGenerateColumns="False" CellPadding="2"  GridLines="None"
                   OnRowCommand="EmployeeDetails_RowCommand" HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" BorderColor="Black" BorderWidth="1px" 
                    OnRowDataBound="EmployeeDetails_updatecells"
                     >
                    <Columns>

                        <asp:BoundField DataField="Column1" HeaderText="JobType"   />
                        <asp:BoundField DataField="Column3" HeaderText="Code"  />
                        <asp:BoundField DataField="sname" HeaderText="Stage"  />
                        <asp:BoundField DataField="Column4" HeaderText="Start Date" SortExpression="Column4"  />
                        <asp:BoundField DataField="Column5" HeaderText="End Date"  />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "leno") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <%--<asp:BoundField DataField="leno" >
                            <ItemStyle CssClass="ItemHidden" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="signoffid" HeaderText="min." >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton AlternateText="Edit" name="btnEdit"  id="btnEdit" runat="server" CausesValidation="false"  CommandName="Edit1"
                                 CommandArgument='<%# DataBinder.Eval(Container.DataItem, "leno")%>' ImageUrl="Images\ebut.jpg" ToolTip="Edit"  />  
                            </ItemTemplate>
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" >
                            <ItemTemplate>
                                <asp:ImageButton AlternateText="Split" name="btnSplit" ToolTip="Split"    ImageUrl="Images\split2.jpg" id="btnSplit" AccessKey="S" 
                                 CommandArgument='<%# DataBinder.Eval(Container.DataItem, "leno")%>' CommandName="Split1"  runat="server"  />  
                            </ItemTemplate>
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                       
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="lightbackground" Height="23px"  />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <HeaderStyle CssClass="darkbackground" />
                    <AlternatingRowStyle CssClass="dullbackground" />
                </asp:GridView>
            </td>
           </tr>
           
        </table>
        </div>
    </form>
</body>
</html>

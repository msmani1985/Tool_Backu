<%@ page language="C#" autoeventwireup="true" inherits="LaunchTarFiles, App_Web_znvsjrxn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="scripts/common.js"></script>
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="dptitle">Target File Details:</div>
        <table align = "center">
            <tr>
                <td>
                    <asp:Label ID="lblNewTarDate" runat="server" Text="Target Received on:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNewtarget" runat="server" CssClass="TxtBox" Width="101px" TabIndex = "20"></asp:TextBox>
                    <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtNewtarget','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer;" id="img11" runat="server" />
                        <asp:CheckBox ID="CheckNewYTR" Text="YTR" runat="server"/>
                </td> 
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvTarFileInfo" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
				        CssClass="lightbackground" width="123%">
					    <HeaderStyle CssClass="darkbackground"  />
					    <AlternatingRowStyle BackColor="#F2F2F2" />
					    <Columns>
						    <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
							    <ItemTemplate>
								    <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
								    <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
								    <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
							    </ItemTemplate>
							    <HeaderStyle Width="40px" />
						    </asp:TemplateField>
						    <asp:TemplateField SortExpression="TaskName" HeaderText="TaskName"  >
							    <ItemTemplate>
								    <asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
								    <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
							    </ItemTemplate>
							    <HeaderStyle Width="80px" />
						    </asp:TemplateField>
					        <asp:TemplateField SortExpression="Lang_name" HeaderText="Language Name" >
							    <ItemTemplate>
								    <asp:Label ID="lblLang_Name" runat="server" Text='<%# Eval("Lang_name") %>'></asp:Label>
								    <asp:HiddenField ID="hid_Lang_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
							    </ItemTemplate>
							    <HeaderStyle Width="130px" />
						    </asp:TemplateField>
						    <asp:TemplateField SortExpression="Soft_Name"  HeaderText="Software Name" >
							    <ItemTemplate>
								    <asp:Label ID="lblgvSoft_Name" runat="server" Text='<%# Eval("Soft_Name") %>'></asp:Label>
								    <asp:HiddenField ID="hid_Soft_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Soft_ID") %>' />
							    </ItemTemplate>
							    <HeaderStyle Width="90px" />
						    </asp:TemplateField>
						    <asp:TemplateField SortExpression="Files" HeaderText="No.of Files" >
							    <ItemTemplate>
								    <asp:TextBox ID="lblgvFiles" Width="50" runat="server" Text='<%# Eval("TarFiles") %>'></asp:TextBox>
							    </ItemTemplate>
							    <HeaderStyle Width="50px" />
						    </asp:TemplateField>
						    <asp:TemplateField HeaderText="Edit">
							    <ItemTemplate>
							        <asp:LinkButton ID="lnkEdit" runat="server" Text = "Click" OnClick = "TarEdit"></asp:LinkButton>
						        </ItemTemplate>
									    <HeaderStyle Width="50px" />
						    </asp:TemplateField>
					    </Columns>
				    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnFPSave" runat="server" Text="Update" OnClick = "Update_Click"  OnClientClick = "return Hidepopup()" CssClass="dpbutton"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

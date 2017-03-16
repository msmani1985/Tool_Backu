<%@ page language="C#" autoeventwireup="true" inherits="projecthistory, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="TitleDiv" class="dptitle" runat="server">
        Project History
    </div>
    
    <div id="notallocatediv" runat="server" align="center"  >
        <table width="95%" cellpadding="0" cellspacing="0" border="0" >
            <tr>
                <td align="left" width="100px" >
                    <asp:Button ID="notallocatebtn" runat="server" Text="Not Allocated" OnClick="notallocatebtn_Click" CssClass="winTABButton" />
                </td>
                <td align="left" style="width: 40px" >
                    <asp:Button ID="liveprojectBtn" runat="server" Text="Live" OnClick="liveprojectBtn_Click" CssClass="winTABButton" />
                </td>
                <td align="left"  width="60px" >
                    <asp:Button ID="completedprojectBtn" runat="server" Text="Completed" OnClick="completedprojectBtn_Click" CssClass="winTABButton" />
                </td>
                <td width="80%">&nbsp;</td>
                <td align="right" width="25px">
                    <asp:ImageButton ID="ExeclexportImgBtn" runat="server" AlternateText="Excel Export"  ImageUrl="~/images/Excel.jpg" OnClick="ExeclexportImgBtn_Click" />
                </td>
            </tr>
            
            <tr>
            
                <td align="left"  colspan="5" >
                    <asp:DataGrid ID="ProjecthistoryDataGrid" runat="server"  
                        AutoGenerateColumns="False"  
                        HeaderStyle-CssClass="darkbackground" SelectedItemStyle-CssClass="editbackground" AlternatingItemStyle-CssClass="dullbackground"
                        CssClass="lightbackground"  >
                    <Columns>
                        <asp:TemplateColumn HeaderText="Project">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "project_name")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Module Name">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "module_name")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Bug Name">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "bug_name")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Descriptions">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"comments") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Owner Name">    
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"Ownername") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Delegate To">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "dname")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"status") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Receive Date">
                            <ItemTemplate> 
                                <%# DataBinder.Eval(Container.DataItem,"project_receive_date") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Start Date">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"startdate") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Due Date">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"duedate") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="End Date">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem,"enddate") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        
                    </Columns>    
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
    <div id="Errordiv" runat="server" class="errorMsg" ></div>
    </form>
</body>
</html>

 
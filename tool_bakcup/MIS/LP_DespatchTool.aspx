<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LP_DespatchTool.aspx.cs" Inherits="LP_DespatchTool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" language="javascript">
         function CheckAll(Checkbox) {
             var grvShift = document.getElementById("<%=grdJobDes.ClientID %>");
            for (i = 1; i < grvShift.rows.length; i++) {
                grvShift.rows[i].cells[9].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
         }
    </script>
    <style type="text/css">
        .auto-style1 {
            background: rgb(180,221,180);
/* Old browsers */background: -moz-linear-gradient(top, rgba(180,221,180,1) 0%, rgba(131,199,131,1) 1%, rgba(0,138,0,1) 57%, rgba(0,112,20,1) 100%); /* FF3.6+ */;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(180,221,180,1)), color-stop(1%,rgba(131,199,131,1)), color-stop(57%,rgba(0,138,0,1)), color-stop(100%,rgba(0,112,20,1))); /* Chrome,Safari4+ */;
            background: -webkit-linear-gradient(top, rgba(180,221,180,1) 0%,rgba(131,199,131,1) 1%,rgba(0,138,0,1) 57%,rgba(0,112,20,1) 100%); /* Chrome10+,Safari5.1+ */;
            background: -o-linear-gradient(top, rgba(180,221,180,1) 0%,rgba(131,199,131,1) 1%,rgba(0,138,0,1) 57%,rgba(0,112,20,1) 100%); /* Opera 11.10+ */;
            background: -ms-linear-gradient(top, rgba(180,221,180,1) 0%,rgba(131,199,131,1) 1%,rgba(0,138,0,1) 57%,rgba(0,112,20,1) 100%); /* IE10+ */;
            background: rgb(180,221,180); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b4ddb4', endColorstr='#007014',GradientType=0 ); /* IE6-9 */;
            color: White;
            text-align: center;
            height: 26px;
            font-size: 12px;
            font-family: Segoe UI;
            font-weight: bold;
        }
        </style>
</head>
<body class="LightBackGound" style="background-repeat:no-repeat;">
    <form id="form1" runat="server">
    <div>
        <table align="center" cellpadding="0" cellspacing="0" style="border:1px solid green;align:center;">
            <tr>
                <td align="center" class="auto-style1" colspan="8" valign="middle">
                    <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                        Font-Size="13px" Text="Despatch Tool"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <strong>Job ID :</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtJobID" runat="server" Height="16px" Width="161px"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:RadioButtonList ID="rbLaunch" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Launch</asp:ListItem>
                        <asp:ListItem Value="2">Non Launch</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="8" align="center">
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" 
                        onclick="btnSearch_Click" Text="Search" ToolTip="Search" Width="70px" />
                    <asp:Button ID="btnClear" runat="server" CssClass="dpbutton" 
                        onclick="btnClear_Click" Text="Clear" ToolTip="Clear" Width="70px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="8">
                    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                        Font-Size="11px" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table align="center">
            <tr>
                <td>
                    <asp:GridView ID="grdJobDes" AllowSorting="True" width="100%" runat="server" AutoGenerateColumns="False"
				        AllowPaging="False" DataKeyField="Jobid"  CssClass="lightbackground" >
				        <FooterStyle BackColor="Transparent"></FooterStyle>
				        <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground"></HeaderStyle>
				        <Columns>
					        <asp:TemplateField HeaderText="JobID">
						        <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "JobID") %>
                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("Pro_ID") %>'  Visible="false"></asp:Label>
                                    <asp:Label ID="lblFP_ID" runat="server" Text='<%# Eval("FP_ID") %>'  Visible="false"></asp:Label>
						        </ItemTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="Project Name">
						        <ItemTemplate>
							        <%# DataBinder.Eval(Container.DataItem, "ProjectName") %>
						        </ItemTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="Customer">
					            <ItemTemplate>
					                <%# DataBinder.Eval(Container.DataItem,"Cust_name") %>
					            </ItemTemplate>
					        </asp:TemplateField>
                            <asp:TemplateField HeaderText="FileName">
					            <ItemTemplate>
					                <%# DataBinder.Eval(Container.DataItem,"Files_name") %>
					            </ItemTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="PE Name" >
						        <ItemTemplate>
							        <%# DataBinder.Eval(Container.DataItem, "ProjectEditor")%>
						        </ItemTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="Task">
						        <ItemTemplate>
							        <%# DataBinder.Eval(Container.DataItem, "TASKNAME") %>
						        </ItemTemplate>
					        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Software">
						        <ItemTemplate>
							        <%# DataBinder.Eval(Container.DataItem, "SOFT_NAME") %>
						        </ItemTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="Pages">
						        <ItemTemplate>
							        <%# DataBinder.Eval(Container.DataItem, "Pages") %>
						        </ItemTemplate>
					        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amends">
						        <ItemTemplate>
							        <%# DataBinder.Eval(Container.DataItem, "Amends") %>
						        </ItemTemplate>
					        </asp:TemplateField>
                            <asp:TemplateField >
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Check" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
				        </Columns>
			        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" CssClass="dpbutton" 
                     Text="Save" ToolTip="Save" Width="70px" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
            <script type="text/javascript" src="Scripts/jquery.min.js"></script>
            <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
            <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
            <script type="text/javascript">
                $(document).ready(function () {
                    gridviewScroll();
                });

                function gridviewScroll() {
                    $('#<%=grdJobDes.ClientID%>').gridviewScroll({
                            width: window.innerWidth - 50,
                            height: window.innerHeight - 300,
                            startHorizontal: 0,
                            barhovercolor: "#848484",
                            barcolor: "#848484"
                        });
                    }
            </script>
    </div>
    </form>
</body>
</html>

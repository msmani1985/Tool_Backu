<%@ page language="C#" autoeventwireup="true" inherits="JobAllocationLaunch, App_Web_vlobbbje" %>
<meta http-equiv="refresh" content="180">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
     <%--<script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=grdEmplyeeTask.ClientID %>').Scrollable({
            ScrollHeight: 50
        });
    });
    </script>--%>
    <script type = "text/javascript">
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block(
                    {
                        message: '<table align = "center"><tr><td>' +
                                '<img src="images/loadingAnim.gif"/></td></tr></table>',
                        css: {},
                        overlayCSS:
                            {
                                backgroundColor: '#000000', opacity: 0.6
                            }
                    }
                );
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        }
        function Hidepopup() {
            $find("popup").hide();
            return false;
        }
        function Hidepopup1() {
            $find("popup1").hide();
            return false;
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="dptitle" id="divTitle" align="left" runat="server">Job Allocation</div>
                        <ol id="toc">
                            <li id="LstGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral" runat="server" TabIndex="1" CommandName="General"  OnClick="lnkGeneral_Click" >General</asp:LinkButton></li>
                            <li id="LstTE" runat="server" visible="false">
                                <asp:LinkButton ID="lnkTE" runat="server" TabIndex="2" CommandName="TE" OnClick="lnkTE_Click">TE</asp:LinkButton></li>
                            <li id="LstDTP" runat="server" visible="false">
                                <asp:LinkButton ID="lnkDTP" runat="server" TabIndex="3" CommandName="DTP" OnClick="lnkDTP_Click">DTP</asp:LinkButton></li>
                            <li id="LstPreDTP" runat="server" visible="false">
                                <asp:LinkButton ID="lnkPreDTP" runat="server" TabIndex="4" CommandName="PreDTP" OnClick="lnkPreDTP_Click">PreDTP</asp:LinkButton></li>
                            <li id="LstDQA" runat="server" visible="false">
                                <asp:LinkButton ID="lnkDQA" runat="server" TabIndex="5" CommandName="DQA" OnClick="lnkDQA_Click">DQA</asp:LinkButton></li>
                            <li id="LstOVA" runat="server" visible="false">
                                <asp:LinkButton ID="lnkOVA" runat="server" TabIndex="6" CommandName="OVA" OnClick="lnkOVA_Click">OVA</asp:LinkButton></li>
                            <li id="LstQC" runat="server">
                                <asp:LinkButton ID="lnkQC" runat="server" TabIndex="7" CommandName="QC" OnClick="lnkQC_Click">QC</asp:LinkButton></li>
                             <li id="LstQCcorr" runat="server">
                                <asp:LinkButton ID="lnkQCcorr" runat="server" TabIndex="8" CommandName="QC Correction" OnClick="lnkQCcorr_Click">QC Correction</asp:LinkButton></li>
                            <li id="LstQA" runat="server">
                                <asp:LinkButton ID="lnkQA" runat="server" TabIndex="9" CommandName="QA" OnClick="lnkQA_Click">QA</asp:LinkButton></li>
                            <li id="LstQAcorr" runat="server">
                                <asp:LinkButton ID="lnkQAcorr" runat="server" TabIndex="10" CommandName="QA Correction" OnClick="lnkQAcorr_Click">QA Correction</asp:LinkButton></li>
                            <li id="LstRFD" runat="server">
                                <asp:LinkButton ID="lnkRFD" runat="server" TabIndex="11" CommandName="RFD" OnClick="lnkRFD_Click">RFD</asp:LinkButton></li>
                        </ol>
        <table>
            <tr>
                <td colspan="2"></td>
                <td align="center">
                    
                </td>
            </tr>
            <tr>
                <td >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <ContentTemplate>
                        <asp:GridView ID="gv_job_allocation_Employee"  
                            CaptionAlign="Left" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" 
                            ForeColor="#333333" BorderStyle="Solid"  
                            GridLines="Vertical" AllowSorting="True" Width="100%" 
                             Font-Names="Segoe UI" Font-Size="11px"
                             OnSorting="gv_job_allocation_Employee_Sorting" OnRowDataBound="gv_job_allocation_Employee_RowDataBound">
                            <HeaderStyle CssClass="header" />
                            <rowstyle backcolor="white"/>
                            <alternatingrowstyle backcolor="#F0FFF0"/>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("slno") %>' Width="30px"></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text = "Click" OnClick = "Edit"></asp:LinkButton>
                                        <asp:HiddenField id="hf_Job_ID" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="hf_FP_ID" Value='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="hf_Amend_ID" Value='<%# DataBinder.Eval(Container.DataItem,"Amend_ID") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="job_his_id" Value='<%# DataBinder.Eval(Container.DataItem,"job_his_id") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="hf_Pages" Value='<%# DataBinder.Eval(Container.DataItem,"Pages") %>' runat="server">
                                        </asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="jobID" HeaderText="Job ID" SortExpression="jobID">
                                </asp:BoundField>
                                <asp:BoundField DataField="ProjectNAME" HeaderText="Name" SortExpression="ProjectNAME">
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:DropDownList OnSelectedIndexChanged="dd_custname_OnSelectedIndexChanged" AutoPostBack="true" 
                                            ID="dd_custname" runat="server" Width="100" ></asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"custname") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:DropDownList OnSelectedIndexChanged="dd_TASKNAME_OnSelectedIndexChanged" AutoPostBack="true" ID="dd_TASKNAME" runat="server" ></asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"TASKNAME") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TASKNAME" HeaderText="TASK" 
                                    SortExpression="TASKNAME" Visible="false">
                                </asp:BoundField>
                                <asp:BoundField DataField="Lang_NAME" HeaderText="Language" 
                                    SortExpression="Lang_NAME">
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:DropDownList OnSelectedIndexChanged="dd_SoftName_OnSelectedIndexChanged" AutoPostBack="true" 
                                            ID="dd_SoftName" runat="server" ></asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Soft_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Soft_Name" HeaderText="Software" 
                                    SortExpression="Soft_Name">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Files_NAME" HeaderText="File Name" 
                                    SortExpression="Files_NAME">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Pages">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPages" runat="server" Text ='<%# Eval("Pages") %>' OnClick = "Pages_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Pages" HeaderText="Pages" 
                                    SortExpression="Pages">
                                    <ItemStyle Width="40px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="AmendName" HeaderText="Stage" 
                                    SortExpression="AmendName">
                                </asp:BoundField>
                                <asp:BoundField DataField="due_dateFrom" HeaderText="Due Date" 
                                    SortExpression="due_dateFrom">
                                </asp:BoundField>
                                 <asp:BoundField DataField="Cur_Workflow" HeaderText="Cur. Workflow" 
                                    SortExpression="Cur_Workflow">
                                </asp:BoundField>
                                <asp:BoundField DataField="username" HeaderText="Cur. Employee" 
                                    SortExpression="username">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox_MoveTask"  runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>

                         <asp:Button ID="btnAdd1" runat="server" Text="Add" Visible="false" />
                         <asp:Panel ID="pnlAddEdit1" Width="500" Height="250" runat="server" CssClass="modalPopup" style = "display:none">
                         <table  align = "center" cellpadding="1" cellspacing="1" border="1" width="475">
                             <tr>
                                 <td>
                                     <asp:GridView ID="gvAllocatedEmp" HorizontalAlign="Center" runat="server" Width="95%" 
                                        AutoGenerateColumns="False" HeaderStyle-CssClass="darkbackground" AlternatingRowStyle-CssClass="dullbackground"
                                        RowStyle-CssClass="lightbackground" CellPadding="4" EmptyDataText="No Data Found.."
                                          OnRowCommand="gvAllocatedEmp_RowCommand"  AllowPaging="true" PageSize="6"
                                         OnPageIndexChanging="gvAllocatedEmp_PageIndexChanging">
                                         <AlternatingRowStyle CssClass="dullbackground"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:BoundField DataField="slno" HeaderText="Sl.No" />
                                            <asp:BoundField DataField="Pages" HeaderText="Assigned Employee's" />
                                             <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="BtnRemove" ToolTip="Reject" AlternateText="Remove" ImageUrl="~/images/tools/no.png" 
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AEID") %>' CommandName="Remove"
                                                    runat="server"/>
                                                    <asp:Label ID="lblENO" runat="server" Text='<%# Eval("ENO") %>' Visible="false" Width="50px"></asp:Label>
                                                    <asp:Label ID="lblFP_ID" runat="server" Text='<%# Eval("FP_ID") %>' Visible="false" Width="50px"></asp:Label>
                                                    <asp:Label ID="lblJob_His_ID" runat="server" Text='<%# Eval("Job_His_ID") %>' Visible="false" Width="50px"></asp:Label>
                                                    <asp:Label ID="lblLP_ID" runat="server" Text='<%# Eval("LP_ID") %>' Visible="false" Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                     </asp:GridView>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="center">
                                     <asp:Button ID="Button1" runat="server" Text="Close" OnClientClick = "return Hidepopup1()" CssClass="dpbutton"/>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:Label ID="PJobID" runat="server" Visible="false"></asp:Label>
                                     <asp:Label ID="PFP_ID" runat="server" Visible="false"></asp:Label>
                                     <asp:Label ID="PJob_His_ID" runat="server" Visible="false"></asp:Label>
                                 </td>
                             </tr>
                         </table>
                         </asp:Panel>
                            <asp:LinkButton ID="lnkFake1" runat="server"></asp:LinkButton>
                            <asp:ModalPopupExtender ID="popup1" runat="server" DropShadow="false"
                            PopupControlID="pnlAddEdit1" TargetControlID = "lnkFake1"
                            BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                         <asp:Button ID="btnAdd" runat="server" Text="Add" Visible="false" />
                         <asp:Panel ID="pnlAddEdit" Width="700" Height="500" runat="server" CssClass="modalPopup" style = "display:none">
                         <table  align = "center" cellpadding="1" cellspacing="1" border="1" width="650">
                             <tr>
                                 <td>
                                     <b>JobID : </b>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblJobID" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <b>ProjectName : </b>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblProjectName" runat="server" ></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <b>FileName : </b>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblFileName" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <b>Task : </b>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblTask" runat="server"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="4">
                                     <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                     <asp:Label ID="lblFP_ID" runat="server" Visible="false"></asp:Label>
                                     <asp:Label ID="lblJob_His_ID" runat="server" Visible="false"></asp:Label>
                                     <asp:Label ID="lblPages" runat="server" Visible="false"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="center" colspan="4">
                                        <asp:Label ID="lbl_Emp" runat="server" Text="Team" Font-Bold = "True" Font-Size="12px" 
                                            ForeColor ="Green" Font-Name="Segoe UI"></asp:Label>
                                        <asp:DropDownList ID="ddl_Team" runat="server" AutoPostBack="true" 
                                            Font-Names="Segoe UI" Font-Size="11px" OnSelectedIndexChanged="ddl_Team_SelectedIndexChanged">
                                            <asp:ListItem Text ="--All--" Value = "0" Selected ="True" ></asp:ListItem>
                                            <asp:ListItem Text ="Indesign(CMB)" Value = "100"></asp:ListItem>
                                            <asp:ListItem Text ="QC(CMB)" Value = "101" ></asp:ListItem>
                                            <asp:ListItem Text ="Word(CMB)" Value = "102" ></asp:ListItem>
                                            <asp:ListItem Text ="Artwork(CMB)" Value = "103"></asp:ListItem>
                                        </asp:DropDownList>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="3" align="center">
                                     <asp:GridView ID="grdEmplyeeTask"  CaptionAlign="Left" runat="server" DataKeyNames="EMPLOYEE_ID"
                                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderStyle="Solid"  
                                        GridLines="Vertical" AllowSorting="True" AllowPaging="true" PageSize="9"  OnRowDataBound="grdEmplyeeTask_RowDataBound" 
                                         OnPageIndexChanging="grdEmplyeeTask_PageIndexChanging"  Font-Names="Segoe UI" Font-Size="11px" >
                                        <HeaderStyle CssClass="GVFixedHeader"  />
                                        <rowstyle backcolor="white"/>
                                        <alternatingrowstyle backcolor="#F0FFF0"/>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("ROWID") %>' Width="20px"></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                                <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMPLOYEE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEMPLOYEE" runat="server" Text='<%# Eval("EMPLOYEE") %>' ></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WorkFlow">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTask" Width="90px" runat="server">
                                                        <asp:ListItem Text ="Process" Value = "1" Selected ="True" ></asp:ListItem>
                                                            <asp:ListItem Text ="QC" Value = "2"></asp:ListItem>
                                                            <asp:ListItem Text ="QC Corr" Value = "3" ></asp:ListItem>
                                                            <asp:ListItem Text ="QA" Value = "4" ></asp:ListItem>
                                                            <asp:ListItem Text ="QA Corr" Value = "5"></asp:ListItem>
                                                            <asp:ListItem Text ="Final Package" Value = "6"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Full File">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chk_Full" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Page From">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPageFrom" runat="server" Width="25px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Page To">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPageTo" runat="server" Width="25px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOrder" onKeyPress="return onKeyPress(event)" Text='<%# DataBinder.Eval(Container.DataItem,"JOB_ORDER") %>' runat="server" Width="20px" MaxLength="2">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allocation">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chk_Emp" runat="server" />
                                                   <%-- <asp:HiddenField ID="hiddenMoveTask" runat="server" 
                                                        Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />
                                                    <asp:HiddenField ID="hf_job_type_id" runat="server" 
                                                        Value='<%# DataBinder.Eval(Container.DataItem,"job_type_id") %>' />
                                                    <asp:HiddenField ID="hf_job_AEID" runat="server" 
                                                        Value='<%# DataBinder.Eval(Container.DataItem,"AEID") %>' />--%>
                                                    <asp:HiddenField ID="hf_emp_id" runat="server" 
                                                        Value='<%# DataBinder.Eval(Container.DataItem,"EMPLOYEE_ID") %>' />
                                                   <%-- <asp:HiddenField ID="hf_task_id" runat="server" 
                                                        Value='<%# DataBinder.Eval(Container.DataItem,"TASK_ID") %>' />--%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                     </td>
                                    <td align="center">
                                     <asp:GridView ID="gvEmpAllocated"  CaptionAlign="Left" runat="server"
                                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderStyle="Solid"  
                                        GridLines="Vertical" AllowSorting="True" AllowPaging="true" PageSize="12" EmptyDataText="No Data Found.."
                                          Font-Names="Segoe UI" Font-Size="11px" OnPageIndexChanging="gvEmpAllocated_PageIndexChanging"  >
                                         <HeaderStyle CssClass="GVFixedHeader"  />
                                        <rowstyle backcolor="white"/>
                                        <alternatingrowstyle backcolor="#F0FFF0"/>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("slno") %>' Width="20px"></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                                <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assigned Pages">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPages" runat="server" Text='<%# Eval("Page") %>' Width="100px"></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>
                                     </asp:GridView>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="4" align="center">
                                     <asp:Button ID="btn_Move" CssClass="dpbutton" runat="server" Text="Assign"  
                                            ToolTip="Assign"  Width="90px" OnClick="btn_Move_Click"/>
                                     <asp:Button ID="btnCancel" runat="server" Text="Close" OnClientClick = "return Hidepopup()" CssClass="dpbutton"/>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="4" align="center">
                                     <span style="font-size: 9pt; color: #ff0000">
                                        <asp:Label ID="lblresultEmp" runat="server" Text=""></asp:Label>
                                     </span>
                                 </td>
                             </tr>
                         </table>
                         </asp:Panel>
                            <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                            <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                            PopupControlID="pnlAddEdit" TargetControlID = "lnkFake"
                            BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>

                         <asp:GridView ID="gv_job_all_Emp_RFD"  
                            CaptionAlign="Left" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" 
                            ForeColor="#333333" BorderStyle="Solid"  
                            GridLines="Vertical" AllowSorting="True" Width="100%"  OnRowCommand="gv_job_all_Emp_RFD_RowCommand"  
                             Font-Names="Segoe UI" Font-Size="11px" OnRowDataBound="gv_job_all_Emp_RFD_RowDataBound" Visible="false">
                            <HeaderStyle CssClass="header" />
                            <rowstyle backcolor="white"/>
                            <alternatingrowstyle backcolor="#F0FFF0"/>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("slno") %>'></asp:Label>
                                        <br />
                                        <asp:HiddenField id="hf_Job_ID" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="hf_FP_ID" Value='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="hf_Amend_ID" Value='<%# DataBinder.Eval(Container.DataItem,"Amend_ID") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="job_his_id" Value='<%# DataBinder.Eval(Container.DataItem,"job_his_id") %>' runat="server">
                                        </asp:HiddenField>
                                        <asp:HiddenField id="hf_Pages" Value='<%# DataBinder.Eval(Container.DataItem,"Pages") %>' runat="server">
                                        </asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="jobID" HeaderText="Job ID" SortExpression="jobID">
                                </asp:BoundField>
                                <asp:BoundField DataField="ProjectNAME" HeaderText="Name" SortExpression="ProjectNAME">
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:DropDownList OnSelectedIndexChanged="dd_custname_OnSelectedIndexChanged" AutoPostBack="true" 
                                            ID="dd_custname" runat="server" ></asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"custname") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:DropDownList OnSelectedIndexChanged="dd_TASKNAME_OnSelectedIndexChanged" AutoPostBack="true" ID="dd_TASKNAME" runat="server" ></asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"TASKNAME") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TASKNAME" HeaderText="TASK" 
                                    SortExpression="TASKNAME" Visible="false">
                                </asp:BoundField>
                                <asp:BoundField DataField="Lang_NAME" HeaderText="Language" 
                                    SortExpression="Lang_NAME">
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:DropDownList OnSelectedIndexChanged="dd_SoftName_OnSelectedIndexChanged" AutoPostBack="true" 
                                            ID="dd_SoftName" runat="server" ></asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Soft_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Files_NAME" HeaderText="File Name" 
                                    SortExpression="Files_NAME">
                                </asp:BoundField>
                                <asp:BoundField DataField="Pages" HeaderText="Pages" 
                                    SortExpression="Pages">
                                </asp:BoundField>
                                <asp:BoundField DataField="AmendName" HeaderText="Stage" 
                                    SortExpression="AmendName">
                                </asp:BoundField>
                                <asp:BoundField DataField="due_dateFrom" HeaderText="Due Date" 
                                    SortExpression="due_dateFrom">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDel" runat="server" Text="Delivery" CssClass="dpbutton"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' CommandName="Delivery" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>

                     </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID = "gv_job_allocation_Employee" />
                            <asp:AsyncPostBackTrigger ControlID = "btn_Move" />
                        </Triggers> 
                    </asp:UpdatePanel> 
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    <div>
    
    </div>    
        <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
        <script type="text/javascript" src="Scripts/jquery.min.js"></script>
        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
        <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        gridviewScroll();
        gridviewScroll1();
    });

    function gridviewScroll() {
        $('#<%=gv_job_allocation_Employee.ClientID%>').gridviewScroll({
            width: 1185,
            height: 445,
            startHorizontal: 0,
            barhovercolor: "#848484",
            barcolor: "#848484"
        });
    }
    function gridviewScroll1() {
        $('#<%=gv_job_all_Emp_RFD.ClientID%>').gridviewScroll({
            width: 1185,
            height: 445,
            startHorizontal: 0,
            barhovercolor: "#848484",
            barcolor: "#848484"
        });
    }
</script>
    </form>
</body>
</html>

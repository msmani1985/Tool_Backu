<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LP_InvoiceCorrection.aspx.cs" Inherits="LP_InvoiceCorrection"  EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/common.js"></script>
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function CheckAll(Checkbox) {
            var grvShift = document.getElementById("<%=gvIBM.ClientID %>");
            for (i = 1; i < grvShift.rows.length; i++) {
                grvShift.rows[i].cells[8].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function CheckAll1(Checkbox) {
            var grvShift = document.getElementById("<%=gvIBMRTE.ClientID %>");
            for (i = 1; i < grvShift.rows.length; i++) {
                grvShift.rows[i].cells[8].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
 <script type="text/javascript">
     var gvelem;
     var gvcolor;
     function setColor(element, val, val1) {
         //alert(gvelem);
         if (gvelem != null) {//alert(gvelem.style.backgroundColor);
             gvelem.style.backgroundColor = gvcolor;
         }
         gvelem = element;
         gvcolor = element.style.backgroundColor;
         element.style.backgroundColor = '#C2C2C2';
         document.form1.hfP_ID.value = val;
         document.form1.hfP_Name.value = val1

     }
     function setMouseOverColor(element) {
         gvcolor = element.style.backgroundColor;
         element.style.backgroundColor = '#C2C2C2';
         element.style.cursor = 'hand';
         element.style.textDecoration = 'underline';
     }
     function setMouseOutColor(element) {
         element.style.backgroundColor = gvcolor;
         element.style.textDecoration = 'none';
     }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 505px;
        }
        .auto-style2 {
            width: 178px;
        }
        .auto-style4 {
            width: 125px;
        }
        .auto-style5 {
            width: 210px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dptitle" id="invtitle" runat="server" >Invoice Correction</div>
        <div>
            <table>
                <tr>    
                    <td  style="background-image: url(images/green-noise-background.png); width: 850px">
                        <ol id="toc">
                            <li id="miGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral"  runat="server" TabIndex = "1" OnClick="lnkGeneral_Click" Text="General" /></li>
                            <li id="miJobInfo" runat="server">
                                <asp:LinkButton ID="lnkJobInfo" TabIndex = "2" runat="server" Text="Invoice Correction" OnClick="lnkJobInfo_Click"/></li>
                            <li id="miIBM" runat="server">
                                <asp:LinkButton ID="lnkIBM" TabIndex = "2" runat="server" Text="IBM Jobs" OnClick="lnkIBM_Click"/></li>
                            <li id="miIBMRTE" runat="server">
                                <asp:LinkButton ID="lnkIBMRTE" TabIndex = "2" runat="server" Text="IBM-RTE Jobs" OnClick="lnkIBMRTE_Click"/></li>
                        </ol>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content" id="tabGeneral" runat="server">
            <table id="Table5">
                <tr class="dpJobGreenHeader">
                    <td  style="background-image: url(images/green-noise-background.png);">
                        <img id="Img8" src="images/tools/information.png" runat="server" />
                        <asp:Label ID="lblProjectSummary" runat="server" Text="Search Summary"></asp:Label>
                        <asp:HiddenField ID="hfP_ID" runat="server"/>
                        <asp:HiddenField ID="hfP_Name" runat="server" />
                    </td>     
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GvNL" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                            CssClass="lightbackground" OnRowDataBound="GvNL_RowDataBound" Width="891px" DataKeyNames="LP_ID">
                            <HeaderStyle CssClass="darkbackground" Height="30px"  />
                            <AlternatingRowStyle BackColor="#F2F2F2" />
                            <Columns>
                                <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                    <ItemTemplate>
                                            <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                            <br />
                                    <asp:HiddenField ID="hfgvNLID" runat="server" Value='<%# Eval("LP_ID") %>' />
                                    <asp:HiddenField ID="hfgvProjectname" runat="server" Value='<%# Eval("projectname") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="jobid" HeaderText="JOBID"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobid" runat="server" Text='<%# Eval("Jobid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="cust_name" HeaderText="Customer"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="title" HeaderText="Project Title" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="received_date"  HeaderText="Rec. Date" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("CREATED_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ProjectEditor" HeaderText="ProjectEditor" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPProjectEditor" runat="server" Text='<%# Eval("ProjectEditor") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Pages" HeaderText="Pages" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPPages" runat="server" Text='<%# Eval("Pages_Count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Task" HeaderText="Task" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPTask" runat="server" Text='<%# Eval("Task") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkGrpInv"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
                $('#<%=GvNL.ClientID%>').gridviewScroll({
                    width: window.innerWidth - 10,
                    height: window.innerHeight - 120,
                    startHorizontal: 0,
                    barhovercolor: "#848484",
                    barcolor: "#848484"
                });
            }
            </script>
        </div>
        <div class="content" id="tabFinalQuote" runat="server">
            <table  width="1000px">
                <tr  class="dpJobGreenHeader">
                    <td  colspan="2">
                        <img class="dpJobGreenHeader" id="Img17" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
                        <asp:Label ID="Label25" runat="server" Text="Project Cost"></asp:Label>
                    </td>
                    <td>
                        <asp:ImageButton ImageUrl="images/tools/j_save.png" runat="server" ID="ImageButton3"  ToolTip="Save" OnClick="imgbtnFinalQuoteSave_Click" />
                    </td>
                </tr>
            </table>
            <table width="1000px">
                <tr>
                    <td>
                        JobID:
                    </td>
                    <td>
                        <asp:TextBox ID="lblPJobID" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        Project Name:
                    </td>
                    <td>
                        <asp:TextBox ID="lblPcode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Customer:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpPCustomer" runat="server"   Width="200px"></asp:DropDownList>
                    </td>
                    <td>
                        Financial Site:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpPFinSite" runat="server"   Width="200px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        PE Name:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpPPEName" runat="server"   Width="200px"></asp:DropDownList>
                    </td>
                    <td >
                        PO Number:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPPONumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        No.of Pages:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPPages" runat="server"></asp:TextBox>
                    </td>
                    <%--<td>
                        Project Cost:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPCost" runat="server"></asp:TextBox>
                    </td>--%>
                </tr>
                <tr>
                    <td>
                        Rec Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRecDate" runat="server"></asp:TextBox>
                            <img alt="Calendar" border="0" height="20" src="images/Calendar.jpg" style="cursor: pointer;" id="img18" runat="server"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPRecDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"/>
                    </td>
                        <td>
                        Completed Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPCompDate" runat="server"></asp:TextBox>
                        <img alt="Calendar" border="0" height="20" src="images/Calendar.jpg" style="cursor: pointer;" id="img19" runat="server"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPCompDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gv_Lmodule" runat="server" AutoGenerateColumns="false" 
                                AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                                HeaderStyle-CssClass="darkbackground" ShowFooter="true" Width="833px" onrowdatabound="gv_Lmodule_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                         <asp:HiddenField ID="hf_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
                                        <asp:TextBox Width="230px" ID="txt_des" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MPTITLe") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_qty" Width="50px" runat="server" Text='<%# Eval("NUMPAGES") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Rate" Width="50px" runat="server" Text='<%# Eval("Rate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Cost" Width="50px" runat="server" Text='<%# Eval("Cost") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price Code">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_pricecode" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PRICECODE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PONumber">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_mponumber" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"MOPONUMBER") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_costtype" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"COSTTYPEID") %>'>
                                            <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_description" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"PAGEDESCRIPTIONID") %>'>
                                            <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PE Contact Name">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_pename" runat="server" Width="200px"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_delete" runat="server"/>
                                    <asp:HiddenField ID="hf_rowmoduleno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"MPROJNO") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="ibtn_Delete" ToolTip="Delete" AlternateText="Delete" runat="server" ImageUrl="~/images/tools/delete.png" />
                                </FooterTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtn_Delete" ToolTip="Delete" AlternateText="Delete" runat="server" ImageUrl="~/images/tools/delete.png" />
                                </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gv_Lmodule1" runat="server" AutoGenerateColumns="false"  Visible="false"
                                AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                                HeaderStyle-CssClass="darkbackground" ShowFooter="true" Width="833px" onrowdatabound="gv_Lmodule1_RowDataBound" OnRowCommand="gv_Lmodule1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:TextBox Width="230px" ID="txt_des" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MPTITLe") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddDesc" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_qty" Width="50px" runat="server" Text='<%# Eval("NUMPAGES") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddQty" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price Code">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_pricecode" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PRICECODE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddPC" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PONumber">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_mponumber" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"MOPONUMBER") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddPO" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_costtype" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"COSTTYPEID") %>'>
                                            <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="Addddl_costtype" runat="server">
                                            <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_description" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"PAGEDESCRIPTIONID") %>'>
                                            <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="Addddl_description" runat="server">
                                            <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PE Contact Name">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_pename" runat="server" Width="200px"></asp:DropDownList>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="Addddl_pename" runat="server" Width="200px"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_delete" runat="server"/>
                                    <asp:HiddenField ID="hf_rowmoduleno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"MPROJNO") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add"  CssClass="dpbutton" CommandName="Add" />
                                </FooterTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtn_Delete" ToolTip="Delete" AlternateText="Delete" OnClick="ibtn_Delete_click" runat="server" ImageUrl="~/images/tools/delete.png" />
                                </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content" id="tabIBM" runat="server">
            <table id="Table5">
                <tr class="dpJobGreenHeader">
                    <td  style="background-image: url(images/green-noise-background.png);" colspan="3">
                        <img id="Img1" src="images/tools/information.png" runat="server" />
                        <asp:Label ID="Label1" runat="server" Text="IBM Jobs"></asp:Label>
                    </td> 
                    <td align="center">
                        <asp:ImageButton ImageUrl="images/tools/j_save.png" runat="server" ID="ImageButton1"  ToolTip="Save" OnClick="btnIBMSave_Click" />
                    </td>    
                </tr>
                <tr>
                    <td class="auto-style4">
                        Project Name:
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtIBMProName" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4">
                        PE Name:
                    </td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="drpIBMPEName" Width="200px" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        Customer:
                    </td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="drpIBMCustomer" runat="server"  Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpIBMCustomer_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        Financial Site:
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="drpIBMFinSite" runat="server" Width="200px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        Rec Date:
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtIBMRecDate" runat="server"></asp:TextBox>
                            <img alt="Calendar" border="0" height="20" src="images/Calendar.jpg" style="cursor: pointer;" id="img3" runat="server"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtIBMRecDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"/>
                    </td>
                        <td class="auto-style2">
                        Completed Date:
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtIBMCompDate" runat="server"></asp:TextBox>
                        <img alt="Calendar" border="0" height="20" src="images/Calendar.jpg" style="cursor: pointer;" id="img4" runat="server"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtIBMCompDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvIBM" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                            CssClass="lightbackground" Width="843px">
                            <HeaderStyle CssClass="darkbackground" Height="30px"  />
                            <AlternatingRowStyle BackColor="#F2F2F2" />
                            <Columns>
                                <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                        <br />
                                        <asp:HiddenField ID="hfgvNLID" runat="server" Value='<%# Eval("NL_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="jobid" HeaderText="JOBID"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobid" runat="server" Text='<%# Eval("Jobid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="cust_name" HeaderText="Customer"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="title" HeaderText="Project Title" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="received_date"  HeaderText="Rec. Date" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("CREATED_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ProjectEditor" HeaderText="ProjectEditor" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPProjectEditor" runat="server" Text='<%# Eval("ProjectEditor") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Pages" HeaderText="Pages" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPPages" runat="server" Text='<%# Eval("Pages_Count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Task" HeaderText="Task" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPTask" runat="server" Text='<%# Eval("Task") %>'></asp:Label>
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
                <//tr>
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
                        $('#<%=gvIBM.ClientID%>').gridviewScroll({
                            width: window.innerWidth - 10,
                            height: window.innerHeight - 200,
                            startHorizontal: 0,
                            barhovercolor: "#848484",
                            barcolor: "#848484"
                        });
                    }
					$(document).ready(function () {
                        gridviewScroll1();
                    });

                    function gridviewScroll1() {
                        $('#<%=gvIBMRTE.ClientID%>').gridviewScroll({
                            width: window.innerWidth - 10,
                            height: window.innerHeight - 200,
                            startHorizontal: 0,
                            barhovercolor: "#848484",
                            barcolor: "#848484"
                        });
                    }
            </script>
        </div>
        <div class="content" id="tabIBMRTE" runat="server">
            <table id="Table5">
                <tr class="dpJobGreenHeader">
                    <td  style="background-image: url(images/green-noise-background.png);" colspan="3">
                        <img id="Img2" src="images/tools/information.png" runat="server" />
                        <asp:Label ID="Label2" runat="server" Text="IBM-RTE Jobs"></asp:Label>
                    </td>    
                    <td align="center">
                        <asp:ImageButton ImageUrl="images/tools/j_save.png" runat="server" ID="imgBtnRTESave"  ToolTip="Save" OnClick="imgBtnRTESave_Click"/>
                    </td> 
                </tr>
                <tr>
                    <td class="auto-style4">
                        Project Name:
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtRTEProName" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4">
                        PE Name:
                    </td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="drpRTEPEName" Width="200px" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        Customer:
                    </td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="drpRTECustomer" runat="server"  Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpRTECustomer_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        Financial Site:
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="drpRTEFinSite" runat="server" Width="200px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        Rec Date:
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtRTERecDate" runat="server"></asp:TextBox>
                            <img alt="Calendar" border="0" height="20" src="images/Calendar.jpg" style="cursor: pointer;" id="img5" runat="server"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtRTERecDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"/>
                    </td>
                        <td class="auto-style2">
                        Completed Date:
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtRTECompDate" runat="server"></asp:TextBox>
                        <img alt="Calendar" border="0" height="20" src="images/Calendar.jpg" style="cursor: pointer;" id="img6" runat="server"
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtRTECompDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvIBMRTE" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                            CssClass="lightbackground" Width="843px">
                            <HeaderStyle CssClass="darkbackground" Height="30px"  />
                            <AlternatingRowStyle BackColor="#F2F2F2" />
                            <Columns>
                                <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                        <br />
                                        <asp:HiddenField ID="hfgvNLID" runat="server" Value='<%# Eval("NL_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="jobid" HeaderText="JOBID"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobid" runat="server" Text='<%# Eval("Jobid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="cust_name" HeaderText="Customer"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="title" HeaderText="Project Title" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="received_date"  HeaderText="Rec. Date" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("CREATED_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ProjectEditor" HeaderText="ProjectEditor" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPProjectEditor" runat="server" Text='<%# Eval("ProjectEditor") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Pages" HeaderText="Pages" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPPages" runat="server" Text='<%# Eval("Pages_Count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Task" HeaderText="Task" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPTask" runat="server" Text='<%# Eval("Task") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField >
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAll1(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Check" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                <//tr>
            </table>
        </div>
    </form>
</body>
</html>

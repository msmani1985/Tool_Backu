<%@ page language="C#" autoeventwireup="true" inherits="Delphi_Job_Projects, App_Web_25d24vps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Projects</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/common.js"></script>
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
           if (document.getElementById("lblBookSummary"))
               document.getElementById("lblBookSummary").innerText = "Book : " + val1;
           else if (document.all.lblBookSummary)
               document.all.lblBookSummary.innerText = "Book : " + val1;
           else if (document.form1.lblBookSummary)
               document.form1.lblBookSummary.innerText = "Book : " + val1;
           doTimer();
       }

       function setMouseOverColor(element) {
           gvcolor = element.style.backgroundColor;
           element.style.backgroundColor = '#C2C2C2';
           element.style.cursor = 'pointer';
           element.style.textDecoration = 'underline';
       }
       function setMouseOutColor(element) {
           element.style.backgroundColor = gvcolor;
           element.style.textDecoration = 'none';
       }


       function imgReason_editor_onclick() {
           window.open("PEName.aspx?form=Projects&type=0&trgname=Dropcomplex", "Projects", "width=600,height=250,status=yes, scrollbars=yes");

       }
    </script>
     <script language="javascript">
         var cnt = 0;
         var tt;
         var timer_is_on = 0;
         var elemn;
         var h = 0;
         var max = 20;
         function timedCount() {
             //alert(document.getElementById('divfooter'));
             elemn = document.getElementById('divfooter');
             h = elemn.style.height.replace('px', '');
             if (timer_is_on && cnt <= max && h <= max) {
                 //ctrl.value=cnt;	
                 if (h == '') h = 0;
                 //alert(h);
                 elemn.style.height = parseInt(h) + 5 + 'px';
                 //alert(cnt);
                 cnt = cnt + 5;
                 tt = setTimeout("timedCount()", 0);
             } else {
                 timer_is_on = 0;
                 cnt = 0;
             }
         }
     
         function doTimer() {
             if (!timer_is_on) {
                 timer_is_on = 1;
                 timedCount();
             } else timer_is_on = 0;
         }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div>
            <table width="100%">
                <tr>
                    <td>
                        <div>
                            <table class="content" width="100%">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="3" style="height: 20px">
                                        <img align="absmiddle" src="images/tools/search.png" />&nbsp;<strong>Search Projects</strong></td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Customer</strong></td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="drpCustomerSearch" runat="server" Width="325px" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="chkViewCompleted" runat="server" Font-Bold="True" Text="Show Completed Jobs"
                                            TabIndex="2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Project No.</strong></td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="318px" CssClass="TxtBoxSearch"
                                            TabIndex="3"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="width: 80pt" CssClass="dpbutton"
                                             TabIndex="4" onclick="btnSearch_Click" />&nbsp;&nbsp;
                                        <asp:HiddenField ID="hfP_ID" runat="server" />
                                        <asp:HiddenField ID="hfP_Name" runat="server" />
                                    </td>
                                </tr>
                          
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <ol id="toc">
                            <li id="miGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral" runat="server"  TabIndex="5" 
                                    onclick="lnkGeneral_Click">General</asp:LinkButton></li>
                            <li id="miProjectDetails" runat="server">
                                <asp:LinkButton ID="lnkProjectdetails" runat="server" 
                                    TabIndex="6" onclick="lnkProjectdetails_Click">Project Details</asp:LinkButton></li>
                            <li id="miProjectEvents" runat="server">
                                <asp:LinkButton ID="lnkProjectEvents" runat="server" TabIndex="9" 
                                    onclick="lnkProjectEvents_Click">Project Logged Events</asp:LinkButton></li>
                            <li id="miProjectBarcode" runat="server">
                                <asp:LinkButton ID="lnkProjectBarcode" runat="server" TabIndex="9" onclick="lnkProjectBarcode_Click" 
                                   >Barcode Scanner</asp:LinkButton></li>
                        </ol>
                        <div class="content" id="tabGeneral" runat="server">
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="4" style="height: 32px">
                                        <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblProjectSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                    <td align="right">
                                        <asp:ImageButton ID="cmd_Excel_Export" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                            ToolTip="Export Excel" onclick="cmd_Excel_Export_Click"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gvProjects" runat="server" Width="100%" 
                                            AutoGenerateColumns="False" Font-Size="8pt" CssClass="lightbackground" AllowSorting="True"
                                            OnSorting="gvProjects_Sorting" onrowdatabound="gvProjects_RowDataBound">
                                            <HeaderStyle CssClass="darkbackground" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>" ></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvProjectID" runat="server" Value='<%# Eval("projectno") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer" SortExpression="CUSTNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CAT #" SortExpression="pcode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpcode" runat="server" Text='<%# Eval("pcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Title" SortExpression="PTITLE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvptitle" runat="server" Text='<%# Eval("PTITLE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Recd. Date" SortExpression="PRECEIVEDDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPRECEIVEDDATE" runat="server" Text='<%# Eval("PRECEIVEDDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Due Date" SortExpression="PDUEDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDUEDATE" runat="server" Text='<%# Eval("PDUEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Despatch Date" SortExpression="PCOMPLETEDDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPCOMPLETEDDATE" runat="server" Text='<%# Eval("PCOMPLETEDDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stage" SortExpression="STYPENAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpStage" runat="server" Text='<%# Eval("STYPENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Digital Product" SortExpression="PDIGITAL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDIGITAL" runat="server" Text='<%# Eval("PDIGITAL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Prod Code" SortExpression="PRODCODE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvProdCode" runat="server" Text='<%# Eval("PRODCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Typesetting Platform" SortExpression="TPLATCODE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTypesetplat" runat="server" Text='<%# Eval("TPLATCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Barcode" SortExpression="Pbarcode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbarcode" runat="server" Text='<%# Eval("Pbarcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabProjectDetails" runat="server">
                            <div id="CUSTOMER_TABLE" class="boxTable">
                            </div>
                            <div id="PARENT_JOB" class="boxTable" style="">
                                <table id="XMLTAGS" border="0" width="100%" cellpadding="2" cellspacing="0">
                                    <tr bgcolor="#f0fff0">
                                        <td colspan="4" class="dpJobGreenHeader">
                                            <img id="imgProjectHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                ID="lblProjectHeader" runat="server" Text="Label">New Project</asp:Label></td>
                                        <td class="dpJobGreenHeader">
                                            <asp:ImageButton ID="cmd_New_Project" ImageUrl="~/images/tools/j_new.png" runat="server"
                                                ToolTip="New Project"  TabIndex="40" onclick="cmd_New_Project_Click" />
                                            <asp:ImageButton ID="cmd_Save_Project" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                ToolTip="Save Project"  TabIndex="41" onclick="cmd_Save_Project_Click" />
                                         
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            Customer:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpProjectCustomer" runat="server" Width="306px" AutoPostBack="True"
                                                 TabIndex="12" 
                                                onselectedindexchanged="drpProjectCustomer_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td>
                                            Received Date:</td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtPRecvDate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPRecvDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img57" runat="server" tabindex="31" /></td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            Cat ID #:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td style="font-size: 8pt">
                                            <asp:TextBox ID="txtProjectNo" runat="server" CssClass="TxtBox" BackColor="#FFFFC0"
                                                Width="200px" MaxLength="150" TabIndex="13"></asp:TextBox>
                                        </td>
                                        <td style="font-size: 8pt">
                                            Due Date:</td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtPDueDate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPDueDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img4" runat="server" tabindex="31" /></td>
                                       
                                    </tr>
                                    <tr style="font-size: 8pt">
                                        <td class="style1">
                                            Project Title:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtPojectTitle" runat="server" CssClass="TxtBox" Width="300px" BackColor="#FFFFC0"
                                                MaxLength="300" TabIndex="14"></asp:TextBox></td>
                                        <td >
                                            Completed Date:</td>
                                        <td colspan="2">
                                           <asp:TextBox ID="txtPComptDate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPComptDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img2" runat="server" tabindex="31" /></td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            Financial Site:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpProjectcustfinsite" runat="server" Width="306px" TabIndex="15">
                                            </asp:DropDownList></td>
                                        <td>
                                            Add Items:</td>
                                       
                                        <td>
                                            <asp:TextBox ID="txtProjectadditem" Width="100"  runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                       
                                    <tr>
                                        <td class="style1">
                                            Project Editor:</td>
                                        <td>
                                            <asp:DropDownList ID="txtProjectEditor" runat="server" CssClass="TxtBox" Width="306px" TabIndex="22" BackColor="#F1F1F1"></asp:DropDownList>
                                           <%-- <img id="imgBD_editor" align="absMiddle" src="images/tools/user_go.png" language="javascript"
                                                onclick="return imgBD_editor_onclick()" style="cursor: pointer" title="Select Editor"
                                                tabindex="23" />
                                            <asp:HiddenField ID="hfBookEditorId" runat="server" />--%>
                                            <img id="img58" language="javascript" 
                                                onclick="return imgReason_editor_onclick()" src="images/tools/new.png" 
                                                style="cursor: pointer; height: 16px;" tabindex="17" 
                                                title="The Language Work Inc" /><asp:ImageButton src="images/tools/Refresh.png" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click"  />
                                                
                                            </td>
                                           <td>
                                               &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                    <td colspan="5">
                                    <table>
                                    <tr>
                                    <td></td>
                                    <td></td>
                                    <td align="center">Start Date</td>
                                    <td></td>
                                    <td align="center">Due Date</td>
                                    <td></td>
                                    <td align="center">Half Due Date</td>
                                    <td></td>
                                    <td align="center">Employee</td>
                                    <td></td>
                                    <td align="center">Actual Dispatch Date</td>
                                    </tr>

                                     <tr>
                                    <td></td>
                                    <td>Page Proof:</td>
                                     <td>
                                        <asp:TextBox ID="txtStartPProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartPProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img17" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDuePProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDuePProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img18" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfPProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfPProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img19" runat="server" tabindex="31" /></td><td></td>
                                         <td><asp:DropDownList ID="dropEmpPProof" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispPProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispPProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img20" runat="server" tabindex="31" /></td>
                                    </tr>
                                     <tr>
                                    <td></td>
                                    <td>1st Revises:</td>
                                     <td>
                                        <asp:TextBox ID="txtStartFirst" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartFirst','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img21" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueFirst" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueFirst','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img22" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfFirst" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfFirst','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img23" runat="server" tabindex="31" /></td><td></td>
                                          <td><asp:DropDownList ID="dropEmpFirst" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispFirst" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispFirst','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img24" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>2nd Revises:</td>
                                     <td>
                                        <asp:TextBox ID="txtStartSecond" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartSecond','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img25" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueSecond" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueSecond','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img26" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfSecond" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfSecond','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img27" runat="server" tabindex="31" /></td><td></td>
                                           <td><asp:DropDownList ID="dropEmpSecond" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispSecond" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispSecond','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img28" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>3rd Revises:</td>
                                    <td>
                                        <asp:TextBox ID="txtStartThird" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartThird','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img29" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueThird" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueThird','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img30" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfThird" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfThird','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img31" runat="server" tabindex="31" /></td><td></td>
                                       <td><asp:DropDownList ID="dropEmpThird" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                       <td><asp:TextBox ID="txtDispThird" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispThird','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img32" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>Final Proof:</td>
                                    <td>
                                        <asp:TextBox ID="txtStartFProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartFProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img33" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueFProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueFProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img34" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfFProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfFProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img35" runat="server" tabindex="31" /></td><td></td>
                                           <td><asp:DropDownList ID="dropEmpFinal" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispFProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispFProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img36" runat="server" tabindex="31" /></td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="5">
                                    <table>
                                    <tr>
                                    <td>Invoice Date:</td>
                                    <td><asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtInvoiceDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img37" runat="server" tabindex="31" />
                                                  
                                                  </td><td></td>
                                    <td>Invoice No.:</td>
                                    <td><asp:TextBox ID="txtInvoiceNo" Width="100" runat="server"></asp:TextBox></td><td></td>
                                    <td>Product Code:</td>
                                    <td><asp:DropDownList ID="dropProdCode" Width="100" runat="server">
                                        </asp:DropDownList>
                                        </td><td></td>
                                    <td>Typesetting Platform:</td><td><asp:DropDownList ID="DropTypeset" Width="80" 
                                            runat="server">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>Current Status:</td>
                                    <td><asp:DropDownList ID="dropCurStatus" Width="100" runat="server">
                                        </asp:DropDownList>
                                    </td><td></td>
                                      <td>Current Stage:</td>
                                    <td><asp:DropDownList ID="DropCurStage" Width="100" runat="server">
                                        </asp:DropDownList>
                                    </td><td></td>
                                      <td>Employee:</td>
                                    <td><asp:DropDownList ID="DropEmpnameMain" Width="100" runat="server">
                                        </asp:DropDownList>
                                    </td><td></td><td>Department:</td><td><asp:DropDownList ID="DropDepartment" Width="80" runat="server">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>          
                                     <tr>
                                    <td>Add Charges:</td>
                                    <td><asp:TextBox ID="txtAddCharge" Width="100" runat="server"></asp:TextBox>
                                    </td><td></td>
                                      <td>P.O Number:</td>
                                    <td><asp:TextBox ID="txtPONumber" Width="100" runat="server"></asp:TextBox>
                                    </td><td></td>
                                      <td>Credit Cost:</td>
                                    <td><asp:TextBox ID="txtCrditCost" Width="70" runat="server"></asp:TextBox> </td><td></td>
                                      <td>Project Cost:</td>
                                    <td colspan="3"><asp:TextBox ID="txtProjectCost" Width="70" Text="215" runat="server"></asp:TextBox>
                                                  
                                                  
                                                </td>
                                    </tr>
                                    <tr>
                                    <td>Input:</td>
                                    <td><asp:DropDownList ID="dropInput" Width="100" runat="server">
                                        </asp:DropDownList>
                                        </td><td></td>
                                      <td>Output:</td>
                                    <td><asp:DropDownList ID="DropOutPut" Width="100" runat="server">
                                        </asp:DropDownList>
                                        </td><td></td>
                                      <td>No. of Characters:</td>
                                    <td><asp:TextBox ID="txtNoCharater" Width="70" runat="server"></asp:TextBox>
                                        </td><td></td>
                                    <td>ISBN:</td>
                                    <td><asp:TextBox ID="txtISBN" Width="70" runat="server"></asp:TextBox>
                                           </td><td></td>
                                    </tr>
                                     <tr>
                                     <td>Credited for India:</td>
                                    <td>
                                        <asp:DropDownList ID="DropCrdInd" runat="server">
                                            <asp:ListItem>N</asp:ListItem>
                                            <asp:ListItem>Y</asp:ListItem>
                                        </asp:DropDownList>
                                    </td><td></td>
                                     <td>Credited:</td>
                                    <td><asp:DropDownList ID="DropCrd" runat="server">
                                        <asp:ListItem>N</asp:ListItem>
                                        <asp:ListItem>Y</asp:ListItem>
                                        </asp:DropDownList>
                                    </td><td></td>
                                    <td>Charged No. of Pages:</td>
                                    <td><asp:TextBox ID="txtChargedPages" Width="70" runat="server"></asp:TextBox>
                                    </td><td></td>
                                      <td>Charged No. of Articles:</td>
                                    <td>
                                        <asp:TextBox ID="txtChargedArticle" Width="70" runat="server"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>Digital Product:</td><td>
                                        <asp:DropDownList ID="DropDigitalProd" runat="server">
                                            <asp:ListItem>N</asp:ListItem>
                                            <asp:ListItem>Y</asp:ListItem>
                                        </asp:DropDownList>
                                        </td><td></td><td>Project Number:</td><td><asp:TextBox ID="txtProjectNumber" 
                                            Width="100" runat="server"></asp:TextBox>
                                        </td><td></td>
                                        <td>No. of Pages:</td><td><asp:TextBox ID="txtPages" Width="70" 
                                            runat="server"></asp:TextBox>
                                        </td><td></td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="3">
                                    <table>
                                    <tr><td></td>
                                    <td>Addl. Cost1</td><td><asp:TextBox ID="txtcost1" Width="250px" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity1</td><td><asp:TextBox ID="txtQty1" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 1</td><td><asp:TextBox ID="txtPrice1" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                    <td>Type of Cost:</td><td></td><td>Work From:</td><td></td>
                                    <td colspan="2">&nbsp;</td><td></td>
                                    </tr>
                                      <tr><td></td>
                                    <td>Addl. Cost2</td><td><asp:TextBox ID="txtcost2" Width="250px" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity2</td><td><asp:TextBox ID="txtQty2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 2</td><td><asp:TextBox ID="txtPrice2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                    <td rowspan="3" >
                                        <asp:RadioButtonList ID="rbTypeCost" runat="server" Height="62px" 
                                            Width="187px">
                                            <asp:ListItem Selected="True" Value="0">Project Cost</asp:ListItem>
                                            <asp:ListItem Value="2">Additional Cost</asp:ListItem>
                                            <asp:ListItem Value="1">Charged Pages Cost</asp:ListItem>
                                            <asp:ListItem Value="3">Charged Articles Cost</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </td>
                                    <td></td>
                                    <td rowspan="2" colspan="2" >
                                        <asp:RadioButtonList ID="rbWorkFrom" runat="server" Width="99px">
                                            <asp:ListItem Value="2">Chennai</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="3">Coimbatore</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </td>
                                    <td rowspan="2" colspan="2">
                                        &nbsp;</td>
                                    </tr>
                                      <tr><td></td>
                                    <td>Addl. Cost3</td><td><asp:TextBox ID="txtcost3" Width="250px" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity3</td><td><asp:TextBox ID="txtQty3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 3</td><td><asp:TextBox ID="txtPrice3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                    </tr>
                                      <tr><td></td>
                                    <td>Addl. Cost4</td><td><asp:TextBox ID="txtCost4" Width="250px" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity4</td><td><asp:TextBox ID="txtQty4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 4</td><td><asp:TextBox ID="txtPrice4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                    </tr>
                                      <tr><td></td>
                                    <td>Addl. Cost5</td><td><asp:TextBox ID="txtCost5" Width="250px" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity5</td><td><asp:TextBox ID="txtQty5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 5</td><td><asp:TextBox ID="txtPrice5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>Description:
                                    </td>
                                    <td> <asp:TextBox ID="txtProjectDesc" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="300px" TabIndex="24"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Comments:</td>
                                        <td >
                                            <asp:TextBox ID="txtProjectComments" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="300px" TabIndex="24"></asp:TextBox></td>
                                    </tr>
                             
                             <%--       <tr><td colspan="2">
                                        <div id="divPopIsBOnHold" class="ModalPopup">
                                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                            width: 163px;">
                                                            &nbsp;Book On Hold
                                                        </td>
                                                        <td align="right" style="background-color: green; color: White; font-weight: bold">
                                                            <a href="#" title="Close" onclick="javascript:closeModalArtHold();" style="color: White;">
                                                                [x]</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;OnHold Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:DropDownList ID="drpBookOnHoldType" runat="server">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;Reason for Hold:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:TextBox ID="txtBookOnHoldReason" runat="server" CssClass="TxtBox" Width="180px"
                                                                MaxLength="300"></asp:TextBox></td>
                                                    </tr>
                                                    <tr bgcolor="Honeydew">
                                                        <td colspan="2" align="center">
                                                            <a class="link1" href="#" onclick="javascript:validSaveItem_hold();"><strong>Submit</strong></a>
                                                            &nbsp; <a class="link1" href="#" onclick="javascript:closeModalArtHold();"><strong>Cancel</strong></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                    </td></tr>--%>
                                </table>
                            </div>
                        </div>
                       
                        
                        <div class="content" id="tabProjectEvents" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img7" align="absmiddle" src="images/tools/events.png" runat="server" />
                                        <asp:Label ID="lblEventsHeader" runat="server" Text="Project Logged Events"></asp:Label>
                                        </td>
                                    <td class="dpJobGreenHeader">
                                        <asp:ImageButton ID="imgbtnEventExport" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                            ToolTip="Export Excel"  />
                                            </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <%--<strong>Book: <a id="aEvents" href="#" onclick="javascript:showhideDiv(this,'divEvents');"
                                            class="link1">(Hide)</a></strong>--%></td>
                                </tr>
                                <tr style="font-size: 8pt; font-family: Verdana">
                                    <td colspan="6">
                                        <div id="divEvents" style="display: block;">
                                            <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CAT #">
                                                        <ItemTemplate>
                                                            <%# Eval("Pcode")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <%# Eval("LEDATE")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                            <%# Eval("EMP_FNAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Events">
                                                        <ItemTemplate>
                                                            <%# Eval("EVDESCRIPTION")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Stage">
                                                        <ItemTemplate>
                                                            <%# Eval("STYPENAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                        No records found.</div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            
                            </table>
                        </div>
                        <div class="content" id="tabProjectBarcode" runat="server">
                        <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img1" align="absmiddle" src="images/tools/events.png" runat="server" />
                                        <asp:Label ID="Label2" runat="server" Text="Barcode"></asp:Label>
                                        </td>
                                    <td class="dpJobGreenHeader">
                                        
                                            </td>
                                </tr>
                                 </table>
                                 <div runat="server" align="center">
                                 <table>
                                <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Barcode No."></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtProBarcode" runat="server"></asp:TextBox>
                                </td>
                                <td><asp:Label ID="Label4" runat="server" Text="CAT #"></asp:Label>
                                
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProCat" runat="server"></asp:TextBox>
                                </td>
                                </tr>
                                <tr><td><asp:Label ID="Label5" runat="server" Text="Stage"></asp:Label>
                                
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropProStage" runat="server">
                                    </asp:DropDownList>
                                </td>
                                </tr>
                                <tr>
                                <td colspan="4" align="center">
                                    <asp:Button CssClass="dpbutton" ID="btnProBarUpdate" runat="server" 
                                        Text="Update" onclick="btnProBarUpdate_Click" />
                                </td>
                                </tr>
                                </table>
                               </div>
                        </div>
                    </td>
                </tr>
               
            </table>
        </div>
        
    </form>
</body>
</html>

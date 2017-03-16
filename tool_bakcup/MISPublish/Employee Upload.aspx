<%@ page language="C#" autoeventwireup="true" inherits="Employee_Upload, App_Web_vlobbbje" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
       <link href="default.css" type="text/css" rel="stylesheet" />
     <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="scripts/common.js"></script>
     <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
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
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=GvEmp.ClientID %>').Scrollable({
                ScrollHeight: 350
            });
        });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="borderdiv" style="text-align:left;width:900px;">
     <table style="width: 593px">
       <tr>
  <td class="dpheader" colspan="10" >
        <asp:Label ID="Label1" runat="server" Text="Upload"></asp:Label></td>
  </tr>
        <tr>
            <td >
            <span style="color: Red">*</span>Attach Excel file
            </td>
            <td >
            <asp:FileUpload ID="fileBrowse" runat="server" />
            </td>
           
            
            
            <td >
            <asp:Button ID="btnSave"  CssClass="dpbutton" runat="server" Text="Upload" OnClick="btnSave_Click" />&nbsp;
            </td>
        </tr>
        <tr>
        <td >
        <asp:HiddenField ID="hfP_ID" runat="server" />
        <asp:HiddenField ID="hfE_ID" runat="server" />
        <asp:HiddenField ID="hfP_Name" runat="server" />
        </td>
        </tr>
        </table>
        <table style="width: 591px">
          <tr>
  <td class="dpheader" colspan="6" >
        <asp:Label ID="Label2" runat="server" Text="Search"></asp:Label></td>
  </tr>
        <tr>
        <td>
        Employee Name/No.
        </td>
        <td style="width: 152px">
            <asp:TextBox ID="txtEmp" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnSearch" CssClass="dpbutton" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </td>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="rdBtnLive" Text="Live" runat="server" AutoPostBack="True" OnCheckedChanged="rdBtnLive_CheckedChanged" />
            <asp:RadioButtonList ID="rbLocation" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="2">Chennai</asp:ListItem>
                <asp:ListItem Value="3">Coimbatore</asp:ListItem>
            </asp:RadioButtonList>
        </td></tr>
        <tr>
        <td>Month/Year</td><td style="width: 152px">
            <asp:TextBox ID="txtMonth" runat="server" Width="33px"></asp:TextBox>/
            
                <asp:TextBox ID="txtYear" runat="server" Width="52px"></asp:TextBox></td>
                
               
                <td><asp:Button ID="btnFilter" CssClass="dpbutton" runat="server" Text="Filter" OnClick="btnFilter_Click"  /></td>
                 <td></td>
                <td><asp:Label Text="(eg. 10/2014)" ID="exlbl" ForeColor="red" runat="server"></asp:Label></td>
        </tr>
        </table>
    </div>
    
     <div><table><tr>
    <td>
    <ol id="toc">
                             <li id="miSummaryDetails" runat="server">
                                <asp:LinkButton ID="LinkSummary" TabIndex = "4" runat="server" OnClick="lnkSummaryDetails_Click"  >Summary</asp:LinkButton></li>
                            <li id="miEmpDetails" runat="server">
                                <asp:LinkButton ID="lnkEmpDetails" TabIndex = "4" runat="server" OnClick="lnkEmpDetails_Click"  >Employee Details</asp:LinkButton></li>
                            <li id="miPersonalDetails" runat="server">
                                <asp:LinkButton ID="lnkPersonalDetails" TabIndex = "4" runat="server" OnClick="lnkPersonalDetails_Click"  >Personal Details</asp:LinkButton></li>
                            <li id="miPreview" runat="server">
                                <asp:LinkButton ID="lnkPreview" TabIndex = "4" runat="server"  Visible="false"  >Preview</asp:LinkButton></li>
                            <li id="miManualLeave" runat="server">
                                <asp:LinkButton ID="lnkManualLeave" TabIndex = "4" runat="server"   OnClick="lnkManualLeave_Click"  >Manual Leave correction</asp:LinkButton></li>
                            <li id="miFeedbackDetails" runat="server">
                                <asp:LinkButton ID="lnkFeedbackDetails" TabIndex = "4" runat="server" OnClick="lnkFeedbackDetails_Click"  >Feedback Details</asp:LinkButton></li>
                           
                        </ol>
                        <div class="borderdiv" id="div_Summary_details" runat="server" style="text-align:center;width:950px;">
                        <table>
                        <tr class="dpJobGreenHeader">
                        <td align="left" style="width: 500px"><asp:Label ID="lblEmployeeSummary" runat="server" Text="Employee Summary"></asp:Label></td>
                     <td align="right"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl"   ToolTip="Export Exl" OnClick="exportExl_Click"  />
                        </td></tr>
                        <tr>
                        <td  colspan="2">
                        <asp:GridView ID="GvEmp" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                      CssClass="lightbackground"  OnRowDataBound="GridView1_RowDataBound" Width="920px" AllowSorting="True" OnSorting="GvEmp_Sorting"  >
                                            <HeaderStyle CssClass="GVFixedHeader"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                            
                                                <asp:TemplateField SortExpression="sl" HeaderText="Sl. No."  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvslno" runat="server" Width="20" Text='<%# Eval("sl") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="empid" HeaderText="Empid"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvempid" runat="server" Text='<%# Eval("refno") %>'></asp:Label>
                                                         <br />
                                                        <asp:HiddenField ID="hfgvempid" runat="server" Value='<%# Eval("refno") %>' />
                                                        <asp:HiddenField ID="hfgvrefno" runat="server" Value='<%# Eval("empname") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="EmpName" HeaderText="Emp Name"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvempName" runat="server" Width="180" Text='<%# Eval("empname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="Designation" HeaderText="Designation" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDesignation"  runat="server" Text='<%# Eval("designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Department"  HeaderText="Department" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDepartment"  runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField SortExpression="DOJ" HeaderText="DOJ" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDOJ" Width="90" runat="server" Text='<%# Eval("DOJ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Prob_CONF_DATE" HeaderText="Prob.Conf Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvProbConfirDate" Width="100" runat="server" Text='<%# Eval("Prob_CONF_DATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField SortExpression="ConfirmationDate" HeaderText="Confirmation Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvConfirDate" Width="100"  runat="server" Text='<%# Eval("ConfirmationDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="DOL" HeaderText="DOL" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDOl" Width="90" runat="server" Text='<%# Eval("DOL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="sex" HeaderText="Gender" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgender"  runat="server" Text='<%# Eval("sex") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                        </td>
                        </tr>
                        </table>
                        </div>
    <div class="borderdiv" id="div_employee_details" runat="server" style="text-align:center;width:900px;" >
    <table id="editemployee" style="width:100%;text-align:left;">
    <tr><td class="dpGreenHeader" align="center" colspan="5" style="height: 30px">Employees Details</td></tr>
       
        <tr><td>Emp ID</td><td>
        <asp:TextBox ID="txtEmpid" Enabled="false" runat="server"></asp:TextBox></td>
        <td rowspan="6" colspan="2" align="center">
          
            <asp:Image ID="imgPhoto" runat="server" Width="126px"  Height="126px" /> </td>
        </tr>
        <tr><td>Emp Name</td><td>
        <asp:TextBox ID="txtEmpName" Enabled="false" runat="server"></asp:TextBox></td></tr>
        <tr><td>Gender</td><td>
        <asp:TextBox ID="txtGender" Enabled="false" runat="server"></asp:TextBox></td></tr>
        <tr><td>DOB</td><td>
        <asp:TextBox ID="txtDOB" Enabled="false" runat="server"></asp:TextBox></td></tr> 
        <tr><td>Father Name</td><td>
        <asp:TextBox ID="txtFname"  runat="server"></asp:TextBox></td></tr>
        <tr>
        <td>Mother Name</td><td>
        <asp:TextBox ID="txtMname"  runat="server"></asp:TextBox></td></tr>
        <tr><td>Marital Status</td><td>
            <asp:DropDownList ID="DropMaritalStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropMaritalStatus_SelectedIndexChanged1" >
               <asp:ListItem Value="SINGLE">Single</asp:ListItem>
                <asp:ListItem Value="MARRIED">Married</asp:ListItem>
            </asp:DropDownList></td>
        <td>Spouse Name</td><td>            
        <asp:TextBox ID="txtSpouse" Enabled="false" runat="server"></asp:TextBox></td></tr>  
        <tr><td colspan="4" class="HeadText">Working Details</td></tr> 
        <tr><td>DOJ</td><td>
        <asp:TextBox ID="txtDOJ" Enabled="false" runat="server"></asp:TextBox></td>
        <td>Branch</td><td>
        <asp:TextBox ID="txtBranch" Enabled="false" runat="server"></asp:TextBox></td></tr>  
        <tr><td>Designation</td><td>
        <asp:TextBox ID="txtDesign" Enabled="false" runat="server"></asp:TextBox></td>
        <td>Department</td><td>            
        <asp:TextBox ID="txtDepart" Enabled="false" runat="server"></asp:TextBox></td></tr> 
        <tr><td>Bank Name</td><td>
        <asp:TextBox ID="txtBank" Enabled="false" runat="server"></asp:TextBox></td>
        <td>Bank A/C No.</td><td>            
        <asp:TextBox ID="txtBankAcc" Enabled="false" runat="server"></asp:TextBox></td></tr>
        <tr><td>PF No.</td><td>
        <asp:TextBox ID="txtPF" Enabled="false" runat="server"></asp:TextBox></td>
        <td>ESI No.</td><td>            
        <asp:TextBox ID="txtESI" Enabled="false" runat="server"></asp:TextBox></td></tr> 
        <tr><td>PAN No.</td><td>
        <asp:TextBox ID="txtPAN" Enabled="false" runat="server"></asp:TextBox></td>
        <td>Prob.Conf. Date</td><td>
        <asp:TextBox ID="txtProbConDate" Enabled="false" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
        <td></td><td></td>
        <td>Confirmation Date</td><td>
        <asp:TextBox ID="txtConDate"  runat="server"></asp:TextBox> <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtConDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dudate"  runat="server" /></td>
        </tr>
        <tr><td colspan="4" class="HeadText">Address Details</td></tr> 
        <tr><td colspan="2"> <strong>Present Address</strong></td><td colspan="2"><strong>Permanent Address</strong> </td></tr>
        <tr><td>Street No.</td><td>
        <asp:TextBox ID="txtPreNo"  runat="server"></asp:TextBox></td>
        <td>Street No.</td><td>            
        <asp:TextBox ID="txtPerNo"  runat="server"></asp:TextBox></td></tr> 
        <tr><td>Street Name</td><td>
        <asp:TextBox ID="txtPreName"  runat="server"></asp:TextBox></td>
        <td>Street Name</td><td>            
        <asp:TextBox ID="txtPerName"  runat="server"></asp:TextBox></td></tr> 
        <tr><td></td><td>
        <asp:TextBox ID="txtPreName1"  runat="server"></asp:TextBox></td>
        <td></td><td>            
        <asp:TextBox ID="txtPerName1"  runat="server"></asp:TextBox></td></tr>
         <tr><td>Street Place</td><td>
        <asp:TextBox ID="txtPrePlace"  runat="server"></asp:TextBox></td>
        <td>Street Place</td><td>            
        <asp:TextBox ID="txtPerPlace"  runat="server"></asp:TextBox></td></tr>
        <tr><td>City</td><td>
        <asp:TextBox ID="txtPreCity"  runat="server"></asp:TextBox></td>
        <td>City</td><td>            
        <asp:TextBox ID="txtPerCity"  runat="server"></asp:TextBox></td></tr>
        <tr><td>State</td><td><asp:TextBox ID="DropPreState" runat="server" Width="134px"></asp:TextBox>
        </td>
        <td>State</td><td>   <asp:TextBox ID="DropPerState" runat="server" Width="136px"></asp:TextBox>         </td></tr>
        <tr><td>Pin No.</td><td>
        <asp:TextBox ID="txtPrePin"  runat="server"></asp:TextBox></td>
        <td>Pin No.</td><td>            
        <asp:TextBox ID="txtPerPin"  runat="server"></asp:TextBox></td></tr>
        <tr><td></td></tr>
        <tr><td>Mobile</td>
        <td><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></td> 
        <td>Phone</td>
        <td><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td></tr>
        <tr><td>Email ID</td><td>
            <asp:TextBox ID="txtEmailid" runat="server"></asp:TextBox></td>
            <td colspan="2">
                <asp:CheckBox ID="CheckAddress" runat="server" Text="Copy the same address to the Permanent Address" AutoPostBack="True" OnCheckedChanged="CheckAddress_CheckedChanged" /></td>
            </tr>
        <tr><td colspan="4" align="center">
            <asp:Button ID="btnUpdate" CssClass="dpbutton" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td></tr>
            <tr><td>.</td></tr>
    </table>
    </div>
    <div class="borderdiv" id="Personal_Details"  style="width:900px;" runat="server">
    <table style="width: 800px">
     <tr>
  <td class="dpheader" colspan="4">
        <asp:Label ID="lalFamilDatails" runat="server" Text="Family Details"></asp:Label></td>
  </tr>
 

 
<tr>
<td colspan="4" > 
    <asp:GridView ID="GridView1" runat="server" ShowFooter="True" 
            PageSize="5" AutoGenerateColumns="False" 
            onrowcancelingedit="GridView1_RowCancelingEdit" 
            onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" 
            onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" BorderWidth="3px" CellPadding="4"  Width="1px"  BorderColor="#999999" BorderStyle="Solid" CellSpacing="1" ForeColor="Black"  >
				<FooterStyle BackColor="#CCCCCC"></FooterStyle>
				<HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px"  ForeColor="White" CssClass="darkbackground1" ></HeaderStyle>
			
	
<Columns>
    
<asp:TemplateField HeaderText="Sl.No" >

<ItemTemplate>

  <asp:Label ID="lblid" runat="server" Text='<%#Eval("Counter") %>'></asp:Label>

</ItemTemplate>

<FooterTemplate>

  <asp:Label ID="lblAdd" runat="server"></asp:Label>

</FooterTemplate>

</asp:TemplateField>

<asp:TemplateField HeaderText="Name">

<ItemTemplate>

  <asp:Label ID="lblname" runat="server" Text='<%#Eval("desc01") %>'></asp:Label>

</ItemTemplate>

<EditItemTemplate>

  <asp:TextBox ID="txtname" runat="server" Text='<%#Eval("desc01") %>'></asp:TextBox>

</EditItemTemplate>

<FooterTemplate>

  <asp:TextBox ID="txtAddname" runat="server"></asp:TextBox>

</FooterTemplate>

</asp:TemplateField>

  <asp:TemplateField HeaderText="Relationship">

<ItemTemplate>

  <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("desc02") %>'></asp:Label>

</ItemTemplate>

<EditItemTemplate>

  <asp:TextBox ID="txtaddress" Width="90" runat="server" Text='<%#Eval("desc02") %>'></asp:TextBox>

</EditItemTemplate>

<FooterTemplate>

  <asp:TextBox ID="txtAddaddress" Width="90" runat="server"></asp:TextBox>

</FooterTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="DOB (MM/DD/YYYY)" >

<ItemTemplate>

  <asp:Label ID="lblage" runat="server" Text='<%#Eval("DOB") %>'></asp:Label>

</ItemTemplate>

<EditItemTemplate>

  <asp:TextBox ID="txtage" Width="120" runat="server" Text='<%#Eval("DOB") %>'></asp:TextBox>

</EditItemTemplate>

<FooterTemplate>

  <asp:TextBox ID="txtAddage" Width="120" runat="server"></asp:TextBox>

</FooterTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="Age" >

<ItemTemplate >

  <asp:Label ID="lblage1" runat="server" Text='<%#Eval("age") %>'></asp:Label>

</ItemTemplate>

<EditItemTemplate>

  <asp:TextBox ID="txtage1" Width="60" runat="server" Text='<%#Eval("age") %>'></asp:TextBox>

</EditItemTemplate>
<FooterTemplate>

  <asp:TextBox ID="txtage1" Width="60" Enabled="false" runat="server"></asp:TextBox>

</FooterTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="Remarks">
<ItemTemplate>
  <asp:Label ID="lblesignation" runat="server" Text='<%#Eval("desc04") %>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
  <asp:TextBox ID="txtdesignation" Width="90" runat="server" Text='<%#Eval("desc04") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
  <asp:TextBox ID="txtAdddesignation" Width="90" runat="server"></asp:TextBox>
</FooterTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Depends">
<ItemTemplate>
  <asp:CheckBox ID="lbldepends" runat="server" Enabled="False" Checked='<%# Convert.ToBoolean(Eval("OPTIONYN")) %>'></asp:CheckBox>
</ItemTemplate>
<EditItemTemplate>
  <asp:CheckBox ID="txtdepends" Width="90" runat="server" Checked='<%# Convert.ToBoolean(Eval("OPTIONYN")) %>' ></asp:CheckBox>
</EditItemTemplate>
<FooterTemplate>
  <asp:CheckBox ID="chkAdddepends" Width="90" runat="server" Checked='<%# Convert.ToBoolean(Eval("OPTIONYN")) %>' ></asp:CheckBox>
</FooterTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Nominee">
<ItemTemplate>
  <asp:CheckBox ID="lblnominee" runat="server" Enabled="False" Checked='<%# Convert.ToBoolean(Eval("NOMINEEYN")) %>'></asp:CheckBox>
</ItemTemplate>
<EditItemTemplate>
  <asp:CheckBox ID="txtnominee" Width="90" runat="server" Checked='<%# Convert.ToBoolean(Eval("NOMINEEYN")) %>'></asp:CheckBox>
</EditItemTemplate>
<FooterTemplate>
  <asp:CheckBox ID="chkAddnominee" Width="90" runat="server" Checked='<%# Convert.ToBoolean(Eval("NOMINEEYN")) %>'></asp:CheckBox>
</FooterTemplate>
</asp:TemplateField>

  <asp:TemplateField HeaderText="Edit">

<ItemTemplate>

  <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" />

<br />

<span onclick="return confirm('Are you sure you want to delete this record?')">
 
  <asp:LinkButton ID="btnDelete" Text="Delete" runat="server" CommandName="Delete"/>
  
 </span>

</ItemTemplate>

<EditItemTemplate>

  <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />

<br />

  <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />

</EditItemTemplate>

<FooterTemplate>

  <asp:Button ID="btnAddRecord" runat="server" Text="Add" CssClass="dpbutton" CommandName="Add"></asp:Button>

</FooterTemplate>

</asp:TemplateField>

</Columns>
        <RowStyle BackColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />

</asp:GridView>
</td>
</tr>
<tr></tr>
  <tr>
  <td class="dpheader" colspan="4" style="width: 771px">
        <asp:Label ID="lblEduDetails" runat="server" Text="Educational Details"></asp:Label></td>
  </tr>
  <tr>
<td colspan="4" style="width: 771px"> 
    <asp:GridView ID="gv_EduDetails" runat="server" ShowFooter="True" 
            PageSize="5" AutoGenerateColumns="False" 
            onrowcancelingedit="gv_EduDetails_RowCancelingEdit" 
            onrowcommand="gv_EduDetails_RowCommand" onrowdeleting="gv_EduDetails_RowDeleting" 
            onrowediting="gv_EduDetails_RowEditing" onrowupdating="gv_EduDetails_RowUpdating" BorderWidth="3px" CellPadding="4" CaptionAlign="Right" Width="269px"  BorderColor="#999999" BorderStyle="Solid" CellSpacing="2" ForeColor="Black"      >
				<FooterStyle BackColor="#CCCCCC"></FooterStyle>
				<HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground1" ></HeaderStyle>
			
	
<Columns>
    
<asp:TemplateField HeaderText="Sl.No" >

<ItemTemplate>

  <asp:Label ID="lblid" runat="server" Text='<%#Eval("Counter") %>'></asp:Label>

</ItemTemplate>

<FooterTemplate>

  <asp:Label ID="lblAdd" runat="server"></asp:Label>

</FooterTemplate>

</asp:TemplateField>

<asp:TemplateField HeaderText="Qualification">

<ItemTemplate>

  <asp:Label ID="lblQualification" runat="server" Text='<%#Eval("desc01") %>'></asp:Label>

</ItemTemplate>

<EditItemTemplate>

  <asp:TextBox ID="txtQualif"  runat="server" Text='<%#Eval("desc01") %>'></asp:TextBox>

</EditItemTemplate>

<FooterTemplate>

  <asp:TextBox ID="txtAddQualif" runat="server"></asp:TextBox>

</FooterTemplate>

</asp:TemplateField>

  <asp:TemplateField HeaderText="Board\University">

<ItemTemplate>

  <asp:Label ID="lblBoard" runat="server" Text='<%#Eval("desc02") %>'></asp:Label>

</ItemTemplate>

<EditItemTemplate>

  <asp:TextBox ID="txtboard" runat="server" Text='<%#Eval("desc02") %>'></asp:TextBox>

</EditItemTemplate>

<FooterTemplate>

  <asp:TextBox ID="txtAddboard" runat="server"></asp:TextBox>

</FooterTemplate>

</asp:TemplateField>

<asp:TemplateField HeaderText="Percentage">

<ItemTemplate>

  <asp:Label ID="lblper" runat="server" Text='<%#Eval("Num01") %>'></asp:Label>

</ItemTemplate>

<EditItemTemplate>

  <asp:TextBox ID="txtper" runat="server" Text='<%#Eval("Num01") %>'></asp:TextBox>

</EditItemTemplate>

<FooterTemplate>

  <asp:TextBox ID="txtAddper" Width="100" runat="server"></asp:TextBox>

</FooterTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="Year of PassOut">

<ItemTemplate>

  <asp:Label ID="lblpass" runat="server" Text='<%#Eval("year") %>'></asp:Label>

</ItemTemplate>

<EditItemTemplate>

  <asp:TextBox ID="txtpass" runat="server" Text='<%#Eval("year") %>'></asp:TextBox>

</EditItemTemplate>

<FooterTemplate>

  <asp:TextBox ID="txtAddpass" Width="100" runat="server"></asp:TextBox>

</FooterTemplate>

</asp:TemplateField>


  <asp:TemplateField HeaderText="Edit">

<ItemTemplate>

  <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" />

<br />

<span onclick="return confirm('Are you sure you want to delete this record?')">
 
  <asp:LinkButton ID="btnDelete" Text="Delete" runat="server" CommandName="Delete"/>
  
 </span>

</ItemTemplate>

<EditItemTemplate>

  <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />

<br />

  <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />

</EditItemTemplate>

<FooterTemplate>

  <asp:Button ID="btnAddRecord" runat="server" Text="Add" CssClass="dpbutton" CommandName="Add"></asp:Button>

</FooterTemplate>

</asp:TemplateField>

</Columns>
        <RowStyle BackColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />

</asp:GridView>
</td>
</tr>
    </table>
    </div> 
    <div class="borderdiv" id="div_Preview"  style="width:800px;" runat="server">
    <table>
    <tr>
    <td>
    <CR:CrystalReportViewer ID="crEmpUpload" runat="server" AutoDataBind="true" />
    </td>
    </tr>
    </table>
    </div>
    <div class="borderdiv1" id="div_LeaveCorrection" runat="server" style="width:900px;" >
    
    <div class="dptitle"> Leave Application</div>

    <br />
    <div id="LeaveDetaildiv" runat="server" align="center"></div>
    <br />
        <table align="center" class="bordertable" cellpadding="2" cellspacing="5" style="width: 812px">
            <tr>
                <td>
                    Emp Code 
                </td>
                <td>:</td>
                <td colspan="2">
                    <asp:Label ID="EmpcodeLbl" runat="server" Text=""></asp:Label>
                </td>
                <td  >
                    Date 
                </td>
                <td >
                    <asp:Label ID="DateLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Name of Applicant 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="NameLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Department 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="DepartmentLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Designation 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="DesignationLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Leave Required 
                </td>
                <td>:</td>
                <td>
                    From
                </td>
                <td>
                    <asp:TextBox ID="FromTxt" runat="server"  ></asp:TextBox>&nbsp; <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar3.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td >
                    To
                </td>
                <td>
                    <asp:TextBox ID="ToTxt" runat="server" Width="113px"  ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar3.aspx?formname=ToTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus();"  src="images/Calendar.jpg" height="20px" border="0" />
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="DateListBtn_Click" OnClientClick="return Validation();" CssClass="dpbutton" /></td>
            </tr>
            
            <tr>
                <td style="height: 74px">
                    Leave Details&nbsp;<br />
                    
                </td>
                <td style="height: 74px">:</td>
                <td colspan="4" align="left" style="height: 74px">
                <asp:GridView ID="grvLeave" runat="server"  autogeneratecolumns="false"  OnRowDataBound="grvLeave_RowDataBound" 
                  EmptyDataText="No data available."  CssClass="lightbackground" Font-Size="9pt" Height="90px" Width="425px" >
                  <AlternatingRowStyle Width="100px" BackColor="#F2F2F2" />
                    <HeaderStyle CssClass="GVFixedHeader"  />
                  <Columns>
                     <asp:TemplateField HeaderText="Date" >
                         <ItemTemplate>
                             <asp:Label ID="lblDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Date") %>'></asp:Label>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="First Half" >
                         <ItemTemplate>
                              <asp:CheckBox ID="ckFirstHalf" runat="server"></asp:CheckBox>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Leave Type" >
                         <ItemTemplate>
                              <asp:DropDownList ID="LeavetypeFirst" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LeavetypeFirst_SelectedIndexChanged" >
                             </asp:DropDownList>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Second Half" >
                         <ItemTemplate>
                             <asp:CheckBox ID="ckSecondHalf" runat="server"></asp:CheckBox>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Leave Type" >
                         <ItemTemplate>
                             <asp:DropDownList ID="LeavetypeSecond" runat="server" >
                             </asp:DropDownList>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                  </Columns>
                 
                </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="Label3" Text="Mode of Information  :"></asp:Label>
                </td>
                <td>:</td>
                <td colspan="4">
                    <asp:DropDownList ID="ModeofInform" runat="server">
                        <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                        <asp:ListItem Value="Mail">Mail</asp:ListItem>
                        <asp:ListItem Value="InPerson">InPerson</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" ID="lblDate" Text="Date :" style="font-weight: 700"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtAppliedDate" runat="server"  Width="101px"  ></asp:TextBox>&nbsp;
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar3.aspx?formname=txtAppliedDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                    <asp:Label runat="server" ID="lalTime" Text="Time :" style="font-weight: 700"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtTime" runat="server" Width="90px" >00:00</asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="Ex.(24:00)" style="color: red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Reason for Applying Leave
                </td>
         :</td>
                <td colspan="4">
                    <asp:TextBox ID="LeaveReasonTxt" runat="server" Height="34px" Width="206px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="SaveBtn" runat="server" Text="Save" CssClass="dpbutton"  OnClick="SaveBtn_Click" />
                </td>
            </tr>
              <tr>
                <td colspan="6" align="center">
                    <asp:Label ID="lblStatus" runat="server" Font-Size="Small" ForeColor="Red" Visible="False"  />
                </td>
            </tr>
            </table>
        <asp:HiddenField ID="PLLeaveHF" runat="server" />
        <asp:HiddenField ID="SLLeaveHF" runat="server" />
        <asp:HiddenField ID="CLLeaveHF" runat="server" />
    </div>
                        <div class="borderdiv" id="div_FeedBack_details" runat="server" style="text-align:center;width:900px;" >
                            <table id="editemployee" style="width:100%;text-align:left;" border="1">
                                <tr>
                                    <td class="dpGreenHeader" align="center" colspan="4" style="height: 30px">FeedBack</td>
                                </tr>
                                <tr>
                                    <td align="left">Emp ID:</td>
                                    <td align="left">
                                        <asp:Label ID="lblFEmpId" Enabled="false" runat="server" Height="20"></asp:Label>
                                    </td>
                                    <td align="left">Emp Name:</td>
                                    <td align="left">
                                        <asp:Label ID="lblFEmpName" Enabled="false" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <u><b>Positive:</b></u>
                                    </td>
                                    <td colspan="2">
                                       <u><b> Negative:</b></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:TextBox ID="txtPositive" TextMode="MultiLine" runat="server" Width="300px" Height="70px"></asp:TextBox>
                                    </td>
                                    <td colspan="2" align="center">
                                       <asp:TextBox ID="txtNegative" TextMode="MultiLine" runat="server" Width="300px" Height="70px"></asp:TextBox>
                                    </td>
                                </tr>
                               
                                <tr>
                                   <td colspan="4" align="center">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add"  CssClass="dpbutton" OnClick="btnAdd_Click"/>
                                    </td>
                                </tr>
                                 <tr>
                                    <td  colspan="4" align="center">
                                        <asp:GridView ID="gvFeedback" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                            CssClass="lightbackground" Width="500px" AllowSorting="True">
                                            <HeaderStyle CssClass="GVFixedHeader"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                <asp:TemplateField SortExpression="sl" HeaderText="Sl.No."  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvslno" runat="server" Text='<%# Eval("slno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Positive" HeaderText="Positive"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPositive" runat="server" Text='<%# Eval("Positive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="200px" Wrap="True" />
                                                    <ItemStyle Width="200px" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Negative" HeaderText="Negative"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvNegative" runat="server" Text='<%# Eval("Negative") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="200px" Wrap="True" />
                                                    <ItemStyle Width="200px" Wrap="True" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>  
    </td></tr></table></div>
    </form>
</body>
</html>

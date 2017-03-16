<%@ page language="C#" autoeventwireup="true" inherits="EmployeeDetails, App_Web_qzq-weby" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
  <link href="default.css" type="text/css" rel="stylesheet" />
<link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="scripts/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div><table><tr>
    <td>
    <ol id="toc">
                            <li id="miEmpDetails" runat="server">
                                <asp:LinkButton ID="lnkEmpDetails" TabIndex = "4" runat="server" OnClick="lnkEmpDetails_Click"  >Employee Details</asp:LinkButton></li>
                            <li id="miPersonalDetails" runat="server">
                                <asp:LinkButton ID="lnkPersonalDetails" TabIndex = "4" runat="server" OnClick="lnkPersonalDetails_Click"  >Personal Details</asp:LinkButton></li>
                           
                        </ol>
    <div class="borderdiv" id="div_employee_details" runat="server" style="text-align:center;width:800px;" >
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
            <asp:DropDownList ID="DropMaritalStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropMaritalStatus_SelectedIndexChanged">
                <asp:ListItem Value="Single">Single</asp:ListItem>
                <asp:ListItem Value="Married">Married</asp:ListItem>
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
        <%--<tr><td>Bank Name</td><td>
        <asp:TextBox ID="txtBank" Enabled="false" runat="server"></asp:TextBox></td>
        <td>Bank A/C No.</td><td>            
        <asp:TextBox ID="txtBankAcc" Enabled="false" runat="server"></asp:TextBox></td></tr>--%>
        <tr><td>PF No.</td><td>
        <asp:TextBox ID="txtPF" Enabled="false" runat="server"></asp:TextBox></td>
        <td>ESI No.</td><td>            
        <asp:TextBox ID="txtESI" Enabled="false" runat="server"></asp:TextBox></td></tr> 
        <tr><td>PAN No.</td><td>
        <asp:TextBox ID="txtPAN" Enabled="false" runat="server"></asp:TextBox></td>
        <td>Confirmation Date</td><td>
        <asp:TextBox ID="txtConDate" Enabled="false" runat="server"></asp:TextBox></td>
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
        <tr><td style="height: 23px">Email ID</td><td style="height: 23px">
            <asp:TextBox ID="txtEmailid" runat="server"></asp:TextBox></td>
            <td colspan="2" style="height: 23px">
                <asp:CheckBox ID="CheckAddress" runat="server" Text="Copy the same address to the Permanent Address" AutoPostBack="True" OnCheckedChanged="CheckAddress_CheckedChanged" /></td>
            </tr>
        <tr><td colspan="4" align="center">
            <asp:Button ID="btnUpdate" CssClass="dpbutton" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td></tr>
            <tr><td>.</td></tr>
    </table>
    </div>
    <div class="borderdiv" id="Personal_Details"  style="width:950px;" runat="server">
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
  <td class="dpheader" colspan="4" style="width: 800px">
        <asp:Label ID="lblEduDetails" runat="server" Text="Educational Details"></asp:Label></td>
  </tr>
  <tr>
<td colspan="4" style="width: 800px"> 
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
    
    </td></tr></table></div>
    </form>
</body>
</html>

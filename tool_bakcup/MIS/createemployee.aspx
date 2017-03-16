<%@ page language="C#" autoeventwireup="true" CodeFile="createemployee.aspx.cs" inherits="createemployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="scripts/ValidationScript.js"></script>
    <script language="javascript" type="text/javascript">
        function calculateappraisaldate(obj)
        {
            if(obj.value!="")
            {
                var appdate=new Date(obj.value);
                alert(appdate.getMonth());
                appdate.setMonth(appdate.getMonth()+7);
                document.form1.nextappraisaldate.value= appdate.getMonth() + "/" + appdate.getDate() + "/" + appdate.getFullYear();
            }
        }
        
        function ValidationTxt()
        {
            if(document.form1.fname.value=="")
            {
                alert("Please give First Name");
                document.form1.fname.focus();
                return false;
            }
            if(document.form1.lastname.value=="")
            {
                alert("Please give Last Name");
                document.form1.lastname.focus();
                return false;
            }
            if(document.form1.username.value=="")
            {
                alert("Please give User Name");
                document.form1.username.focus();
                return false;
            }
            if(document.form1.employee_number.value=="")
            {
                alert("Please give Join Date");
                document.form1.employee_number.focus();
                return false;
            }
            if(document.form1.dateofbirth.value=="")
            {
                alert("Please give Birth Date");
                document.form1.dateofbirth.focus();
                return false;
            }
            if(document.form1.dateofjoin.value=="")
            {
                alert("Please give Join Date");
                document.form1.dateofjoin.focus();
                return false;
            }
            if(isNaN(document.form1.PLTxt.value) || document.form1.PLTxt.value>12)
            {
                alert("Please give only Numbers, Eligible PL leave with in 12 days");
                document.form1.PLTxt.focus();
                return false;
            }
           if(isNaN(document.form1.SLTxt.value) || document.form1.SLTxt.value>5)
            {
                alert("Please give only Numbers, Eligible SL leave with in 5 days");
                document.form1.SLTxt.focus();
                return false;
            }
            
             return true;   
        }
        function Validation2()
        {
            if(isNaN(document.form1.EmpNoNameTxt.value) && document.form1.EmpnoRBtn.checked )
            {
                alert("Please Enter Number Olny");
                document.form1.EmpNoNameTxt.focus();
                return false;
            }
            else if(document.form1.EmpNoNameTxt.value=="" && document.form1.EmpnoRBtn.checked)
            {
                alert("Please Give EmployeeNo");
                document.form1.EmpNoNameTxt.focus();
                return false;
            }
            else if(document.form1.EmpNoNameTxt.value=="" && document.form1.EmpNameRBtn2.checked)
            {
                alert("Please Give EmployeeName");
                document.form1.EmpNoNameTxt.focus();
                return false;
            }
        }
        function confirmation()
        {
            if(document.getElementById("Transfer").checked==false)
            {
                alert("Please select transfer employee option and then select transfer location icon");
                return false ;
            }
            if(confirm("Are you sure you want to transfer another location?"))
            {
                showhiddiv();
                return false;
             }  
            else
                return false;
        }
        function showhiddiv()
        {
            obj=document.getElementById("div_employee_details");
            //obj.className="div_style";
            obj.className="div_employee_details";
            obj2=document.getElementById("div_transfer_location");
            obj2.className="div_display";
        }
        function hidediv()
        {
            obj=document.getElementById("div_employee_details");
            //obj.className="div_style";
            obj.className="";
            obj2=document.getElementById("div_transfer_location");
            obj2.className="div_hide";
            document.getElementById("transfer_location").value=0;
        }
        function Checkvalue()
        {
          
            if(document.getElementById("transfer_location").value=="0")
            {
                alert("Please select Location");
                return false;    
            }
            else if(document.getElementById("transfer_location").value==document.getElementById("location").value)
            {
                alert("Current location and transfer location are same, So please select another location");
                return false;
            }
            return true;
        }
    
    </script>
    <style>
        div.div_employee_details
        {
            position: absolute;
            top: 0; /* These positions makes sure that the overlay */
            bottom: 0;  /* will cover the entire parent */
            left: 0;
            width: 100%;
            opacity:0.4;
             background: LightGrey;
            filter:alpha(opacity=30)
        }
        
    </style>
<link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">Employee Personal Records</div>
    <div class="borderdiv" id="SearchDiv" style="width:650px;" runat="server">
    
                <table style="width:100%;text-align:left;">
                    <tr>
                        <td colspan="3" class="HeadText">Search Employee</td>
                    </tr>
                    <tr>
                        <td colspan="3" >
                            By Employee No.<asp:RadioButton ID="EmpnoRBtn" runat="server" GroupName="Employee" OnCheckedChanged="EmpnoRBtn_CheckedChanged" AutoPostBack="true"/>
                            &nbsp;&nbsp;
                            By Employee Name<asp:RadioButton ID="EmpNameRBtn2" runat="server" GroupName="Employee" OnCheckedChanged="EmpnoRBtn_CheckedChanged" AutoPostBack="true" />
                            <%--ByEmpNo<asp:CheckBox ID="EmpnoCheckBox" runat="server" />&nbsp;&nbsp;
                            ByName<asp:CheckBox ID="NameCheckBox" runat="server" />--%>
                        </td>
                    </tr>
                    <tr >
                        <td style="padding-top:10pt;" width="20%">
                            <asp:Label ID="EmployeeLbl" runat="server" Text="Employee No."></asp:Label>
                        </td>
                        <td style="padding-top:10pt;" width="25%" > 
                            <asp:TextBox ID="EmpNoNameTxt" runat="server"></asp:TextBox>
                        </td>
                        <td style="padding-top:10pt;" >
                            <asp:Button CssClass="dpbutton" ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" OnClientClick="return Validation2();" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="ErrorDiv" runat="server" align="center" class="errorMsg" ></div>
            <div id="GridDiv" runat="server" style="width:650px;padding-bottom:10pt;margin-left: 10px;">
                <asp:DataGrid ID="EMPLOYEEGRID" AllowSorting="True" width="100%" runat="server" AutoGenerateColumns="False"
				AllowPaging="False" DataKeyField="EMPLOYEE_ID" BorderStyle="Solid" CssClass="bordertable"
				GridLines="None" BorderWidth="1px" CellPadding="3"  OnItemCommand="Employee_ItemCommand"    >
				<FooterStyle BackColor="Transparent"></FooterStyle>
				<HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Emp. No.">
						<ItemTemplate>
						    <asp:LinkButton ID="EmpNoId" runat="server" CommandName="Display" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EMPLOYEE_ID") %>'>
						        <%# DataBinder.Eval(Container.DataItem, "EMPLOYEE_NUMBER")%>
						    </asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Employee Name">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "FNAME") %>   <%# DataBinder.Eval(Container.DataItem,"SURNAME") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Location">
					    <ItemTemplate>
					        <%# DataBinder.Eval(Container.DataItem,"Location_name") %>
					    </ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Designation" >
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "DESIGNATION_NAME")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Date Of Joining">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "JOINED_DATE").ToString().Substring(0,DataBinder.Eval(Container.DataItem,"JOINED_DATE").ToString().IndexOf(" ")) %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Gender">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "GENDER") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Barcode">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "BARCODE") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "STATUS") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText=" ">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "EMPLOYEE_ID") %>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle Visible="False" NextPageText="" PrevPageText=""></PagerStyle>
			</asp:DataGrid>	
    </div>
    
    <div class="borderdiv" id="div_employee_details" style="text-align:center;width:650px;" >
        
    <table id="editemployee" style="width:100%;text-align:left;">
    <tr><td class="dpGreenHeader" align="center" colspan="5">Add/Edit Employees</td></tr>
    <tr><td style="height: 23px">First Name<font color="red">*</font></td><td style="height: 23px"><asp:TextBox ID="fname" runat="server" ></asp:TextBox> </td><td style="height: 23px">Employee Number</td><td style="height: 23px"><asp:TextBox ID="employee_number" runat="server" ></asp:TextBox></td></tr>
    <tr><td>Last Name<font color="red">*</font></td><td><asp:TextBox ID="lastname" runat="server" ></asp:TextBox></td><td>User Name<font color="red">*</font></td><td><asp:TextBox ID="username" runat="server" ></asp:TextBox><asp:ImageButton ImageUrl="~/images/uv2.jpg" AlternateText="UserValidity" ToolTip="User Validity" ID="UserValidBtn" runat="server" OnClick="UserValidBtn_Click" /><br /><asp:Label ID="ValidationLbl" CssClass="displaymsg" runat="server" Text=""></asp:Label></td></tr>
    <tr><td>Gender</td><td><asp:DropDownList ID="gender" runat="server"></asp:DropDownList></td><td>Date of Birth<font color="red">*</font></td><td><asp:TextBox ID="dateofbirth" runat="server" ></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=dateofbirth','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td></tr>
    <tr><td>Designation</td><td><asp:DropDownList ID="designation" DataTextField="designation_name" DataValueField="designation_id" runat="server" Width="200px"></asp:DropDownList></td><td>Date of Joining<font color="red">*</font></td><td><asp:TextBox ID="dateofjoin" runat="server"  ></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=dateofjoin','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td></tr><%--<asp:DropDownList Visible=false ID="department" runat="server"></asp:DropDownList>--%>
    <tr><td>Employee Team</td><td><asp:DropDownList ID="ddlempteam" DataTextField="employee_team_name" DataValueField="employee_team_id" runat="server" ></asp:DropDownList></td>
    <td>Report To</td><td><asp:DropDownList ID="ddlreportto" DataTextField="EMP_FULLNAME" DataValueField="EMPLOYEE_ID" runat="server" ></asp:DropDownList></td></tr>
    <tr><td >Email Address</td><td><asp:TextBox ID="Txtmailid" runat="server" Width="170px"></asp:TextBox></td><td>
        <asp:Label ID="nextappdatelbl" runat="server" Text="Date of Next Appraisal"></asp:Label></td><td><asp:TextBox ID="nextappraisaldate" runat="server" ></asp:TextBox></td></tr>
    <tr><td>Location</td><td><asp:DropDownList ID="location" DataTextField="location_name" DataValueField="location_id" runat="server" AutoPostBack="True" OnSelectedIndexChanged="location_SelectedIndexChanged"></asp:DropDownList></td><td>Date of Resignation</td><td><asp:TextBox ID="dateofresigned" runat="server" ></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=dateofresigned','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td></tr>
    <tr><td>Contact Address</td><td><asp:TextBox ID="address" Height="65px" Width="150px"  TextMode="MultiLine" runat="server" ></asp:TextBox></td><td>Permanent Address</td><td><asp:TextBox ID="paddress" Height="65px" Width="150px"  TextMode="MultiLine" runat="server"></asp:TextBox></td></tr>
    <tr><td style="height: 23px">Telephone No.</td><td style="height: 23px"><asp:TextBox ID="phoneno" runat="server"></asp:TextBox></td><td style="height: 23px">Mobile No.</td><td style="height: 23px"><asp:TextBox ID="mobileno" runat="server"></asp:TextBox></td></tr>
    <tr><td>Skill Set</td><td><asp:TextBox ID="skillset" Height="65px" Width="150px"  TextMode="MultiLine" runat="server" ></asp:TextBox></td>
    <td colspan="2"><table width="100%" ><tr><td width="40%" >Eligible PL</td><td ><asp:TextBox ID="PLTxt" runat="server"></asp:TextBox></td></tr>
    <tr><td  >Eligible SL</td><td><asp:TextBox ID="SLTxt" runat="server"></asp:TextBox></td></tr>
    </table></td></tr>
    <tr><td colspan="3" style="height: 60px"><input type="text" id="txtEmpId" value="" runat="server" disabled /><asp:HiddenField ID="EmpId_HF" runat="server" /></td><td style="height: 60px"><asp:CheckBox ID="obsolete" Checked=false Text="Remove Employee" runat="server" AutoPostBack="True" OnCheckedChanged="obsolete_CheckedChanged" /><br/><asp:CheckBox ID="Transfer" Checked=false Text="Transfer Employee" Enabled="false" runat="server" AutoPostBack="True" OnCheckedChanged="Transfer_CheckedChanged" /><asp:ImageButton ID="img_transfer" runat="server" ImageUrl="~/images/Transfer.jpeg" ToolTip="Transfer Location" Enabled="false" OnClientClick="return confirmation();" /> </td></tr>    
    <tr><td colspan="4"><div id="id_transfer_resigned" runat="server"><table style="border:solid 1px gray" width="100%"><tr><td >Comment</td><td><asp:TextBox ID="Comment" ForeColor="red" Font-Bold="true" Height="65px" Width="150px" TextMode="multiLine" runat="server" MaxLength="10"></asp:TextBox></td></tr></table></div></td></tr>
    <tr><td align="center" colspan="4"><asp:Button ID="btnSubmit" CssClass="dpbutton" Text="Add" runat="server" OnClientClick="return ValidationTxt();" OnClick="btnSubmit_Click" />&nbsp;&nbsp;<asp:Button  CssClass="dpbutton" ID="btnClear" Text="Clear" runat="server" OnClick="btnClear_Click" />  </td></tr>
        <asp:HiddenField ID="HFApp_id" runat="server" />
    </table>
    </div>
    <div id="div_transfer_location" runat="server" class="div_hide" ><table width="250px" style="border:solid 1px green"><tr><td colspan="2" align="center"><b>Transfer Location</b></td></tr><tr><td>Transfer Location</td><td><asp:DropDownList ID="Transfer_Location" runat="server" DataTextField="location_name" DataValueField="location_id"></asp:DropDownList></td></tr><tr><td colspan="2" align="center"><asp:Button ID="btn_transfer_ok" runat="Server" Text="Ok" CssClass="dpbutton" OnClick="btn_transfer_ok_Click" OnClientClick="return Checkvalue()" /><button id="btn_exit" onclick="hidediv()" Class="dpbutton">Exit</button></td></tr></table></div>
    </form>
    
</body>
</html>

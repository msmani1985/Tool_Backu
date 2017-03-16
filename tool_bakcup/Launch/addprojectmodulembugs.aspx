<%@ page language="C#" autoeventwireup="true" inherits="addprojectmodulembugs, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="default.css" rel="stylesheet"  type="text/css"/>
    <title>Project/Module/Module Bugs</title>
    <script language="javascript">
        function Validation()
        {
           // var Re=/^([1-31]+){1,2}\/([1-12]+){1,2}\/([\d]+){4,4}/;
            if(document.getElementById("projectnameTxt").value=="")
            {
                alert("You must give Project Name");
                document.getElementById("projectnameTxt").focus();
                return false;
            }
            if(document.getElementById("projecttypeDDList").value=="")
            {
                alert("You must select Project Type");
                document.getElementById("projecttypeDDList").focus();
                return false;
            }
            if(document.getElementById("receiveddateTxt").value=="")
            {
                alert("You must give Received Date ");
                document.getElementById("receiveddateTxt").focus();
                return false;
            }

            return true;
        }
        function Validation2()
        {
            //var Re=/^([1-31]+){1,2}\/([1-12]+){1,2}\/([\d]+){4,4}/;
            
            if(document.getElementById("projectDDList").value=="")
            {
                alert("Please select  Project");
                document.getElementById("projectDDList").focus();
                return false;
            }
            if(document.getElementById("mnameTxt").value=="")
            {
                alert("Please give Module Name");
                document.getElementById("mnameTxt").focus();
                return false;
            }
//            if(document.getElementById("mcreatedbyTxt").value=="")
//            {
//                alert("Please give Createby");
//                document.getElementById("mcreatedbyTxt").focus();
//                return false;
//            }

            if(document.getElementById("startdateTxt").value=="")
            {
                alert("Please give Start Date");
                document.getElementById("startdateTxt").focus();
                return false;
            }

//            if(document.getElementById("enddateTxt").value=="")
//            {
//                alert("Please give End Date");
//                document.getElementById("enddateTxt").focus();
//                return false;
//            }

            if(document.getElementById("duedateTxt").value=="")
            {
                alert("Please give Due Date");
                document.getElementById("duedateTxt").focus();
                return false;
            }

            
            return true;
        }
        function Validation3()
        {
           // var Re=/^([1-31]+){1,2}\/([1-12]+){1,2}\/([\d]+){4,4}/;
//            if(document.getElementById("bugcreatedbyTxt").value=="")
//            {
//                alert("Please give CreateBy");
//                document.getElementById("bugcreatedbyTxt").focus();
//                return false
//            }
//            if(document.getElementById("bugdgatedateTxt").value=="")
//            {
//                alert("Please give Delegate Date");
//                document.getElementById("bugdgatedateTxt").focus();
//                return false
//            }


            if(document.getElementById("BugProjDDList").value=="")
            {
                alert("Please select Project");
                document.getElementById("BugProjDDList").focus();
                return false
            }
            
            if(document.getElementById("ModuleDDList").value=="")
            {
                alert("Please select Module");
                document.getElementById("ModuleDDList").focus();
                return false
            }
            
            if(document.getElementById("bugnameTxt").value=="")
            {
                alert("Please give Bug Name");
                document.getElementById("bugnameTxt").focus();
                return false
            }

            
            if(document.getElementById("bugsdateTxt").value=="")
            {
                alert("Please give Start Date");
                document.getElementById("bugsdateTxt").focus();
                return false
            }

            
//            if(document.getElementById("bugenddateTxt").value=="")
//            {
//                alert("Please give End Date");
//                document.getElementById("bugenddateTxt").focus();
//                return false
//            }

            
            if(document.getElementById("bugduedateTxt").value=="")
            {
                alert("Please give Due Date");
                document.getElementById("bugduedateTxt").focus();
                return false
            }

            return true;
        }
        function ModuleConfirmationFn()
        {
        
            if(document.getElementById("AMenuDDList").value=="")
            {
                alert("Please give Available List");
                document.getElementById("AMenuDDList").focus();
                return false;
            }
            if(document.getElementById("startdateTxt").value=="")
            {
                alert("Please give Start Date");
                document.getElementById("startdateTxt").focus();
                return false;
            }
//            if(document.getElementById("enddateTxt").value=="")
//            {
//                alert("Please give End Date");
//                document.getElementById("enddateTxt").focus();
//                return false;
//            }
            if(document.getElementById("duedateTxt").value=="")
            {
                alert("Please give Due Date");
                document.getElementById("duedateTxt").focus();
                return false;
            }
            var combo1=document.getElementById("delegateDDList");
            //alert(combo1.options[combo1.selectedIndex].text);
            
            if(document.form1.moduleendChck.checked==false && document.form1.modulerejectChck.checked===false )
            {
                var answer=confirm("This Module delegate to " + combo1.options[combo1.selectedIndex].text +",It's Ok?");
                if (answer)
                    return true;
                else
                    return false;
            }
            return true;
        }
        function ModuleBugConfirmationFn()
        {
            if(document.getElementById("ModuleDDList").value=="")
            {
                alert("Please select Module");
                document.getElementById("ModuleDDList").focus();
                return false;
            }
            if(document.getElementById("AbugsDDList").value=="")
            {
                alert("Please select Available Bugs");
                document.getElementById("AbugsDDList").focus();
                return false;
            }
            if(document.getElementById("bugsdateTxt").value=="")
            {
                alert("Please give Start Date");
                document.getElementById("bugsdateTxt").focus();
                return false
            }

            
//            if(document.getElementById("bugenddateTxt").value=="")
//            {
//                alert("Please give End Date");
//                document.getElementById("bugenddateTxt").focus();
//                return false
//            }

            
            if(document.getElementById("bugduedateTxt").value=="")
            {
                alert("Please give Due Date");
                document.getElementById("bugduedateTxt").focus();
                return false
            }
            
            var combo1=document.getElementById("bugdelegatetoDDList");
            //alert(combo1.options[combo1.selectedIndex].text);
            if(document.form1.modulebugsChck.checked==false && document.form1.mbrejectChck.checked===false )
            {
                var answer=confirm("This ModuleBug delegate to " + combo1.options[combo1.selectedIndex].text +",It's Ok?");
                if (answer)
                    return true;
                else
                    return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="TitleDiv" class="dptitle" runat="server">
        Project
    </div>
    <div id="CommonDiv" runat="server" align="center">
        <table>
            <tr>
                <td style="height: 21px">
                    Type
                </td>
                <td style="height: 21px">
                    <asp:DropDownList ID="typeDDList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="typeDDList_SelectedIndexChanged">
                        <asp:ListItem Value="1">Project</asp:ListItem>
                        <asp:ListItem Value="2">Module</asp:ListItem>
                        <asp:ListItem Value="3">Module Bugs</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="ProjectDiv" runat="server" align="center">
        <table class="bordertable" cellpadding="2" cellspacing="2" width="500px">
            <tr>
                <td align="right">
                    <asp:Label  ID="Label1" runat="server"  Text="Project Name"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="projectnameTxt" runat="server"></asp:TextBox>
                </td>
               
            </tr>
            
            <tr>
                 <td align="right">
                    <asp:Label ID="Label2" runat="server" Text="Project Type"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="projecttypeDDList" DataTextField="project_type_name" DataValueField="project_type_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" >
                    <asp:Label ID="Label3" runat="server" Text="Project Owner"></asp:Label>
                </td>
                <td align="left" >
                    <asp:DropDownList ID="pownerDDList" DataValueField="employee_id" DataTextField="empname" runat="server">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td align="right" >
                    <asp:Label ID="Label4" runat="server" Text="Received Date"></asp:Label>
                </td>
                <td align="left" >
                    <asp:TextBox ID="receiveddateTxt" runat="server" ></asp:TextBox> <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=receiveddateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label5" runat="server" Text="Description"></asp:Label>
                </td>
                <td  align="left">
                    <asp:TextBox ID="descriptionTxt" TextMode="multiline" runat="server" Width="220px" ></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="projectSaveBtn" runat="server" Text="Create" OnClientClick="return Validation();" OnClick="projectSaveBtn_Click" />
                    
                </td>
            </tr>
        </table>
    </div>
    
    <div id="ModuleDiv" align="center" runat="server">
        <table class="bordertable" width="500px">
            <tr>
                <td align="right" style="height: 21px" >
                    <asp:Label ID="Label6" runat="server" Text="Project"></asp:Label>
                </td>
                <td align="left" style="height: 21px" >
                    <asp:DropDownList ID="projectDDList" runat="server" DataValueField="project_id" DataTextField="project_name" AutoPostBack="True" OnSelectedIndexChanged="projectDDList_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label26" runat="server" Text="Available Module"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="AMenuDDList" runat="server" DataTextField="module_name" DataValueField="module_id" AutoPostBack="True" OnSelectedIndexChanged="AMenuDDList_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" >
                    <asp:Label ID="Label7" runat="server" Text="Module Name"></asp:Label>
                </td>
                <td align="left" >
                    <asp:TextBox ID="mnameTxt" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    <asp:Label ID="Label8" runat="server" Text="Module Type"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="mtypeDDList" DataTextField="module_type_name" DataValueField="module_type_id" runat="server">
                    </asp:DropDownList>
                </td>
               
            </tr>
            
            <tr>
                <td align="right">
                    <asp:Label ID="Label15" runat="server" Text="DelegateTo"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="delegateDDList" runat="server" DataTextField="empname" DataValueField="employee_id">
                    </asp:DropDownList>
                </td>
               
            </tr>
            <tr>
                <td align="right">
                   <asp:Label ID="Label17" runat="server" Text="Delegate Date"></asp:Label>
               </td>
               <td align="left">
                   <asp:Label ID="delegatedateLbl" runat="server" Text="Label"></asp:Label>
               </td>
            </tr>
            <tr>
                 <td align="right">
                    <asp:Label ID="Label9" runat="server" Text="Created By"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="mcreatedbyTxtLbl" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label18" runat="server" Text="Start Date"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="startdateTxt" runat="server" ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=startdateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                
            </tr>
            <tr>
                <td align="right" style="height: 27px">
                    <asp:Label ID="Label19" runat="server" Text="End Date"></asp:Label>
                </td>
                <td align="left" style="height: 27px">
                    <asp:TextBox ID="enddateTxt" runat="server" ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=enddateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label20" runat="server" Text="Due Date"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="duedateTxt" runat="server" ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=duedateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                 
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label10" runat="server" Text="Module Description"></asp:Label>
                </td>
                <td align="left" > 
                    <asp:TextBox TextMode="MultiLine" ID="mdescriptionTxt" runat="server" Width="200px" Height="32px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label29" runat="server" Text="Module Ended"></asp:Label>
                </td>
                <td align="left">
                    <asp:CheckBox ID="moduleendChck" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Label ID="Label30" runat="server" Text="Moduel Rejected"></asp:Label>
                    <asp:CheckBox ID="modulerejectChck" runat="server" />
                </td>
            </tr>
            
            <tr>
                <td colspan="2" align="center" style="height: 22px">
                    <asp:Button ID="ModuleSaveBtn" runat="server" Text="Add" OnClientClick="return Validation2();" OnClick="ModuleSaveBtn_Click" />
                    <asp:Button ID="ModuleEditBtn" runat="server" Text="Update" OnClick="ModuleEditBtn_Click" OnClientClick="return ModuleConfirmationFn();"  />
                    <asp:Button ID="ModuelClearBtn" runat="server" Text="Clear" OnClick="ModuelClearBtn_Click" />
                </td>
            </tr>
        </table>
    </div>
  
    <div id="BugModuleDiv" runat="server" align="center" >
        <table class="bordertable" width="500px">
            <tr>
                <td align="right" >
                    <asp:Label ID="Label11" runat="server" Text="Project"></asp:Label>
                </td>
                <td align="left" >
                    <asp:DropDownList ID="BugProjDDList" runat="server" DataTextField="project_name" DataValueField="project_id" AutoPostBack="True" OnSelectedIndexChanged="BugProjDDList_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
               
               
            </tr>
            <tr>
                 <td align="right" >
                    <asp:Label ID="Label16" runat="server" Text="Module"></asp:Label>
                </td>
                <td align="left" >
                    <asp:DropDownList ID="ModuleDDList" runat="server" DataTextField="module_name" DataValueField="module_id" OnSelectedIndexChanged="ModuleDDList_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label28" runat="server" Text="Available Bugs"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="AbugsDDList" runat="server" DataTextField="bug_name" DataValueField="bug_id" AutoPostBack="True" OnSelectedIndexChanged="AbugsDDList_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 23px">
                    <asp:Label ID="Label27" runat="server" Text="Bug Name"></asp:Label>
                </td>
                <td align="left" style="height: 23px">
                    <asp:TextBox ID="bugnameTxt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label12" runat="server" Text="Bug Type"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="bugtypeDDList" runat="server" DataValueField="bug_type_id" DataTextField="bug_type_name">
                    </asp:DropDownList>
                </td>
                
            </tr>
           
            <tr>
                <td align="right">
                    <asp:Label ID="Label21" runat="server" Text="DelegateTo"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="bugdelegatetoDDList" runat="server" DataValueField="employee_id" DataTextField="empname">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label22" runat="server" Text="Delegate Date"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="bugdgatedateLbl" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
             <tr>
                <td align="right">
                    <asp:Label ID="Label13" runat="server" Text="Created By"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="bugcreatedbyLbl" runat="server" Text="Label"></asp:Label>
                </td>
               
            </tr>
            <tr>
                <td align="right">    
                    <asp:Label ID="Label23" runat="server" Text="Start Date"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="bugsdateTxt" runat="server" ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=bugsdateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                
            </tr>
            <tr>    
                <td align="right" style="height: 27px">
                    <asp:Label ID="Label24" runat="server" Text="End Date"></asp:Label>
                </td>
                <td align="left" style="height: 27px">
                    <asp:TextBox ID="bugenddateTxt" runat="server" ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=bugenddateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label25" runat="server" Text="Due Date"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="bugduedateTxt" runat="server" ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=bugduedateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                 
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label14" runat="server" Text="Description"></asp:Label>
                </td>
                <td  align="left">
                    <asp:TextBox ID="bugdescriptionTxt" TextMode="multiLine" runat="server" Width="226px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label31" runat="server" Text="ModuleBugs Ended "></asp:Label>
                </td>
                <td align="left">
                    <asp:CheckBox ID="modulebugsChck" runat="server" />&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label32" runat="server" Text="ModuleBugs Rejected"></asp:Label>
                    <asp:CheckBox ID="mbrejectChck" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    
                </td>
                <td align="left">
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" >
                    <asp:Button ID="bugmodulesaveBtn" runat="server" Text="Create" OnClick="bugmodulesaveBtn_Click" OnClientClick="return Validation3();" />
                    <asp:Button ID="bugmoduleeditBtn" runat="server" Text="Update" OnClick="bugmoduleeditBtn_Click" OnClientClick="return ModuleBugConfirmationFn();" />
                    <asp:Button ID="bugClearBtn" runat="server" Text="Clear" OnClick="bugClearBtn_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="sqlfrmsignoff, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Time Sheet Page</title>
    <link href="default.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function showhiddiv(obj1,obj2)
    {
        //obj1="employeetimesheet";
        //obj2="UpdateDiv";
        
        obj1div=document.getElementById(obj1);
        obj1div.className="employeetimesheet";
        obj2div=document.getElementById(obj2);
        obj2div.className="div_small";
        return false;
    }
    function hidediv(obj1,obj2)
    {
        obj1div=document.getElementById(obj1);
        obj1div.className="";
        obj2div=document.getElementById(obj2);
        obj2div.className="div_hide";
    }
    
    function showsplit()
    {
        obj1=document.getElementById("employeetimesheet");
        obj1.className="employeetimesheet";
        obj2=document.getElementById("UpdateDiv");
        obj2.className="div_small";
        obj3=document.getElementById("InsertDiv");
        obj3.className="div_small2";
    }
    function hidesplit()
    {
        obj1=document.getElementById("employeetimesheet");
        obj1.className="";
        obj2=document.getElementById("UpdateDiv");
        obj2.className="div_hide";
        obj3=document.getElementById("InsertDiv");
        obj3.className="div_hide";
    }
    
    function validate(str)
    {
        alert('Invalid Date format,Please give correct format \'MM/DD/YYYY 00:00:00 AM(PM)\'');
        if(str=="insert")
            showsplit();
        else
            showhiddiv('employeetimesheet','UpdateDiv');
    }
   /* function validation(str)
    {
       // var RegEx = /^((((([13578])|(1[0-2]))[\-\/\s]?(([1-9])|([1-2][0-9])|(3[01])))|((([469])|(11))[\-\/\s]?(([1-9])|([1-2][0-9])|(30)))|(2[\-\/\s]?(([1-9])|([1-2][0-9]))))[\-\/\s]?\d{4})(\s((([1-9])|(1[02]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$/;
        //var RegEx= /^[0-3][0-9]\:[0-1][0-9]\:[0-2][0-9][0-9][0-9]\:[0-1][0-2]\:[0-5][0-9]\:[0-5][0-9]$/;
        var RegEx=/^((((([13578])|(1[0-2]))[\-\/\s]?(([1-9])|([1-2][0-9])|(3[01])))|((([469])|(11))[\-\/\s]?(([1-9])|([1-2][0-9])|(30)))|(2[\-\/\s]?(([1-9])|([1-2][0-9]))))[\-\/\s]?\d{4})(\s((([0-9][0-9])|(1[02]))\:([0-5][0-9])((\s )|(\:([0-5][0-9])))))?$/;

        alert(RegEx);
        alert(document.getElementById("USDateTxt").value.match(RegEx));
        //alert(obj.value);
        if(!document.getElementById("USDateTxt").value.match(RegEx))
        {
            alert("Invalid Date Format");
            document.getElementById("USDateTxt").focus();
            return false;
        }
        if(!document.getElementById("UEDateTxt").value.match(RegEx))
        {
            alert("Invalid Date Format");
            document.getElementById("UEDateTxt").focus();
            alert("enddate");
            return false;
        }
        if(str!="Insert")
            return true
        //split button click validate both update text and insert text
        if(!document.getElementById("ISDateTxt").value.match(RegEx))
        {
            alert("Invalid Date Format");
            document.getElementById("ISDateTxt").focus();
            return false;
        }
        if(!document.getElementById("IEDateTxt").value.match(RegEx))
        {
            alert("Invalid Date Format");
            alert(document.getElementById("IEDateTxt").value);
            document.getElementById("IEDateTxt").focus();
            return false;
        }
        return true;
    }
    */
    function Validation(source,args)
        {
            var elem=document.getElementById('txtJobNumber').value;
            if(isNaN(elem))
                args.IsValid=false;
            else
                args.IsVaild=true;
        }
    function Delete_Record()
    {
        if(confirm("Are you sure, you want to delete this Record?"))
            return true;
        else
            return false;
    }
    </script>
    <style>
        div.employeetimesheet
        {
            position: absolute;
            top: 0; /* These positions makes sure that the overlay */
            bottom: 0;  /* will cover the entire parent */
            left: 0;
            width: 100%;
            opacity:0.4;
             background: LightGrey;
            filter:alpha(opacity=50)
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="invtitle" runat="server" >
        Time Sheet Sign Off
    </div>
    <div id="employeetimesheet">
        <table width="700px" class="bordertable"  style="padding-top:20px; padding-bottom:20px;" align="center" >
            <tr>
                <td align="right">
                    Employee 
                </td>
                <td >
                    <asp:DropDownList ID="Employeeddlist" DataTextField="EMP_FULLNAME" DataValueField="EMPLOYEE_ID" runat="server">
                    </asp:DropDownList>
                </td>
                 <td align="right" valign="middle">
                    Start Time
                </td>
                <td>
                    <asp:TextBox ID="SDateTxt" runat="server"></asp:TextBox>&nbsp;
                    <img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=SDateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" />
                </td>
                <td align="right">
                    End Time
                </td>
                <td>
                    <asp:TextBox ID="EDateTxt" runat="server"></asp:TextBox>&nbsp;
                    <img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=EDateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" />
                </td>
            </tr>
            <tr>
            <td colspan="6" align="center">
                <asp:Button ID="Submit_Btn" runat="server"  CssClass="dpbutton" Text="Submit" OnClick="Submit_Btn_Click" />&nbsp;
            </td>
            </tr>
        </table>  
    </div>
    <br />
    <div id="UpdateDiv" runat="server" class="div_hide">
        <table width="550px" align="Center" class="bordertable">
            <tr><td colspan="4" align="center" class="dpGreentitle">Update Time</td></tr>
            <tr>
                <td>Start Time</td><td><asp:TextBox ID="USDateTxt" runat="server"></asp:TextBox></td>
                <td>End Time</td><td><asp:TextBox ID="UEDateTxt" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="loggedid" runat="server" />
                </td>
            </tr>
            <tr><td colspan="4" align="center">
                    <asp:Button ID="Update_Btn" CssClass="dpbutton" runat="server" Text="Update" OnClick="Update_Btn_Click"  />
                    &nbsp; <button id="btnexit" runat="server" value="Exit" onclick="hidediv('employeetimesheet','UpdateDiv');" style="font-family: Verdana, Tahoma, Sans-Serif">Exit</button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    
    <div class="div_hide" id="InsertDiv"  runat="server" >
            <table style="width:550px" id="InsertTable" align="center">
                <tr><td colspan="4" align="center" class="dpGreentitle">Insert Task</td></tr>
                <tr>
                    <td  align="left">
                        Task Name:
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlstyles" DataTextField="task_name" DataValueField="task_id" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList Visible="false"  ID="ddltask_type" DataTextField="task_id" DataValueField="task_type_id" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr><td>Job Type</td>
                    <td><asp:DropDownList ID="ddljobtype" runat="server">
                        <asp:ListItem Text="Issue" Value="6" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Article" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Value="7" text="Chapter"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Module" Value="8"></asp:ListItem>
                    </asp:DropDownList></td>
                    <td>Job No.</td>
                    <td><asp:TextBox ID="txtJobNumber" runat="server" Text=""></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Enter Number only" ClientValidationFunction="Validation" ControlToValidate="txtJobNumber"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" >
                        Start Time
                    </td>
                     <td >
                        <asp:TextBox ID="ISDateTxt" runat="server"></asp:TextBox>
                    </td>
                    <td align="left">
                        End Time
                    </td>
                    <td >
                        <asp:TextBox ID="IEDateTxt" runat="server"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <td align="left">Pages</td>
                    <td><asp:TextBox ID="TxtPages" runat="server" Text=""></asp:TextBox></td>
                    <td align="left">Comments</td>
                    <td><asp:TextBox ID="Txtcmnts" runat="server" Text=""></asp:TextBox></td>
               </tr>
                <tr>
                     <td colspan="4" align="center">
                        <asp:Button ID="InsertBtn" runat="server" CssClass="dpbutton" Text="Insert" OnClick="InsertBtn_Click" />
                        &nbsp;<button id="InbtnExit" value="Exit" onclick="hidesplit();" style="font-family: Verdana, Tahoma, Sans-Serif">Exit</button>
                        <asp:HiddenField ID="JobIdHField" runat="server" />
                    </td>
                </tr>
            </table>
       </div>
    <br />
    <div id="DivError" class="errorMsg" runat="server">
    </div>
    <div id="DivEmp" runat="server">
        <table align="center" width="750px">
        <tr><td align="right"><asp:Button CssClass="dpbutton" ID="signoff_btn" Text="Sign Off" runat="server" OnClick="signoff_btn_Click" /></td></tr>
        <tr><td>
            <asp:GridView ID="GVEmployeeTask" runat="server" AutoGenerateColumns="false" CssClass="lightbackground"
             HeaderStyle-CssClass="darkbackground"  BorderColor="Black" BorderWidth="1px" OnRowCommand="GVEmployeeTask_RowCommand" 
             OnDataBound="GVEmployeeTask_DataBound" OnRowDataBound="GvEmployeeTask_RowDataBound" width="750px"
            >
            <Columns>
                <asp:BoundField HeaderText="JobType" DataField="job_type_name" SortExpression="job_type_name" />
                <asp:BoundField HeaderText="Code" DataField="code" SortExpression="code" />
                <asp:BoundField HeaderText="Event" DataField="task_name" SortExpression="task_name" />
                <asp:BoundField HeaderText="Start Date" DataField="start_time" SortExpression="start_time" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HtmlEncode="false"  />
                <asp:BoundField HeaderText="End Date" DataField="end_time" SortExpression="end_time" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HtmlEncode="false" />
                <asp:BoundField HeaderText="Stage" DataField="job_stage_name" SortExpression="job_stage_name" />
                <asp:BoundField HeaderText="Pages" DataField="achived_value" SortExpression="achived_value" />
                <asp:BoundField HeaderText="Comments" DataField="comments" SortExpression="comments" />
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="HFAppEmpid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"approved_employee_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" AlternateText="Edit" runat="server" CausesValidation="false" CommandName="Edit1"
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"loggedevent_id") %>' ImageUrl="~/images/tools/edit.png" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnSplit" AlternateText="Split" runat="server" CausesValidation="false" CommandName="Split" 
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"loggedevent_id")%>' ImageUrl="~/images/tools/SplitBtn.png" ToolTip="Split" />
                         
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="Btn_Delete" AlternateText="Delete" runat="server" CausesValidation="false" CommandName="cmd_Delete"
                         OnClientClick="return Delete_Record();" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"loggedevent_id") %>' ImageUrl="~/images/tools/no.png" ToolTip="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
            <asp:HiddenField ID="colorrowindex" runat="server" />
        </td></tr>
        </table>
    </div>
    </form>
</body>
</html>

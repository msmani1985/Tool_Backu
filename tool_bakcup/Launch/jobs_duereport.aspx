<%@ page language="C#" autoeventwireup="true" inherits="jobs_duereport, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Jobs Report - Duedate</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script src="Javascript/Script.min.js" type="text/javascript"></script>
    <script language="javascript">
        $(document).ready(function()
        {
            if($("#ch_catsdueoption").is(':checked'))
              {
                $("#dd_catsduedatelist").show();
                $("#lbl_duedate").attr('disabled',true);
                $("#dd_duedatelist").attr('disabled',true);
                $("p").css('color','red');
              }
              else
              {
                $("#dd_catsduedatelist").hide();
                $("#lbl_duedate").attr('disabled',false);
                $("#dd_duedatelist").attr('disabled',false);
                $("#lbl_duedate").css('color','red');
                $("p").css('color','black');
              }
            $("#ch_catsdueoption").change(function()
            {
              if($("#ch_catsdueoption").is(':checked'))
              {
                $("#dd_catsduedatelist").show();
                $("#lbl_duedate").attr('disabled',true);
                $("#dd_duedatelist").attr('disabled',true);
                $("p").css('color','red');
              }
              else
              {
                $("#dd_catsduedatelist").hide();
                $("#lbl_duedate").attr('disabled',false);
                $("#dd_duedatelist").attr('disabled',false);
                $("#lbl_duedate").css('color','red');
                $("p").css('color','black');
              }
              
              
            }
            );
        }
        );
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Jobs Due
    </div>
    <div>
        <table align="center" class="bordertable" cellpadding="2" cellspacing="2" >
            <tr><td>Customer</td><td><asp:DropDownList ID="dd_customerlist" DataValueField="customer_id" DataTextField="cust_name" runat="server"></asp:DropDownList></td><td>Job Type</td>
            <td><asp:DropDownList ID="dd_jobtype" runat="server"><asp:ListItem Text="Issue" Value="6" Selected="true"></asp:ListItem><asp:ListItem Text="Article" Value="5"></asp:ListItem><asp:ListItem Text="Book" Value="2"></asp:ListItem><asp:ListItem Text="Chapter" Value="7"></asp:ListItem><asp:ListItem Text="Project" Value="4"></asp:ListItem><asp:ListItem Text="Module" Value="8"></asp:ListItem></asp:DropDownList></td>
            <td rowspan="3" valign="bottom"><asp:Button ID="btn_submit" Text="Submit" CssClass="dpbutton" runat="server" OnClick="btn_submit_Click" /></td>
            </tr>
            <tr><td colspan="4" height="2px" style="background-image:url('Images/line.gif');background-repeat:repeat-x;"></td></tr>
            <tr><td ><asp:Label ID="lbl_duedate" Text="Due Date" runat="server"></asp:Label></td><td colspan="3"><asp:DropDownList ID="dd_duedatelist" runat="server"><asp:ListItem Text="0 Day Left" Value="0"></asp:ListItem><asp:ListItem Text="1 Day Left" Value="1"></asp:ListItem><asp:ListItem Text="2 Days Left" Value="2"></asp:ListItem><asp:ListItem Text="3 Days Left" Value="3"></asp:ListItem></asp:DropDownList></td>
            </tr>
            <tr>
            <td colspan="4"><table><tr><td><p><asp:CheckBox ID="ch_catsdueoption" runat="server" Text="CATs DueDate" /></p></td>
            <%--<td><asp:Label ID="lbl_catsdue" Text="CATs Due Date" runat="server"></asp:Label></td>--%><td ><asp:DropDownList ID="dd_catsduedatelist" runat="server"><asp:ListItem Text="0 Day Left" Value="0"></asp:ListItem><asp:ListItem Text="1 Day Left" Value="1"></asp:ListItem><asp:ListItem Text="2 Days Left" Value="2"></asp:ListItem><asp:ListItem Text="3 Days Left" Value="3"></asp:ListItem></asp:DropDownList></td>
            </tr></table>
            </td>
            
            </tr>
            
            
        </table>
    </div>
    <br />
    <div align="center">
    <table width="100%"><tr><td align="right"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  /></td></tr>
    <tr><td><asp:GridView ID="gv_duedatedetails" HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" AutoGenerateColumns="true" runat="server"></asp:GridView></td></tr>
    <tr><td><asp:Label ID="lbl_error" runat="server" Text="" CssClass="error"></asp:Label></td></tr>
    </table>

    
    </div>
    </form>
</body>
</html>

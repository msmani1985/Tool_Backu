<%@ page language="C#" autoeventwireup="true" inherits="jobs_duereportib, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Jobs Due Report Page</title>
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
        Artwork Dues Report
    </div>
    <div >
        <table align="center" class="bordertable" cellpadding="2" cellspacing="2" >
        <tr><td>Customer</td><td><asp:DropDownList ID="dd_customerlist" DataValueField="CUSTNO" DataTextField="CUSTNAME" runat="server"></asp:DropDownList></td>
        <td><asp:Label ID="lbl_jobtype" Text="Job Type" runat="server" Visible="false"></asp:Label></td><td><asp:DropDownList ID="dd_jobtype" runat="server" Visible="false"><asp:ListItem Text="Issue" Value="6" Selected="true"></asp:ListItem><asp:ListItem Text="Article" Value="5"></asp:ListItem></asp:DropDownList></td>
        <td rowspan="3" valign="bottom"><asp:Button ID="btn_submit" Text="Submit" CssClass="dpbutton" runat="server" OnClick="btn_submit_Click" /></td>
        </tr>
        <tr><td colspan="4" height="2px" style="background-image:url('Images/line.gif');background-repeat:repeat-x;"></td></tr>
        <tr><td colspan="4">
            <table><tr><td><asp:Label ID="lbl_duedate" Text="Due Date" runat="server"></asp:Label></td>
            <td ><asp:DropDownList ID="dd_duedatelist" runat="server"><asp:ListItem Text="0 Day Left" Value="0"></asp:ListItem><asp:ListItem Text="1 Day Left" Value="1"></asp:ListItem><asp:ListItem Text="2 Days Left" Value="2"></asp:ListItem><asp:ListItem Text="3 Days Left" Value="3"></asp:ListItem>
        <asp:ListItem Text="4 Days Left" Value="4"></asp:ListItem><asp:ListItem Text="5 Days Left" Value="5"></asp:ListItem><asp:ListItem Text="6 Days Left" Value="6"></asp:ListItem><asp:ListItem Text="7 Days Left" Value="7"></asp:ListItem>
        <asp:ListItem Text="8 Days Left" Value="8"></asp:ListItem><asp:ListItem Text="9 Days Left" Value="9"></asp:ListItem><asp:ListItem Text="10 Days Left" Value="10"></asp:ListItem>
        </asp:DropDownList></td>
        <td>&nbsp;</td>
        <td><p><asp:CheckBox ID="ch_catsdueoption" runat="server" Text="CATs DueDate" /></p></td>
        <td ><asp:DropDownList ID="dd_catsduedatelist" runat="server"><asp:ListItem Text="0 Day Left" Value="0"></asp:ListItem><asp:ListItem Text="1 Day Left" Value="1"></asp:ListItem><asp:ListItem Text="2 Days Left" Value="2"></asp:ListItem><asp:ListItem Text="3 Days Left" Value="3"></asp:ListItem></asp:DropDownList></td>
         </tr></table>
        </td></tr>
        <tr>
        
        
        </tr>
        </table>
    </div>
    <br />
    <div id="div_jobduedetails" runat="server" align="center">
    <table><tr><td align="right"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  /></td></tr>
    <tr><td> <asp:GridView ID="gv_jobsdue" runat="server" AutoGenerateColumns="false" RowStyle-HorizontalAlign="left"
    HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground">
    <Columns>
        <asp:BoundField HeaderText="Article Id" DataField="job_id" />
        <asp:BoundField HeaderText="Article Barcode" DataField="ABARCODE" />
        <asp:BoundField HeaderText="Article Code" DataField="AARTICLECODE" />
        <asp:BoundField HeaderText="Stage" DataField="STYPENAME" />
        <asp:BoundField HeaderText="Art Work" DataField="AARTWORKPIECES" />
        <asp:BoundField HeaderText="No. of Colours" DataField="ARTNOCOLOUR" />
        <asp:BoundField HeaderText="Due Date" DataField="ADUEDATE" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
        <asp:BoundField HeaderText="Cats Due Date" DataField="CATSDUEDATE" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
        <asp:BoundField HeaderText="Received Date" DataField="ACREATIONDATE" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
        <asp:BoundField HeaderText="Art Barcode" DataField="ARTBARCODE" />
        <asp:BoundField HeaderText="Art Name" DataField="ARTNAME" />
    </Columns>
    </asp:GridView></td></tr>
    </table>
    </div>
    <div id="div_error" runat="server" class="errorMsg">No Records Found</div>
    </form>
</body>
</html>

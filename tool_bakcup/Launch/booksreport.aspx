<%@ page language="C#" autoeventwireup="true" inherits="booksreport, App_Web_olx2vwmy" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Books Report Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <style>
    .headerbackcolor
        {
            background-color:#EAFEE2; /*WhiteSmoke;*/
            border-bottom:solid 2px Gray;
            color:Green;
            font-size:10pt;font-weight:bold;
        }
        .div_bookdetails
        {
            /*position: absolute;*/
            /*top: 0; /* These positions makes sure that the overlay */
            /*bottom: 0;  /* will cover the entire parent */
            /*left: 0;*/
            width: 100%;
            opacity:0.4;
             background: LightGrey;
            filter:alpha(opacity=30)
        }
        .showdiv
        {
            visibility:visible;
            /*position:absolute;*/
        }
    
    </style>
    <script type="text/javascript" src="Javascript/Script.min.js"></script>
    <script language="javascript" type="text/javascript" >
    
    function getxyposition(e)
        {
            //$("#hf_posx").val(e.pageX);
            //$("#hf_posy").val(e.pageY);
            document.getElementById("hf_posx").value=event.x;
            document.getElementById("hf_posy").value=event.y;
            alert(document.getElementById("hf_posx").value);
            alert(document.getElementById("hf_posy").value);
            document.getElementById("div_chapterdetails").style.visibility='visible';
            return false;
        }
        
        function displaydiv()
        {
            document.getElementById("div_chapterdetails").style.visibility='visible';
            document.getElementById("div_chapterdetails").style.top = document.getElementById("hf_posy").value;
            document.getElementById("div_chapterdetails").style.left = "150px";
            document.getElementById("div_chapterdetails").style.position = "absolute";
//            document.getElementById("div_bookdetails").style.opacity=0.4;
//            document.getElementById("div_bookdetails").style.filter="alpha(opacity=40)";
//            document.getElementById("div_bookdetails").style.background="background";
//            //document.getElementById("div_bookdetails").className="div_bookdetails";
            document.form1.className="div_bookdetails";
        }
        function hidediv()
        {
            document.getElementById("div_chapterdetails").className="div_hide";
            document.form1.className="showdiv";
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Books Report
    </div>
    <div align="center">
        <table class="bordertable"><tr><td>
            Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td align="left">
        <asp:RadioButtonList ID="rb_type" runat="server" RepeatDirection="Horizontal"><asp:ListItem Text="Live" Value="Live"></asp:ListItem>
        <asp:ListItem Text="Despatch" Value="Despatch"></asp:ListItem><asp:ListItem Text="Invoiced" Value="Invoiced"></asp:ListItem>
        </asp:RadioButtonList>
        </td>
        <td>&nbsp;<asp:Button ID="btn_submit" Text="Submit" CssClass="dpbutton" runat="server" OnClick="btn_submit_Click" /></td>
        </tr>
        </table>
         <asp:HiddenField ID="hf_posx" runat="server" Value=""  />
        <asp:HiddenField ID="hf_posy" runat="server" Value=""  />
    </div>
    <br />
    <div id="div_bookdetails" runat="server" align="center">
    <table>
    <tr><td align="right"><asp:ImageButton ID="img_bookexport" ImageUrl="~/images/tools/j_excel.png" runat="server" ToolTip="Export Excel" OnClick="img_bookexport_Click" /></td></tr>
    <tr><td>
    <asp:GridView ID="gv_bookdetails" runat="server" AutoGenerateColumns="false" CssClass="lightbackground" 
        HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" OnRowCommand="bookdetails_rowcommand"
        >
        <Columns>
            <asp:BoundField DataField="parent_job_id" HeaderText="Job No." />
            <asp:BoundField DataField="cust_name" HeaderText="Customer" />
            <asp:BoundField DataField="name" HeaderText="Book No." />
            <asp:BoundField DataField="title" HeaderText="Book Title" />
            <asp:TemplateField HeaderText="chaptercount">
                <ItemTemplate><asp:LinkButton ID="lnk_chapter" CommandName="Chapter" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"parent_job_id") %>' runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"chaptercount") %>'></asp:LinkButton></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="invoice_no" HeaderText="Invoice No." />
            <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
        </Columns>
        </asp:GridView>
    </td></tr>
    </table>
        
    </div>
    <br />
    <div id="div_chapterdetails" runat="server" style="visibility:hidden;">
        <table class="bordertable"><tr class="headerbackcolor"><td style="border-bottom:solid 1px green;">Logged Events</td>
        <td align="right"><input type="button" id="btn_close" class="dpbutton" value="Close" onclick="hidediv();" />&nbsp;
        <input type="button" onclick="exportexcel('loggedevents')" value="Export Excel" src="~/images/tools/j_excel.png" id="img_excelexport" />
        </td>
        </tr>
        <tr><td colspan="2">
            <asp:GridView ID="gv_eventdetails" runat="server" AutoGenerateColumns="false" CssClass="lightbackground"
            HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" Caption="<font size='3' color='green'><b>Logged Events</b></font>">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="Job" />
                <asp:BoundField DataField="job_stage_name" HeaderText="Job Stage" />
                <asp:BoundField DataField="task_name" HeaderText="Task" />
                <asp:BoundField DataField="start_time" HeaderText="Start Time" />
                <asp:BoundField DataField="end_time" HeaderText="End Time" />
                <asp:BoundField DataField="total_time" HeaderText="Duration" />
                <asp:BoundField DataField="fname" HeaderText="Employee" />
                <asp:BoundField DataField="comments" HeaderText="Comments" />
            </Columns>
            </asp:GridView>
        </td></tr>
        <tr><td colspan="2">&nbsp;</td></tr>
        <tr class="headerbackcolor"><td>Chapter</td><td align="right">
        &nbsp;<asp:ImageButton ID="imgbtnEventExport2" ImageUrl="~/images/tools/j_excel.png" runat="server" ToolTip="Export Excel" /></td></tr>
        <tr><td colspan="2"><asp:GridView ID="gv_chapterdetails" runat="server" AutoGenerateColumns="false" CssClass="lightbackground"
        HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" Caption="<font size='3' color='green'><b>Chapter</b></font>">
        <Columns>
            <asp:BoundField DataField="name" HeaderText="Job" />
            <asp:BoundField DataField="job_stage_name" HeaderText="Job Stage" />
            <asp:BoundField DataField="task_name" HeaderText="Task" />
            <asp:BoundField DataField="start_time" HeaderText="Start Time" />
            <asp:BoundField DataField="end_time" HeaderText="End Time" />
            <asp:BoundField DataField="total_time" HeaderText="Duration" />
            <asp:BoundField DataField="fname"  HeaderText="Employee" />
            <asp:BoundField DataField="comments" HeaderText="Comments" />
        </Columns>
        </asp:GridView></td></tr>
        </table>
       
    </div>
    <br />
    <div id="div_error" runat="server" class="errorMsg" visible="false">No Records </div>
    </form>
    <script language="javascript" type="text/javascript">
        var xmlHTTP;
        function exportexcel()
        {
        alert("test");
            try
            {
                // Firefox, Opera 8.0+, Safari
                xmlHTTP=new xmlHTTPRequest();
            }
            catch(e)
            {
                // Internet Explorer
                 try
                 {
                    xmlHTTP=new ActiveXObject("Msxml2.XMLHTTP");
                 }
                 catch(e)
                 {
                    try
                    {
                        xmlHTTP=new ActiveXObject("Microsoft.XMLHTTP");
                    }
                    catch(e)
                    {
                        alert("Your browser does not support AJAX!");
                        return false;
                    }
                 }
            }
            xmlHTTP.onreadystatechange=function()
            {
                if(xmlHTTP.readyState==4)
                {
                    if(xmlHTTP.responseText)
                    {
                        alert(xmlHTTP.responseText);
                        var iframetext = document.createElement("iframe");
                        
                        iframetext.contentWindow.document.body.innerHTML=xmlHTTP.responseText;
                        //iframetext.style.display="none";
                        alert(iframetext.contentWindow.document.body.innerHTML);
                        document.body.appendChild(iframetext); 
                    }
                }
            }
            xmlHTTP.open("Get","booksreport_exportexcel.aspx",true);
            xmlHTTP.send(null);
           // return false;
        }
    </script>
</body>
</html>

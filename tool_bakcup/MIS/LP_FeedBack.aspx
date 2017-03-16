<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LP_FeedBack.aspx.cs" Inherits="LP_FeedBack" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
         function setHeight(source) {

             var txtContent = source.value;
             if (txtContent.length < 22) {
                 source.style.height = 25;
             }
             else if (txtContent.length < 75) {
                 source.style.height = ((25 / 22) * (txtContent.length)).toString() + 'px';
             }
             else if (txtContent.length < 150) {
                 source.style.height = ((20 / 22) * (txtContent.length)).toString() + 'px';
             }
             else {
                 source.style.height = ((14 / 22) * (txtContent.length)).toString() + 'px';
             }
         }
    </script>
    <%--<script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="style/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function fnShowCalendar(ClientID, width, height) {
            var popup = null;
            settings = 'width=' + width + ',height=' + height + ',location=no,directories=no,menubar=no,toolbar=no,status=no,scrollbars=no,resizable=no,dependent=no';
            popup = window.open('DatePicker.aspx?Ctl=' + ClientID, 'DatePicker', settings);
            popup.focus();
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
          <asp:ServiceReference Path="~/Service.asmx" />
        </Services>
    </asp:ScriptManager>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/common.js"></script>
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="style/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var gvelem;
        var gvcolor;
        function setColor(element,val){      
            //alert(gvelem);
            if(gvelem!=null){//alert(gvelem.style.backgroundColor);
                gvelem.style.backgroundColor = gvcolor;
            }
            gvelem = element;
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            document.form1.hfFBID.value = val;
            if(document.getElementById("lblProjectSummary"))
                document.getElementById("lblProjectSummary").innerText = "Project : " + val;
            else if(document.all.lblProjectSummary)
                document.all.lblProjectSummary.innerText = "Project : " + val;
        }
        function setMouseOverColor(element)
        {
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor='#C2C2C2';
            element.style.cursor='hand';
            element.style.textDecoration='underline';
        }
        function setMouseOutColor(element)
        {
            element.style.backgroundColor=gvcolor;
            element.style.textDecoration='none';
        }
    </script>
    <script type="text/javascript">
        
        function imgBD_editor_onclick() {
            if (document.form1.drpCustomer != null && document.form1.drpCustomer.value != "0")
                window.open("NonLaunchcontacts.aspx?form=Projects&type=0&trgname=txtPEName&trgid=hfprojectEditorId&cid=" + document.form1.drpCustomer.value + "&lid=" + document.form1.drpLocation.value, "Contacts", "width=800,height=600,status=yes, scrollbars=yes");
            else alert("Select a customer");
        }
    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type = "text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type = "text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel = "Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtResPerson.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetCustomers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    var text = this.value.split(/,\s*/);
                    text.pop();
                    text.push(i.item.value);
                    text.push("");
                    this.value = text.join(", ");
                    var value = $("[id*=hfEmpId]").val().split(/,\s*/);
                    value.pop();
                    value.push(i.item.val);
                    value.push("");
                    $("#[id*=hfEmpId]")[0].value = value.join(", ");
                    return false;
                },
                minLength: 1
            });

        });
    </script>
    <script type="text/javascript">
                $(document).ready(function () {
                    $("#<%=txtInvPer.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetCustomers1") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    var text = this.value.split(/,\s*/);
                    text.pop();
                    text.push(i.item.value);
                    text.push("");
                    this.value = text.join(", ");
                    var value = $("[id*=hfInvEmpID]").val().split(/,\s*/);
                    value.pop();
                    value.push(i.item.val);
                    value.push("");
                    $("#[id*=hfInvEmpID]")[0].value = value.join(", ");
                    return false;
                },
                minLength: 1
            });

        });
    </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=txtInformPersons.ClientID %>").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%=ResolveUrl("~/Service.asmx/GetCustomers2") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                        select: function (e, i) {
                            var text = this.value.split(/,\s*/);
                            text.pop();
                            text.push(i.item.value);
                            text.push("");
                            this.value = text.join(", ");
                            var value = $("[id*=hfInfEmpId]").val().split(/,\s*/);
                            value.pop();
                            value.push(i.item.val);
                            value.push("");
                            $("#[id*=hfInfEmpId]")[0].value = value.join(", ");
                            return false;
                        },
                        minLength: 1
                    });

                });
    </script>
<%--    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtAddPunch.ClientID %>").dynDateTime({
                    showsTime: true,
                    ifFormat: "%Y-%m-%d %H:%M",
                    daFormat: "%l;%M %p, %e %m,  %Y",
                    align: "BR",
                    electric: false,
                    singleClick: false,
                    displayArea: ".siblings('.dtcDisplayArea')",
                    button: ".next()"
                });
            });
        </script>--%>
    <div>
        <div>
            <table>
                <tr>
                    <td>
                        <ol id="toc">
                            <li id="miSummaryDetails" runat="server">
                                <asp:LinkButton ID="LinkSummary" TabIndex = "4" runat="server" OnClick="LinkSummary_Click">Summary</asp:LinkButton></li>
                            <li id="miFeedbackDetails" runat="server">
                                <asp:LinkButton ID="lnkFeedbackDetails" TabIndex = "4" runat="server" OnClick="lnkFeedbackDetails_Click">Feedback Details</asp:LinkButton></li>
                        </ol>
                        <div class="borderdiv" id="div_Summary_details" runat="server" style="text-align:center;width:900px;">
                            <table>
                                <tr class="dpJobGreenHeader">
                                    <td align="left" style="width: 700px">
                                        <asp:Label ID="lblFeedbackSummary" runat="server" Text="FeedBack Summary"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl" 
                                            ToolTip="Export Exl" OnClick="exportExl_Click"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GvFB" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                      CssClass="lightbackground" width="100%" OnRowDataBound="GvFB_RowDataBound">
                                            <HeaderStyle CssClass="darkbackground"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                 <asp:TemplateField SortExpression="FB_ID" HeaderText="Sl.No" >
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblFP_ID" runat="server" Text='<%# id++ %>' ></asp:Label>
                                                               <br />
                                                        <asp:HiddenField ID="hfgvFBID" runat="server" Value='<%# Eval("FB_ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField SortExpression="jobid" HeaderText="JOBID"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobid" runat="server" Text='<%# Eval("Jobid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="custname" HeaderText="Customer"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("custname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="ProjectName" HeaderText="Project Title" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="RecDate"  HeaderText="Rec. Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("RecDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="PENAME" HeaderText="PE Name" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPENAME" runat="server" Text='<%# Eval("PENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                              
                                                <asp:TemplateField SortExpression="PN_Status" HeaderText="FeedBack P/N?" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPN_Status" runat="server" Text='<%# Eval("PN_Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Domain" HeaderText="Domain" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDomain" runat="server" Text='<%# Eval("Domain") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="Remarks" HeaderText="Remarks" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                                        <asp:Label ID="lblAppBy" runat="server" Text='<%# Eval("ApprovedBy") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfFBID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>  
                        <div class="borderdiv" id="div_Feedback_details" runat="server" style="text-align:center;width:900px;" >
                            <table style="width:100%;text-align:left;">
                                <tr>
                                    <td class="dpGreenHeader" align="center" colspan="5" style="height: 30px">
                                        FeedBack Details
                                    </td>
                                </tr>
                                <tr>
                                    <td>FeedBack P/N? :</td>
                                    <td><asp:DropDownList ID="dropFeedBackYN" runat="server">
                                        <asp:ListItem Value="0">Positive</asp:ListItem>
                                        <asp:ListItem Value="1">Negative</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>Date :</td>
                                    <td>
                                        <asp:TextBox ID="txtPdate" runat="server" ></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPdate" 
                                                                PopupButtonID="txtPdate" Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Customer :</td>
                                    <td>
                                        <asp:DropDownList ID="drpCustomer" runat="server" Width="200px" 
                                            AutoPostBack="True" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged">
                                        </asp:DropDownList>   
                                    </td>
                                    <td>Location :</td>
                                    <td>
                                        <asp:DropDownList ID="drpLocation" runat="server" Width="100px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Job Unique ID :</td>
                                    <td><asp:TextBox ID="txtJobID" runat="server" AutoPostBack="true" OnTextChanged="txtJobID_TextChanged"></asp:TextBox></td>
                                    <td>Project Name :</td>
                                    <td>
                                        <asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PE Name :</td>
                                    <td>
                                        <asp:TextBox ID="txtPEName" runat="server"></asp:TextBox>
                                        <img id="imgBD_editor" src="images/tools/user_go.png" language="javascript" runat="server"
                                                onclick="return imgBD_editor_onclick()" style="cursor: pointer" title="Select PE"/>
                                        <asp:HiddenField ID="hfprojectEditorId" runat="server"  />
                                    </td>
                                    <td>Remarks :</td>
                                    <td><asp:DropDownList ID="dropRemarks" runat="server">
                                        <asp:ListItem Value="0">External</asp:ListItem>
                                        <asp:ListItem Value="1">Internal</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Platform :</td>
                                    <td>
                                        <div style="width:140px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">
                                            <asp:CheckBoxList  Width="120px" ID="lboxSW" SelectionMode="Multiple" runat="server"></asp:CheckBoxList>
                                        </div>
                                    </td>
                                    <td>FeedBack :</td>
                                    <td><asp:TextBox ID="txtFB" runat="server" TextMode="MultiLine" Width="180px" Height="80px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Key Elements :</td>
                                    <td><asp:TextBox ID="txtKeyEle" runat="server"  TextMode="MultiLine" Width="180px" Height="80px"></asp:TextBox></td>
                                    <td>Domain :</td>
                                    <td><asp:DropDownList ID="dropDomain" runat="server">
                                        <asp:ListItem Value="0">Cutomer Service</asp:ListItem>
                                        <asp:ListItem Value="1">Process & Quality</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                        Involved Persons :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInvPer" runat="server" TextMode="MultiLine" Width="180px" Height="80px"> </asp:TextBox>
                                        <asp:HiddenField ID="hfInvEmpID" runat="server" />
                                    </td>
                                    <td>Responsible Person :</td>
                                    <td>
                                        <asp:TextBox ID="txtResPerson" runat="server"  TextMode="MultiLine" Width="180px" Height="80px"> </asp:TextBox>
                                        <asp:HiddenField ID="hfEmpId" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Error Analysis :</td>
                                    <td><asp:TextBox ID="txtErrorAns" runat="server"  TextMode="MultiLine" Width="180px" Height="80px"></asp:TextBox></td>
                                    <td>Preventive Action Plan :</td>
                                    <td><asp:TextBox ID="txtPreAction" runat="server"  TextMode="MultiLine" Width="180px" Height="80px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table style="width:100%">
                                            <tr>
                                                <td colspan="3">
                                                    <b>Delay Details :</b>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    Informed Y/N?
                                                </td>
                                                <td>
                                                    Informed Date
                                                </td>
                                                <td>
                                                    Informed/Uniformed By
                                                </td>
                                            </tr>
                                             <tr align="center">
                                                <td>
                                                    <asp:DropDownList ID="dropInformStatus" runat="server">
                                                        <asp:ListItem Value="0">Yes</asp:ListItem>
                                                        <asp:ListItem Value="1">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInformDate" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInformDate" 
                                                                PopupButtonID="txtInformDate" Format="dd/MM/yyyy">
                                                    </asp:CalendarExtender>
                                                    <%--<img src="images/calender.png" />--%>
                                                    <%--<script type="text/javascript">
                                                        $(document).ready(function () {
                                                            $("#<%=txtAddPunch.ClientID %>").dynDateTime({
                                                                 showsTime: true,
                                                                 ifFormat: "%Y-%m-%d %H:%M",
                                                                 daFormat: "%l;%M %p, %e %m,  %Y",
                                                                 align: "BR",
                                                                 electric: false,
                                                                 singleClick: false,
                                                                 displayArea: ".siblings('.dtcDisplayArea')",
                                                                 button: ".next()"
                                                             });
                                                         });
                                                    </script>--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInformPersons" runat="server"  TextMode="MultiLine" Width="180px" Height="40px"> </asp:TextBox>
                                                    <asp:HiddenField ID="hfInfEmpId" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="dpbutton" OnClick="btnSave_Click"/>
                                        <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="dpbutton" OnClick="btnClear_Click"/>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="dpbutton" OnClick="btnSubmit_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

<%@ control language="C#" autoeventwireup="true" inherits="headerCJRange, App_Web_lruasnqi" %>
<div>
        <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
            <tr><td>Customer</td><td><select>
            <option>Taylor and Francis</option>
            <option>Taylor and Francis Scandivia</option>
            <option>Lund</option>
            <option>World Bank</option>
            </select></td>
            <td>Type</td><td><select>
            <option>Journal</option>
            <option>Book</option>
            <option>Project</option>
            </select></td>           </tr>
            <tr><td>From</td><td><Input type="text" id="fromdate" runat="server" /></td>
            <td rowspan="2" colspan="2" valign="bottom" runat="server"   >
                <asp:Button Text="" style="width:72px;height:26px;background:url(images/submit.jpg)" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click" />
            </td></tr>
            <tr><td>To</td><td><Input type="text" id="Text1" /></td></tr>            
        </table>
</div>       

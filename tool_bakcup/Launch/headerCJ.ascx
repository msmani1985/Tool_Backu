<%@ control language="C#" autoeventwireup="true" inherits="headerCJ, App_Web_lruasnqi" %>
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
            </select></td>
            <td align=center >
            <asp:Button Text="" style="width:72px;height:26px;background:url(images/submit.jpg)" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click" />
            </td></tr>
        </table>
</div>       

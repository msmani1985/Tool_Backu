// JScript File

function ChkEmptyTxtBx(obj,msg)
{
    if(obj.value=="")
    {
        alert(msg);
        obj.focus();
        return false;
    }
    return true;
}
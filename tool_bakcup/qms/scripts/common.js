
function OnlyAllowNumbers(field, e){
	var reKeyboardChars = /[\x00\x08\x0D0]/;
	var reValidChars = /\d/;
	var keycode = window.Event ? e.which : e.keyCode;
	if(keycode==null)keycode = e.keyCode;
	var strChar = String.fromCharCode (keycode);
	if (!reValidChars.test (strChar) && !reKeyboardChars.test (strChar)){
		return false;
	}  
	return true;
}

function OnlyAllowAlphanumerics(field, e){
	var reKeyboardChars = /[\x00\x08\x0D]/;
	var reValidChars = /[0-9,A-Z,a-z]/;
	var keycode = window.Event ? e.which : e.keyCode;
	if(keycode==null)keycode = e.keyCode;
	var strChar = String.fromCharCode (keycode);
	if (!reValidChars.test (strChar) && !reKeyboardChars.test (strChar)){
		return false;
	}  
	return true;
}

function TextCounter(field, maxlimit){
    if (field.value.length > maxlimit)
        field.value = field.value.substring(0, maxlimit);
}


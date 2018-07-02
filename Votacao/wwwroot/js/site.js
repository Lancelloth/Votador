// Write your Javascript code.
var password = document.getElementById("inputPassword")
    , confirm_password = document.getElementById("inputPasswordConfirmacao");

function validatePassword() {
    if (password.value != confirm_password.value) {
        confirm_password.setCustomValidity("Passwords não batem, digite-os novamente");
    } else {
        confirm_password.setCustomValidity('');
    }
}

password.onchange = validatePassword;
confirm_password.onkeyup = validatePassword;
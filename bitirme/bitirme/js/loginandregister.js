var welcomeScreen = document.querySelector(".welcomeScreen"),
    loginScreen = document.querySelector(".login"),
    registerScreen = document.querySelector(".register"),
    btnLogin = document.querySelector(".btn-login"),
    btnRegister = document.querySelector(".btn-register");

document.addEventListener('DOMContentLoaded', function () {
    setTimeout(removeWelcome, 8000);
    registerScreen.style.display = "none";
});

function removeWelcome() {
    welcomeScreen.style.display = "none";
}
function goToRegisterPage() {
    loginScreen.style.display = "none";
    registerScreen.style.display = "block";
}
function goToLoginPage() {
    loginScreen.style.display = "block";
    registerScreen.style.display = "none";
}
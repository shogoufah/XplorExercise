// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ValidateEmail() {
    let email = document.getElementById("email").value;
    let text;
    if (email == "" {
        text = "Email is required!";
    } else {
        text = "";
    }
    document.getElementById('error').innerHTML = text;
}

function sayHello() {
    alert("Hello World")
}
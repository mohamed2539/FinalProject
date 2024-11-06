
const container = document.getElementById('container');
const registarBtn =document.getElementById('register');
const  loginBtn = document.getElementById('Login');

registarBtn.addEventListener('click' , ()=> {

    container.classList.add("active");
});

loginBtn.addEventListener('click' , ()=> {

    container.classList.remove("active");
});

//this function to print invoice

function printPage() {
    window.print();
}

function printSelectedArea() {
    var printContents = document.getElementById('printArea').innerHTML;
    var originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
    location.reload(); // Reload the page to restore original content
}
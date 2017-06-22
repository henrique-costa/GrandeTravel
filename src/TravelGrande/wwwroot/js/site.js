/*
 **********************************************************
 * OPAQUE NAVBAR SCRIPT
 **********************************************************
 */

// Toggle tranparent navbar when the user scrolls the page

$(window).scroll(function () {
    if ($(this).scrollTop() > 50)  /*height in pixels when the navbar becomes non opaque*/ {
        $('.opaque-navbar').addClass('opaque');
    } else {
        $('.opaque-navbar').removeClass('opaque');
    }
});


/*
 **********************************************************
 *  MODAL
 **********************************************************
 */



// Get the modal
var modal = document.getElementById('myModal');

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal 
btn.onclick = function () {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}


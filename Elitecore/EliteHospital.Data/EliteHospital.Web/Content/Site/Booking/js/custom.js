

if ($(window).width() < 1025) {
    $(".hdr-lft").append($(".hdr-top"));
}
if ($(window).width() < 1025) {
    $(".left-main").append($(".hdr-search"));
}

if ($(window).width() < 1025 && $(window).width() > 767) {

    function openNav() {
        document.getElementById("sidenav").style.width = "300px";
        document.getElementById("header-lt").style.opacity = "0.8";
        document.getElementById("cont-box").style.opacity = "0.8";
        document.body.style.backgroundColor = "#000";
    }

    function closeNav() {
        document.getElementById("sidenav").style.width = "0";
        document.getElementById("header-lt").style.opacity = "1";
        document.getElementById("cont-box").style.opacity = "1";
        document.body.style.backgroundColor = "#fff";
    }

}
if ($(window).width() < 768) {

    function openNav() {
        document.getElementById("sidenav").style.width = "100%";
    }

    function closeNav() {
        document.getElementById("sidenav").style.width = "0";
    }

}
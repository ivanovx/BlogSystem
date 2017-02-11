var BlogSystem = BlogSystem || {};

BlogSystem.onGetCommentsSuccess = function () {
    $("#show-comments-btn").hide();
    $("#new-comment").removeClass("hidden");

    console.log("Success get posts comment");
};

BlogSystem.onAddCommentSuccess = function () {
    $.notify("Comment successfully created!", "success");

    $("#new-comment").hide();

    console.log("Comment successfully created!");
};

BlogSystem.onCreateCommentFailure = function(data) {
    $.notify("An error has occurred. Please try again later...", "error");

    console.log(data);
};

$(window).scroll(function () {
    if ($(document).scrollTop() > 50) {
        $(".navbar").addClass("navbar-shrink");
    } else {
        $(".navbar").removeClass("navbar-shrink");
    }
});

$(document).ready(function() {
    $("html").niceScroll({
        cursorcolor: "rgb(49, 50, 51)", // change cursor color in hex
        cursoropacitymin: 0, // change opacity when cursor is inactive (scrollabar "hidden" state), range from 1 to 0
        cursoropacitymax: 1, // change opacity when cursor is active (scrollabar "visible" state), range from 1 to 0
        cursorwidth: "10px", // cursor width in pixel (you can also write "5px")
        cursorborder: "1px solid #fff", // css definition for cursor border
        cursorborderradius: "5px", // border radius in pixel for cursor
        zindex: 11,  // change z-index for scrollbar div
        scrollspeed: 60, // scrolling speed
        mousescrollstep: 40, // scrolling speed with mouse wheel (pixel)
        touchbehavior: false, // enable cursor-drag scrolling like touch devices in desktop computer
        hwacceleration: true, // use hardware accelerated scroll when supported
        boxzoom: false, // enable zoom for box content
        dblclickzoom: true, // (only when boxzoom=true) zoom activated when double click on box
        gesturezoom: true, // (only when boxzoom=true and with touch devices) zoom activated when pinch out/in on box
        grabcursorenabled: true, // (only when touchbehavior=true) display "grab" icon
        autohidemode: false
});
});
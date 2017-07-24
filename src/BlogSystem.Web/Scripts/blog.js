var BlogSystem = BlogSystem || {};

$("#new-comment").hide();

BlogSystem.onGetCommentsSuccess = function () {
    $("#show-comments").hide();
    $("#new-comment").show();

    console.log("Success get posts comment");
};

BlogSystem.onAddCommentSuccess = function () {
    $.notify("Comment successfully created!", "success");

    $("#new-comment").hide();

    console.log("Comment successfully created!");
};

BlogSystem.onCreateCommentFailure = function (data) {
    $.notify("An error has occurred. Please try again later...", "error");

    console.log(data);
};

/*
$(window).scroll(function () {
    if ($(document).scrollTop() > 50) {
        $(".navbar").addClass("navbar-shrink");
    } else {
        $(".navbar").removeClass("navbar-shrink");
    }
});*/

var isMobile = window.matchMedia("only screen and (max-width: 760px)");

if (!isMobile.matches) {
    $(window).bind("scroll", function () {
        if ($(window).scrollTop() > 340) {
            $(".navbar").addClass("fixed-top");
        } else {
            $(".navbar").removeClass("fixed-top");
        }
    });
}
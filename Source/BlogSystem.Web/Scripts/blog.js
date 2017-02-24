var BlogSystem = BlogSystem || {};

$(window).load(function() {
    $("#loading").fadeOut("slow");
});

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

/*
$(document).ready(function() {
    $("html").niceScroll({
        cursorcolor: "rgb(49, 50, 51)",
        cursorwidth: "10px",
        cursorborderradius: "5px",
        scrollspeed: 60,
        mousescrollstep: 40
    });
});
*/
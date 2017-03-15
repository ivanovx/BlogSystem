var BlogSystem = BlogSystem || {};

$("#new-comment").hide();

BlogSystem.onGetCommentsSuccess = function () {
    $("#show-comments-btn").hide();
    $("#new-comment").show();

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

$(document).ready(function () {
    $("pre").each(function (i, block) {
        hljs.highlightBlock(block);
    });
});
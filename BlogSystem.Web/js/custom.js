$(document).ready(function () {
    $(".social-network li a, .options_box .color a").tooltip();

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $(".scrollup").fadeIn();
        } else {
            $(".scrollup").fadeOut();
		}
    });

    $(".scrollup").click(function () {
        $("html, body").animate({ scrollTop: 0 }, 1000);
	    return false;
	});
});
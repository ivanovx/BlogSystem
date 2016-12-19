/*global jQuery:false */
jQuery(document).ready(function($) {
"use strict";
		
		
	

		
		// tooltip
		$('.social-network li a, .options_box .color a').tooltip();


		
		//scroll to top
		$(window).scroll(function(){
			if ($(this).scrollTop() > 100) {
				$('.scrollup').fadeIn();
				} else {
				$('.scrollup').fadeOut();
			}
		});
		$('.scrollup').click(function(){
			$("html, body").animate({ scrollTop: 0 }, 1000);
				return false;
		});
   

});
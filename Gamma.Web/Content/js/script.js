"use strict";

/* ==== Jquery Functions ==== */
(function($) {
	
	/* ==== Tool Tip ==== */
	$('[data-toggle="tooltip"]').tooltip();	
	
	/* ==== Item Slider ==== */	
  	$(".itemgrid").owlCarousel({      
	   loop:true,
		margin:25,
		nav:true,
		responsiveClass:true,
		responsive:{
			0:{
				items:1,
				nav:false
			},
			768:{
				items:2,
				nav:false
			},
			1170:{
				items:4,
				nav:false,
				loop:true
			}
		}
  	});	
	
	/* ==== Testimonials Slider ==== */	
  	$(".testimonialsList").owlCarousel({      
	   loop:true,
		margin:30,
		nav:false,
		responsiveClass:true,
		responsive:{
			0:{
				items:1,
				nav:false
			},
			768:{
				items:2,
				nav:false
			},
			1170:{
				items:3,
				nav:false,
				loop:true
			}
		}
  	});
	
	
	/* ==== Revolution Slider ==== */
	if($('.tp-banner').length > 0){
		$('.tp-banner').show().revolution({
			delay:6000,
	        startheight:650,
	        startwidth: 1140,
	        hideThumbs: 1000,
	        navigationType: 'none',
	        touchenabled: 'on',
	        onHoverStop: 'on',
	        navOffsetHorizontal: 0,
	        navOffsetVertical: 0,
	        dottedOverlay: 'none',
	        fullWidth: 'on'
		});
	}
	
	
	
	
})(jQuery);
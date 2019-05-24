$.noConflict();

jQuery(document).ready(function($) {

	"use strict";

	[].slice.call( document.querySelectorAll( "select.cs-select" ) ).forEach( function(el) {
		new SelectFx(el);
	});

    jQuery(".selectpicker").selectpicker;


    $('.search-trigger').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').addClass('open');
	});

	$('.search-close').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').removeClass('open');
	});

	$('.equal-height').matchHeight({
		property: 'max-height'
	});

	// var chartsheight = $('.flotRealtime2').height();
	// $('.traffic-chart').css('height', chartsheight-122);


	// Counter Number
	$('.count').each(function () {
		$(this).prop('Counter',0).animate({
			Counter: $(this).text()
		}, {
			duration: 3000,
			easing: 'swing',
			step: function (now) {
				$(this).text(Math.ceil(now));
			}
		});
	});


	 
	 
	// Menu Trigger
	$("#menuToggle").on("click", function() {
		var windowWidth = $(window).width();   		 
		if (windowWidth<1010) { 
			$('body').removeClass('open'); 
			if (windowWidth<760){ 
				$('#left-panel').slideToggle(); 
			} else {
				$('#left-panel').toggleClass('open-menu');  
			} 
		} else {
			$('body').toggleClass('open');
			$('#left-panel').removeClass('open-menu');  
		} 
			 
	}); 

	 
	$(".menu-item-has-children.dropdown").each(function() {
		$(this).on('click', function() {
			var $tempText = $(this).children('.dropdown-toggle').html();
			$(this).children('.sub-menu').prepend('<li class="subtitle">' + $tempText + '</li>'); 
		});
	});


	// Load Resize 
	$(window).on("load resize", function() {
	    const windowWidth = $(window).width();  		 
	    if (windowWidth<1010) {
			$("body").addClass("small-device"); 
		} else {
			$("body").removeClass("small-device");  
		}

	});

    // -------------------------------------------------

    //$(".table-row").click(function() {
    //    window.document.location = $(this).data("href");
    //});
    
    //$(".table-row").click(function (e) {
    //    e.preventDefault();
    //    var link = $(this).attr("id");
    //    $("#results").load(link);
    //});

    $('[data-toggle-second="tooltip"]').tooltip();

    $('.stop-propagation').on('click', function (e) {
        e.stopPropagation();
    });
});

jQuery(document).on('click', '.table-row', function () {
    var route = $(this).attr("id");
    $("#results").load(route);
})
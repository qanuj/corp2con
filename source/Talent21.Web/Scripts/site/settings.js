(function($) {
	"use strict";

	$(window).load(function() {
		$("#loader").fadeOut("slow");
	});

    $(document).ready(function() {

        // ====================================================================

        // Header scroll function

        $(window).scroll(function() {
            var scroll = $(window).scrollTop();
            if (scroll > 50) {
                $("#header-background").slideDown(300);
            } else {
                $("#header-background").slideUp(300);
            }
        });

        // ====================================================================

        // Slider

        $('#slider').css({ 'height': (($(window).height() - 0)) + 'px' });
        $(window).resize(function() {
            $('#slider').css({ 'height': (($(window).height() - 0)) + 'px' });
        });

        var Page = (function() {

            var $navArrows = $('#nav-arrows'),
                $nav = $('#nav-dots > span'),
                slitslider = $('#slider').slitslider({
                    onBeforeChange: function(slide, pos) {

                        $nav.removeClass('nav-dot-current');
                        $nav.eq(pos).addClass('nav-dot-current');

                    }
                }),

                init = function() {

                    initEvents();

                },
                initEvents = function() {

                    // add navigation events
                    $navArrows.children(':last').on('click', function() {

                        slitslider.next();
                        return false;

                    });

                    $navArrows.children(':first').on('click', function() {

                        slitslider.previous();
                        return false;

                    });

                    $nav.each(function(i) {

                        $(this).on('click', function(event) {

                            var $dot = $(this);

                            if (!slitslider.isActive()) {

                                $nav.removeClass('nav-dot-current');
                                $dot.addClass('nav-dot-current');

                            }

                            slitslider.jump(i + 1);
                            return false;

                        });

                    });

                };

            return { init: init };

        })();

        Page.init();

        // Counterup

        $('.number').counterUp({
            delay: 10, // the delay time in ms
            time: 1000 // the speed time in ms
        });

        // ====================================================================

        // Form Sliders

        $('#years').noUiSlider({
            start: [3],
            connect: "lower",
            step: 1,
            range: {
                'min': 0,
                'max': 15
            },
            format: wNumb({
                decimals: 0
            })
        });

        $("#years").Link('lower').to($("#years-field"));

        $('#salary').noUiSlider({
            start: [40000, 80000],
            connect: true,
            step: 1000,
            range: {
                'min': 0,
                'max': 150000
            },
            format: wNumb({
                decimals: 0,
                thousand: '.',
                prefix: '$'
            })
        });

        $("#salary").Link('lower').to($("#salary-field-lower"));
        $("#salary").Link('upper').to($("#salary-field-upper"));

        // Scroll Reveal

        window.sr = new scrollReveal({
            reset: true,
            move: '50px',
            mobile: false
        });

    });

})(jQuery);
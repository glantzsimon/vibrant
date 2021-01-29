function buttonBehaviours() {

    function displaySpinnerOnFormSubmit() {
        $("form").submit(function () {
            if ($(this).valid()) {
                var button = $(this).find("button.btn");
                button.button('loading');
            }
        });
    }

    function displayPageSpinnerOnLinkClick() {
        $("a").click(function (e) {
            if (
                e.ctrlKey ||
                e.shiftKey ||
                e.metaKey ||
                (e.button && e.button === 1)
            ) {
                return;
            }

            var el = $(this);
            if (!el.hasClass("carousel-control") && !el.attr("target")) {
                var href = (el.attr("href"));
                if (href) {
                    if (href !== "#") {
                        $("#pageSpinner").show();
                        $("#pageOverlay").show();
                    }
                }
            }
        });
    }

    var init = function () {
        displaySpinnerOnFormSubmit();
        displayPageSpinnerOnLinkClick();
    };

    return {
        init: init,
        displayPageSpinnerOnLinkClick: displayPageSpinnerOnLinkClick
    };

};
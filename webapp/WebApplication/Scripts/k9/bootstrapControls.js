function bootstrapControls(config) {

    function initBootstrapDateTimePickers() {
        $("div.dateonly").datetimepicker({
            format: "L"
        });

        $("div.datetime").datetimepicker({
            format: "L"
        });

        $("div.dateonly, div.datetime").on("dp.change", function (e) {
            var linkId = e.target.firstElementChild.id;
            var formattedDate = e.date.format("YYYY-MM-DD");
            $("input[linkid=" + linkId + "]").val(formattedDate);
        });
    }
    
    function initBootstrapSelect() {
        $(".selectpicker").selectpicker({
            size: 8
        });
        $("select").each(function () {
            this.getSelectedText = function () {
                return $(this).parent().find("li[data-original-index=" + this.selectedIndex + "] span.text").html();
            };
        });
    }

    function initCollapsibleDivs() {
        $("div.inline-collapsible-panel-link").click(function () {
            var $el = $(this);
            var $container = $el.parent();
            var $hiddenPanel = $container.find("div.panel-container");

            if ($hiddenPanel.is(":visible")) {
                $hiddenPanel.hide();
                $el.html(getDictionaryItem("ReadMore"));
            } else {
                $hiddenPanel.show();
                $el.html(getDictionaryItem("ReadLess"));
            }
        });
    }

    function initBootstrapSortable() {
        $("ul.sortable").each(function () {
            var $sortableList = $(this);
            var displayIndexFieldName = $sortableList.data("displayIndexFieldName");

            if ($sortableList.length > 0) {
                new Sortable($sortableList[0],
                    {
                        dataIdAttr: 'data-id',

                        onSort: function (e) {
                            var children = e.to.children;
                            var results = [];

                            for (var i = 0; i < children.length; i++) {
                                var id = $(children[i]).data("id");
                                if (id) {
                                    results.push({ id: $(children[i]).data("id"), displayIndex: i });

                                    // Update display index field
                                    $sortableList.find("input[name*='" + displayIndexFieldName + "'][data-id='" + id + "']").val(i);
                                }
                            }

                            $sortableList.trigger("indexChanged", { items: results });
                        }
                    });
            }
        });
    }

    function initTextScroller() {
        $(".scroller-container").vTicker({
            speed: 500,
            pause: 3000,
            showItems: 3,
            animation: "fade",
            mousePause: false,
            height: 0,
            direction: "up"
        });

    }

    function initDateTimeValidation() {
        $.validator.methods.date = function (value, element) {
            return this.optional(element) ||
                moment(value, config.dateFormat, true).isValid() ||
                moment(value, "YYYY-MM-DD", true).isValid();
        };
    }

    function initToolTips() {
        $('[data-toggle="tooltip"]').tooltip();
    }

    function selectFirstFormInput() {
        $("form").find("input[type=text], textarea, select").filter(":not(#main-search):visible:first").focus();
    }

    function initCollapsiblePanels() {
        $("a.collapsible-panel-link").mouseup(function () {
            var el = $(this);
            var i = el.find("i.fa");
            if (el.attr("aria-expanded") !== "true") {
                i.removeClass("fa-caret-down");
                i.addClass("fa-caret-up");
            } else {
                i.removeClass("fa-caret-up");
                i.addClass("fa-caret-down");
            }
        });

        $(".collapsible-panel-anchor").click(function () {
            var el = $(this);
            var panelId = el.attr("data-panel-id");

            window.setTimeout(function () {
                $("[data-panel-id='" + panelId + "'].panel-collapse").collapse("show");
            }, 200);
        });
    }

    function initQuantityInputs() {
        let $typeInput = $("form").find("select[data-input-id='ingredient-type'], select[data-input-id='product-type'], input[data-input-id='product-type']");
        let quantityFn = function ($el) {
            if ($el.length > 0) {
                let isHidden = $el[0].type === "hidden";
                var type = isHidden ? $el[0].value : $el[0].getSelectedText();
                var measure = type === "Liquid" ? "ml" : type === "Capsules" ? "capsules" : "mg";
                let $labels = isHidden ? $el.parent().find("span[data-input-id='quantity']") : $el.closest("form").find("span[data-input-id='quantity']");
                $labels.each(function () {
                    var $label = $(this);
                    if ($label.parent().find("input").attr("id") === "AmountPerServing" && measure === "capsules") {
                        $label.text("mg");
                    } else {
                        $label.text(measure);
                    }
                });
            }
        };
        $typeInput.change(function () {
            quantityFn($typeInput);
        });
        quantityFn($typeInput);
    }

    function initGlossary() {
        $("span.glossary").each(function () {
            var $el = $(this);
            var word = ($el).attr("key") || $el.html().replace(/\s+/g, "");
            var glossaryItem = config.glossaryItems.find(e => e.Name.toLowerCase() === word.toLowerCase());

            if (glossaryItem) {
                $el.tooltip({
                    title: glossaryItem.Description
                });
            }
        });
    }

    function getDictionaryItem(key) {
        var dictionaryItem = config.dictionaryItems.find(e => e.Name.toLowerCase() === key.toLowerCase());

        if (dictionaryItem) {
            return dictionaryItem.Description;
        }
    }

    function initGauges(maxValue) {
        var opts = {
            angle: 0.15, // The span of the gauge arc
            lineWidth: 0.44,
            radiusScale: 1, // Relative radius
            pointer: {
                length: 0.6, // Relative to gauge radius
                strokeWidth: 0.035, // The thickness
                color: "#000000"
            },
            limitMax: false, // If false, max value increases automatically if value > maxValue
            limitMin: false, // If true, the min value of the gauge will be fixed
            colorStart: "#6FADCF",
            colorStop: "#8FC0DA",
            strokeColor: "#E0E0E0",
            generateGradient: true,
            highDpiSupport: true
        };

        $(".gauge").each(function () {
            var $el = $(this);
            var value = parseInt($el.attr("data-value") || 0);
            var maxValue = parseInt($el.attr("data-max-value") || 10);
            var isInverted = $el.attr("data-invert") === "true" ? true : false;

            if (isInverted) {
                opts.percentColors = [[0.0, "#08F321"], [0.50, "#FCDA14"], [1.0, "#FD0510"]];
            }
            else {
                opts.percentColors = [[0.0, "#FD0510"], [0.50, "#FCDA14"], [1.0, "#08F321"]];
            }

            var gauge = new Gauge($el[0]).setOptions(opts);
            gauge.maxValue = maxValue;
            gauge.setMinValue(0);
            gauge.animationSpeed = 32;
            gauge.set(value);
        });
    }

    function initDataTables() {
        $(".k9-datatable").DataTable({
            "columnDefs": customColumnDefs || [],
            "order": customOrder || []
        });
    }

    var init = function () {
        initBootstrapDateTimePickers();
        initBootstrapSelect();
        initDateTimeValidation();
        initToolTips();
        initTextScroller();
        initCollapsiblePanels();
        initQuantityInputs();
        initGlossary();
        initGauges();
        initDataTables();
        initBootstrapSortable();
        initCollapsibleDivs();
    };

    return {
        init: init,
        selectFirstFormInput: selectFirstFormInput
    };

}
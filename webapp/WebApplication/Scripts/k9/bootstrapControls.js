function bootstrapControls(config) {
    window.$my = {
        $divDates: $("div.dateonly"),
        $divDateTimes: $("div.datetime"),
        $forms: $("form")
    };

    function initBootstrapDateTimePickers() {
        $my.$divDates.datetimepicker({
            format: "L"
        });

        $my.$divDateTimes.datetimepicker({
            format: "L"
        });

        var bindDatesFunction = function (e) {
            var linkId = e.target.firstElementChild.id;
            var formattedDate = e.date.format("YYYY-MM-DD");
            $("input[linkid=" + linkId + "]").val(formattedDate);
        };

        $my.$divDates.on("dp.change", bindDatesFunction);
        $my.$divDateTimes.on("dp.change", bindDatesFunction);
    }

    function initBootstrapSelect() {
        let $selects = $("select");
        for (var i = 0; i < $selects.length; i++) {
            $selects[i].getSelectedText = function () {
                return $(this).parent().find("li[data-original-index=" + this.selectedIndex + "] span.text").html();
            };
        }
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
        let $ulSortables = $("ul.sortable");
        for (var j = 0; j < $ulSortables.length; j++) {
            var $sortableList = $($ulSortables[i]);
            var displayIndexFieldName = $sortableList.data("displayIndexFieldName");

            if ($sortableList.length > 0) {
                new Sortable($sortableList[0],
                    {
                        dataIdAttr: "data-id",

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
        }
    }

    function initTextScroller() {
        $("div.scroller-container").vTicker({
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
        $("[data-toggle='tooltip']").tooltip();
    }

    function selectFirstFormInput() {
        if ($my.$forms.length)
            $my.$forms.find("input[type=text], textarea, select").filter(":not(#main-search):visible:first").focus();
    }

    function initCollapsiblePanels() {
        let $collapsiblePanels = $("a.collapsible-panel-link");
        if ($collapsiblePanels.length) {
            $collapsiblePanels.mouseup(function() {
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
        }

        let $collapsiblePanelAnchors = $(".collapsible-panel-anchor");
        if ($collapsiblePanelAnchors.length) {
            $collapsiblePanelAnchors.click(function() {
                var el = $(this);
                var panelId = el.data("panel-id");

                window.setTimeout(function() {
                        $("div[data-panel-id='" + panelId + "'].panel-collapse").collapse("show");
                    },
                    200);
            });
        }
    }

    function initQuantityInputs() {
        if ($my.$forms.length) {
            let $typeInput = $my.$forms.find("select[data-input-id='ingredient-type'], select[data-input-id='product-type'], input[data-input-id='product-type']");
            
            let quantityFn = function ($el) {
                if ($el.length > 0) {
                    let isHidden = $el[0].type === "hidden";
                    let type = isHidden ? $el[0].value : $el[0].getSelectedText();
                    let measure = type === "Liquid" ? "ml" : type === "Capsules" ? "capsules" : "mg";
                    let $labels = isHidden ? $el.parent().find("span[data-input-id='quantity']") : $el.closest("form").find("span[data-input-id='quantity']");
                    
                    if ($labels.length) {
                        for (var i = 0; i < $labels.length; i++) {
                            var $label = $labels[i];
                            if ($label.parent().find("input").attr("id") === "AmountPerServing" && measure === "capsules") {
                                $label.text("mg");
                            } else {
                                $label.text(measure);
                            }   
                        }
                    }
                }
            };
            $typeInput.change(function () {
                quantityFn($typeInput);
            });
            quantityFn($typeInput);
        }
    }

    function initGlossary() {
        var $glossarySpans = $("span.glossary");
        if ($glossarySpans.length) {
            for (var i = 0; i < $glossarySpans.length; i++) {
                var $el = $glossarySpans[i];
                var word = ($el).attr("key") || $el.html().replace(/\s+/g, "");
                var glossaryItem = config.glossaryItems.find(e => e.Name.toLowerCase() === word.toLowerCase());

                if (glossaryItem) {
                    $el.tooltip({
                        title: glossaryItem.Description
                    });
                }
            }
        }
    }

    function getDictionaryItem(key) {
        var dictionaryItem = config.dictionaryItems.find(e => e.Name.toLowerCase() === key.toLowerCase());

        if (dictionaryItem) {
            return dictionaryItem.Description;
        }
    }

    function initGauges() {
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

        var $gauges = $(".gauge");
        if ($gauges.length) {
            for (var i = 0; i < $gauges.length; i++) {
                var $el = $gauges[i];
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
            }
        }
    }

    function initDataTables() {
        let $dataTables = $("table.k9-datatable");
        if ($dataTables.length) {
            $dataTables.DataTable({
                "columnDefs": customColumnDefs || [],
                "order": customOrder || []
            });
        }
    }

    var init = function () {
        initBootstrapDateTimePickers();
        initBootstrapSelect();
        initDateTimeValidation();
        //initToolTips();
        initTextScroller();
        initCollapsiblePanels();
        initQuantityInputs();
        initGlossary();
        //initGauges();
        initDataTables();
        initBootstrapSortable();
        initCollapsibleDivs();
    };

    return {
        init: init,
        selectFirstFormInput: selectFirstFormInput
    };

}
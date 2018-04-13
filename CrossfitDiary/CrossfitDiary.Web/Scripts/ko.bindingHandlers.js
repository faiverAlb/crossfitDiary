

/* Bootstrap select picker custom binding from Internet  */

ko.bindingHandlers.selectPicker = {
    after: ['options'],   /* KO 3.0 feature to ensure binding execution order */
    init: function (element, valueAccessor, allBindingsAccessor) {
      //todo: not implemented for bootstrap 4
//        var $element = $(element);
//        $element.addClass('selectpicker').selectpicker();
//
//        var doRefresh = function () {
//            $element.selectpicker('refresh');
//        }, subscriptions = [];
//
//        // KO 3 requires subscriptions instead of relying on this binding's update
//        // function firing when any other binding on the element is updated.
//
//        // Add them to a subscription array so we can remove them when KO
//        // tears down the element.  Otherwise you will have a resource leak.
//        var addSubscription = function (bindingKey) {
//            var targetObs = allBindingsAccessor.get(bindingKey);
//
//            if (targetObs && ko.isObservable(targetObs)) {
//                subscriptions.push(targetObs.subscribe(doRefresh));
//            }
//        };
//
//        addSubscription('options');
//        addSubscription('value');           // Single
//        addSubscription('selectedOptions'); // Multiple
//
//        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
//            while (subscriptions.length) {
//                subscriptions.pop().dispose();
//            }
//        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
    }
};


ko.bindingHandlers.inputmask =
{
    init: function (element, valueAccessor, allBindingsAccessor) {
        var mask = valueAccessor();
        var definitions = valueAccessor().definitions || {};
        var observable = mask.value;
        if (ko.isObservable(observable)) {
            $(element)
                .on('focusout change',
                    function() {
                        if ($(element).inputmask('isComplete')) {
                            observable($(element).val());
                        } else {
                            observable(null);
                        }
                    });
        }
        $(element).inputmask({ mask: mask, definitions: definitions });
    },
    update: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
        var mask = valueAccessor();
        var observable = mask.value;
        if (ko.isObservable(observable)) {
            var valuetoWrite = observable();
            $(element).val(valuetoWrite);
        }
    }
};


ko.bindingHandlers.datepicker = {
  init: function (element, valueAccessor, allBindingsAccessor) {
    var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

    var options = {
      uiLibrary: 'bootstrap4',
      iconsLibrary: 'fontawesome',
      icons: {
        rightIcon: '<i class="far fa-calendar-alt"></i>'
      },
      value: today.toLocaleDateString(),
      maxDate: today
    };
    $(element).datepicker(options);
        //initialize datepicker with some optional options
//        var options = allBindingsAccessor().dateTimePickerOptions || {
//            format: 'DD/MM/YYYY'
////            , minDate: date.setDate(date.getDate() - 1)
//            , ignoreReadonly: true
//            , locale: 'ru'
//        };
//        var defaults = {
//            icons: {
//                time: "fa fa-clock-o",
//                date: "fa fa-calendar",
//                up: "fa fa-arrow-up",
//                down: "fa fa-arrow-down",
//                previous: 'fa fa-chevron-left',
//                next: 'fa fa-chevron-right',
//                today: 'fa fa-calendar-check-o',
//                clear: 'fa fa-eraser',
//                close: 'fa fa-times'
//            }
//        };
//        options = $.extend(true, {}, defaults, options);
//        $(element).datetimepicker(options);
//
//        //when a user changes the date, update the view model
//        ko.utils.registerEventHandler(element, "dp.change", function (event) {
//            var value = valueAccessor();
//            if (ko.isObservable(value)) {
//                if (event.date === false) {
//                    value(null);
//                } else {
//                    value(event.date);
//                }
//            }
//        });
//
//        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
//            var picker = $(element).data("DateTimePicker");
//            if (picker) {
//                picker.destroy();
//            }
//        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

//        var picker = $(element).data("DateTimePicker");
//        //when the view model is updated, update the widget
//        if (picker) {
//            var koDate = ko.utils.unwrapObservable(valueAccessor());
//
//            //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
//            koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;
//
//            picker.date(koDate);
//        }
    }
};

ko.bindingHandlers.datepicker_old = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var date = new Date();
        var options = allBindingsAccessor().dateTimePickerOptions || {
            format: 'DD/MM/YYYY'
//            , minDate: date.setDate(date.getDate() - 1)
            , ignoreReadonly: true
            , locale: 'ru'
        };
        var defaults = {
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-calendar-check-o',
                clear: 'fa fa-eraser',
                close: 'fa fa-times'
            }
        };
        options = $.extend(true, {}, defaults, options);
        $(element).datetimepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "dp.change", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                if (event.date === false) {
                    value(null);
                } else {
                    value(event.date);
                }
            }
        });

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            var picker = $(element).data("DateTimePicker");
            if (picker) {
                picker.destroy();
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

        var picker = $(element).data("DateTimePicker");
        //when the view model is updated, update the widget
        if (picker) {
            var koDate = ko.utils.unwrapObservable(valueAccessor());

            //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
            koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;

            picker.date(koDate);
        }
    }
};


ko.bindingHandlers.sort = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var asc = false;
        element.style.cursor = 'pointer';
        if ($(element).hasClass("sorting_initial")) {
            $(element).removeClass("sorting_initial");
        } else {
            $(element).addClass("sorting_initial");
        }
        element.onclick = function() {
            var value = valueAccessor();
            var prop = value.prop;
            var data = value.arr;

            $(element).siblings().removeClass("sorting_asc");
            $(element).siblings().removeClass("sorting_desc");

            asc = !asc;
            if (asc) {
                $(element).removeClass("sorting_asc");
                $(element).addClass("sorting_desc");

                data.sort(function(left, right) {
                    return left[prop]() == right[prop]() ? 0 : left[prop]() < right[prop]() ? -1 : 1;
                });
            } else {
                $(element).removeClass("sorting_desc");
                $(element).addClass("sorting_asc");

                data.sort(function(left, right) {
                    return left[prop]() == right[prop]() ? 0 : left[prop]() > right[prop]() ? -1 : 1;
                });
            }
        };
    }
};


﻿

/* Bootstrap select picker custom binding from Internet  */

ko.bindingHandlers.selectPicker = {
    after: ['options'],   /* KO 3.0 feature to ensure binding execution order */
    init: function (element, valueAccessor, allBindingsAccessor) {
        var $element = $(element);
        $element.addClass('selectpicker').selectpicker();

        var doRefresh = function () {
            $element.selectpicker('refresh');
        }, subscriptions = [];

        // KO 3 requires subscriptions instead of relying on this binding's update
        // function firing when any other binding on the element is updated.

        // Add them to a subscription array so we can remove them when KO
        // tears down the element.  Otherwise you will have a resource leak.
        var addSubscription = function (bindingKey) {
            var targetObs = allBindingsAccessor.get(bindingKey);

            if (targetObs && ko.isObservable(targetObs)) {
                subscriptions.push(targetObs.subscribe(doRefresh));
            }
        };

        addSubscription('options');
        addSubscription('value');           // Single
        addSubscription('selectedOptions'); // Multiple

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            while (subscriptions.length) {
                subscriptions.pop().dispose();
            }
        });
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


//ko.bindingHandlers.datepicker = {
//    init: function (element, valueAccessor, allBindingsAccessor) {
//        //initialize datepicker with some optional options
//        var options = allBindingsAccessor().datepickerOptions || {
//            useCurrent: false,
//            autoclose: true
//        };
//        $(element).datepicker(options);
//
//        //when a user changes the date, update the view model
//        ko.utils.registerEventHandler(element, "changeDate", function (event) {
//            var value = valueAccessor();
//            if (ko.isObservable(value)) {
//                value(event.date);
//            }
//        });
//    },
//    update: function (element, valueAccessor) {
//        var value = ko.utils.unwrapObservable(valueAccessor());
//        $(element).val(value);
//    }
//};

//ko.bindingHandlers.datepicker = {
//    init: function(element, valueAccessor, allBindingsAccessor) {
//        //initialize datepicker with some optional options
//        var options = allBindingsAccessor().dateTimePickerOptions || {};
//        $(element).datetimepicker(options);
//
//        //when a user changes the date, update the view model
//        ko.utils.registerEventHandler(element,
//            "dp.change",
//            function(event) {
//                var value = valueAccessor();
//                if (ko.isObservable(value)) {
//                    if (event.date != null && !(event.date instanceof Date)) {
//                        value(event.date.toDate());
//                    } else {
//                        value(event.date);
//                    }
//                }
//            });
//
//        ko.utils.domNodeDisposal.addDisposeCallback(element,
//            function() {
//                var picker = $(element).data("DateTimePicker");
//                if (picker) {
//                    picker.destroy();
//                }
//            });
//    },
//    update: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
//
//        var picker = $(element).data("DateTimePicker");
//        //when the view model is updated, update the widget
//        if (picker) {
//            var koDate = ko.utils.unwrapObservable(valueAccessor());
//            koDate = koDate || new Date();
//            //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
//            koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;
//
//            picker.date(koDate);
//        }
//    }
//};


ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || { format: 'DD/MM/YYYY HH:mm' };
        $(element).datetimepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "dp.change", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                value(event.date);
            }
        });
    },
    update: function (element, valueAccessor) {
        var widget = $(element).data("DateTimePicker");
        //when the view model is updated, update the widget
        if (widget) {
            var date = ko.utils.unwrapObservable(valueAccessor());
            widget.date(date);
        }
    }
};


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


ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {
            useCurrent: false,
            autoclose: true
        };
        $(element).datepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "changeDate", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                value(event.date);
            }
        });
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).val(value);
    }
};
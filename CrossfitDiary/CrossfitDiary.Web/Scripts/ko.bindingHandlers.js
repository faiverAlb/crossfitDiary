

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


ko.bindingHandlers.scroll = {

    updating: true,

    init: function (element, valueAccessor, allBindingsAccessor) {
        var self = this;
        self.updating = true;
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(window).off("scroll.ko.scrollHandler");
            self.updating = false;
        });
    },

    update: function (element, valueAccessor, allBindingsAccessor) {
        var props = allBindingsAccessor().scrollOptions;
        var offset = props.offset ? props.offset : "0";
        var loadFunc = props.loadFunc;
        var load = ko.utils.unwrapObservable(valueAccessor());
        var self = this;

        if (load) {
            element.style.display = "";
            $(window).on("scroll.ko.scrollHandler", function () {
                if (($(document).height() - offset <= $(window).height() + $(window).scrollTop())) {
                    if (self.updating) {
                        loadFunc();
                        self.updating = false;
                    }
                } else {
                    self.updating = true;
                }
            });
        } else {
            element.style.display = "none";
            $(window).off("scroll.ko.scrollHandler");
            self.updating = false;
        }
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

function getFormattedDate(date) {
  var year = date.getFullYear();

  var month = (1 + date.getMonth()).toString();
  month = month.length > 1 ? month : '0' + month;

  var day = date.getDate().toString();
  day = day.length > 1 ? day : '0' + day;

  return day + '/' + month + '/' + year;
}

ko.bindingHandlers.datepicker = {
  init: function(element, valueAccessor, allBindingsAccessor) {
    let date = valueAccessor()();
    let format = 'dd/mm/yyyy';

    var options = {
      uiLibrary: 'bootstrap4',
      iconsLibrary: 'fontawesome',
      icons: {
        rightIcon: '<i class="far fa-calendar-alt"></i>'
      },
      value: getFormattedDate(date),
      maxDate: new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()),
      weekStartDay: 1,
      format: format
    };
    ko.utils.registerEventHandler(element,
      "change",
      function (event) {
        let datePickerElement = element;
        var formattedDate = moment(datePickerElement.value, "DD/MM/YYYY");
        var value = valueAccessor();
        value(formattedDate.toDate());
      });
    let datepicker = $(element).datepicker(options);

    datepicker.value();
  },
  update: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
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


ko.utils.showModalFromTemplate = function (modalProperties) {
  return innerModalMarkup(modalProperties.templateName
    , modalProperties.model
    , modalProperties.title
    , modalProperties.onOkModel
    , modalProperties.onCancelModel
    , modalProperties.additionalCss
    , modalProperties.onRefreshModel
    , "#shared-bootstrap-modal-template"
    , modalProperties.isLarge);

};
function innerModalMarkup(templateName, model, title, onOkModel, onCancelModel, additionalCss, onRefreshModel, modalContainer, isLarge, additionalFunctions) {
    model.modalHeader = title;
    model.modalLarge = isLarge ? "modal-lg" : "";
    model.templateName = templateName;
    var onOk = onOkModel || {};
    model.onOk = onOk.okFunction;
    model.okText = onOk.okText;
    model.okClass = onOk.cssClass||"";
    var onCancel = onCancelModel || {};
    model.onCancel = onCancel.onCancelFunction || null;
    model.showCancel = onCancel.isShown != undefined ? onCancel.isShown : true;
    model.cancelText = onCancel.cancelText || "Cancel";

    model.ShowOk = ko.computed({
        read: function() {
            return model.onOk != null;
        },
        deferEvaluation: true
    });
    model.EnableOk = onOk.enableFunction != null ? onOk.enableFunction : true;
    var onRefresh = onRefreshModel || {};
    model.onRefresh = onRefresh.onRefreshFunction || null;
    model.ShowRefresh = ko.computed({
        read: function() {
            return model.onRefresh != null;
        },
        deferEvaluation: true
    });
    var $modalMarkup = $($(modalContainer).html());
    ko.applyBindings(model, $modalMarkup[0]);

   
    if (additionalCss)
        $modalMarkup.find('.modal-body').addClass(additionalCss);
    $modalMarkup.find('[role="tablist"]').on("click", '[role="tab"]', function (e) {
        e.preventDefault();
        if ($(this).next('.panel-collapse.collapse.in').length > 0) {
            $(this).next().collapse('hide');
        } else {
            $(this).next().collapse('show');
        }
    });
    if (additionalFunctions) {
        var modalBody = $modalMarkup.find('.modal-body');
        for (var i = 0; i <= additionalFunctions.length - 1; i++) {
            modalBody.on(additionalFunctions[i].event,
                additionalFunctions[i].functionToExecute);
        }
    }

    $modalMarkup.find('ul.nav-tabs')
        .on('click',
            'li:not(.active)',
            function() {
                $(this)
                    .addClass('active')
                    .siblings()
                    .removeClass('active')
                    .closest('div.tabs')
                    .find('div.tab-pane')
                    .removeClass('in active')
                    .eq($(this).index())
                    .addClass('in active');
            });

    $modalMarkup.modal({ show: true })
        .on("hidden.bs.modal",
            function () {
                $modalMarkup.detach();
            });

};

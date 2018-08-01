var General;
(function (General) {
    var ErrorMessageViewModel = (function () {
        function ErrorMessageViewModel() {
            var _this = this;
            this.message = ko.observable(null);
            this.message = ko.observable(null);
            this.isDismissable = ko.observable(true);
            this.hasMessage = ko.pureComputed(function () { return _this.message() !== null && _this.message() !== undefined; });
        }
        ErrorMessageViewModel.prototype.addMessage = function (message, isDismissable, scrollToError) {
            if (isDismissable === void 0) { isDismissable = true; }
            if (scrollToError === void 0) { scrollToError = true; }
            this.message(message);
            this.isDismissable(isDismissable);
            if (scrollToError) {
                var elem = document.getElementsByClassName("errors-container")[0];
                if (elem) {
                    elem.scrollIntoView({ behavior: "smooth" });
                }
                ;
            }
            ;
        };
        ;
        ErrorMessageViewModel.prototype.deleteMessage = function () {
            this.message(null);
            this.isDismissable(true);
        };
        ;
        ErrorMessageViewModel.prototype.ajaxFail = function (xmlRequest) {
            if (xmlRequest.responseText) {
                this.message(xmlRequest.statusText + " : \"" + xmlRequest.responseText + "\"");
                return;
            }
            if (xmlRequest.statusText) {
                this.message(xmlRequest.statusText);
                return;
            }
            this.message(xmlRequest.stack);
        };
        return ErrorMessageViewModel;
    }());
    General.ErrorMessageViewModel = ErrorMessageViewModel;
})(General || (General = {}));
//# sourceMappingURL=ErrorMessageViewModel.js.map
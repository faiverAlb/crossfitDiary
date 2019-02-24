var General;
(function (General) {
    var ErrorMessageViewModel = /** @class */ (function () {
        //    message: KnockoutObservable<string> = ko.observable<string>(null);
        //    isDismissable: KnockoutObservable<boolean>;
        //    hasMessage: KnockoutComputed<boolean>;
        function ErrorMessageViewModel() {
            //      this.message = ko.observable<string>(null);
            //      this.isDismissable = ko.observable(true);
            //      this.hasMessage = ko.pureComputed(() => this.message() !== null && this.message() !== undefined);
        }
        ErrorMessageViewModel.prototype.addMessage = function (message, isDismissable, scrollToError) {
            if (isDismissable === void 0) { isDismissable = true; }
            if (scrollToError === void 0) { scrollToError = true; }
            //      this.message(message);
            //      this.isDismissable(isDismissable);
            ///
            //      if (scrollToError) {
            //        let elem = document.getElementsByClassName("errors-container")[0];
            //        if (elem) {
            //          elem.scrollIntoView({ behavior: "smooth" });
            //        };
            //      };
        };
        ;
        ErrorMessageViewModel.prototype.deleteMessage = function () {
            //      this.message(null);
            //      this.isDismissable(true);
        };
        ;
        ErrorMessageViewModel.prototype.ajaxFail = function (xmlRequest) {
            //      if (xmlRequest.responseText) {
            //        this.message(`${xmlRequest.statusText} : "${xmlRequest.responseText}"`);
            //        return;
            //      }
            //      if (xmlRequest.statusText) {
            //        this.message(xmlRequest.statusText);
            //        return;
            //      }
            //      this.message(xmlRequest.stack);
        };
        return ErrorMessageViewModel;
    }());
    General.ErrorMessageViewModel = ErrorMessageViewModel;
})(General || (General = {}));
//# sourceMappingURL=ErrorMessageViewModel.js.map
var General;
(function (General) {
    var BaseController = (function () {
        function BaseController() {
            this.isDataLoading = ko.observable(false);
        }
        return BaseController;
    }());
    General.BaseController = BaseController;
})(General || (General = {}));
//# sourceMappingURL=BaseController.js.map
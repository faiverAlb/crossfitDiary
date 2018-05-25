var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Pages;
(function (Pages) {
    var BaseController = General.BaseController;
    var CrossfitterService = General.CrossfitterService;
    var ErrorMessageViewModel = General.ErrorMessageViewModel;
    var HomePageController = (function (_super) {
        __extends(HomePageController, _super);
        /* Computeds */
        function HomePageController(parameters) {
            var _this = _super.call(this) || this;
            _this.parameters = parameters;
            _this.page = 1;
            _this.pageSize = 45;
            _this.loadElements = function () {
                _this.isDataLoading(true);
                _this._service.getAllCrossfittersWorkouts(_this.parameters.userId, _this.parameters.exerciseId, _this.page, _this.pageSize)
                    .then(function (data) {
                    ko.utils.arrayPushAll(_this.allWorkouts, data);
                    _this.hasMoreElements(data.length === _this.pageSize);
                })
                    .fail(function (response) {
                    _this.errorMessager.addMessage(response.responseText, false);
                })
                    .finally(function () {
                    _this.isDataLoading(false);
                    _this.page += 1;
                });
            };
            _this.removeWorkoutConfirmation = function (crossfitterWorkoutId) {
                ko.utils.showModalFromTemplate({
                    templateName: Pages.TemplatesNames.ConfirmToRemoveWorkout,
                    model: { crossfitterWorkoutId: crossfitterWorkoutId },
                    title: "Sure to remove workout?",
                    onOkModel: {
                        okFunction: _this.removeWorkout,
                        okText: "Delete",
                        cssClass: "btn-danger"
                    }
                });
            };
            _this.removeWorkout = function (model) {
                _this.isDataLoading(true);
                _this._service.removeWorkout(model.crossfitterWorkoutId)
                    .then(function () {
                    _this.isDataLoading(false);
                    window.location.href = "/";
                })
                    .fail(function (response) {
                    _this.isDataLoading(false);
                    _this.errorMessager.addMessage(response.responseText, false);
                });
            };
            _this.errorMessager = new ErrorMessageViewModel();
            _this._service = new CrossfitterService(parameters.pathToApp, _this.isDataLoading);
            _this.allWorkouts = ko.observableArray([]);
            _this.hasMoreElements = ko.observable(false);
            _this.loadElements();
            return _this;
        }
        return HomePageController;
    }(BaseController));
    Pages.HomePageController = HomePageController;
    ;
})(Pages || (Pages = {}));
//# sourceMappingURL=HomePageController.js.map
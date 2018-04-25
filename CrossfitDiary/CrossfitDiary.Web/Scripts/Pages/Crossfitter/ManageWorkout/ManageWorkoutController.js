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
    var CrossfitterService = General.CrossfitterService;
    var BaseController = General.BaseController;
    var ErrorMessageViewModel = General.ErrorMessageViewModel;
    ko.validation.init({
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block',
        decorateInputElement: true,
        insertMessages: false,
        grouping: {
            deep: true,
            live: true,
            observable: true
        }
    });
    var ManageWorkoutController = (function (_super) {
        __extends(ManageWorkoutController, _super);
        /* Computeds */
        function ManageWorkoutController(parameters, preselectedWorkoutId, preselectedCrossfitterWorkoutId) {
            if (preselectedWorkoutId === void 0) { preselectedWorkoutId = null; }
            if (preselectedCrossfitterWorkoutId === void 0) { preselectedCrossfitterWorkoutId = null; }
            var _this = _super.call(this) || this;
            _this.parameters = parameters;
            _this.onWorkoutToShowAction = function (isCleanLogModel) {
                if (isCleanLogModel) {
                    _this._logWorkoutController(null);
                    return;
                }
                _this._logWorkoutController(new Pages.LogWorkoutController(_this._createWorkoutController.workoutToDisplay(), true, _this._service, _this.errorMessager));
            };
            /* Сivilians */
            _this._service = new CrossfitterService(parameters.pathToApp, _this.isDataLoading);
            _this.errorMessager = new ErrorMessageViewModel();
            _this._createWorkoutController = new Pages.CreateWorkoutController(_this._service, _this.errorMessager, _this.onWorkoutToShowAction, preselectedWorkoutId, preselectedCrossfitterWorkoutId);
            /* Observables */
            _this._logWorkoutController = ko.observable(null);
            _this._isCreateNewWorkout = ko.observable(true);
            return _this;
        }
        return ManageWorkoutController;
    }(BaseController));
    Pages.ManageWorkoutController = ManageWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ManageWorkoutController.js.map
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
    var ToLogWorkoutViewModelObservable = Models.ToLogWorkoutViewModelObservable;
    var BaseController = General.BaseController;
    var LogWorkoutController = (function (_super) {
        __extends(LogWorkoutController, _super);
        /* Computeds */
        function LogWorkoutController(workoutToUse, isCreateAndLogWorkout, service, errorMessager) {
            var _this = _super.call(this) || this;
            _this.workoutToUse = workoutToUse;
            _this.isCreateAndLogWorkout = isCreateAndLogWorkout;
            _this.service = service;
            _this.errorMessager = errorMessager;
            _this._logWorkout = function () {
                if (_this.checkAndShowErrors()) {
                    return;
                }
                _this.createAndLogWorkout();
            };
            _this._logToCreate = ko.observable(new ToLogWorkoutViewModelObservable(workoutToUse.model.workoutType, workoutToUse.getId()));
            return _this;
        }
        LogWorkoutController.prototype.checkAndShowErrors = function () {
            var hasErrors = false;
            if (this.workoutToUse.errors().length > 0) {
                this.workoutToUse.errors.showAllMessages();
                hasErrors = true;
            }
            if (this._logToCreate().errors().length > 0) {
                this._logToCreate().errors.showAllMessages();
                hasErrors = true;
            }
            return hasErrors;
        };
        LogWorkoutController.prototype.createAndLogWorkout = function () {
            var _this = this;
            var workoutModel = this.workoutToUse.toPlainObject();
            var model = {
                newWorkoutViewModel: workoutModel,
                logWorkoutViewModel: this._logToCreate().toPlainObject()
            };
            this.service.createAndLogWorkout(model)
                .then(function () {
                window.location.href = "/Home";
            })
                .fail(function (response) {
                _this.errorMessager.addMessage(response.responseText, false);
            });
        };
        return LogWorkoutController;
    }(BaseController));
    Pages.LogWorkoutController = LogWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=LogWorkoutController.js.map
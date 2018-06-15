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
    var WorkoutViewModel = Models.WorkoutViewModel;
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var BaseKeyValuePairModel = General.BaseKeyValuePairModel;
    var WorkoutType = Models.WorkoutType;
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
        function ManageWorkoutController(parameters, preselectedWorkoutObject, preselectedCrossfitterWorkoutId) {
            if (preselectedCrossfitterWorkoutId === void 0) { preselectedCrossfitterWorkoutId = null; }
            var _this = _super.call(this) || this;
            _this.parameters = parameters;
            _this.preselectedCrossfitterWorkoutId = preselectedCrossfitterWorkoutId;
            _this.handleLogWorkoutController = function (needToCleanLog, logModel) {
                if (needToCleanLog) {
                    _this._logWorkoutController(null);
                    return;
                }
                _this._logWorkoutController(new Pages.LogWorkoutController(_this.workoutToDisplay(), true, _this._service, _this.errorMessager, _this._isEditMode, logModel));
            };
            _this.loadExercises = function () {
                return _this._service
                    .getExercises()
                    .then(function (exercises) {
                    _this._exercises = exercises;
                });
            };
            _this.loadPersonLogging = function () {
                return _this._service.getPersonLoggingInfo(_this.preselectedCrossfitterWorkoutId)
                    .then(function (logModel) {
                    _this.handleLogWorkoutController(false, logModel);
                })
                    .fail(function (response) {
                    _this.errorMessager.addMessage(response.responseText, false);
                });
            };
            /* Сivilians */
            _this._service = new CrossfitterService(parameters.pathToApp, _this.isDataLoading);
            _this.errorMessager = new ErrorMessageViewModel();
            _this.preselectedWorkout = new WorkoutViewModel().deserialize(preselectedWorkoutObject);
            _this._isEditMode = _this.preselectedWorkout != null && preselectedCrossfitterWorkoutId != null;
            _this._isRepeatMode = _this.preselectedWorkout != null && preselectedCrossfitterWorkoutId == null;
            _this._isDefaultMode = _this.preselectedWorkout == null && preselectedCrossfitterWorkoutId == null;
            /* Observables */
            _this._logWorkoutController = ko.observable(null);
            _this.selectedWorkoutType = ko.observable(null);
            _this.workoutToDisplay = ko.observable(null);
            var workout = _this.workoutToDisplay();
            _this.selectedWorkoutTypeId = ko.observable(workout == null ? null : workout.model.workoutType);
            if (workout != null) {
                _this.handleLogWorkoutController(false);
            }
            _this.selectedWorkoutType.subscribe(function (selectedWorkoutType) {
                if ((selectedWorkoutType == undefined || selectedWorkoutType == null)) {
                    _this.handleLogWorkoutController(true);
                    if (_this._isEditMode === false) {
                        _this.workoutToDisplay(null);
                    }
                    return;
                }
                _this.selectedWorkoutTypeId(_this.selectedWorkoutType().id);
                var model = new WorkoutViewModel({
                    workoutType: _this.selectedWorkoutType().id,
                    exercisesToDoList: [],
                    children: [],
                    isInnerWorkout: false
                });
                _this.workoutToDisplay(new WorkoutViewModelObservable(model, _this._exercises));
                _this.handleLogWorkoutController(false);
            });
            _this.loadExercises()
                .then(function () {
                if (_this._isEditMode === false && _this._isRepeatMode === false) {
                    _this.selectedWorkoutType(new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime]));
                }
            })
                .then(function () {
                if (_this.preselectedWorkout != null) {
                    _this.workoutToDisplay(new WorkoutViewModelObservable(_this.preselectedWorkout, _this._exercises));
                }
            })
                .then(function () {
                if (_this._isRepeatMode) {
                    _this.handleLogWorkoutController(false);
                }
                return _this._isEditMode ? _this.loadPersonLogging() : null;
            });
            return _this;
        }
        ManageWorkoutController.prototype.selectWorkoutType = function (workoutType) {
            this.selectedWorkoutType(new BaseKeyValuePairModel(workoutType, WorkoutType[workoutType]));
        };
        return ManageWorkoutController;
    }(BaseController));
    Pages.ManageWorkoutController = ManageWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ManageWorkoutController.js.map
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
    var BaseKeyValuePairModel = General.BaseKeyValuePairModel;
    var WorkoutType = Models.WorkoutType;
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var WorkoutViewModel = Models.WorkoutViewModel;
    var BaseController = General.BaseController;
    var CreateWorkoutController = (function (_super) {
        __extends(CreateWorkoutController, _super);
        /* Computeds */
        function CreateWorkoutController(service) {
            var _this = _super.call(this) || this;
            _this.service = service;
            _this.loadExercises = function () {
                _this.service
                    .getExercises()
                    .then(function (exercises) {
                    _this._exercises(exercises);
                });
            };
            _this.createWorkout = function () {
                if (_this.workoutToCreate().errors().length > 0) {
                    _this.workoutToCreate().errors.showAllMessages();
                    return;
                }
                var workoutToCreate = _this.workoutToCreate().toPlainObject();
                _this.service.createWorkout(workoutToCreate)
                    .then(function () {
                    window.location.href = "/Home";
                })
                    .fail(function (error) {
                    console.log(error);
                });
            };
            _this.clearState = function () {
                _this.selectedWorkoutType(null);
            };
            _this._workoutTypes = ko.observable(new Array(new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime]), new BaseKeyValuePairModel(WorkoutType.AMRAP, WorkoutType[WorkoutType.AMRAP]), new BaseKeyValuePairModel(WorkoutType.EMOM, WorkoutType[WorkoutType.EMOM]), new BaseKeyValuePairModel(WorkoutType.NotForTime, WorkoutType[WorkoutType.NotForTime])));
            _this.selectedWorkoutType = ko.observable(null);
            _this.workoutToCreate = ko.observable(null);
            _this._exercises = ko.observableArray([]);
            _this._selectedExercise = ko.observable(null);
            ko.computed(function () {
                _this._selectedExercise(null);
                if (!_this.selectedWorkoutType()) {
                    return;
                }
                if (_this._exercises().length === 0) {
                    return;
                }
                var model = new WorkoutViewModel(_this.selectedWorkoutType().id, []);
                _this.workoutToCreate(new WorkoutViewModelObservable(model, false));
            });
            ko.computed(function () {
                var exercise = _this._selectedExercise();
                if (!exercise) {
                    return;
                }
                _this.workoutToCreate().addExerciseToList(exercise);
                _this._selectedExercise(null);
            });
            _this.loadExercises();
            return _this;
        }
        return CreateWorkoutController;
    }(BaseController));
    Pages.CreateWorkoutController = CreateWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=CreateWorkoutController.js.map
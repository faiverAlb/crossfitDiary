var Pages;
(function (Pages) {
    var BaseKeyValuePairModel = General.BaseKeyValuePairModel;
    var WorkoutType = Models.WorkoutType;
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var WorkoutViewModel = Models.WorkoutViewModel;
    var CreateWorkoutController = (function () {
        /* Computeds */
        function CreateWorkoutController(service) {
            var _this = this;
            this.service = service;
            this.loadExercises = function () {
                _this.service
                    .getExercises()
                    .then(function (exercises) {
                    _this._exercises(exercises);
                });
            };
            this.createWorkout = function () {
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
            this.clearState = function () {
                _this.selectedWorkoutType(null);
            };
            this._workoutTypes = ko.observable(new Array(new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime]), new BaseKeyValuePairModel(WorkoutType.AMRAP, WorkoutType[WorkoutType.AMRAP]), new BaseKeyValuePairModel(WorkoutType.EMOM, WorkoutType[WorkoutType.EMOM]), new BaseKeyValuePairModel(WorkoutType.NotForTime, WorkoutType[WorkoutType.NotForTime])));
            this.selectedWorkoutType = ko.observable(null);
            this.workoutToCreate = ko.observable(null);
            this._exercises = ko.observableArray([]);
            this._selectedExercise = ko.observable(null);
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
            this.loadExercises();
        }
        return CreateWorkoutController;
    }());
    Pages.CreateWorkoutController = CreateWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=CreateWorkoutController.js.map
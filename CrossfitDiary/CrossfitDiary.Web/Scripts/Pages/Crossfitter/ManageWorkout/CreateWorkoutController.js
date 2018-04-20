var Pages;
(function (Pages) {
    var BaseKeyValuePairModel = General.BaseKeyValuePairModel;
    var WorkoutType = Models.WorkoutType;
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var WorkoutViewModel = Models.WorkoutViewModel;
    var CreateWorkoutController = (function () {
        /* Computeds */
        function CreateWorkoutController(service, errorMessager) {
            var _this = this;
            this.service = service;
            this.errorMessager = errorMessager;
            this.loadExercises = function () {
                _this.service
                    .getExercises()
                    .then(function (exercises) {
                    _this._exercises(exercises);
                });
            };
            this.loadAvailableWorkouts = function () {
                _this.service.getAvailableWorkouts()
                    .then(function (availableWorkouts) {
                    _this._availableWorkouts(availableWorkouts);
                })
                    .fail(function (response) {
                    _this.errorMessager.addMessage(response.responseText, false);
                });
            };
            this._workoutTypes = ko.observable(new Array(new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime]), new BaseKeyValuePairModel(WorkoutType.AMRAP, WorkoutType[WorkoutType.AMRAP]), new BaseKeyValuePairModel(WorkoutType.EMOM, WorkoutType[WorkoutType.EMOM]), new BaseKeyValuePairModel(WorkoutType.NotForTime, WorkoutType[WorkoutType.NotForTime])));
            this.selectedWorkoutType = ko.observable(null);
            this.workoutToDisplay = ko.observable(null);
            this._exercises = ko.observableArray([]);
            this._selectedExercise = ko.observable(null);
            this._availableWorkouts = ko.observableArray([]);
            this.selectedWorkout = ko.observable(null);
            ko.computed(function () {
                _this._selectedExercise(null);
                if (!_this.selectedWorkoutType()) {
                    _this.workoutToDisplay(null);
                    return;
                }
                if (_this._exercises().length === 0) {
                    return;
                }
                _this.selectedWorkout(null);
                var model = new WorkoutViewModel(_this.selectedWorkoutType().id, []);
                _this.workoutToDisplay(new WorkoutViewModelObservable(model, false));
            });
            ko.computed(function () {
                var workout = _this.selectedWorkout();
                if (!workout) {
                    _this.workoutToDisplay(null);
                    return;
                }
                _this.selectedWorkoutType(null);
                _this.workoutToDisplay(new WorkoutViewModelObservable(workout, false));
            });
            ko.computed(function () {
                var exercise = _this._selectedExercise();
                if (!exercise) {
                    return;
                }
                _this.workoutToDisplay().addExerciseToList(exercise);
                _this._selectedExercise(null);
            });
            Q.all([this.loadExercises(), this.loadAvailableWorkouts()]);
        }
        return CreateWorkoutController;
    }());
    Pages.CreateWorkoutController = CreateWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=CreateWorkoutController.js.map
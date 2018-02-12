var Pages;
(function (Pages) {
    var BaseKeyValuePairModel = General.BaseKeyValuePairModel;
    var WorkoutType = Models.WorkoutType;
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var WorkoutViewModel = Models.WorkoutViewModel;
    var CreateWorkoutController = (function () {
        function CreateWorkoutController(parameters, service) {
            var _this = this;
            this.loadExercises = function () {
                _this._service
                    .getExercises()
                    .then(function (exercises) {
                    _this._exercises(exercises);
                });
            };
            this.canCreateWorkout = function () {
                if (_this._workoutToCreate().errors().length > 0) {
                    _this._workoutToCreate().errors.showAllMessages();
                    return false;
                }
                return true;
            };
            this.createWorkout = function () {
                if (_this._workoutToCreate().errors().length > 0) {
                    _this._workoutToCreate().errors.showAllMessages();
                    return;
                }
                //      this._service.createWorkout(this.toJSON());
            };
            this._service = service;
            this._workoutTypes = ko.observable(new Array(new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime]), new BaseKeyValuePairModel(WorkoutType.AMRAP, WorkoutType[WorkoutType.AMRAP]), new BaseKeyValuePairModel(WorkoutType.EMOM, WorkoutType[WorkoutType.EMOM]), new BaseKeyValuePairModel(WorkoutType.NotForTime, WorkoutType[WorkoutType.NotForTime])));
            this._selectedWorkoutType = ko.observable(null);
            this._workoutToCreate = ko.observable(null);
            this._exercises = ko.observableArray([]);
            this._selectedExercise = ko.observable();
            ko.computed(function () {
                _this._selectedExercise(null);
                if (!_this._selectedWorkoutType()) {
                    return;
                }
                if (_this._exercises().length === 0) {
                    return;
                }
                var model = new WorkoutViewModel(_this._selectedWorkoutType().id, []);
                _this._workoutToCreate(new WorkoutViewModelObservable(model, false));
            });
            ko.computed(function () {
                var exercise = _this._selectedExercise();
                if (!exercise) {
                    return;
                }
                _this._workoutToCreate().addExerciseToList(exercise);
                _this._selectedExercise(null);
            });
            this.loadExercises();
        }
        return CreateWorkoutController;
    }());
    Pages.CreateWorkoutController = CreateWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=CreateWorkoutController.js.map
var Pages;
(function (Pages) {
    var BaseKeyValuePairModel = General.BaseKeyValuePairModel;
    var WorkoutType = Models.WorkoutType;
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var WorkoutViewModel = Models.WorkoutViewModel;
    var CreateWorkoutController = (function () {
        /* Computeds */
        function CreateWorkoutController(service, errorMessager, onWorkoutToShowAction, preselectedWorkoutId, preselectedCrossfitterWorkoutId) {
            if (preselectedWorkoutId === void 0) { preselectedWorkoutId = null; }
            if (preselectedCrossfitterWorkoutId === void 0) { preselectedCrossfitterWorkoutId = null; }
            var _this = this;
            this.service = service;
            this.errorMessager = errorMessager;
            this.onWorkoutToShowAction = onWorkoutToShowAction;
            this.preselectedWorkoutId = preselectedWorkoutId;
            this.preselectedCrossfitterWorkoutId = preselectedCrossfitterWorkoutId;
            this.loadExercises = function () {
                return _this.service
                    .getExercises()
                    .then(function (exercises) {
                    _this._exercises(exercises);
                });
            };
            this.loadPersonLogging = function () {
                return _this.service.getPersonLoggingInfo(_this.preselectedCrossfitterWorkoutId)
                    .then(function (logModel) {
                    _this.onWorkoutToShowAction(false, _this._isEditMode, logModel);
                })
                    .fail(function (response) {
                    _this.errorMessager.addMessage(response.responseText, false);
                });
            };
            this.findAndSetSelectedWorkout = function () {
                for (var i = 0; i < _this._availableWorkouts().length; i++) {
                    var workoutToFind = _this._availableWorkouts()[i];
                    if (workoutToFind.id == _this.preselectedWorkoutId) {
                        _this.selectedWorkout(workoutToFind);
                        break;
                    }
                }
            };
            this.loadAvailableWorkouts = function () {
                return _this.service.getAvailableWorkouts()
                    .then(function (availableWorkouts) {
                    _this._availableWorkouts(availableWorkouts);
                })
                    .then(function () {
                    if (_this._isEditMode || _this._isRepeatMode) {
                        _this.findAndSetSelectedWorkout();
                    }
                })
                    .fail(function (response) {
                    _this.errorMessager.addMessage(response.responseText, false);
                });
            };
            this._isEditMode = preselectedWorkoutId != null && preselectedCrossfitterWorkoutId != null;
            this._isRepeatMode = preselectedWorkoutId != null && preselectedCrossfitterWorkoutId == null;
            this._isDefaultMode = preselectedWorkoutId == null && preselectedCrossfitterWorkoutId == null;
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
                var model = new WorkoutViewModel({
                    workoutType: _this.selectedWorkoutType().id,
                    exercisesToDoList: []
                });
                _this.workoutToDisplay(new WorkoutViewModelObservable(model));
            });
            ko.computed(function () {
                var workout = _this.selectedWorkout();
                if (!workout) {
                    _this.workoutToDisplay(null);
                    return;
                }
                _this.selectedWorkoutType(null);
                _this.workoutToDisplay(new WorkoutViewModelObservable(workout));
            });
            ko.computed(function () {
                var exercise = _this._selectedExercise();
                if (!exercise) {
                    return;
                }
                _this.workoutToDisplay().addExerciseToList(exercise);
                _this._selectedExercise(null);
            });
            this.selectedWorkoutType.subscribe(function (selectedWorkoutType) {
                if (selectedWorkoutType == undefined || selectedWorkoutType == null) {
                    _this.onWorkoutToShowAction(true, _this._isEditMode);
                    return;
                }
                _this.onWorkoutToShowAction(false, _this._isEditMode);
            });
            this.selectedWorkout.subscribe(function (selectedWorkout) {
                if (selectedWorkout == undefined || selectedWorkout == null) {
                    _this.onWorkoutToShowAction(true, _this._isEditMode);
                    return;
                }
                _this.onWorkoutToShowAction(false, _this._isEditMode);
            });
            this.loadExercises()
                .then(function () {
                return _this.loadAvailableWorkouts();
            })
                .then(function () {
                return _this._isEditMode ? _this.loadPersonLogging() : null;
            });
        }
        return CreateWorkoutController;
    }());
    Pages.CreateWorkoutController = CreateWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=CreateWorkoutController.js.map
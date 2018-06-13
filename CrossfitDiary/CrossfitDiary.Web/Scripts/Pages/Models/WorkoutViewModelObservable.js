var Models;
(function (Models) {
    var WorkoutViewModelObservable = (function () {
        function WorkoutViewModelObservable(model, exercises) {
            var _this = this;
            this.model = model;
            this.exercises = exercises;
            this.addExerciseToList = function (exerciseViewModel) {
                _this._exercisesToBeDone.push(new Models.ExerciseViewModelObservable(exerciseViewModel));
            };
            this.removeSimpleRoutineFromToDo = function (index) {
                _this._exercisesToBeDone.splice(index(), 1);
            };
            this.addSimpleRoutineFromToDo = function (exerciseViewModel) {
                _this._exercisesToBeDone.push(new Models.ExerciseViewModelObservable(exerciseViewModel.model));
            };
            this.moveSimpleRoutineUp = function (index) {
                if (index > 0) {
                    var rowList = _this._exercisesToBeDone();
                    _this._exercisesToBeDone.splice(index - 1, 2, rowList[index], rowList[index - 1]);
                }
            };
            this.moveSimpleRoutineDown = function (index) {
                var rowList = _this._exercisesToBeDone();
                if (index < rowList.length - 1) {
                    _this._exercisesToBeDone.splice(index, 2, rowList[index + 1], rowList[index]);
                }
            };
            this.toPlainObject = function () {
                var workoutToCreate = new Models.WorkoutViewModel({
                    id: _this.model.id,
                    title: _this._title(),
                    roundsCount: _this._roundsCount(),
                    timeToWork: _this._timeToWork(),
                    timeCap: _this._timeCap(),
                    restBetweenExercises: _this._restBetweenExercises(),
                    restBetweenRounds: _this._restBetweenRounds(),
                    workoutType: _this.model.workoutType,
                    exercisesToDoList: _this._exercisesToBeDone().map(function (item) { return item.toPlainObject(); }),
                    children: _this._innerWorkouts().map(function (item) { return item.toPlainObject(); })
                });
                return workoutToCreate;
            };
            this.addInnerForTimeWorkout = function () {
                _this._innerWorkouts.push(new WorkoutViewModelObservable(new Models.WorkoutViewModel({
                    id: 0,
                    title: null,
                    workoutType: Models.WorkoutType.ForTime,
                    exercisesToDoList: [],
                    children: []
                }), _this.exercises));
            };
            this.addInnerWorkout = function () {
                _this.addInnerForTimeWorkout();
            };
            /* Сivilians */
            this._workoutTypeTitle = Models.WorkoutType[model.workoutType];
            this._exercisesToBeDone = ko.observableArray(model.exercisesToDoList.map(function (item) {
                return new Models.ExerciseViewModelObservable(item);
            }));
            this._innerWorkouts = ko.observableArray(model.children.map(function (workout) {
                return new WorkoutViewModelObservable(workout, exercises);
            }));
            this._canSeeRoundsCount = this.model.workoutType === Models.WorkoutType.ForTime;
            this._isForTimeManyInnersType = this.model.workoutType === Models.WorkoutType.ForTimeManyInners;
            this._canSeeTimeToWork = this.model.workoutType === Models.WorkoutType.AMRAP || this.model.workoutType === Models.WorkoutType.EMOM;
            this._isWorkoutsContainer = this.model.workoutType === Models.WorkoutType.ForTimeManyInners;
            this._canSeeGeneralWorkoutInfo = this._canSeeTimeToWork || this._canSeeRoundsCount;
            /* Observables */
            this._id = ko.observable(model.id);
            this._title = ko.observable(model.title);
            this._restBetweenExercises = ko.observable(model.restBetweenExercises);
            this._restBetweenRounds = ko.observable(model.restBetweenRounds);
            this._timeToWork = ko.observable(model.timeToWork);
            this._timeCap = ko.observable(model.timeCap);
            this._roundsCount = ko.observable(model.roundsCount);
            this._exercises = ko.observableArray(exercises);
            this._selectedExercise = ko.observable(null);
            ko.computed(function () {
                var exercise = _this._selectedExercise();
                if (!exercise) {
                    return;
                }
                _this.addExerciseToList(exercise);
                _this._selectedExercise(null);
            });
            this._timeToWork.extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeTimeToWork;
                    }
                }
            });
            this._timeCap.extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeRoundsCount;
                    }
                }
            });
            this._roundsCount.extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeRoundsCount;
                    }
                }
            });
            this._exercisesToBeDone.extend({
                minLength: {
                    message: "At least one exercise is required",
                    onlyIf: function () {
                        return _this._isWorkoutsContainer === false;
                    }
                }
            });
            this.errors = ko.validation.group(this);
            //  If first time then add default first workout
            if (model.children.length === 0 && this.model.workoutType === Models.WorkoutType.ForTimeManyInners) {
                this.addInnerForTimeWorkout();
            }
        }
        WorkoutViewModelObservable.prototype.getId = function () {
            return this._id();
        };
        return WorkoutViewModelObservable;
    }());
    Models.WorkoutViewModelObservable = WorkoutViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=WorkoutViewModelObservable.js.map
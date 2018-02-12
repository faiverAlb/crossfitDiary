var Models;
(function (Models) {
    var WorkoutViewModel = (function () {
        function WorkoutViewModel(workoutType, exercises) {
            this.workoutType = workoutType;
            this.exercisesToDoList = exercises;
        }
        return WorkoutViewModel;
    }());
    Models.WorkoutViewModel = WorkoutViewModel;
    var WorkoutViewModelObservable = (function () {
        function WorkoutViewModelObservable(model, isReadOnlyMode) {
            var _this = this;
            this.model = model;
            /* Сivilians */
            this._isReadOnlyMode = isReadOnlyMode;
            this._exercisesToBeDone = ko.observableArray(model.exercisesToDoList.map(function (item) {
                return new Models.ExerciseViewModelObservable(item);
            }));
            /* Observables */
            this._title = ko.observable(model.title);
            this._restBetweenExercises = ko.observable(model.restBetweenExercises);
            this._restBetweenRounds = ko.observable(model.restBetweenRounds);
            this._timeToWork = ko.observable();
            this._roundsCount = ko.observable();
            /* Computeds */
            this._canSeeRoundsCount = ko.computed(function () {
                return _this.model.workoutType === Models.WorkoutType.ForTime;
            });
            this._anyUsualExercises = ko.computed(function () {
                return ko.utils.arrayFirst(_this._exercisesToBeDone(), function (exercise) { return exercise.model.isAlternative === false; }) != null;
            });
            this._canSeeTimeToWork = ko.computed(function () {
                return _this.model.workoutType === Models.WorkoutType.AMRAP || _this.model.workoutType === Models.WorkoutType.EMOM;
            });
            this._timeToWork.extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeTimeToWork();
                    }
                }
            });
            this._roundsCount.extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeRoundsCount();
                    }
                }
            });
            this.errors = ko.validation.group(this);
        }
        WorkoutViewModelObservable.prototype.addExerciseToList = function (exerciseViewModel) {
            this._exercisesToBeDone.push(new Models.ExerciseViewModelObservable(exerciseViewModel));
        };
        WorkoutViewModelObservable.prototype.removeSimpleRoutineFromToDo = function (index) {
            this._exercisesToBeDone.splice(index(), 1);
        };
        return WorkoutViewModelObservable;
    }());
    Models.WorkoutViewModelObservable = WorkoutViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=WorkoutViewModel.js.map
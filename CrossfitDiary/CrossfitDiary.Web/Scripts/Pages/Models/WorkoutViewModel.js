var Models;
(function (Models) {
    var WorkoutViewModel = (function () {
        function WorkoutViewModel(params) {
            if (params == null) {
                return;
            }
            this.id = params.id;
            this.title = params.title;
            this.detailedTitle = params.detailedTitle;
            this.roundsCount = params.roundsCount;
            this.timeToWork = params.timeToWork;
            this.restBetweenExercises = params.restBetweenExercises;
            this.restBetweenRounds = params.restBetweenRounds;
            this.workoutType = params.workoutType;
            this.exercisesToDoList = params.exercisesToDoList;
        }
        WorkoutViewModel.prototype.deserialize = function (jsonInput) {
            if (jsonInput == null) {
                return null;
            }
            return new WorkoutViewModel({
                id: jsonInput.id,
                title: jsonInput.title,
                detailedTitle: jsonInput.detailedTitle,
                roundsCount: jsonInput.roundsCount,
                timeToWork: jsonInput.timeToWork,
                restBetweenExercises: jsonInput.restBetweenExercises,
                restBetweenRounds: jsonInput.restBetweenRounds,
                workoutType: jsonInput.workoutType,
                exercisesToDoList: jsonInput.exercisesToDoList
            });
        };
        return WorkoutViewModel;
    }());
    Models.WorkoutViewModel = WorkoutViewModel;
    var WorkoutViewModelObservable = (function () {
        function WorkoutViewModelObservable(model) {
            var _this = this;
            this.model = model;
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
                var workoutToCreate = new WorkoutViewModel({
                    id: _this.model.id,
                    title: _this._title(),
                    detailedTitle: _this.model.detailedTitle,
                    roundsCount: _this._roundsCount(),
                    timeToWork: _this._timeToWork(),
                    restBetweenExercises: _this._restBetweenExercises(),
                    restBetweenRounds: _this._restBetweenRounds(),
                    workoutType: _this.model.workoutType,
                    exercisesToDoList: _this._exercisesToBeDone().map(function (item) { return item.toPlainObject(); })
                });
                return workoutToCreate;
            };
            /* Сivilians */
            this._workoutTypeTitle = Models.WorkoutType[model.workoutType];
            this._exercisesToBeDone = ko.observableArray(model.exercisesToDoList.map(function (item) {
                return new Models.ExerciseViewModelObservable(item);
            }));
            /* Observables */
            this._id = ko.observable(model.id);
            this._title = ko.observable(model.title);
            this._restBetweenExercises = ko.observable(model.restBetweenExercises);
            this._restBetweenRounds = ko.observable(model.restBetweenRounds);
            this._timeToWork = ko.observable(model.timeToWork);
            this._roundsCount = ko.observable(model.roundsCount);
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
            this._canSeeGeneralWorkoutInfo = ko.computed(function () {
                return _this._canSeeTimeToWork() || _this._canSeeRoundsCount();
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
            this._exercisesToBeDone.extend({
                minLength: {
                    message: "At least one exercise is required"
                }
            });
            this.errors = ko.validation.group(this);
        }
        WorkoutViewModelObservable.prototype.getId = function () {
            return this._id();
        };
        return WorkoutViewModelObservable;
    }());
    Models.WorkoutViewModelObservable = WorkoutViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=WorkoutViewModel.js.map
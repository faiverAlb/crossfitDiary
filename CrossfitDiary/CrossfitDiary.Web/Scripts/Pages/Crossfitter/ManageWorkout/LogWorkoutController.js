var Pages;
(function (Pages) {
    var WorkoutTypes = Models.WorkoutType;
    var LogWorkoutController = (function () {
        function LogWorkoutController(lightModel, logFunction) {
            var _this = this;
            this.lightModel = lightModel;
            this.logFunction = logFunction;
            this.updateInputsVisibility = function () {
                if (!_this.lightModel.selectedWorkoutType || !_this.lightModel.simpleRoutines) {
                    return;
                }
                var selectedTypeValue = _this.lightModel.selectedWorkoutType.Value;
                /* Rounds */
                _this.canSeeTotalRounds(selectedTypeValue == WorkoutTypes.AMRAP);
                //        /* Distance input */
                _this.canSeePassedDistance(_this.checkWorkoutContainsDistanceExercise() && selectedTypeValue == WorkoutTypes.EMOM);
                /* General time */
                _this.canSeeTotalTime(selectedTypeValue == WorkoutTypes.ForTime);
            };
            this.checkWorkoutContainsDistanceExercise = function () {
                return ko.utils.arrayFirst(_this.lightModel.simpleRoutines, function (routine) {
                    var foundDistanceMeasure = ko.utils.arrayFirst(routine.exerciseMeasures(), function (measure) { return measure.measureType() == Models.ExerciseMeasureType.Distance; });
                    return foundDistanceMeasure;
                }) != null;
            };
            this.logWorkout = function () {
                _this.logFunction();
            };
            this.toJSON = function () {
                var date = _this.plannedDate().toDate().toUTCString();
                var model = {
                    selectedWorkoutId: _this.lightModel.selectedWorkout ? _this.lightModel.selectedWorkout().id : null,
                    crossfitterWorkoutId: _this.lightModel.crossfitterWorkoutId,
                    roundsFinished: _this.totalRoundsFinished(),
                    partialRepsFinished: _this.partialRepsFinished(),
                    timePassed: _this.totalTime(),
                    distance: _this.distance(),
                    wasFinished: _this.wasFinished(),
                    isRx: _this.isRx(),
                    date: date
                };
                return model;
            };
            this.canLogWorkout = function () {
                if (_this.errors().length > 0) {
                    _this.errors.showAllMessages();
                    return false;
                }
                return true;
            };
            this.canSeeTotalRounds = ko.observable(false);
            this.canSeePassedDistance = ko.observable(false);
            this.canSeeTotalTime = ko.observable(false);
            this.date = lightModel.date;
            this.logWorkoutText = ko.observable(lightModel.logWorkoutText);
            this.totalRoundsFinished = ko.observable()
                .extend({
                required: {
                    onlyIf: function () {
                        return _this.canSeeTotalRounds();
                    }
                }
            });
            this.partialRepsFinished = ko.observable();
            this.plannedDate = ko.observable(new Date());
            this.distance = ko.observable()
                .extend({
                required: {
                    onlyIf: function () {
                        return _this.canSeePassedDistance();
                    }
                }
            });
            this.wasFinished = ko.observable();
            this.isRx = ko.observable(true);
            this.IsModified = ko.observable();
            this.totalTime = ko.observable()
                .extend({
                required: {
                    onlyIf: function () {
                        return _this.canSeeTotalTime();
                    }
                }
            });
            ko.computed(function () {
                _this.updateInputsVisibility();
            });
            this.errors = ko.validation.group(this);
        }
        return LogWorkoutController;
    }());
    Pages.LogWorkoutController = LogWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=LogWorkoutController.js.map
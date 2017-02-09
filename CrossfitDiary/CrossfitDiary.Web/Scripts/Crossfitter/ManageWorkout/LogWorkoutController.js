var LogWorkoutController = function (lightModel, logFunction) {
    var self = this;

    self.canSeeTotalRounds = ko.observable(false);
    self.canSeePassedDistance = ko.observable(false);
    self.canSeeTotalTime = ko.observable(false);
    self.date = lightModel.date;

    self.logWorkoutText = ko.observable(lightModel.logWorkoutText);

    self.totalRoundsFinished = ko.observable()
        .extend({
            required: {
                onlyIf: function() {
                    return self.canSeeTotalRounds();
                }
            }
        });

    self.partialRepsFinished = ko.observable();

    self.distance = ko.observable()
        .extend({
            required: {
                onlyIf: function() {
                    return self.canSeePassedDistance();
                }
            }
        });

    
    self.wasFinished = ko.observable();
    self.isRx = ko.observable(true);
    self.IsModified = ko.observable();
    self.totalTime = ko.observable()
        .extend({
            required: {
                onlyIf: function() {
                    return self.canSeeTotalTime();
                }
            }
        });


    


    self.logWorkout = function () {
        logFunction();
    };

    function updateInputsVisibility() {
        if (!lightModel.selectedWorkoutType || !lightModel.simpleRoutines) {
            return;
        }
        var selectedTypeValue = lightModel.selectedWorkoutType.Value;

        /* Rounds */
        self.canSeeTotalRounds(selectedTypeValue == Crossfitter.WorkoutTypes.AMRAP);

//        /* Distance input */
        self.canSeePassedDistance(checkWorkoutContainsDistanceExercise() && selectedTypeValue == Crossfitter.WorkoutTypes.EMOM);

        /* General time */
        self.canSeeTotalTime(selectedTypeValue == Crossfitter.WorkoutTypes.ForTime);
    }

    function checkWorkoutContainsDistanceExercise() {
        return ko.utils.arrayFirst(lightModel.simpleRoutines, function (routine) {
            var foundDistanceMeasure = ko.utils.arrayFirst(routine.exerciseMeasures(), function (measure) {
                return measure.measureType() == Crossfitter.ExerciseMeasureTypes.Distance;
            });
            return foundDistanceMeasure;
        }) != null;
    }

    ko.computed(function() {
        updateInputsVisibility();
    });


    self.errors = ko.validation.group(self);
    self.canLogWorkout = function () {
        if (self.errors().length > 0) {
            self.errors.showAllMessages();
            return false;
        }
        return true;
    };



    self.toJSON = function() {
        var model = {
            selectedWorkoutId: lightModel.selectedWorkout ? lightModel.selectedWorkout().id : null,
            roundsFinished: self.totalRoundsFinished(),
            partialRepsFinished: self.partialRepsFinished(),
            timePassed: self.totalTime(),
            distance: self.distance(),
            wasFinished: self.wasFinished(),
            isRx: self.isRx(),
            date: self.date
        };
        return model;
    };

    return self;
};

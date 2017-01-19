var CreateWorkoutController = function (parameters) {
    var self = new CrossfitterController(parameters);
    self.exercises = ko.observableArray(parameters.viewModel.exercises);

    self.hasAnyRoutines = ko.computed(function() {
        return self.simpleRoutines().length > 0;
    });

    //  HACK for copy
    var alternativeExercises = JSON.parse(JSON.stringify(parameters.viewModel.exercises));

    for (var i = 0; i < alternativeExercises.length; i++) {
        alternativeExercises[i].isAlternative = true;
    }
    self.alternativeExercises = ko.observableArray(alternativeExercises);

    self.selectedExercise = ko.observable();
    self.selectedAlternativeExercise = ko.observable();

    self.workoutTypes = ko.observableArray(parameters.viewModel.workoutTypes);

    ko.computed(function() {
        if (self.selectedWorkoutType() || !self.selectedWorkoutType()) {
            self.simpleRoutines([]);
        }
    });
    self.errors = ko.validation.group(self);

    ko.computed(function() {
        var exercise = self.selectedExercise();
        if (!exercise) {
            return;
        }

        self.simpleRoutines.push(new SimpleRoutine(exercise, self.selectedWorkoutType().Value != Crossfitter.WorkoutTypes.Tabata));
        self.selectedExercise('');
    });

    ko.computed(function () {
        var exercise = self.selectedAlternativeExercise();
        if (!exercise) {
            return;
        }

        self.simpleRoutines.push(new SimpleRoutine(exercise));
        self.selectedAlternativeExercise('');
    });

    self.prepareCreateWorkout = function () {
        if (self.errors().length > 0) {
            self.errors.showAllMessages();
            return;
        }
    };

    self.clearState = function () {
        self.selectedWorkoutType(null);
    };

    return self;
};

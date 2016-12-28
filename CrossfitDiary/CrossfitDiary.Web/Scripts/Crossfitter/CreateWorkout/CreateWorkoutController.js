var CreateWorkoutController = function (parameters) {
    var self = new CrossfitterController(parameters);
    self.exercises = ko.observableArray(parameters.viewModel.exercises);
    self.alternativeExercises = ko.observableArray(parameters.viewModel.exercises);

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

        self.simpleRoutines.push(new SimpleRoutine(exercise));
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

    self.createWorkout = function () {
        if (self.errors().length > 0) {
            self.errors.showAllMessages();
            return;
        }
        self.service.createWorkout(self.toJSON());
    };

    return self;
};

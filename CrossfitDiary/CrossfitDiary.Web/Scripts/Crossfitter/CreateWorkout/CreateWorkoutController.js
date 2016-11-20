var CreateWorkoutController = function (parameters) {
    var self = new CrossfitterController(parameters);
    self.exercises = ko.observableArray(parameters.viewModel.exercises);
    self.selectedExercise = ko.observable();
    self.workoutTypes = ko.observableArray(parameters.viewModel.workoutTypes);

    ko.computed(function () {
        var exercise = self.selectedExercise();
        if (!exercise) {
            return;
        }

        self.simpleRoutines.push(new SimpleRoutine(exercise));
        self.selectedExercise('');
    });


    self.createWorkout = function () {
        self.service.createWorkout(self.toJSON());
    };

    return self;
};

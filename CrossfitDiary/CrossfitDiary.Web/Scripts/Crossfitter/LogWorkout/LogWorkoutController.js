var LogWorkoutController = function (parameters) {
    var self = this;
    self.availableWorkouts = ko.observableArray(parameters.viewModel.availableWorkouts);
    self.selectedWorkout = ko.observable();
    
    self.workoutToDisplay = ko.observable();
    self.logWorkout = function () {
        self.service.logWorkout(self.toJSON());
    };
    ko.computed(function () {
        var workout = self.selectedWorkout();
        if (!workout) {
            return;
        }

        self.workoutToDisplay(new CrossfitterController(workout));
//        self.simpleRoutines.push(new SimpleRoutine(exercise));
//        self.selectedExercise('');
    });

    return self;
};

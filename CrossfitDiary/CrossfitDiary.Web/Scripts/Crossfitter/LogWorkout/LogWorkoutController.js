var LogWorkoutController = function (parameters) {
    var self = this;
    self.availableWorkouts = ko.observableArray(parameters.viewModel.availableWorkouts);
    self.selectedWorkout = ko.observable();
    
    self.canSeeExercises = false;
    self.workoutToDisplay = ko.observable();
    self.logWorkout = function () {
        self.service.logWorkout(self.toJSON());
    };
    ko.computed(function () {
        var workout = self.selectedWorkout();
        if (!workout) {
            return;
        }
        workout.canSeeExercises = false;
        self.workoutToDisplay(new CrossfitterController(workout));
    });

    return self;
};

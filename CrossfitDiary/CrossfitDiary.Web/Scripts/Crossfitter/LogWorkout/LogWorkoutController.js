var LogWorkoutController = function (parameters) {
    var self = new CrossfitterController(parameters);
    self.availableWorkouts = ko.observableArray(parameters.viewModel.availableWorkouts);
    self.selectedWorkout = ko.observable();

    self.logWorkout = function () {
        self.service.logWorkout(self.toJSON());
    };

    return self;
};

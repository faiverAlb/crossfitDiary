var LogWorkoutController = function (parameters) {
    var self = new CrossfitterController(parameters);

    self.logWorkout = function () {
        self.service.logWorkout(self.toJSON());
    };
    debugger;
    return self;
};

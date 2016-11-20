var CreateWorkoutController = function (parameters) {
    var self = new CrossfitterController(parameters);

    self.createWorkout = function () {
        self.service.createWorkout(self.toJSON());
    };

    return self;
};

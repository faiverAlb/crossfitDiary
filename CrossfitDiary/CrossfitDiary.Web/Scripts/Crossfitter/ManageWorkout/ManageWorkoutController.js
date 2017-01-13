var ManageWorkoutController = function (parameters) {
    var self = this;

    self.createWorkoutViewModel = new CreateWorkoutController(parameters);

    return self;
};

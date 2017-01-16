var ManageWorkoutController = function (parameters) {
    var self = this;

    self.createWorkoutController = new CreateWorkoutController(parameters);
    self.chooseExistingWorkoutController = new ChooseExistingWorkoutController(parameters);

    self.manageWorkoutClick = function (isCreateNewWorkout) {
        if (isCreateNewWorkout) {
            self.chooseExistingWorkoutController.clearState();
        } else {
            self.createWorkoutController.clearState();
        }
    };

    return self;
};

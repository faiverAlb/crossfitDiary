var ManageWorkoutController = function (parameters) {
    var self = this;

    self.createWorkoutController = new CreateWorkoutController(parameters);
    self.chooseExistingWorkoutController = new ChooseExistingWorkoutController(parameters);

    self.isAnyContainersVisible = ko.observable(false);

    self.manageWorkoutClick = function (isCreateNewWorkout) {
        if (isCreateNewWorkout) {
            self.chooseExistingWorkoutController.clearState();
            self.createWorkoutController.isContainerVisible(!self.createWorkoutController.isContainerVisible());
            self.chooseExistingWorkoutController.isContainerVisible(false);
        } else {
            self.createWorkoutController.clearState();
            self.createWorkoutController.isContainerVisible(false);
            self.chooseExistingWorkoutController.isContainerVisible(!self.chooseExistingWorkoutController.isContainerVisible());
        }
        
    };

    ko.computed(function() {
        self.isAnyContainersVisible((self.chooseExistingWorkoutController.isContainerVisible() && self.chooseExistingWorkoutController.selectedWorkout())
                                 || (self.createWorkoutController.isContainerVisible() && self.createWorkoutController.selectedWorkoutType()));
    });

    return self;
};

var ManageWorkoutController = function (parameters) {
    var self = this;
    self.createWorkoutController = new CreateWorkoutController(parameters);
    self.chooseExistingWorkoutController = new ChooseExistingWorkoutController(parameters);

    self.logWorkoutController = ko.observable();
    self.isAnyContainersVisible = ko.observable(false);

    var logFunction = function () {
        debugger;
    };


    var createLogController = function (lightLogModel) {
        self.logWorkoutController(new LogWorkoutController(lightLogModel, logFunction));
    };

    self.chooseExistingWorkoutController.workoutToDisplay.subscribe(function (newValue) {
        if (!newValue) {
            return;
        }
        var lightLogModel = {
            selectedWorkoutType: newValue.selectedWorkoutType(),
            simpleRoutines: newValue.simpleRoutines()
        };

        createLogController(lightLogModel);
    });

    self.createWorkoutController.simpleRoutines.subscribe(function (newValue) {
        if (!newValue || newValue.length === 0) {
            return;
        }
        var lightLogModel = {
            selectedWorkoutType: self.createWorkoutController.selectedWorkoutType(),
            simpleRoutines: self.createWorkoutController.simpleRoutines()
        };
        createLogController(lightLogModel);
    });

    self.manageWorkoutClick = function (isCreateNewWorkout) {
        if (isCreateNewWorkout) {
            self.chooseExistingWorkoutController.clearState();
        } else {
            self.createWorkoutController.clearState();
        }
        self.logWorkoutController(null);
    };


    ko.computed(function() {
        self.isAnyContainersVisible(self.logWorkoutController() != null && (self.chooseExistingWorkoutController.selectedWorkout() || self.createWorkoutController.hasAnyRoutines()));
    });

    return self;
};

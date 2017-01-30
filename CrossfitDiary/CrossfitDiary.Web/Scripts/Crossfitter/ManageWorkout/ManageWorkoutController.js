﻿var ManageWorkoutController = function (parameters) {
    var self = this;
    self.createWorkoutController = new CreateWorkoutController(parameters);
    self.chooseExistingWorkoutController = new ChooseExistingWorkoutController(parameters);
    self.service = new CrossfitterService(parameters.pathToApp);

    self.logWorkoutController = ko.observable();
    self.isAnyContainersVisible = ko.observable(false);

    self.isCreateNewWorkoutPressed = ko.observable(false);

    var logFunction = function() {
        var canLogWorkout = self.logWorkoutController().canLogWorkout();

        if (self.isCreateNewWorkoutPressed()) {
            var canCreateWorkout = self.createWorkoutController.canCreateCreateWorkout();
            if (canCreateWorkout && canLogWorkout) {
                var model = {
                    newWorkoutViewModel: self.createWorkoutController.getCreateWorkoutModel(),
                    logWorkoutViewModel: self.logWorkoutController().toJSON()
                };
                self.service.createAndLogWorkout(model);
            }
        } else {
            if (canLogWorkout) {
                self.service.logWorkout(self.logWorkoutController().toJSON());
            }
        }
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
            simpleRoutines: newValue.simpleRoutines(),
            selectedWorkout: self.chooseExistingWorkoutController.selectedWorkout,
            logWorkoutText:"Log selected workout"
        };

        createLogController(lightLogModel);
    });

    self.createWorkoutController.simpleRoutines.subscribe(function (newValue) {
        if (!newValue || newValue.length === 0) {
            return;
        }
        var lightLogModel = {
            selectedWorkoutType: self.createWorkoutController.selectedWorkoutType(),
            simpleRoutines: self.createWorkoutController.simpleRoutines(),
            logWorkoutText: "Create and log workout"
        };
        createLogController(lightLogModel);
    });

    self.manageWorkoutClick = function (isCreateNewWorkout) {
        if (isCreateNewWorkout) {
            self.chooseExistingWorkoutController.clearState();
        } else {
            self.createWorkoutController.clearState();
        }
        self.isCreateNewWorkoutPressed(isCreateNewWorkout);
        self.logWorkoutController(null);
    };


    ko.computed(function() {
        self.isAnyContainersVisible(self.logWorkoutController() != null && (self.chooseExistingWorkoutController.selectedWorkout() || self.createWorkoutController.hasAnyRoutines()));
    });

    return self;
};

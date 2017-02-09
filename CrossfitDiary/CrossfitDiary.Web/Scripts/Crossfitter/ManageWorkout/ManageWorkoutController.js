var ManageWorkoutController = function (parameters) {
    var self = this;
    self.createWorkoutController = new CreateWorkoutController(parameters);
    self.chooseExistingWorkoutController = new ChooseExistingWorkoutController(parameters);
    self.service = new CrossfitterService(parameters.pathToApp);

    self.logWorkoutController = ko.observable();
    self.isAnyContainersVisible = ko.observable(false);
    self.wantToPlanWorkout = ko.observable(false);

    self.withoutPreparedWorkout = ko.observable(parameters.viewModel.selectedWorkoutId == null);
    self.plannedDate = ko.observable(new Date(parameters.viewModel.planDate))
        .extend({
            required: {
                onlyIf: function() {
                    return self.wantToPlanWorkout();
                }
            }
        });

    self.isCreateNewWorkoutPressed = ko.observable(false);


    self.logWorkoutContainerClick = function() {
        self.wantToPlanWorkout(false);
    };

    self.planWorkoutContainerClick = function() {
        self.wantToPlanWorkout(!self.wantToPlanWorkout());
    };

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

    self.planWorkout = function () {
        var canPlanWorkout = self.plannedDate.isValid();
        if (self.isCreateNewWorkoutPressed()) {
            var canCreateWorkout = self.createWorkoutController.canCreateCreateWorkout();
            if (canCreateWorkout && canPlanWorkout) {
                var model = {
                    newWorkoutViewModel: self.createWorkoutController.getCreateWorkoutModel(),
                    logWorkoutViewModel: self.logWorkoutController().toJSON()
                };
                model.logWorkoutViewModel.isPlanned = true;
                self.service.createAndLogWorkout(model);
            }
        } else {
            if (canPlanWorkout) {
                var logModel = self.logWorkoutController().toJSON();
                logModel.isPlanned = true;
                self.service.logWorkout(logModel);
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
            logWorkoutText: "Log selected workout",
            date: parameters.viewModel.planDate
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
            logWorkoutText: "Create and log workout",
            date: parameters.viewModel.planDate
        };
        createLogController(lightLogModel);
    });


    self.manageWorkoutClick = function (isCreateNewWorkout) {
        self.chooseExistingWorkoutController.clearState();
        self.createWorkoutController.clearState();

        self.isCreateNewWorkoutPressed(isCreateNewWorkout);
        self.logWorkoutController(null);
    };


    ko.computed(function () {
        self.isAnyContainersVisible(self.logWorkoutController() != null && (self.chooseExistingWorkoutController.selectedWorkout() || self.createWorkoutController.hasAnyRoutines()));
    });

    function init() {
        if (self.withoutPreparedWorkout() == false) {
            var lightLogModel = {
                selectedWorkoutType: self.chooseExistingWorkoutController.workoutToDisplay().selectedWorkoutType(),
                simpleRoutines: self.chooseExistingWorkoutController.workoutToDisplay().simpleRoutines(),
                selectedWorkout: self.chooseExistingWorkoutController.selectedWorkout,
                logWorkoutText: "Log selected workout",
                date: parameters.viewModel.planDate
            };

            createLogController(lightLogModel);
        }  
    };

    init();

    return self;
};

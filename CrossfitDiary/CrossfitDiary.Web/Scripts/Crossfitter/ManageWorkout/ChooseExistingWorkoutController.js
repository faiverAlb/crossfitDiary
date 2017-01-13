var ChooseExistingWorkoutController = function (parameters) {

    var self = this;
    self.service = new CrossfitterService(parameters.pathToApp);
    self.availableWorkouts = ko.observableArray(parameters.viewModel.availableWorkouts);
    self.selectedWorkout = ko.observable();
    self.isReadOnlyMode = true;

    self.workoutToDisplay = ko.observable();

    ko.computed(function () {
        var workout = self.selectedWorkout();
        if (!workout) {
            return;
        }
        workout.isReadOnlyMode = self.isReadOnlyMode;
        self.workoutToDisplay(new CrossfitterController(workout));

    });


    self.toJSON = function () {
        var model = {
            selectedWorkoutId: self.selectedWorkout().id,
            roundsFinished: self.totalRoundsFinished,
            partialRepsFinished: self.partialRepsFinished,
            timePassed: self.totalTime,
            distance: self.distance,
            wasFinished: self.wasFinished,
            isRx: self.isRx()
        };
        return model;
    };

    return self;
};

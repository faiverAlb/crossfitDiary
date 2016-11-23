var LogWorkoutController = function (parameters) {
    var self = this;
    self.availableWorkouts = ko.observableArray(parameters.viewModel.availableWorkouts);
    self.selectedWorkout = ko.observable();
    self.isReadOnlyMode = true;

    self.totalRoundsFinished = ko.observable();
    self.partialRoundsFinished = ko.observable();

    self.distance = ko.observable();
    self.generalPoints = ko.observable();

    self.wasFinished = ko.observable();
    self.isRx = ko.observable();
    self.IsModified = ko.observable();
    self.workoutToDisplay = ko.observable();

    self.canSeeTotalRouns = ko.computed(function () {
        return self.workoutToDisplay() && self.workoutToDisplay().canSeeRoundsCount();
    });

    self.logWorkout = function () {
        self.service.logWorkout(self.toJSON());
    };
    ko.computed(function() {
        var workout = self.selectedWorkout();
        if (!workout) {
            return;
        }
        workout.isReadOnlyMode = self.isReadOnlyMode;
        self.workoutToDisplay(new CrossfitterController(workout));
    });

    return self;
};

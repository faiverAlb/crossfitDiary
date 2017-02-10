var ChooseExistingWorkoutController = function (parameters) {

    var self = this;
    self.service = new CrossfitterService(parameters.pathToApp);
    self.availableWorkouts = ko.observableArray();

    self.selectedWorkout = ko.observable();
    self.isReadOnlyMode = true;

    self.hasPredefinedWorkout = parameters.viewModel.crossfitterWorkout != null;

    self.workoutToDisplay = ko.observable();

    ko.computed(function () {
        var workout = self.selectedWorkout();
        if (!workout) {
            return;
        }
        workout.isReadOnlyMode = self.isReadOnlyMode;
        self.workoutToDisplay(new CrossfitterController(workout));

    });

    self.clearState = function() {
        self.selectedWorkout(null);
    };


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

    function loadAvailableWorkouts() {
        self.service.getAvailableWorkouts().then(function (availableWorkouts) {
            self.availableWorkouts(availableWorkouts);
        });
    };

    function init() {
        loadAvailableWorkouts();
        if (self.hasPredefinedWorkout) {
            var foundWorkout = ko.utils.arrayFirst(self.availableWorkouts(), function (item) {
                return item.id == parameters.viewModel.crossfitterWorkout.selectedWorkoutId;
            });

            if (foundWorkout) {
                self.selectedWorkout(foundWorkout);
            }
        }  
    };

    init();

    return self;
};

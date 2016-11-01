var CrossfitterController = function (parameters) {
    var self = this;

    var service = new CrossfitterService(parameters.pathToApp);

    self.exercises = ko.observableArray(parameters.exercises);
    self.selectedExercise = ko.observable();

    self.simpleRoutines = ko.observableArray([]);

    self.roundsCount = ko.observable();
    self.timeToWork = ko.observable();

    self.title = ko.observable();
    self.restBetweenExercises = ko.observable();
    self.restBetweenRounds = ko.observable();

    ko.computed(function() {
        var exercise = self.selectedExercise();
        if (!exercise) {
            return;
        }

        self.simpleRoutines.push(new SimpleRoutine(exercise));
        self.selectedExercise('');
    });

    self.removeSimpleRoutineFromToDo = function(index) {
        self.simpleRoutines.splice(index(), 1);
    };

    self.canCreateWorkout = ko.computed(function() {
        return self.simpleRoutines().length > 0;
    });

    self.createWorkout = function () {
        service.createWorkout();
    };

    return self;
};

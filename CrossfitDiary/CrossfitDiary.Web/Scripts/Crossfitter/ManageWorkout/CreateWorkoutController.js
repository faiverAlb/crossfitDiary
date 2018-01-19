var CreateWorkoutController = function (parameters) {
    var self = new CrossfitterController(parameters);
    self.exercises = ko.observableArray();
    self.alternativeExercises = ko.observableArray();

    self.service = new CrossfitterService(parameters.pathToApp);

    self.hasAnyRoutines = ko.computed(function() {
        return self.simpleRoutines().length > 0;
    });

  

    self.selectedExercise = ko.observable();
    self.selectedAlternativeExercise = ko.observable();

    self.workoutTypes = ko.observableArray(parameters.viewModel.workoutTypes);

    ko.computed(function() {
        if (self.selectedWorkoutType() || !self.selectedWorkoutType()) {
            self.simpleRoutines([]);
        }
    });
    self.errors = ko.validation.group(self);

    ko.computed(function() {
        var exercise = self.selectedExercise();
        if (!exercise) {
            return;
        }

        self.simpleRoutines.push(new SimpleRoutine(exercise, self.selectedWorkoutType().Value != WorkoutTypes.Tabata));
        self.selectedExercise('');
    });

    ko.computed(function () {
        var exercise = self.selectedAlternativeExercise();
        if (!exercise) {
            return;
        }

        self.simpleRoutines.push(new SimpleRoutine(exercise));
        self.selectedAlternativeExercise('');
    });

    self.canCreateCreateWorkout = function () {
        if (self.errors().length > 0) {
            self.errors.showAllMessages();
            return false;
        }
        return true;
    };

        self.createWorkout = function () {
        if (self.errors().length > 0) {
            self.errors.showAllMessages();
            return;
        }
        self.service.createWorkout(self.toJSON());
        self.service.createWorkout(self.toJSON());
    };


    self.clearState = function () {
        self.selectedWorkoutType(null);
    };


    self.getCreateWorkoutModel = function() {
        return self.toJSON();
    }

    function loadExercises() {
        self.service.getExercises().then(function (exercises) {
            self.exercises(exercises);

            //  HACK for copy
            var alternativeExercises = JSON.parse(JSON.stringify(exercises));
            for (var i = 0; i < alternativeExercises.length; i++) {
                alternativeExercises[i].isAlternative = true;
            }
            self.alternativeExercises(alternativeExercises);
        });

    };

    function init() {
        loadExercises();
    };

    init();

    return self;
};

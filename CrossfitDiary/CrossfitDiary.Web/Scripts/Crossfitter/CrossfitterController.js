ko.validation.init({
    errorElementClass: 'has-error',
    errorMessageClass: 'help-block',
    decorateInputElement: true,
    insertMessages: false,
    grouping: {
        deep: true,
        live: true,
        observable: true
    }
});

var CrossfitterController = function (parameters) {
    var self = this;
    
    var service = new CrossfitterService(parameters.pathToApp);
    self.service = service;
    self.isReadOnlyMode = parameters.isReadOnlyMode != undefined ? parameters.isReadOnlyMode : false;
    self.simpleRoutines = ko.observableArray([]);

    if (parameters.exercisesToDoList) {
        for (var i = 0; i < parameters.exercisesToDoList.length; i++) {
            var exerciseToDo = parameters.exercisesToDoList[i];
            self.simpleRoutines.push(new SimpleRoutine(exerciseToDo));
        }
    }
    self.selectedWorkoutType = ko.observable(parameters.selectedWorkoutType);




    self.title = ko.observable(parameters.title);
    self.restBetweenExercises = ko.observable(parameters.restBetweenExercises);
    self.restBetweenRounds = ko.observable(parameters.restBetweenRounds);


    self.canSeeRoundsCount = ko.computed(function () {
        if (!self.selectedWorkoutType()) {
            return false;
        }
        var selectedType = self.selectedWorkoutType().Value;
        return selectedType == Crossfitter.WorkoutTypes.ForTime;
    });

    self.roundsCount = ko.observable(parameters.roundsCount).extend({
        required: {
            onlyIf:function() {
                return self.canSeeRoundsCount();
            }
        }
    });


    self.canSeeTimeToWork = ko.computed(function () {
        if (!self.selectedWorkoutType()) {
            return false;
        }
        var selectedType = self.selectedWorkoutType().Value;
        return selectedType == Crossfitter.WorkoutTypes.AMRAP
            || selectedType == Crossfitter.WorkoutTypes.EMOM
            || selectedType == Crossfitter.WorkoutTypes.E2MOM;
    });



    self.timeToWork = ko.observable(parameters.timeToWork)
        .extend({
            required: {
                onlyIf: function() {
                    return self.canSeeTimeToWork();
                }
            }
        });

    self.removeSimpleRoutineFromToDo = function(index) {
        self.simpleRoutines.splice(index(), 1);
    };

    self.canCreateWorkout = ko.computed(function() {
        return self.simpleRoutines().length > 0;
    });

    self.toJSON = function () {
        var model = {
            title: self.title(),
            roundsCount: self.roundsCount(),
            timeToWork: self.timeToWork(),
            restBetweenExercises: self.restBetweenExercises(),
            restBetweenRounds: self.restBetweenRounds(),
            workoutTypeViewModel: self.selectedWorkoutType().Value,
            exercisesToDoList: []
        };
        $.each(self.simpleRoutines(), function (index, routine) {
            var innerRoutineJson = routine.toJSON();
            model.exercisesToDoList.push(innerRoutineJson);
        });

        return model;
    };

    return self;
};

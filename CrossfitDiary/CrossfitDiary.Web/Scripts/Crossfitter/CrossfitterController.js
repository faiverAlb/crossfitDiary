﻿var CrossfitterController = function (parameters) {
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


    self.roundsCount = ko.observable(parameters.roundsCount);
    self.timeToWork = ko.observable(parameters.timeToWork);

    self.title = ko.observable(parameters.title);
    self.restBetweenExercises = ko.observable(parameters.restBetweenExercises);
    self.restBetweenRounds = ko.observable(parameters.restBetweenRounds);

    self.selectedWorkoutType = ko.observable(parameters.selectedWorkoutType);

    self.canSeeRoundsCount = ko.computed(function () {
        if (!self.selectedWorkoutType()) {
            return false;
        }
        var selectedType = self.selectedWorkoutType().Value;
        return selectedType == Crossfitter.WorkoutTypes.ForTime;
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
            var innderRoutineJson = routine.toJSON();
            model.exercisesToDoList.push(innderRoutineJson);
        });

        return model;
    };

    return self;
};

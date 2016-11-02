﻿var CrossfitterController = function (parameters) {
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

    self.toJSON = function() {
        var model = {
            title: self.title(),
            roundsCount: self.roundsCount(),
            timeToWork: self.timeToWork(),
            restBetweenExercises: self.restBetweenExercises(),
            restBetweenRounds: self.restBetweenRounds(),
            exercisesToDoList: []
        };
        $.each(self.simpleRoutines(), function (index, routine) {
            var innderRoutineJson = routine.toJSON();
            model.exercisesToDoList.push(innderRoutineJson);
        });

        return model;
    };

    self.createWorkout = function () {
        service.createWorkout(self.toJSON);
    };

    return self;
};

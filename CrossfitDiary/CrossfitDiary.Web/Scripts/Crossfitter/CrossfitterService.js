var CrossfitterService = function(pathToApp) {
    var self = this;

    self.createWorkout = function(model) {
        return $.ajax({
            type: 'POST',
            url: pathToApp + "api/createWorkout",
            data: model,
            async: false
        }).done(function() {
            window.location = "/Home";
        });
    };

    self.logWorkout = function(model) {
        return $.post({
            url: pathToApp + "api/logWorkout",
            data: model
        }).done(function() {
            window.location = "/Home";
        });
    };

    self.createAndLogWorkout = function(model) {
        return $.post({
            url: pathToApp + "api/createAndLogNewWorkout",
            data: model
        }).done(function() {
            window.location = "/Home";
        });
    };

    return self;
};
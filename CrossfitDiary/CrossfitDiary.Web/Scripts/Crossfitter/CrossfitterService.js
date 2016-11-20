var CrossfitterService = function(pathToApp) {
    var self = this;

    self.createWorkout = function(model) {
        return $.ajax({
            type: 'POST',
            url: pathToApp + "api/createWorkout",
            data: model,
            async: false
        });
    };

    self.logWorkout = function(model) {
        debugger;
    };

    return self;
};
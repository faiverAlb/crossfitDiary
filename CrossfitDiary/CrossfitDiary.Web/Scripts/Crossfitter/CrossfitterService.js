var CrossfitterService = function(pathToApp) {
    var self = this;

    self.createWorkout = function(model) {
        modalLoading.init(true);
        return $.ajax({
            type: 'POST',
            url: pathToApp + "api/createWorkout",
            data: model,
            async: false
        }).done(function() {
            modalLoading.init(false);
            window.location = "/Home";
        });
    };

    self.logWorkout = function(model) {
        modalLoading.init(true);
        return $.post({
            url: pathToApp + "api/logWorkout",
            data: model
        }).done(function() {
            modalLoading.init(false);
            window.location = "/Home";
        });
    };

    self.createAndLogWorkout = function (model) {
        modalLoading.init(true);

        return $.post({
            url: pathToApp + "api/createAndLogNewWorkout",
            data: model
        }).done(function () {
            modalLoading.init(false);
            window.location = "/Home";
        });
    };

    return self;
};
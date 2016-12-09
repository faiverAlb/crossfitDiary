var HomePageController = function (parameters) {
    var self = this;

    self.weekWorkouts = parameters.viewModel.weekWorkouts;


    self.toJSON = function () {
        var model = {};
        return model;
    };

    return self;
};

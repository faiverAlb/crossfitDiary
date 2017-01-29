var HomePageController = (function () {
    function HomePageController(parameters) {
        this.parameters = parameters;
        this.toJSON = function () {
            var model = {};
            return model;
        };
        this.weekWorkouts = this.parameters.viewModel.weekWorkouts;
    }
    return HomePageController;
}());
;

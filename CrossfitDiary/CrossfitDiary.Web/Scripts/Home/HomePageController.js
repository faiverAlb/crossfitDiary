var HomePageController = (function () {
    function HomePageController(parameters) {
        var _this = this;
        this.parameters = parameters;
        this.removeWorkout = function (crossfitterWorkoutId) {
            _this._service.removeWorkout(crossfitterWorkoutId)
                .finally(function () {
                window.location.href = "/Home";
            });
        };
        this.allWorkouts = this.parameters.viewModel.allWorkouts;
        this._service = new CrossfitterService(parameters.pathToApp);
    }
    return HomePageController;
}());
;
//# sourceMappingURL=HomePageController.js.map
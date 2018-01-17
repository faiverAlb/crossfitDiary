var PrideController = (function () {
    function PrideController(basicParameters) {
        var _this = this;
        this.loadExercises = function () {
            _this._service.getExercises()
                .then(function (exercises) {
                _this._exercises(exercises);
            });
        };
        this._service = new CrossfitterService(basicParameters.pathToApp);
        this._exercises = ko.observableArray([]);
        this._selectedExercise = ko.observable();
        this.loadExercises();
        ko.computed(function () {
            var exercise = _this._selectedExercise();
            if (!exercise) {
                return;
            }
            debugger;
        });
    }
    return PrideController;
}());
//# sourceMappingURL=PrideController.js.map
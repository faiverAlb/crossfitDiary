var Pages;
(function (Pages) {
    var CrossfitterService = General.CrossfitterService;
    var ObservablePersonExerciseRecord = Models.ObservablePersonExerciseRecord;
    var ErrorMessageViewModel = General.ErrorMessageViewModel;
    var PrideController = (function () {
        /* Computeds */
        function PrideController(basicParameters) {
            var _this = this;
            this.loadExercises = function () {
                _this._service.getStatisticalExercises()
                    .then(function (exercises) {
                    _this._exercises(exercises);
                })
                    .fail(function (response) {
                    _this.errorMessager.addMessage(response.responseText, false);
                });
            };
            this.errorMessager = new ErrorMessageViewModel();
            this.isDataLoading = ko.observable(false);
            this._service = new CrossfitterService(basicParameters.pathToApp, this.isDataLoading);
            this._exercises = ko.observableArray([]);
            this._personMaximums = ko.observableArray([]);
            this._allPersonsMaximums = ko.observableArray([]);
            this._selectedExercise = ko.observable();
            this.loadExercises();
            ko.computed(function () {
                var exercise = _this._selectedExercise();
                if (!exercise) {
                    return;
                }
                _this._service.getPersonExerciseMaximumWeight(exercise.id)
                    .then(function (personMaximums) {
                    _this._personMaximums(personMaximums);
                })
                    .then(function () {
                    return _this._service.getAllPersonsExerciseMaximumWeights(exercise.id);
                })
                    .then(function (allPersonsRecords) {
                    _this._allPersonsMaximums($.map(allPersonsRecords, function (item) { return new ObservablePersonExerciseRecord(item.personName, item.maximumWeight, item.date, item.workoutTitle, item.positionBetweenOthers, item.isItMe); }));
                })
                    .fail(function (response) {
                    _this.errorMessager.addMessage(response.responseText, false);
                });
            });
        }
        return PrideController;
    }());
    Pages.PrideController = PrideController;
})(Pages || (Pages = {}));
//# sourceMappingURL=PrideController.js.map
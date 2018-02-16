var Models;
(function (Models) {
    var IExerciseMeasureTypeViewModel = (function () {
        function IExerciseMeasureTypeViewModel() {
        }
        return IExerciseMeasureTypeViewModel;
    }());
    Models.IExerciseMeasureTypeViewModel = IExerciseMeasureTypeViewModel;
    var ExerciseMeasureTypeViewModelObservable = (function () {
        function ExerciseMeasureTypeViewModelObservable(model) {
            var _this = this;
            this.toJSON = function () { return ({
                exerciseMeasureType: {
                    measureType: _this.measureType(),
                    measureValue: _this.measureValue()
                }
            }); };
            this.measureType = ko.observable(model.measureType);
            this.measureValue = ko.observable();
            this.measureValue.extend({ required: model.isRequired });
            if (parseFloat(model.measureValue)) {
                this.measureValue(parseFloat(model.measureValue).toString());
            }
            this.measureDesciption = ko.observable(model.description);
            this.shortMeasureDescription = ko.observable(model.shortMeasureDescription);
        }
        return ExerciseMeasureTypeViewModelObservable;
    }());
    Models.ExerciseMeasureTypeViewModelObservable = ExerciseMeasureTypeViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseMeasureTypeViewModelObservable.js.map
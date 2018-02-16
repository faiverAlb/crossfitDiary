var Models;
(function (Models) {
    var ExerciseMeasureTypeViewModel = (function () {
        function ExerciseMeasureTypeViewModel(measureType, measureValue, description, shortMeasureDescription, isRequired) {
            this.measureType = measureType;
            this.measureValue = measureValue;
            this.description = description;
            this.shortMeasureDescription = shortMeasureDescription;
            this.isRequired = isRequired;
        }
        return ExerciseMeasureTypeViewModel;
    }());
    Models.ExerciseMeasureTypeViewModel = ExerciseMeasureTypeViewModel;
    var ExerciseMeasureTypeViewModelObservable = (function () {
        function ExerciseMeasureTypeViewModelObservable(model) {
            var _this = this;
            this.model = model;
            this.toPlainObject = function () {
                var plainObject = new ExerciseMeasureTypeViewModel(_this.measureType(), _this.measureValue(), _this.measureDesciption(), _this.shortMeasureDescription(), _this.model.isRequired);
                return plainObject;
            };
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
//# sourceMappingURL=ExerciseMeasureTypeViewModel.js.map
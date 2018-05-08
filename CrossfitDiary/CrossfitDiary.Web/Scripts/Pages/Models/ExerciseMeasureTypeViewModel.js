var Models;
(function (Models) {
    var ExerciseMeasureTypeViewModel = (function () {
        function ExerciseMeasureTypeViewModel(params) {
            if (params == null) {
                return;
            }
            debugger;
            this.measureType = params.measureType;
            this.measureValue = params.measureValue;
            this.description = params.description;
            this.shortMeasureDescription = params.shortMeasureDescription;
            this.isRequired = params.isRequired;
        }
        ExerciseMeasureTypeViewModel.prototype.deserialize = function (input) {
            debugger;
            if (input == null) {
                return null;
            }
            return new ExerciseMeasureTypeViewModel({
                measureType: input.measureType,
                measureValue: input.measureValue,
                description: input.description,
                isRequired: input.isRequired,
                shortMeasureDescription: input.shortMeasureDescription,
            });
        };
        return ExerciseMeasureTypeViewModel;
    }());
    Models.ExerciseMeasureTypeViewModel = ExerciseMeasureTypeViewModel;
    ;
    var ExerciseMeasureTypeViewModelObservable = (function () {
        function ExerciseMeasureTypeViewModelObservable(model) {
            var _this = this;
            this.model = model;
            this.toPlainObject = function () {
                var plainObject = new ExerciseMeasureTypeViewModel({
                    measureType: _this.measureType(),
                    measureValue: _this.measureValue(),
                    description: _this.measureDesciption(),
                    shortMeasureDescription: _this.shortMeasureDescription(),
                    isRequired: _this.model.isRequired
                });
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
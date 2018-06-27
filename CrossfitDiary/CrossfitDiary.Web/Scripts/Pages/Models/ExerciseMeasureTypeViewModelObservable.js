var Models;
(function (Models) {
    var ExerciseMeasureTypeViewModelObservable = (function () {
        function ExerciseMeasureTypeViewModelObservable(model, personMaximumWeight) {
            var _this = this;
            this.model = model;
            this.toPlainObject = function () {
                var plainObject = new Models.ExerciseMeasureTypeViewModel({
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
            this._canSeePersonalRecord = personMaximumWeight != null;
        }
        return ExerciseMeasureTypeViewModelObservable;
    }());
    Models.ExerciseMeasureTypeViewModelObservable = ExerciseMeasureTypeViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseMeasureTypeViewModelObservable.js.map
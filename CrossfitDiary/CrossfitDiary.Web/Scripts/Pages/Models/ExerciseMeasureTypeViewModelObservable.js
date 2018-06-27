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
            this._canSeePersonalRecord = personMaximumWeight != null && personMaximumWeight > 0;
            if (this._canSeePersonalRecord) {
                this.personalRecordPercent = ko.computed(function () {
                    var inputValueString = _this.measureValue();
                    if (parseFloat(inputValueString)) {
                        var actualValue = parseFloat(inputValueString);
                        var calc = (actualValue / personMaximumWeight) * 100;
                        var calcString = Math.round((calc + 0.0001) * 10) / 10;
                        return calcString + "% of Personal Record";
                    }
                });
            }
        }
        return ExerciseMeasureTypeViewModelObservable;
    }());
    Models.ExerciseMeasureTypeViewModelObservable = ExerciseMeasureTypeViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseMeasureTypeViewModelObservable.js.map
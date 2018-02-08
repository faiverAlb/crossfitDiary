var Models;
(function (Models) {
    var ExerciseMeasureTypeViewModel = (function () {
        function ExerciseMeasureTypeViewModel(model, isRequired) {
            var _this = this;
            this.toJSON = function () { return ({
                exerciseMeasureType: {
                    measureType: _this.measureType(),
                    measureValue: _this.measureValue()
                }
            }); };
            this.measureType = ko.observable(model.measureType);
            this.measureValue = ko.observable();
            this.measureValue.extend({ required: isRequired });
            if (parseFloat(model.measureValue)) {
                this.measureValue(parseFloat(model.measureValue).toString());
            }
            this.measureDesciption = ko.observable(model.description);
            this.shortMeasureDescription = ko.observable(model.shortMeasureDescription);
        }
        return ExerciseMeasureTypeViewModel;
    }());
    Models.ExerciseMeasureTypeViewModel = ExerciseMeasureTypeViewModel;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseMeasureTypeViewModel.js.map
var Models;
(function (Models) {
    var ExerciseMeasureViewModelObservable = (function () {
        function ExerciseMeasureViewModelObservable(model) {
            var _this = this;
            this.model = model;
            this.toPlainObject = function () {
                var plainObject = new Models.ExerciseMeasureViewModel({
                    exerciseMeasureType: _this._exerciseMeasureType.toPlainObject()
                });
                return plainObject;
            };
            this._exerciseMeasureType = new Models.ExerciseMeasureTypeViewModelObservable(model.exerciseMeasureType);
        }
        return ExerciseMeasureViewModelObservable;
    }());
    Models.ExerciseMeasureViewModelObservable = ExerciseMeasureViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseMeasureViewModelObservable.js.map
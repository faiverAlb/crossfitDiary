var Models;
(function (Models) {
    var ExerciseMeasureViewModelObservable = (function () {
        function ExerciseMeasureViewModelObservable(model) {
            var _this = this;
            this.model = model;
            this.toPlainObject = function () {
                var plainObject = new ExerciseMeasureViewModel({
                    exerciseMeasureType: _this._exerciseMeasureType.toPlainObject()
                });
                return plainObject;
            };
            this._exerciseMeasureType = new Models.ExerciseMeasureTypeViewModelObservable(model.exerciseMeasureType);
        }
        return ExerciseMeasureViewModelObservable;
    }());
    Models.ExerciseMeasureViewModelObservable = ExerciseMeasureViewModelObservable;
    var ExerciseMeasureViewModel = (function () {
        function ExerciseMeasureViewModel(params) {
            if (params == null) {
                return;
            }
            this.exerciseMeasureType = params.exerciseMeasureType;
        }
        ExerciseMeasureViewModel.prototype.deserialize = function (input) {
            if (input == null) {
                return null;
            }
            return new ExerciseMeasureViewModel({
                exerciseMeasureType: new Models.ExerciseMeasureTypeViewModel().deserialize(input)
            });
        };
        return ExerciseMeasureViewModel;
    }());
    Models.ExerciseMeasureViewModel = ExerciseMeasureViewModel;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseMeasureViewModel.js.map
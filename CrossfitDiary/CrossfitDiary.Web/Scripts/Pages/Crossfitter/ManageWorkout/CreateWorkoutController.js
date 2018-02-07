var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Pages;
(function (Pages) {
    var SimpleRoutine = Crossfitter.SimpleRoutine;
    var CreateWorkoutController = (function (_super) {
        __extends(CreateWorkoutController, _super);
        function CreateWorkoutController(parameters) {
            var _this = _super.call(this, parameters) || this;
            _this.loadExercises = function () {
                _this._service.getExercises().then(function (exercises) {
                    ko.utils.arrayPushAll(_this.exercises, exercises);
                    //  HACK for copy
                    var alternativeExercises = JSON.parse(JSON.stringify(exercises));
                    for (var i = 0; i < alternativeExercises.length; i++) {
                        alternativeExercises[i].isAlternative = true;
                    }
                    _this.alternativeExercises(alternativeExercises);
                });
            };
            _this.canCreateCreateWorkout = function () {
                if (_this.errors().length > 0) {
                    _this.errors.showAllMessages();
                    return false;
                }
                return true;
            };
            _this.createWorkout = function () {
                if (_this.errors().length > 0) {
                    _this.errors.showAllMessages();
                    return;
                }
                _this._service.createWorkout(_this.toJSON());
            };
            _this.clearState = function () {
                _this.selectedWorkoutType(null);
            };
            _this.getCreateWorkoutModel = function () {
                return this.toJSON();
            };
            _this.exercises = ko.observableArray([]);
            _this.alternativeExercises = ko.observableArray();
            _this.hasAnyRoutines = ko.computed(function () {
                return this.simpleRoutines().length > 0;
            });
            _this.selectedExercise = ko.observable();
            _this.selectedAlternativeExercise = ko.observable();
            _this.workoutTypes = ko.observableArray(parameters.viewModel.workoutTypes);
            ko.computed(function () {
                if (this.selectedWorkoutType() || !this.selectedWorkoutType()) {
                    this.simpleRoutines([]);
                }
            });
            _this.errors = ko.validation.group(_this);
            ko.computed(function () {
                var exercise = this.selectedExercise();
                if (!exercise) {
                    return;
                }
                this.simpleRoutines.push(new SimpleRoutine(exercise, this.selectedWorkoutType().Value != WorkoutTypes.Tabata));
                this.selectedExercise('');
            });
            ko.computed(function () {
                var exercise = this.selectedAlternativeExercise();
                if (!exercise) {
                    return;
                }
                this.simpleRoutines.push(new SimpleRoutine(exercise));
                this.selectedAlternativeExercise('');
            });
            _this.loadExercises();
            return _this;
        }
        return CreateWorkoutController;
    }(Pages.CrossfitterController));
    Pages.CreateWorkoutController = CreateWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=CreateWorkoutController.js.map
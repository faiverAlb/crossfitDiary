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
    ko.validation.init({
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block',
        decorateInputElement: true,
        insertMessages: false,
        grouping: {
            deep: true,
            live: true,
            observable: true
        }
    });
    var BaseController = General.BaseController;
    var CrossfitterService = General.CrossfitterService;
    var SimpleRoutine = Crossfitter.SimpleRoutine;
    var CrossfitterController = (function (_super) {
        __extends(CrossfitterController, _super);
        function CrossfitterController(parameters) {
            var _this = _super.call(this) || this;
            _this.parameters = parameters;
            _this.removeSimpleRoutineFromToDo = function (index) {
                _this.simpleRoutines.splice(index(), 1);
            };
            _this.toJSON = function () {
                var model = {
                    title: _this.title(),
                    roundsCount: _this.roundsCount(),
                    timeToWork: _this.timeToWork(),
                    restBetweenExercises: _this.restBetweenExercises(),
                    restBetweenRounds: _this.restBetweenRounds(),
                    workoutTypeViewModel: _this.selectedWorkoutType().Value,
                    exercisesToDoList: []
                };
                $.each(_this.simpleRoutines(), function (index, routine) {
                    var innerRoutineJson = routine.toJSON();
                    model.exercisesToDoList.push(innerRoutineJson);
                });
                return model;
            };
            _this._service = new CrossfitterService(parameters.pathToApp);
            _this.isReadOnlyMode = parameters.isReadOnlyMode != undefined ? parameters.isReadOnlyMode : false;
            _this.simpleRoutines = ko.observableArray([]);
            if (parameters.exercisesToDoList) {
                for (var i = 0; i < parameters.exercisesToDoList.length; i++) {
                    var exerciseToDo = parameters.exercisesToDoList[i];
                    _this.simpleRoutines.push(new SimpleRoutine(exerciseToDo, false));
                }
            }
            _this.selectedWorkoutType = ko.observable(parameters.selectedWorkoutType);
            _this.title = ko.observable(parameters.title);
            _this.restBetweenExercises = ko.observable(parameters.restBetweenExercises);
            _this.restBetweenRounds = ko.observable(parameters.restBetweenRounds);
            _this.canSeeRoundsCount = ko.computed(function () {
                if (!_this.selectedWorkoutType()) {
                    return false;
                }
                var selectedType = _this.selectedWorkoutType().Value;
                return selectedType === WorkoutTypes.ForTime || selectedType === WorkoutTypes.Tabata;
            });
            _this.anyUsualExercises = ko.computed(function () {
                return ko.utils.arrayFirst(_this.simpleRoutines(), function (routine) { return routine.exercise.isAlternative === false; }) != null;
            });
            _this.canSeeAlternativeExercises = ko.computed(function () {
                if (!_this.selectedWorkoutType()) {
                    return false;
                }
                var selectedType = _this.selectedWorkoutType().Value;
                var typeIsNeeded = selectedType == WorkoutTypes.EMOM || selectedType == WorkoutTypes.E2MOM;
                return typeIsNeeded && _this.anyUsualExercises();
            });
            _this.anyAlternative = ko.computed(function () {
                return ko.utils.arrayFirst(_this.simpleRoutines(), function (routine) { return routine.exercise.isAlternative; }) != null;
            });
            _this.roundsCount = ko.observable(parameters.roundsCount).extend({
                required: {
                    onlyIf: function () {
                        return _this.canSeeRoundsCount();
                    }
                }
            });
            _this.canSeeTimeToWork = ko.computed(function () {
                if (!_this.selectedWorkoutType()) {
                    return false;
                }
                var selectedType = _this.selectedWorkoutType().Value;
                return selectedType == WorkoutTypes.AMRAP
                    || selectedType == WorkoutTypes.EMOM
                    || selectedType == WorkoutTypes.E2MOM;
            });
            _this.timeToWork = ko.observable(parameters.timeToWork)
                .extend({
                required: {
                    onlyIf: function () {
                        return _this.canSeeTimeToWork();
                    }
                }
            });
            _this.canCreateWorkout = ko.computed(function () {
                return _this.simpleRoutines().length > 0;
            });
            return _this;
        }
        return CrossfitterController;
    }(BaseController));
    Pages.CrossfitterController = CrossfitterController;
    ;
})(Pages || (Pages = {}));
//# sourceMappingURL=CrossfitterController.js.map
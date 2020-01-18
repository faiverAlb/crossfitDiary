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
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import Vue from "vue";
import { WodSubType } from "../../../models/viewModels/WodSubType";
import { Component } from "vue-property-decorator";
import CrossfitterService from "../../../CrossfitterService";
import { SpinnerModel } from "../../../models/viewModels/SpinnerModel";
import { WorkoutViewModel } from "../../../models/viewModels/WorkoutViewModel";
import { ToLogWorkoutViewModel } from "../../../models/viewModels/ToLogWorkoutViewModel";
import { ErrorAlertModel } from "../../../models/viewModels/ErrorAlertModel";
import { WindowHelper } from "../../../helpers/WindowHelper";
import VeeValidate from "vee-validate";
import { Action, Getter, State } from "vuex-class";
import { PlanningWorkoutViewModel } from "../../../models/viewModels/PlanningWorkoutViewModel";
import { faCheck } from "@fortawesome/free-solid-svg-icons/faCheck";
import { library } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
library.add(faCheck);
Vue.use(VeeValidate);
var namespace = "workoutEdit";
var WorkoutTypeComponent = /** @class */ (function (_super) {
    __extends(WorkoutTypeComponent, _super);
    function WorkoutTypeComponent() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.spinner = new SpinnerModel(false);
        _this.model = new WorkoutViewModel();
        _this.toLogModel = new ToLogWorkoutViewModel();
        _this.errorAlertModel = new ErrorAlertModel();
        _this.planWorkoutModel = new PlanningWorkoutViewModel();
        _this.wodSubTypes = [
            { text: 'Skill', value: WodSubType.Skill },
            { text: 'Workout', value: WodSubType.Wod },
            { text: 'Accessory', value: WodSubType.AccessoryWork }
        ];
        return _this;
    }
    WorkoutTypeComponent.prototype.mounted = function () {
        if (workouter != null && workouter.toLogWorkoutRawModel != null) {
            workouter.toLogWorkoutRawModel = new ToLogWorkoutViewModel().deserialize(workouter.toLogWorkoutRawModel);
            this.model = workouter.toLogWorkoutRawModel.workoutViewModel;
            this.toLogModel = workouter.toLogWorkoutRawModel;
        }
        else if (workouter != null && workouter.workoutViewModel != null) {
            this.model = new WorkoutViewModel().deserialize(workouter.workoutViewModel);
        }
        this.planWorkoutModel = new PlanningWorkoutViewModel();
        this.planWorkoutModel.workoutViewModel = this.model;
        this.setIsFindMaxWeight(this.model.findMaxWeight);
    };
    WorkoutTypeComponent.prototype.logWorkout = function () {
        var _this = this;
        this.$validator.validate().then(function (isValid) {
            if (isValid) {
                var workoutModel = _this.model;
                var model = {
                    newWorkoutViewModel: workoutModel,
                    logWorkoutViewModel: _this.toLogModel
                };
                var crossfitterService = new CrossfitterService();
                _this.spinner.activate();
                crossfitterService
                    .createAndLogWorkout(model)
                    .then(function (data) {
                    window.location.href = "\\";
                })
                    .catch(function (data) {
                    _this.spinner.disable();
                    _this.errorAlertModel.setError(data.response.statusText);
                });
            }
        });
    };
    WorkoutTypeComponent.prototype.showLogWorkoutModal = function () {
        var _this = this;
        this.$validator.validate();
        var scrollToErrors = this.$el.querySelector("[aria-invalid=true]");
        if (scrollToErrors) {
            WindowHelper.scrollToTargetAdjusted(scrollToErrors);
        }
        this.$validator.validate().then(function (isValid) {
            if (isValid) {
                _this.$refs.logWorkoutModal.show();
            }
        });
    };
    WorkoutTypeComponent.prototype.planWorkoutAction = function () {
        var _this = this;
        this.$validator.validate();
        var scrollToErrors = this.$el.querySelector("[aria-invalid=true]");
        if (scrollToErrors) {
            WindowHelper.scrollToTargetAdjusted(scrollToErrors);
        }
        this.$validator.validate().then(function (isValid) {
            if (isValid) {
                var crossfitterService = new CrossfitterService();
                _this.spinner.activate();
                crossfitterService
                    .createAndPlanWorkout(_this.planWorkoutModel)
                    .then(function (data) {
                    window.location.href = "\\";
                })
                    .catch(function (data) {
                    _this.spinner.disable();
                    _this.errorAlertModel.setError(data.response.statusText);
                });
            }
        });
    };
    WorkoutTypeComponent.prototype.onIsFindMaxWeightChange = function () {
        this.setIsFindMaxWeight(this.model.findMaxWeight);
    };
    __decorate([
        Action("setIsFindMaxWeight", { namespace: namespace })
    ], WorkoutTypeComponent.prototype, "setIsFindMaxWeight", void 0);
    __decorate([
        Getter('isFindMaxWeightGetter', { namespace: namespace })
    ], WorkoutTypeComponent.prototype, "isFindMaxWeight", void 0);
    __decorate([
        State("workoutEdit")
    ], WorkoutTypeComponent.prototype, "workoutEdit", void 0);
    WorkoutTypeComponent = __decorate([
        Component({
            components: {
                FontAwesomeIcon: FontAwesomeIcon
            }
        })
    ], WorkoutTypeComponent);
    return WorkoutTypeComponent;
}(Vue));
export { WorkoutTypeComponent };
//# sourceMappingURL=workoutTypeMixin.js.map
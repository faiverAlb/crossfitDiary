/* Font awesome icons */
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver
/* public components */
import Vue from "vue";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import BFormCheckbox from "bootstrap-vue/es/components/form-checkbox/form-checkbox";
/* app components */
import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import PlannedWorkoutDisplayComponent from "./components/planned-workout-display-component.vue";
import CrossfitterService from "./CrossfitterService";
import ErrorAlertComponent from "./components/error-alert-component.vue";
/* models and styles */
import "./style/persons.scss";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import { WorkoutViewModel } from "./models/viewModels/WorkoutViewModel";
var apiService = new CrossfitterService();
Vue.component("b-form-checkbox", BFormCheckbox);
var vue = new Vue({
    el: "#home-page-container",
    template: "\n    <div class=\"home-container\">\n      <div class=\"row\">\n        <div class=\"col\">\n          <ErrorAlertComponent :errorAlertModel=\"errorAlertModel\"></ErrorAlertComponent>\n        </div>\n      </div>\n      <PlannedWorkoutDisplayComponent :plannedWorkouts=\"plannedWorkouts\" @logWorkout=\"logWorkout\"></PlannedWorkoutDisplayComponent>\n      <div class=\"row\">\n        <div class=\"offset-5\">\n          <spinner\n            :status=\"spinner.status\"\n            :size=\"spinner.size\"\n            :color=\"spinner.color\"\n            :depth=\"spinner.depth\"\n            :rotation=\"spinner.rotation\"\n            :speed=\"spinner.speed\">\n          </spinner>\n        </div>\n      </div>\n      <div class=\"container person-setting\">\n      <div\n        class=\"row\"\n        v-if=\"activities\"\n      >\n        <div class=\"offset-lg-3 col col-lg-5\">\n          <b-form-checkbox\n            id=\"showOnlyUserWods\"\n            name=\"shouShowOnlyUserWods\"\n            v-model=\"showOnlyUserWods\"\n            @change=\"changeShowUserWods\">\n            Show only my wods\n          </b-form-checkbox>\n        </div>\n      </div>\n    </div>\n      <PersonsActivitiesComponent :activities=\"activities\"/>\n    </div>\n    ",
    components: {
        PersonsActivitiesComponent: PersonsActivitiesComponent,
        PlannedWorkoutDisplayComponent: PlannedWorkoutDisplayComponent,
        Spinner: Spinner,
        ErrorAlertComponent: ErrorAlertComponent
    },
    data: function () {
        return {
            activities: ToLogWorkoutViewModel[0],
            plannedWorkouts: WorkoutViewModel,
            spinner: new SpinnerModel(true),
            errorAlertModel: new ErrorAlertModel(),
            showOnlyUserWods: false
        };
    },
    mounted: function () {
        var _this = this;
        this.showOnlyUserWods = workouter.showOnlyUserWods;
        this.spinner.activate();
        apiService
            .getPlannedWorkoutsForToday()
            .then(function (data) {
            _this.plannedWorkouts = data;
        })
            .catch(function (data) {
            _this.errorAlertModel.setError(data.response.statusText);
        });
        apiService
            .getAllCrossfittersWorkouts()
            .then(function (data) {
            _this.activities = data;
            _this.spinner.disable();
        })
            .catch(function (data) {
            _this.errorAlertModel.setError(data.response.statusText);
            _this.spinner.disable();
        });
    },
    methods: {
        logWorkout: function (logModel) {
            var _this = this;
            this.spinner.activate();
            apiService
                .quickLogWorkout(logModel)
                .then(function () { return apiService.getAllCrossfittersWorkouts(); })
                .then(function (data) {
                _this.activities = data;
                _this.spinner.disable();
            })
                .catch(function (data) {
                _this.spinner.disable();
                _this.errorAlertModel.setError(data.response.statusText);
            });
        },
        changeShowUserWods: function (showOnlyUserWods) {
            var _this = this;
            this.spinner.activate();
            apiService
                .setShowOnlyUserWods(showOnlyUserWods)
                .then(function () { return apiService.getAllCrossfittersWorkouts(); })
                .then(function (data) {
                _this.activities = data;
                _this.spinner.disable();
            })
                .catch(function (data) {
                _this.errorAlertModel.setError(data.response.statusText);
                _this.spinner.disable();
            });
        }
    }
});
//# sourceMappingURL=persons-entry.js.map
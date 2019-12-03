/* Font awesome icons */
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver
/* public components */
import Vue from "vue";
import Spinner from "vue-spinner-component/src/Spinner.vue";
/* app components */
import CrossfitterService from "./CrossfitterService";
import ErrorAlertComponent from "./components/error-alert-component.vue";
import WorkoutDisplayComponent from "./workout-display-component.vue";
/* models and styles */
import "./style/wods-list.scss";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import { WorkoutViewModel } from "./models/viewModels/WorkoutViewModel";
var apiService = new CrossfitterService();
var vue = new Vue({
    el: "#wods-library-container",
    template: "\n    <div class=\"wods-library-container\">\n    <div class=\"row\">\n        <div class=\"offset-5\">\n          <spinner\n            :status=\"spinner.status\"\n            :size=\"spinner.size\"\n            :color=\"spinner.color\"\n            :depth=\"spinner.depth\"\n            :rotation=\"spinner.rotation\"\n            :speed=\"spinner.speed\">\n          </spinner>\n        </div>\n      </div>\n      <div class=\"container\"\n            v-for=\"item in workouts\"\n            :key=\"item.id\">\n                <div class=\"row\">\n                  <WorkoutDisplayComponent :workoutViewModel=\"item\"></WorkoutDisplayComponent>\n                </div>\n              </div>\n    </div>\n    ",
    components: {
        Spinner: Spinner,
        ErrorAlertComponent: ErrorAlertComponent,
        WorkoutDisplayComponent: WorkoutDisplayComponent,
    },
    data: function () {
        return {
            workouts: WorkoutViewModel,
            spinner: new SpinnerModel(true),
            errorAlertModel: new ErrorAlertModel()
        };
    },
    mounted: function () {
        var _this = this;
        this.spinner.activate();
        apiService
            .getWorkoutsList()
            .then(function (data) {
            debugger;
            _this.workouts = data;
            _this.spinner.disable();
        })
            .catch(function (data) {
            _this.errorAlertModel.setError(data.response.statusText);
            _this.spinner.disable();
        });
    },
    methods: {}
});
//# sourceMappingURL=wods-list-entry.js.map
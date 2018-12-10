import Vue from "vue";
import "./style/persons.scss";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService";
var apiService = new CrossfitterService();
import { dom } from "@fortawesome/fontawesome-svg-core";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import ErrorAlertComponent from "./components/error-alert-component.vue";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver
var vue = new Vue({
    el: "#home-page-container",
    template: "\n    <div class=\"home-container\">\n      <div class=\"row\">\n        <div class=\"col\">\n          <ErrorAlertComponent :errorAlertModel=\"errorAlertModel\"></ErrorAlertComponent>\n        </div>\n      </div>\n      <div class=\"row\">\n        <div class=\"offset-5\">\n          <spinner \n            :status=\"spinner.status\"  \n            :size=\"spinner.size\" \n            :color=\"spinner.color\"  \n            :depth=\"spinner.depth\" \n            :rotation=\"spinner.rotation\"\n            :speed=\"spinner.speed\">\n          </spinner>\n        </div>\n      </div>\n      <PersonsActivitiesComponent :activities=\"activities\"/> \n    </div>\n    ",
    components: {
        PersonsActivitiesComponent: PersonsActivitiesComponent,
        Spinner: Spinner,
        ErrorAlertComponent: ErrorAlertComponent
    },
    data: function () {
        return {
            activities: ToLogWorkoutViewModel[0],
            spinner: new SpinnerModel(true),
            errorAlertModel: new ErrorAlertModel()
        };
    },
    mounted: function () {
        var _this = this;
        this.spinner.activate();
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
    }
});
//# sourceMappingURL=persons-entry.js.map
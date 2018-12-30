import Vue from "vue";
import "./style/persons.scss";
import CrossfitterService from "./CrossfitterService";
var apiService = new CrossfitterService();
import { dom } from "@fortawesome/fontawesome-svg-core";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import ErrorAlertComponent from "./components/error-alert-component.vue";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver
new Vue({
    el: "#ManageWorkoutPageContainer",
    template: "\n    <div class=\"manage-workout-page\">\n     \n    </div>\n    ",
    components: {
        Spinner: Spinner,
        ErrorAlertComponent: ErrorAlertComponent
    },
    data: function () {
        return {
            spinner: new SpinnerModel(true),
            errorAlertModel: new ErrorAlertModel()
        };
    },
    mounted: function () {
    }
});
//# sourceMappingURL=workout-enty.js.map
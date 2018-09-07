import Vue from "vue";
import "./style/persons.scss";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService";
var apiService = new CrossfitterService();
import { faHome } from "@fortawesome/free-solid-svg-icons/faHome";
import { faRocket } from "@fortawesome/free-solid-svg-icons/faRocket";
import { faTrophy } from "@fortawesome/free-solid-svg-icons/faTrophy"; //fa-sign-out-alt
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons/faSignOutAlt";
import { library } from "@fortawesome/fontawesome-svg-core";
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver
library.add(faHome, faRocket, faTrophy, faSignOutAlt);
new Vue({
    el: "#home-page-container",
    template: '<div class="home-container"><PersonsActivitiesComponent :activities="activities"/> </div>',
    components: {
        PersonsActivitiesComponent: PersonsActivitiesComponent
    },
    data: function () {
        return {
            activities: ToLogWorkoutViewModel[0]
        };
    },
    mounted: function () {
        var _this = this;
        apiService.getAllCrossfittersWorkouts().then(function (data) {
            _this.activities = data;
        });
    }
});
//# sourceMappingURL=persons-entry.js.map